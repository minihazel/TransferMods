namespace TransferMods
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.panelAppPaths = new System.Windows.Forms.GroupBox();
            this.lblMainFolder = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pathMainFolder = new System.Windows.Forms.TextBox();
            this.pathMainServerMods = new System.Windows.Forms.TextBox();
            this.pathMainClientMods = new System.Windows.Forms.TextBox();
            this.btnBrowseMainFolder = new System.Windows.Forms.Button();
            this.panelNewSPT = new System.Windows.Forms.GroupBox();
            this.btnBrowseNew = new System.Windows.Forms.Button();
            this.pathNewSPT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sptCompatibilityTitle = new System.Windows.Forms.Label();
            this.sptCompatibility = new System.Windows.Forms.Label();
            this.chkServerMods = new System.Windows.Forms.CheckBox();
            this.chkClientMods = new System.Windows.Forms.CheckBox();
            this.btnTransferMods = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Label();
            this.checkBoxtimer = new System.Windows.Forms.Timer(this.components);
            this.chkOrder = new System.Windows.Forms.CheckBox();
            this.panelAppPaths.SuspendLayout();
            this.panelNewSPT.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAppPaths
            // 
            this.panelAppPaths.Controls.Add(this.btnBrowseMainFolder);
            this.panelAppPaths.Controls.Add(this.pathMainClientMods);
            this.panelAppPaths.Controls.Add(this.pathMainServerMods);
            this.panelAppPaths.Controls.Add(this.pathMainFolder);
            this.panelAppPaths.Controls.Add(this.label3);
            this.panelAppPaths.Controls.Add(this.label2);
            this.panelAppPaths.Controls.Add(this.lblMainFolder);
            this.panelAppPaths.ForeColor = System.Drawing.Color.LightGray;
            this.panelAppPaths.Location = new System.Drawing.Point(12, 12);
            this.panelAppPaths.Name = "panelAppPaths";
            this.panelAppPaths.Size = new System.Drawing.Size(720, 114);
            this.panelAppPaths.TabIndex = 0;
            this.panelAppPaths.TabStop = false;
            this.panelAppPaths.Text = "App paths";
            // 
            // lblMainFolder
            // 
            this.lblMainFolder.AutoSize = true;
            this.lblMainFolder.Location = new System.Drawing.Point(6, 25);
            this.lblMainFolder.Name = "lblMainFolder";
            this.lblMainFolder.Size = new System.Drawing.Size(82, 17);
            this.lblMainFolder.TabIndex = 1;
            this.lblMainFolder.Text = "Main folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server mods:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Client mods:";
            // 
            // pathMainFolder
            // 
            this.pathMainFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.pathMainFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pathMainFolder.ForeColor = System.Drawing.Color.LightGray;
            this.pathMainFolder.Location = new System.Drawing.Point(116, 22);
            this.pathMainFolder.Name = "pathMainFolder";
            this.pathMainFolder.Size = new System.Drawing.Size(500, 24);
            this.pathMainFolder.TabIndex = 1;
            // 
            // pathMainServerMods
            // 
            this.pathMainServerMods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.pathMainServerMods.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pathMainServerMods.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pathMainServerMods.ForeColor = System.Drawing.Color.LightGray;
            this.pathMainServerMods.Location = new System.Drawing.Point(116, 52);
            this.pathMainServerMods.Name = "pathMainServerMods";
            this.pathMainServerMods.Size = new System.Drawing.Size(589, 17);
            this.pathMainServerMods.TabIndex = 4;
            this.pathMainServerMods.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pathMainServerMods_MouseDown);
            // 
            // pathMainClientMods
            // 
            this.pathMainClientMods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.pathMainClientMods.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pathMainClientMods.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pathMainClientMods.ForeColor = System.Drawing.Color.LightGray;
            this.pathMainClientMods.Location = new System.Drawing.Point(116, 79);
            this.pathMainClientMods.Name = "pathMainClientMods";
            this.pathMainClientMods.Size = new System.Drawing.Size(589, 17);
            this.pathMainClientMods.TabIndex = 5;
            this.pathMainClientMods.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pathMainClientMods_MouseDown);
            // 
            // btnBrowseMainFolder
            // 
            this.btnBrowseMainFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseMainFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowseMainFolder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnBrowseMainFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseMainFolder.Location = new System.Drawing.Point(629, 19);
            this.btnBrowseMainFolder.Name = "btnBrowseMainFolder";
            this.btnBrowseMainFolder.Size = new System.Drawing.Size(76, 28);
            this.btnBrowseMainFolder.TabIndex = 1;
            this.btnBrowseMainFolder.Text = "Browse";
            this.btnBrowseMainFolder.UseVisualStyleBackColor = true;
            this.btnBrowseMainFolder.Click += new System.EventHandler(this.btnBrowseMainFolder_Click);
            // 
            // panelNewSPT
            // 
            this.panelNewSPT.Controls.Add(this.chkOrder);
            this.panelNewSPT.Controls.Add(this.btnQuit);
            this.panelNewSPT.Controls.Add(this.btnTransferMods);
            this.panelNewSPT.Controls.Add(this.chkClientMods);
            this.panelNewSPT.Controls.Add(this.chkServerMods);
            this.panelNewSPT.Controls.Add(this.sptCompatibility);
            this.panelNewSPT.Controls.Add(this.sptCompatibilityTitle);
            this.panelNewSPT.Controls.Add(this.btnBrowseNew);
            this.panelNewSPT.Controls.Add(this.pathNewSPT);
            this.panelNewSPT.Controls.Add(this.label4);
            this.panelNewSPT.Enabled = false;
            this.panelNewSPT.ForeColor = System.Drawing.Color.LightGray;
            this.panelNewSPT.Location = new System.Drawing.Point(12, 141);
            this.panelNewSPT.Name = "panelNewSPT";
            this.panelNewSPT.Size = new System.Drawing.Size(720, 161);
            this.panelNewSPT.TabIndex = 1;
            this.panelNewSPT.TabStop = false;
            this.panelNewSPT.Text = "Select SPT install to transfer mods to";
            // 
            // btnBrowseNew
            // 
            this.btnBrowseNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowseNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnBrowseNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseNew.Location = new System.Drawing.Point(629, 20);
            this.btnBrowseNew.Name = "btnBrowseNew";
            this.btnBrowseNew.Size = new System.Drawing.Size(76, 28);
            this.btnBrowseNew.TabIndex = 2;
            this.btnBrowseNew.Text = "Browse";
            this.btnBrowseNew.UseVisualStyleBackColor = true;
            this.btnBrowseNew.Click += new System.EventHandler(this.btnBrowseNew_Click);
            // 
            // pathNewSPT
            // 
            this.pathNewSPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.pathNewSPT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pathNewSPT.ForeColor = System.Drawing.Color.LightGray;
            this.pathNewSPT.Location = new System.Drawing.Point(143, 23);
            this.pathNewSPT.Name = "pathNewSPT";
            this.pathNewSPT.Size = new System.Drawing.Size(473, 24);
            this.pathNewSPT.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Transfer mods to:";
            // 
            // sptCompatibilityTitle
            // 
            this.sptCompatibilityTitle.AutoSize = true;
            this.sptCompatibilityTitle.Font = new System.Drawing.Font("Bahnschrift Light", 8F);
            this.sptCompatibilityTitle.Location = new System.Drawing.Point(6, 54);
            this.sptCompatibilityTitle.Name = "sptCompatibilityTitle";
            this.sptCompatibilityTitle.Size = new System.Drawing.Size(116, 13);
            this.sptCompatibilityTitle.TabIndex = 6;
            this.sptCompatibilityTitle.Text = "Version compatibility :";
            this.sptCompatibilityTitle.Visible = false;
            // 
            // sptCompatibility
            // 
            this.sptCompatibility.AutoSize = true;
            this.sptCompatibility.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.sptCompatibility.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.sptCompatibility.Location = new System.Drawing.Point(140, 52);
            this.sptCompatibility.Name = "sptCompatibility";
            this.sptCompatibility.Size = new System.Drawing.Size(85, 17);
            this.sptCompatibility.TabIndex = 7;
            this.sptCompatibility.Text = "Compatible!";
            this.sptCompatibility.Visible = false;
            // 
            // chkServerMods
            // 
            this.chkServerMods.AutoSize = true;
            this.chkServerMods.Checked = true;
            this.chkServerMods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkServerMods.Enabled = false;
            this.chkServerMods.Location = new System.Drawing.Point(9, 81);
            this.chkServerMods.Name = "chkServerMods";
            this.chkServerMods.Size = new System.Drawing.Size(164, 21);
            this.chkServerMods.TabIndex = 8;
            this.chkServerMods.Text = "Transfer server mods";
            this.chkServerMods.UseVisualStyleBackColor = true;
            // 
            // chkClientMods
            // 
            this.chkClientMods.AutoSize = true;
            this.chkClientMods.Checked = true;
            this.chkClientMods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClientMods.Enabled = false;
            this.chkClientMods.Location = new System.Drawing.Point(9, 105);
            this.chkClientMods.Name = "chkClientMods";
            this.chkClientMods.Size = new System.Drawing.Size(158, 21);
            this.chkClientMods.TabIndex = 9;
            this.chkClientMods.Text = "Transfer client mods";
            this.chkClientMods.UseVisualStyleBackColor = true;
            // 
            // btnTransferMods
            // 
            this.btnTransferMods.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransferMods.Enabled = false;
            this.btnTransferMods.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnTransferMods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferMods.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.btnTransferMods.Location = new System.Drawing.Point(192, 98);
            this.btnTransferMods.Name = "btnTransferMods";
            this.btnTransferMods.Size = new System.Drawing.Size(402, 38);
            this.btnTransferMods.TabIndex = 10;
            this.btnTransferMods.Text = "Transfer mods to";
            this.btnTransferMods.UseVisualStyleBackColor = true;
            this.btnTransferMods.Click += new System.EventHandler(this.btnTransferMods_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.BackColor = System.Drawing.Color.Transparent;
            this.btnQuit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuit.Location = new System.Drawing.Point(602, 98);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(103, 38);
            this.btnQuit.TabIndex = 11;
            this.btnQuit.Text = "   Quit";
            this.btnQuit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            this.btnQuit.MouseEnter += new System.EventHandler(this.btnQuit_MouseEnter);
            this.btnQuit.MouseLeave += new System.EventHandler(this.btnQuit_MouseLeave);
            // 
            // checkBoxtimer
            // 
            this.checkBoxtimer.Tick += new System.EventHandler(this.checkBoxtimer_Tick);
            // 
            // chkOrder
            // 
            this.chkOrder.AutoSize = true;
            this.chkOrder.Enabled = false;
            this.chkOrder.Location = new System.Drawing.Point(9, 129);
            this.chkOrder.Name = "chkOrder";
            this.chkOrder.Size = new System.Drawing.Size(148, 21);
            this.chkOrder.TabIndex = 12;
            this.chkOrder.Text = "Transfer order.json";
            this.chkOrder.UseVisualStyleBackColor = true;
            this.chkOrder.CheckedChanged += new System.EventHandler(this.chkOrder_CheckedChanged);
            // 
            // mainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(744, 316);
            this.Controls.Add(this.panelNewSPT);
            this.Controls.Add(this.panelAppPaths);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer SPT Mods";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.panelAppPaths.ResumeLayout(false);
            this.panelAppPaths.PerformLayout();
            this.panelNewSPT.ResumeLayout(false);
            this.panelNewSPT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox panelAppPaths;
        private System.Windows.Forms.Label lblMainFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pathMainFolder;
        private System.Windows.Forms.TextBox pathMainClientMods;
        private System.Windows.Forms.TextBox pathMainServerMods;
        private System.Windows.Forms.Button btnBrowseMainFolder;
        private System.Windows.Forms.GroupBox panelNewSPT;
        private System.Windows.Forms.Button btnBrowseNew;
        private System.Windows.Forms.TextBox pathNewSPT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label sptCompatibilityTitle;
        private System.Windows.Forms.Label sptCompatibility;
        private System.Windows.Forms.CheckBox chkClientMods;
        private System.Windows.Forms.CheckBox chkServerMods;
        private System.Windows.Forms.Button btnTransferMods;
        private System.Windows.Forms.Label btnQuit;
        private System.Windows.Forms.Timer checkBoxtimer;
        private System.Windows.Forms.CheckBox chkOrder;
    }
}

