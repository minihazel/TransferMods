using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TransferMods
{
    public partial class mainForm : Form
    {
        public string currentDir = Environment.CurrentDirectory;
        public Color idle = Color.FromArgb(255, 100, 100, 100);
        public Color hoverColor = Color.FromArgb(255, 40, 40, 50);

        private readonly string cantDetectSptInFolder = "We could not detect an SPT installation in this folder, please try again.";
        private readonly string modsFolder = "user\\mods";
        private readonly string bepInExFolder = "BepInEx\\plugins";
        private readonly string tarkovExe = "EscapeFromTarkov.exe";
        private readonly string orderFile = "order.json";

        public mainForm() => InitializeComponent();

        public void ListPaths(string path)
        {
            if (CheckPath(currentDir))
                EnableSecondPanel(path);
            else
            {
                ShowMessage("It appears that you\'re running outside of an SPT installation.\n\nPlease click Browse next to \'Main Folder\' and find the installation that you want to transfer from.");
                btnBrowseMainFolder.FlatAppearance.BorderColor = Color.MediumSpringGreen;
                btnBrowseMainFolder.ForeColor = Color.MediumSpringGreen;
            }
        }

        public void ShowMessage(string content) => MessageBox.Show(content, Text, MessageBoxButtons.OK);

        public void EnableSecondPanel(string path)
        {
            var pathValid = CheckPath(path);

            if (pathValid)
            {
                pathMainFolder.Text = path;
                pathMainServerMods.Text = Path.Combine(path, modsFolder);
                pathMainClientMods.Text = Path.Combine(path, bepInExFolder);
            }
            else
                ShowMessage(cantDetectSptInFolder);

            btnBrowseMainFolder.FlatAppearance.BorderColor = pathValid ? idle : Color.MediumSpringGreen;
            btnBrowseMainFolder.ForeColor = pathValid ? Color.LightGray : Color.MediumSpringGreen;

            panelNewSPT.Enabled = pathValid;
        }

        public void CheckCompatibility(string oldSPT, string newSPT)
        {
            sptCompatibility.Visible = true;
            sptCompatibilityTitle.Visible = true;

            string oldAki = Path.Combine(oldSPT, tarkovExe);
            string newAki = Path.Combine(newSPT, tarkovExe);

            bool oldAkiExist = File.Exists(oldAki);
            bool newAkiExist = File.Exists(newAki);

            if (oldAkiExist && newAkiExist)
            {
                string oldMinor = GetMinorVersion(oldAki);
                string newMinor = GetMinorVersion(newAki);
                var compatible = oldMinor == newMinor;

                sptCompatibility.ForeColor = compatible ? Color.MediumSpringGreen : Color.IndianRed;
                sptCompatibility.Text = compatible ? $"Compatible!" : $"Potentially incompatible versions [Main: {oldMinor} Transfer: {newMinor}]";
            }
        }

        private static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            var dir = new DirectoryInfo(sourceDir);

            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destinationDir);
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }

            //Directory.Delete(sourceDir, true);
        }

        private void mainForm_Load(object sender, EventArgs e) => ListPaths(currentDir);

        private bool CheckPath(string path)
        {
            bool existsAkiServer = File.Exists(Path.Combine(path, "Aki.Server.exe"));
            bool existsakiLauncher = File.Exists(Path.Combine(path, "Aki.Launcher.exe"));
            bool existsakiData = Directory.Exists(Path.Combine(path, "Aki_Data"));

            bool existsUser = Directory.Exists(Path.Combine(path, modsFolder));
            bool existsBepIn = Directory.Exists(Path.Combine(path, bepInExFolder));

            return existsAkiServer && existsakiLauncher && existsakiData && existsUser && existsBepIn;
        }

        private void pathMainServerMods_MouseDown(object sender, MouseEventArgs e) => lblMainFolder.Select();

        private void pathMainClientMods_MouseDown(object sender, MouseEventArgs e) => lblMainFolder.Select();

        private void btnQuit_MouseEnter(object sender, EventArgs e) => btnQuit.ForeColor = Color.MediumSpringGreen;

        private void btnQuit_MouseLeave(object sender, EventArgs e) => btnQuit.ForeColor = Color.LightGray;

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quit and exit?", Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Exit();
        }

        private string GetMinorVersion(string aki)
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(aki);
            string[] parts = info.FileVersion.ToString().Split('.');
            if (parts.Count() < 3)
                throw new DirectoryNotFoundException($"Unable to get minor version for aki path: {aki}");
            return parts[3];
        }

        private void btnBrowseMainFolder_Click(object sender, EventArgs e)
        {
            var sptPath = OpenSPTFolder();
            if (!string.IsNullOrEmpty(sptPath))
                EnableSecondPanel(Path.GetDirectoryName(sptPath));
        }

        private void btnBrowseNew_Click(object sender, EventArgs e)
        {
            var sptPath = OpenSPTFolder();
            if (!string.IsNullOrEmpty(sptPath))
                SetupControls(Path.GetDirectoryName(sptPath));
        }

        private string OpenSPTFolder()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = false,
                FileName = "Enter the SPT folder and click Open"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
                return ofd.FileName;
            return null;
        }

        private void checkBoxtimer_Tick(object sender, EventArgs e)
        {
            if (chkServerMods.Checked || chkClientMods.Checked)
                btnTransferMods.Enabled = true;
            else if (!chkServerMods.Checked && !chkClientMods.Checked)
                btnTransferMods.Enabled = false;
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e) => checkBoxtimer.Stop();

        private void chkOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOrder.Checked)
            {
                ShowMessage("This option will overwrite any existing order.json in the transferring install." +
                    "\n\n" +
                    "Proceed with caution.");
            }
        }

        private bool IsClientModFile(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            return fileName != "aki-core.dll"
                || fileName != "aki-custom.dll"
                || fileName != "aki-debugging.dll"
                || fileName != "aki-singleplayer.dll"
                || fileName != "ConfigurationManager.dll";
        }

        private bool ShowTransferDialog(int modsCount, params string[] modStrings)
        {
            var modsString = string.Join("\n", modStrings);
            return MessageBox.Show($"Would you like to transfer {modsCount} mods to {Path.GetFileName(pathNewSPT.Text)}?" +
                    $"\n\n" +
                    $"{modsString}\n" +
                    $"\n\n" +
                    $"Click \'Yes\' to transfer.", Text, MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        private (int counter, int serverCounter) CopyDirectories(IEnumerable<string> directories, string newFolder)
        {
            int counter = 0;
            int serverCounter = 0;
            foreach (string mod in directories)
            {
                bool itemExists = Directory.Exists(mod);
                bool newItemExists = Directory.Exists(Path.Combine(newFolder, Path.GetFileName(mod)));
                if (itemExists && !newItemExists)
                {
                    counter++;
                    serverCounter++;
                    CopyDirectory(mod, Path.Combine(newFolder, Path.GetFileName(mod)), true);
                }
            }
            return (counter, serverCounter);
        }

        private void btnTransferMods_Click(object sender, EventArgs e)
        {
            List<string> serverList = Directory.GetDirectories(pathMainServerMods.Text).ToList();
            List<string> clientFolders = Directory.GetDirectories(pathMainClientMods.Text).ToList();
            List<string> clientFiles = Directory.GetFiles(pathMainClientMods.Text).Where(x => IsClientModFile(x)).ToList();

            int serverMods = serverList.Count;
            int clientMods = clientFiles.Count + clientFolders.Count;

            if (chkServerMods.Checked && chkClientMods.Checked)
            {
                int counter = 0;
                int serverCounter = 0;
                int clientCounter = 0;

                if (ShowTransferDialog(serverMods + clientMods, $"{serverMods} server mods", $"{clientMods} client mods"))
                {
                    string newServerModsFolder = Path.Combine(pathNewSPT.Text, modsFolder);
                    string newClientModsFolder = Path.Combine(pathNewSPT.Text, bepInExFolder);

                    var copyServerListResult = CopyDirectories(serverList, newServerModsFolder);
                    counter += copyServerListResult.counter;
                    serverCounter += copyServerListResult.serverCounter;

                    var copyClientFoldersResult = CopyDirectories(clientFolders, newClientModsFolder);
                    counter += copyClientFoldersResult.counter;
                    clientCounter += copyClientFoldersResult.serverCounter;

                    var copyClientFilesResult = CopyFiles(clientFiles, newClientModsFolder);
                    counter += copyClientFilesResult.counter;
                    clientCounter += copyClientFilesResult.clientCounter;

                    ShowSuccessfulTransfer($"{serverCounter} server mods", $"{clientCounter} client mods\n");
                }
            }
            else if (chkServerMods.Checked && !chkClientMods.Checked)
            {
                int servercounter = 0;
                int counter = 0;

                if (ShowTransferDialog(serverMods, $"{serverMods} server mods"))
                {
                    string newServerModsFolder = Path.Combine(pathNewSPT.Text, modsFolder);
                    var copyServerListResult = CopyDirectories(serverList, newServerModsFolder);
                    counter += copyServerListResult.counter;
                    servercounter += copyServerListResult.serverCounter;
                    ShowSuccessfulTransfer($"{servercounter} server mods");
                }
            }
            else if (!chkServerMods.Checked && chkClientMods.Checked)
            {
                int counter = 0;
                int clientcounter = 0;

                if (ShowTransferDialog(clientMods, $"{clientMods} client mods"))
                {
                    string newClientModsFolder = Path.Combine(pathNewSPT.Text, bepInExFolder);
                    var copyClientFoldersResult = CopyDirectories(clientFolders, newClientModsFolder);
                    counter += copyClientFoldersResult.counter;

                    var copyClientFilesResult = CopyFiles(clientFiles, newClientModsFolder);
                    counter += copyClientFilesResult.counter;
                    clientcounter += copyClientFilesResult.clientCounter;

                    ShowSuccessfulTransfer($"{clientcounter} client mods");
                }
            }

            if (chkOrder.Checked)
            {
                string newServerModsFolder = Path.Combine(pathNewSPT.Text, modsFolder);
                string newOrderFile = Path.Combine(newServerModsFolder, orderFile);

                string oldOrderFile = Path.Combine(pathMainServerMods.Text, orderFile);
                if (File.Exists(oldOrderFile))
                    File.Copy(oldOrderFile, newOrderFile);
            }
        }

        private (int counter, int clientCounter) CopyFiles(IEnumerable<string> files, string newFolder)
        {
            int counter = 0;
            int clientCounter = 0;
            foreach (string mod in files)
            {
                bool itemExists = File.Exists(mod);
                bool newItemExists = File.Exists(Path.Combine(newFolder, Path.GetFileName(mod)));
                if (itemExists && !newItemExists)
                {
                    counter++;
                    clientCounter++;
                    File.Copy(mod, Path.Combine(newFolder, Path.GetFileName(mod)));
                }
            }
            return (counter, clientCounter);
        }

        private void ShowSuccessfulTransfer(params string[] modStrings)
        {
            var modsString = string.Join("\n", modStrings);
            if (MessageBox.Show($"Mod transfer successful!\n" +
                    $"Clients mods have been transferred to {Path.GetFileName(pathNewSPT.Text)}" +
                    $"\n\n" +
                    $"{modsString}\n" +
                    $"\n\n" +
                    $"All transferrable mods were successfully transferred!" +
                    $"\n\n" +
                    $"Click \'Yes\' to close this window and keep {Text} open\n" +
                    $"Click \'No\' to close {Text}", Text, MessageBoxButtons.YesNo) == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void mainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void mainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (items.Length == 1)
            {
                FileAttributes attr = File.GetAttributes(items[0]);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    SetupControls(items[0]);
                else
                    ShowMessage("File-drop detected. Please only drag-and-drop an SPT folder.");
            }
            else
                ShowMessage("Two or more items detected. Please only drag-and-drop 1 SPT folder.");
        }

        private void SetupControls(string path)
        {
            var pathValid = CheckPath(path);
            if (pathValid)
            {
                pathNewSPT.Text = path;
                string folderName = Path.GetFileName(path);
                chkOrder.Enabled = true;

                btnTransferMods.Text = $"Transfer mods to {folderName}";

                checkBoxtimer.Start();
                CheckCompatibility(pathMainFolder.Text, pathNewSPT.Text);
            }
            else
                ShowMessage(cantDetectSptInFolder);

            chkServerMods.Enabled = pathValid;
            chkClientMods.Enabled = pathValid;
            btnTransferMods.Enabled = pathValid;
        }
    }
}