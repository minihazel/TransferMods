using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransferMods
{
    public partial class mainForm : Form
    {
        public string currentDir = Environment.CurrentDirectory;
        public Color idle = Color.FromArgb(255, 100, 100, 100);
        public Color hoverColor = Color.FromArgb(255, 40, 40, 50);

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            listPaths(currentDir);
        }

        public void listPaths(string path)
        {
            bool existsAkiServer = File.Exists(Path.Combine(currentDir, "Aki.Server.exe"));
            bool existsakiLauncher = File.Exists(Path.Combine(currentDir, "Aki.Launcher.exe"));
            bool existsakiData = Directory.Exists(Path.Combine(currentDir, "Aki_Data"));

            bool existsUser = Directory.Exists(Path.Combine(currentDir, "user\\mods"));
            bool existsBepIn = Directory.Exists(Path.Combine(currentDir, "BepInEx\\plugins"));

            if (existsAkiServer && existsakiLauncher && existsakiData && existsUser && existsBepIn)
            {
                enableSecondPanel(path);
            }
            else
            {
                showMessage("It appears that you\'re running outside of an SPT installation.\n\nPlease click Browse next to \'Main Folder\' and find the installation that you want to transfer from.");
                btnBrowseMainFolder.FlatAppearance.BorderColor = Color.MediumSpringGreen;
                btnBrowseMainFolder.ForeColor = Color.MediumSpringGreen;
            }
        }

        public void showMessage(string content)
        {
            MessageBox.Show(content, this.Text, MessageBoxButtons.OK);
        }

        private void pathMainServerMods_MouseDown(object sender, MouseEventArgs e)
        {
            lblMainFolder.Select();
        }

        private void pathMainClientMods_MouseDown(object sender, MouseEventArgs e)
        {
            lblMainFolder.Select();
        }

        private void btnQuit_MouseEnter(object sender, EventArgs e)
        {
            btnQuit.ForeColor = Color.MediumSpringGreen;
        }

        private void btnQuit_MouseLeave(object sender, EventArgs e)
        {
            btnQuit.ForeColor = Color.LightGray;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quit and exit?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public void enableSecondPanel(string path)
        {
            bool existsAkiServer = File.Exists(Path.Combine(path, "Aki.Server.exe"));
            bool existsakiLauncher = File.Exists(Path.Combine(path, "Aki.Launcher.exe"));
            bool existsakiData = Directory.Exists(Path.Combine(path, "Aki_Data"));

            bool existsUser = Directory.Exists(Path.Combine(path, "user\\mods"));
            bool existsBepIn = Directory.Exists(Path.Combine(path, "BepInEx\\plugins"));

            if (existsAkiServer && existsakiLauncher && existsakiData && existsUser && existsBepIn)
            {
                pathMainFolder.Text = path;
                pathMainServerMods.Text = Path.Combine(path, "user\\mods");
                pathMainClientMods.Text = Path.Combine(path, "BepInEx\\plugins");
                btnBrowseMainFolder.FlatAppearance.BorderColor = idle;
                btnBrowseMainFolder.ForeColor = Color.LightGray;

                panelNewSPT.Enabled = true;
            }
            else
            {
                showMessage("We could not detect an SPT installation in this folder, please try again.");
                btnBrowseMainFolder.FlatAppearance.BorderColor = Color.MediumSpringGreen;
                btnBrowseMainFolder.ForeColor = Color.MediumSpringGreen;

                panelNewSPT.Enabled = false;
            }
        }

        public void checkCompatibility(string oldSPT, string newSPT)
        {
            sptCompatibility.Visible = true;
            sptCompatibilityTitle.Visible = true;

            string oldAki = Path.Combine(oldSPT, "EscapeFromTarkov.exe");
            string newAki = Path.Combine(newSPT, "EscapeFromTarkov.exe");

            bool oldAkiExist = File.Exists(oldAki);
            bool newAkiExist = File.Exists(newAki);

            if (oldAkiExist && newAkiExist)
            {
                FileVersionInfo oldInfo = FileVersionInfo.GetVersionInfo(oldAki);
                FileVersionInfo newInfo = FileVersionInfo.GetVersionInfo(newAki);

                string[] oldParts = oldInfo.FileVersion.ToString().Split('.');
                string oldMinor = oldParts[3];

                string[] newParts = newInfo.FileVersion.ToString().Split('.');
                string newMinor = newParts[3];

                if (oldMinor == newMinor)
                {
                    sptCompatibility.ForeColor = Color.MediumSpringGreen;
                    sptCompatibility.Text = $"Compatible!";
                }
                else
                {
                    sptCompatibility.ForeColor = Color.IndianRed;
                    sptCompatibility.Text = $"Potentially incompatible versions [Main: {oldMinor} Transfer: {newMinor}]";
                }
            }
        }

        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
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

        private void btnBrowseMainFolder_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ValidateNames = false;
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = false;
            ofd.FileName = "Enter the SPT folder and click Open";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fullPath = Path.GetDirectoryName(ofd.FileName);
                enableSecondPanel(fullPath);
            }
        }

        private void btnBrowseNew_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ValidateNames = false;
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = false;
            ofd.FileName = "Enter the SPT folder and click Open";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fullPath = Path.GetDirectoryName(ofd.FileName);

                bool existsAkiServer = File.Exists(Path.Combine(fullPath, "Aki.Server.exe"));
                bool existsakiLauncher = File.Exists(Path.Combine(fullPath, "Aki.Launcher.exe"));
                bool existsakiData = Directory.Exists(Path.Combine(fullPath, "Aki_Data"));

                bool existsUser = Directory.Exists(Path.Combine(fullPath, "user\\mods"));
                bool existsBepIn = Directory.Exists(Path.Combine(fullPath, "BepInEx\\plugins"));

                if (existsAkiServer && existsakiLauncher && existsakiData && existsUser && existsBepIn)
                {
                    pathNewSPT.Text = fullPath;
                    string folderName = Path.GetFileName(fullPath);

                    chkServerMods.Enabled = true;
                    chkClientMods.Enabled = true;
                    chkOrder.Enabled = true;

                    btnTransferMods.Enabled = true;
                    btnTransferMods.Text = $"Transfer mods to {folderName}";

                    checkBoxtimer.Start();
                    checkCompatibility(pathMainFolder.Text, pathNewSPT.Text);
                }
                else
                {
                    showMessage("We could not detect an SPT installation in this folder, please try again.");
                    chkServerMods.Enabled = false;
                    chkClientMods.Enabled = false;
                    btnTransferMods.Enabled = false;
                }
            }
        }

        private void checkBoxtimer_Tick(object sender, EventArgs e)
        {
            if (chkServerMods.Checked || chkClientMods.Checked)
            {
                btnTransferMods.Enabled = true;
            }
            else if (!chkServerMods.Checked && !chkClientMods.Checked)
            {
                btnTransferMods.Enabled = false;
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            checkBoxtimer.Stop();
        }

        private void chkOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOrder.Checked)
            {
                showMessage("This option will overwrite any existing order.json in the transferring install." +
                    "\n\n" +
                    "Proceed with caution.");
            }
        }

        private void btnTransferMods_Click(object sender, EventArgs e)
        {
            List<string> serverList = new List<string>();
            List<string> clientFolders = new List<string>();
            List<string> clientFiles = new List<string>();

            string[] serverMods = Directory.GetDirectories(pathMainServerMods.Text);
            foreach (string item in serverMods)
            {
                serverList.Add(item);
            }

            string[] clientFolderMods = Directory.GetDirectories(pathMainClientMods.Text);
            foreach (string item in clientFolderMods)
            {
                clientFolders.Add(item);
            }

            string[] clientFilesMods = Directory.GetFiles(pathMainClientMods.Text);
            foreach (string item in clientFilesMods)
            {
                if (Path.GetFileName(item) != "aki-core.dll" ||
                    Path.GetFileName(item) != "aki-custom.dll" ||
                    Path.GetFileName(item) != "aki-debugging.dll" ||
                    Path.GetFileName(item) != "aki-singleplayer.dll" ||
                    Path.GetFileName(item) != "ConfigurationManager.dll")
                {
                    clientFiles.Add(item);
                }
            }

            int server_mods = serverList.Count;
            int client_mods = clientFiles.Count + clientFolders.Count;

            if (chkServerMods.Checked && chkClientMods.Checked)
            {
                int counter = 0;
                int servercounter = 0;
                int clientcounter = 0;

                if (MessageBox.Show($"Would you like to transfer {server_mods + client_mods} mods to {Path.GetFileName(pathNewSPT.Text)}?" +
                    $"\n\n" +
                    $"{server_mods} server mods" +
                    $"{client_mods} client mods\n" +
                    $"\n\n" +
                    $"Click \'Yes\' to transfer.", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string newServerModsFolder = Path.Combine(pathNewSPT.Text, "user\\mods");
                    string newClientModsFolder = Path.Combine(pathNewSPT.Text, "BepInEx\\plugins");
                    if (serverList.Count > 0)
                    {
                        foreach (string server_mod in serverList)
                        {
                            bool item_exists = Directory.Exists(server_mod);
                            bool newitem_exists = Directory.Exists(Path.Combine(newServerModsFolder, Path.GetFileName(server_mod)));
                            if (item_exists && !newitem_exists)
                            {
                                counter++;
                                servercounter++;
                                CopyDirectory(server_mod, Path.Combine(newServerModsFolder, Path.GetFileName(server_mod)), true);
                            }
                        }
                    }

                    if (clientFolders.Count > 0)
                    {
                        foreach (string client_mod in clientFolders)
                        {
                            bool item_exists = Directory.Exists(client_mod);
                            bool newitem_exists = Directory.Exists(Path.Combine(newClientModsFolder, Path.GetFileName(client_mod)));
                            if (item_exists && !newitem_exists)
                            {
                                counter++;
                                clientcounter++;
                                CopyDirectory(client_mod, Path.Combine(newClientModsFolder, Path.GetFileName(client_mod)), true);
                            }
                        }
                    }

                    if (clientFiles.Count > 0)
                    {
                        foreach (string client_mod in clientFiles)
                        {
                            bool item_exists = File.Exists(client_mod);
                            bool newitem_exists = File.Exists(Path.Combine(newClientModsFolder, Path.GetFileName(client_mod)));
                            if (item_exists && !newitem_exists)
                            {
                                counter++;
                                clientcounter++;
                                File.Copy(client_mod, Path.Combine(newClientModsFolder, Path.GetFileName(client_mod)));
                            }
                        }
                    }

                    if (MessageBox.Show($"Mod transfer successful!\n" +
                    $"Server & client mods have been transferred to {Path.GetFileName(pathNewSPT.Text)}" +
                    $"\n\n" +
                    $"{servercounter} server mods" +
                    $"{clientcounter} client mods\n" +
                    $"\n\n" +
                    $"All transferrable mods were successfully transferred!" +
                    $"\n\n" +
                    $"Click \'Yes\' to close this window and keep {this.Text} open\n" +
                    $"Click \'No\' to close {this.Text}", this.Text, MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
            }
            else if (chkServerMods.Checked && !chkClientMods.Checked)
            {
                int servercounter = 0;
                int counter = 0;
                if (MessageBox.Show($"Would you like to transfer {server_mods} mods to {Path.GetFileName(pathNewSPT.Text)}?" +
                    $"\n\n" +
                    $"{server_mods} server mods" +
                    $"\n\n" +
                    $"Click \'Yes' to transfer.", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string newServerModsFolder = Path.Combine(pathNewSPT.Text, "user\\mods");
                    if (serverList.Count > 0)
                    {
                        foreach (string server_mod in serverList)
                        {
                            bool item_exists = Directory.Exists(server_mod);
                            bool newitem_exists = Directory.Exists(Path.Combine(newServerModsFolder, Path.GetFileName(server_mod)));
                            if (item_exists && !newitem_exists)
                            {
                                counter++;
                                servercounter++;
                                CopyDirectory(server_mod, Path.Combine(newServerModsFolder, Path.GetFileName(server_mod)), true);
                            }
                        }
                    }

                    if (MessageBox.Show($"Mod transfer successful!\n" +
                    $"Server mods have been transferred to {Path.GetFileName(pathNewSPT.Text)}" +
                    $"\n\n" +
                    $"{servercounter} server mods" +
                    $"\n\n" +
                    $"All transferrable mods were successfully transferred!" +
                    $"\n\n" +
                    $"Click \'Yes\' to close this window and keep {this.Text} open\n" +
                    $"Click \'No\' to close {this.Text}", this.Text, MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
            }
            else if (!chkServerMods.Checked && chkClientMods.Checked)
            {
                int counter = 0;
                int clientcounter = 0;
                if (MessageBox.Show($"Would you like to transfer {client_mods} mods to {Path.GetFileName(pathNewSPT.Text)}?" +
                    $"\n\n" +
                    $"{client_mods} client mods" +
                    $"\n\n" +
                    $"Click \'Yes' to transfer.", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string newClientModsFolder = Path.Combine(pathNewSPT.Text, "BepInEx\\plugins");

                    if (clientFolders.Count > 0)
                    {
                        foreach (string client_mod in clientFolders)
                        {
                            bool item_exists = Directory.Exists(client_mod);
                            bool newitem_exists = Directory.Exists(Path.Combine(newClientModsFolder, Path.GetFileName(client_mod)));
                            if (item_exists && !newitem_exists)
                            {
                                counter++;
                                CopyDirectory(client_mod, Path.Combine(newClientModsFolder, Path.GetFileName(client_mod)), true);
                            }
                        }
                    }

                    if (clientFiles.Count > 0)
                    {
                        foreach (string client_mod in clientFiles)
                        {
                            bool item_exists = File.Exists(client_mod);
                            bool newitem_exists = File.Exists(Path.Combine(newClientModsFolder, Path.GetFileName(client_mod)));
                            if (item_exists && !newitem_exists)
                            {
                                counter++;
                                clientcounter++;
                                File.Copy(client_mod, Path.Combine(newClientModsFolder, Path.GetFileName(client_mod)));
                            }
                        }
                    }

                    if (MessageBox.Show($"Mod transfer successful!\n" +
                    $"Clients mods have been transferred to {Path.GetFileName(pathNewSPT.Text)}" +
                    $"\n\n" +
                    $"{clientcounter} client mods" +
                    $"\n\n" +
                    $"All transferrable mods were successfully transferred!" +
                    $"\n\n" +
                    $"Click \'Yes\' to close this window and keep {this.Text} open\n" +
                    $"Click \'No\' to close {this.Text}", this.Text, MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
            }

            if (chkOrder.Checked)
            {
                string newServerModsFolder = Path.Combine(pathNewSPT.Text, "user\\mods");
                string newOrderFile = Path.Combine(newServerModsFolder, "order.json");

                string oldOrderFile = Path.Combine(pathMainServerMods.Text, "order.json");
                bool oldExists = File.Exists(oldOrderFile);
                if (oldExists)
                {
                    File.Copy(oldOrderFile, newOrderFile);
                }
            }
        }

        private void mainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void mainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (items.Length == 1)
            {
                FileAttributes attr = File.GetAttributes(items[0]);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string fullPath = items[0];

                    bool existsAkiServer = File.Exists(Path.Combine(fullPath, "Aki.Server.exe"));
                    bool existsakiLauncher = File.Exists(Path.Combine(fullPath, "Aki.Launcher.exe"));
                    bool existsakiData = Directory.Exists(Path.Combine(fullPath, "Aki_Data"));

                    bool existsUser = Directory.Exists(Path.Combine(fullPath, "user\\mods"));
                    bool existsBepIn = Directory.Exists(Path.Combine(fullPath, "BepInEx\\plugins"));

                    if (existsAkiServer && existsakiLauncher && existsakiData && existsUser && existsBepIn)
                    {
                        pathNewSPT.Text = fullPath;
                        string folderName = Path.GetFileName(fullPath);

                        chkServerMods.Enabled = true;
                        chkClientMods.Enabled = true;
                        chkOrder.Enabled = true;

                        btnTransferMods.Enabled = true;
                        btnTransferMods.Text = $"Transfer mods to {folderName}";

                        checkBoxtimer.Start();
                        checkCompatibility(pathMainFolder.Text, pathNewSPT.Text);
                    }
                    else
                    {
                        showMessage("We could not detect an SPT installation in this folder, please try again.");
                        chkServerMods.Enabled = false;
                        chkClientMods.Enabled = false;
                        btnTransferMods.Enabled = false;
                    }
                }
                else
                {
                    showMessage("File-drop detected. Please only drag-and-drop an SPT folder.");
                }
            }
            else
            {
                showMessage("Two or more items detected. Please only drag-and-drop 1 SPT folder.");
            }
        }
    }
}
