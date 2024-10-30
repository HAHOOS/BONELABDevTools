namespace BonelabDevMode
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_connect = new Button();
            btn_disconnect = new Button();
            btn_levelreload = new Button();
            btn_reloadpallet = new Button();
            tb_pallet = new TextBox();
            tb_command = new TextBox();
            groupBox1 = new GroupBox();
            btn_updateAC = new Button();
            checkBox_autoCompleteBarcodes = new CheckBox();
            btn_command = new Button();
            label_coordinates = new Label();
            groupBox2 = new GroupBox();
            btn_unloadpallet = new Button();
            btn_loadpallet = new Button();
            groupBox3 = new GroupBox();
            tb_ipport = new TextBox();
            connectionStatus = new Label();
            groupBox4 = new GroupBox();
            btn_loadLevel = new Button();
            tb_level = new TextBox();
            groupBox5 = new GroupBox();
            cb_logWarnings = new CheckBox();
            cb_logDebug = new CheckBox();
            cb_logErrors = new CheckBox();
            cb_logMessage = new CheckBox();
            lv_logs = new ListView();
            btn_clearLogs = new Button();
            groupBox6 = new GroupBox();
            tb_avatar = new TextBox();
            btn_setAvatar = new Button();
            groupBox7 = new GroupBox();
            tb_spawn = new TextBox();
            btn_spawn = new Button();
            label_currentLevel = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // btn_connect
            // 
            btn_connect.BackColor = SystemColors.ControlLight;
            btn_connect.FlatStyle = FlatStyle.Flat;
            btn_connect.ForeColor = SystemColors.ControlText;
            btn_connect.Location = new Point(6, 50);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(81, 23);
            btn_connect.TabIndex = 0;
            btn_connect.Text = "Connect";
            btn_connect.UseVisualStyleBackColor = false;
            btn_connect.Click += btn_connect1;
            // 
            // btn_disconnect
            // 
            btn_disconnect.BackColor = SystemColors.ControlLight;
            btn_disconnect.Enabled = false;
            btn_disconnect.FlatStyle = FlatStyle.Flat;
            btn_disconnect.ForeColor = SystemColors.ControlText;
            btn_disconnect.Location = new Point(93, 50);
            btn_disconnect.Name = "btn_disconnect";
            btn_disconnect.Size = new Size(80, 23);
            btn_disconnect.TabIndex = 1;
            btn_disconnect.Text = "Disconnect";
            btn_disconnect.UseVisualStyleBackColor = false;
            btn_disconnect.Click += btn_disconnect1;
            // 
            // btn_levelreload
            // 
            btn_levelreload.BackColor = SystemColors.ControlLight;
            btn_levelreload.FlatStyle = FlatStyle.Flat;
            btn_levelreload.ForeColor = SystemColors.ControlText;
            btn_levelreload.Location = new Point(6, 22);
            btn_levelreload.Name = "btn_levelreload";
            btn_levelreload.Size = new Size(167, 23);
            btn_levelreload.TabIndex = 2;
            btn_levelreload.Text = "Reload Current Level";
            btn_levelreload.UseVisualStyleBackColor = false;
            btn_levelreload.Click += btn_levelreload1;
            // 
            // btn_reloadpallet
            // 
            btn_reloadpallet.BackColor = SystemColors.ControlLight;
            btn_reloadpallet.FlatStyle = FlatStyle.Flat;
            btn_reloadpallet.ForeColor = SystemColors.ControlText;
            btn_reloadpallet.Location = new Point(6, 78);
            btn_reloadpallet.Name = "btn_reloadpallet";
            btn_reloadpallet.Size = new Size(295, 23);
            btn_reloadpallet.TabIndex = 3;
            btn_reloadpallet.Text = "Reload Pallet";
            btn_reloadpallet.UseVisualStyleBackColor = false;
            btn_reloadpallet.Click += btn_reloadpallet1;
            // 
            // tb_pallet
            // 
            tb_pallet.BackColor = SystemColors.ControlLight;
            tb_pallet.BorderStyle = BorderStyle.FixedSingle;
            tb_pallet.Location = new Point(6, 21);
            tb_pallet.Name = "tb_pallet";
            tb_pallet.PlaceholderText = "PalletAuthor.PalletName";
            tb_pallet.Size = new Size(295, 23);
            tb_pallet.TabIndex = 4;
            // 
            // tb_command
            // 
            tb_command.BackColor = SystemColors.ControlLight;
            tb_command.BorderStyle = BorderStyle.FixedSingle;
            tb_command.Location = new Point(6, 22);
            tb_command.Name = "tb_command";
            tb_command.PlaceholderText = "Command";
            tb_command.Size = new Size(415, 23);
            tb_command.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_updateAC);
            groupBox1.Controls.Add(checkBox_autoCompleteBarcodes);
            groupBox1.Controls.Add(btn_command);
            groupBox1.Controls.Add(tb_command);
            groupBox1.ForeColor = SystemColors.ControlLightLight;
            groupBox1.Location = new Point(12, 125);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(497, 92);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Command";
            // 
            // btn_updateAC
            // 
            btn_updateAC.BackColor = SystemColors.ControlLight;
            btn_updateAC.FlatStyle = FlatStyle.Flat;
            btn_updateAC.ForeColor = SystemColors.ControlText;
            btn_updateAC.Location = new Point(168, 52);
            btn_updateAC.Name = "btn_updateAC";
            btn_updateAC.Size = new Size(319, 28);
            btn_updateAC.TabIndex = 11;
            btn_updateAC.Text = "Update auto-complete";
            btn_updateAC.UseVisualStyleBackColor = false;
            btn_updateAC.Click += Btn_updateAC_Click;
            // 
            // checkBox_autoCompleteBarcodes
            // 
            checkBox_autoCompleteBarcodes.AutoSize = true;
            checkBox_autoCompleteBarcodes.Location = new Point(6, 58);
            checkBox_autoCompleteBarcodes.Name = "checkBox_autoCompleteBarcodes";
            checkBox_autoCompleteBarcodes.Size = new Size(156, 19);
            checkBox_autoCompleteBarcodes.TabIndex = 10;
            checkBox_autoCompleteBarcodes.Text = "Auto complete barcodes";
            checkBox_autoCompleteBarcodes.UseVisualStyleBackColor = true;
            checkBox_autoCompleteBarcodes.CheckedChanged += CheckBox_autoCompleteBarcodes_CheckedChanged;
            // 
            // btn_command
            // 
            btn_command.BackColor = SystemColors.ControlLight;
            btn_command.FlatStyle = FlatStyle.Flat;
            btn_command.ForeColor = SystemColors.ControlText;
            btn_command.Location = new Point(431, 22);
            btn_command.Name = "btn_command";
            btn_command.Size = new Size(56, 23);
            btn_command.TabIndex = 8;
            btn_command.Text = "Send";
            btn_command.UseVisualStyleBackColor = false;
            btn_command.Click += btn_command1;
            // 
            // label_coordinates
            // 
            label_coordinates.AutoSize = true;
            label_coordinates.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_coordinates.ForeColor = SystemColors.ControlLightLight;
            label_coordinates.Location = new Point(18, 220);
            label_coordinates.Name = "label_coordinates";
            label_coordinates.Size = new Size(128, 13);
            label_coordinates.TabIndex = 9;
            label_coordinates.Text = "Player Coordinates: N/A";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btn_unloadpallet);
            groupBox2.Controls.Add(btn_loadpallet);
            groupBox2.Controls.Add(tb_pallet);
            groupBox2.Controls.Add(btn_reloadpallet);
            groupBox2.ForeColor = SystemColors.ControlLightLight;
            groupBox2.Location = new Point(198, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(311, 107);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Pallet";
            // 
            // btn_unloadpallet
            // 
            btn_unloadpallet.BackColor = SystemColors.ControlLight;
            btn_unloadpallet.FlatStyle = FlatStyle.Flat;
            btn_unloadpallet.ForeColor = SystemColors.ControlText;
            btn_unloadpallet.Location = new Point(161, 50);
            btn_unloadpallet.Name = "btn_unloadpallet";
            btn_unloadpallet.Size = new Size(140, 22);
            btn_unloadpallet.TabIndex = 6;
            btn_unloadpallet.Text = "Unload Pallet";
            btn_unloadpallet.UseVisualStyleBackColor = false;
            btn_unloadpallet.Click += btn_unloadpallet1;
            // 
            // btn_loadpallet
            // 
            btn_loadpallet.BackColor = SystemColors.ControlLight;
            btn_loadpallet.FlatStyle = FlatStyle.Flat;
            btn_loadpallet.ForeColor = SystemColors.ControlText;
            btn_loadpallet.Location = new Point(6, 50);
            btn_loadpallet.Name = "btn_loadpallet";
            btn_loadpallet.Size = new Size(140, 22);
            btn_loadpallet.TabIndex = 5;
            btn_loadpallet.Text = "Load Pallet";
            btn_loadpallet.UseVisualStyleBackColor = false;
            btn_loadpallet.Click += btn_loadpallet1;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.Black;
            groupBox3.Controls.Add(tb_ipport);
            groupBox3.Controls.Add(connectionStatus);
            groupBox3.Controls.Add(btn_connect);
            groupBox3.Controls.Add(btn_disconnect);
            groupBox3.ForeColor = SystemColors.ControlLightLight;
            groupBox3.Location = new Point(12, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(180, 107);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Connection";
            // 
            // tb_ipport
            // 
            tb_ipport.BackColor = SystemColors.ControlLight;
            tb_ipport.BorderStyle = BorderStyle.FixedSingle;
            tb_ipport.Location = new Point(6, 21);
            tb_ipport.Name = "tb_ipport";
            tb_ipport.PlaceholderText = "ws://IP:Port/console";
            tb_ipport.Size = new Size(167, 23);
            tb_ipport.TabIndex = 7;
            tb_ipport.Text = "ws://localhost:50152/console";
            // 
            // connectionStatus
            // 
            connectionStatus.AutoSize = true;
            connectionStatus.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            connectionStatus.ForeColor = Color.Red;
            connectionStatus.Location = new Point(6, 83);
            connectionStatus.Name = "connectionStatus";
            connectionStatus.Size = new Size(83, 13);
            connectionStatus.TabIndex = 2;
            connectionStatus.Text = "Not connected";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btn_loadLevel);
            groupBox4.Controls.Add(tb_level);
            groupBox4.Controls.Add(btn_levelreload);
            groupBox4.ForeColor = SystemColors.ControlLightLight;
            groupBox4.Location = new Point(12, 249);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(180, 113);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "Level";
            // 
            // btn_loadLevel
            // 
            btn_loadLevel.BackColor = SystemColors.ControlLight;
            btn_loadLevel.FlatStyle = FlatStyle.Flat;
            btn_loadLevel.ForeColor = SystemColors.ControlText;
            btn_loadLevel.Location = new Point(6, 84);
            btn_loadLevel.Name = "btn_loadLevel";
            btn_loadLevel.Size = new Size(167, 23);
            btn_loadLevel.TabIndex = 6;
            btn_loadLevel.Text = "Load Level";
            btn_loadLevel.UseVisualStyleBackColor = false;
            btn_loadLevel.Click += btn_loadLevel1;
            // 
            // tb_level
            // 
            tb_level.BackColor = SystemColors.ControlLight;
            tb_level.BorderStyle = BorderStyle.FixedSingle;
            tb_level.Location = new Point(6, 55);
            tb_level.Name = "tb_level";
            tb_level.PlaceholderText = "Level Barcode";
            tb_level.Size = new Size(167, 23);
            tb_level.TabIndex = 5;
            // 
            // groupBox5
            // 
            groupBox5.BackColor = SystemColors.ActiveCaptionText;
            groupBox5.Controls.Add(cb_logWarnings);
            groupBox5.Controls.Add(cb_logDebug);
            groupBox5.Controls.Add(cb_logErrors);
            groupBox5.Controls.Add(cb_logMessage);
            groupBox5.Controls.Add(lv_logs);
            groupBox5.Controls.Add(btn_clearLogs);
            groupBox5.ForeColor = SystemColors.ControlLightLight;
            groupBox5.Location = new Point(12, 368);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(497, 208);
            groupBox5.TabIndex = 11;
            groupBox5.TabStop = false;
            groupBox5.Text = "Logs";
            // 
            // cb_logWarnings
            // 
            cb_logWarnings.AutoSize = true;
            cb_logWarnings.Checked = true;
            cb_logWarnings.CheckState = CheckState.Checked;
            cb_logWarnings.Location = new Point(272, 154);
            cb_logWarnings.Name = "cb_logWarnings";
            cb_logWarnings.Size = new Size(76, 19);
            cb_logWarnings.TabIndex = 14;
            cb_logWarnings.Text = "Warnings";
            cb_logWarnings.UseVisualStyleBackColor = true;
            // 
            // cb_logDebug
            // 
            cb_logDebug.AutoSize = true;
            cb_logDebug.Checked = true;
            cb_logDebug.CheckState = CheckState.Checked;
            cb_logDebug.Location = new Point(139, 154);
            cb_logDebug.Name = "cb_logDebug";
            cb_logDebug.Size = new Size(61, 19);
            cb_logDebug.TabIndex = 13;
            cb_logDebug.Text = "Debug";
            cb_logDebug.UseVisualStyleBackColor = true;
            cb_logDebug.CheckedChanged += Filter;
            // 
            // cb_logErrors
            // 
            cb_logErrors.AutoSize = true;
            cb_logErrors.Checked = true;
            cb_logErrors.CheckState = CheckState.Checked;
            cb_logErrors.Location = new Point(431, 154);
            cb_logErrors.Name = "cb_logErrors";
            cb_logErrors.Size = new Size(56, 19);
            cb_logErrors.TabIndex = 12;
            cb_logErrors.Text = "Errors";
            cb_logErrors.UseVisualStyleBackColor = true;
            cb_logErrors.CheckedChanged += Filter;
            // 
            // cb_logMessage
            // 
            cb_logMessage.AutoSize = true;
            cb_logMessage.Checked = true;
            cb_logMessage.CheckState = CheckState.Checked;
            cb_logMessage.Location = new Point(6, 154);
            cb_logMessage.Name = "cb_logMessage";
            cb_logMessage.Size = new Size(77, 19);
            cb_logMessage.TabIndex = 11;
            cb_logMessage.Text = "Messages";
            cb_logMessage.UseVisualStyleBackColor = true;
            cb_logMessage.CheckedChanged += Filter;
            // 
            // lv_logs
            // 
            lv_logs.BackColor = SystemColors.ActiveCaptionText;
            lv_logs.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lv_logs.ForeColor = SystemColors.ControlLightLight;
            lv_logs.HeaderStyle = ColumnHeaderStyle.None;
            lv_logs.HideSelection = true;
            lv_logs.Location = new Point(6, 22);
            lv_logs.MultiSelect = false;
            lv_logs.Name = "lv_logs";
            lv_logs.ShowGroups = false;
            lv_logs.Size = new Size(481, 126);
            lv_logs.TabIndex = 10;
            lv_logs.UseCompatibleStateImageBehavior = false;
            lv_logs.View = View.Details;
            // 
            // btn_clearLogs
            // 
            btn_clearLogs.BackColor = SystemColors.ControlLight;
            btn_clearLogs.FlatStyle = FlatStyle.Flat;
            btn_clearLogs.ForeColor = SystemColors.ControlText;
            btn_clearLogs.Location = new Point(6, 179);
            btn_clearLogs.Name = "btn_clearLogs";
            btn_clearLogs.Size = new Size(481, 23);
            btn_clearLogs.TabIndex = 9;
            btn_clearLogs.Text = "Clear";
            btn_clearLogs.UseVisualStyleBackColor = false;
            btn_clearLogs.Click += btn_clearLogs1;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(tb_avatar);
            groupBox6.Controls.Add(btn_setAvatar);
            groupBox6.ForeColor = SystemColors.ControlLightLight;
            groupBox6.Location = new Point(198, 249);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(311, 45);
            groupBox6.TabIndex = 9;
            groupBox6.TabStop = false;
            groupBox6.Text = "Avatar";
            // 
            // tb_avatar
            // 
            tb_avatar.BackColor = SystemColors.ControlLight;
            tb_avatar.BorderStyle = BorderStyle.FixedSingle;
            tb_avatar.Location = new Point(6, 15);
            tb_avatar.Name = "tb_avatar";
            tb_avatar.PlaceholderText = "Avatar barcode";
            tb_avatar.Size = new Size(217, 23);
            tb_avatar.TabIndex = 4;
            // 
            // btn_setAvatar
            // 
            btn_setAvatar.BackColor = SystemColors.ControlLight;
            btn_setAvatar.FlatStyle = FlatStyle.Flat;
            btn_setAvatar.ForeColor = SystemColors.ControlText;
            btn_setAvatar.Location = new Point(229, 15);
            btn_setAvatar.Name = "btn_setAvatar";
            btn_setAvatar.Size = new Size(72, 23);
            btn_setAvatar.TabIndex = 3;
            btn_setAvatar.Text = "Set Avatar";
            btn_setAvatar.UseVisualStyleBackColor = false;
            btn_setAvatar.Click += btn_setAvatar1;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(tb_spawn);
            groupBox7.Controls.Add(btn_spawn);
            groupBox7.ForeColor = SystemColors.ControlLightLight;
            groupBox7.Location = new Point(198, 317);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(311, 45);
            groupBox7.TabIndex = 10;
            groupBox7.TabStop = false;
            groupBox7.Text = "Spawn";
            // 
            // tb_spawn
            // 
            tb_spawn.BackColor = SystemColors.ControlLight;
            tb_spawn.BorderStyle = BorderStyle.FixedSingle;
            tb_spawn.Location = new Point(6, 15);
            tb_spawn.Name = "tb_spawn";
            tb_spawn.PlaceholderText = "Spawnable barcode";
            tb_spawn.Size = new Size(229, 23);
            tb_spawn.TabIndex = 4;
            // 
            // btn_spawn
            // 
            btn_spawn.BackColor = SystemColors.ControlLight;
            btn_spawn.FlatStyle = FlatStyle.Flat;
            btn_spawn.ForeColor = SystemColors.ControlText;
            btn_spawn.Location = new Point(241, 15);
            btn_spawn.Name = "btn_spawn";
            btn_spawn.Size = new Size(60, 23);
            btn_spawn.TabIndex = 3;
            btn_spawn.Text = "Spawn";
            btn_spawn.UseVisualStyleBackColor = false;
            btn_spawn.Click += btn_spawn1;
            // 
            // label_currentLevel
            // 
            label_currentLevel.AutoSize = true;
            label_currentLevel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_currentLevel.ForeColor = SystemColors.ControlLightLight;
            label_currentLevel.Location = new Point(18, 233);
            label_currentLevel.Name = "label_currentLevel";
            label_currentLevel.Size = new Size(133, 13);
            label_currentLevel.TabIndex = 12;
            label_currentLevel.Text = "Current Scene/Level: N/A";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(517, 588);
            Controls.Add(label_currentLevel);
            Controls.Add(groupBox7);
            Controls.Add(label_coordinates);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "BONELAB Developer Mode Tools";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion Windows Form Designer generated code

        private Button btn_connect;
        private Button btn_disconnect;
        private Button btn_levelreload;
        private Button btn_reloadpallet;
        private TextBox tb_pallet;
        private TextBox tb_command;
        private GroupBox groupBox1;
        private Button btn_command;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TextBox tb_ipport;

        private void btn_connect1(object sender, System.EventArgs e)
        {
            DevMode.ConnectButton(tb_ipport.Text);
        }

        private void btn_disconnect1(object sender, System.EventArgs e)
        {
            DevMode.DisconnectButton();
        }
        private void btn_levelreload1(object sender, System.EventArgs e)
        {
            DevMode.ReloadLevel();
        }

        private void btn_reloadpallet1(object sender, System.EventArgs e)
        {
            DevMode.ReloadPallet(tb_pallet.Text);
        }

        private void btn_loadpallet1(object sender, System.EventArgs e)
        {
            DevMode.SendCommand($"aw.load {tb_pallet.Text}");
        }

        private void btn_unloadpallet1(object sender, System.EventArgs e)
        {
            DevMode.SendCommand($"aw.unload {tb_pallet.Text}");
        }

        private void btn_setAvatar1(object sender, System.EventArgs e)
        {
            DevMode.SendCommand($"avatar {tb_avatar.Text}");
        }

        private void btn_spawn1(object sender, System.EventArgs e)
        {
            DevMode.SendCommand($"spawn {tb_spawn.Text}");
        }

        private void btn_loadLevel1(object sender, System.EventArgs e)
        {
            DevMode.SendCommand($"level {tb_level.Text}");
        }

        private void btn_command1(object sender, System.EventArgs e)
        {
            DevMode.SendCommand(tb_command.Text);
        }

        private void btn_clearLogs1(object sender, System.EventArgs e)
        {
            lv_logs.Items.Clear();
            Form1.AllLogs.Clear();
            DevMode.DontLog = [];
        }

        private Label connectionStatus;
        private GroupBox groupBox5;
        private Button btn_clearLogs;
        private Label label_coordinates;
        private CheckBox checkBox_autoCompleteBarcodes;
        private CheckBox cb_logMessage;
        private ListView lv_logs;
        private CheckBox cb_logDebug;
        private CheckBox cb_logErrors;
        private CheckBox cb_logWarnings;
        private Button btn_unloadpallet;
        private Button btn_loadpallet;
        private Button btn_loadLevel;
        private TextBox tb_level;
        private GroupBox groupBox6;
        private TextBox tb_avatar;
        private Button btn_setAvatar;
        private GroupBox groupBox7;
        private TextBox tb_spawn;
        private Button btn_spawn;
        private Label label_currentLevel;
        private Button btn_updateAC;
    }
}