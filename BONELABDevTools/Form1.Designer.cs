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
            tb_reloadpallet = new TextBox();
            tb_command = new TextBox();
            groupBox1 = new GroupBox();
            btn_command = new Button();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            connectionStatus = new Label();
            groupBox4 = new GroupBox();
            groupBox5 = new GroupBox();
            btn_clearLogs = new Button();
            logsListBox = new ListBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // btn_connect
            // 
            btn_connect.BackColor = SystemColors.ControlLight;
            btn_connect.FlatStyle = FlatStyle.Flat;
            btn_connect.ForeColor = SystemColors.ControlText;
            btn_connect.Location = new Point(6, 22);
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
            btn_disconnect.Location = new Point(93, 22);
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
            btn_levelreload.Size = new Size(174, 23);
            btn_levelreload.TabIndex = 2;
            btn_levelreload.Text = "Reload Level";
            btn_levelreload.UseVisualStyleBackColor = false;
            btn_levelreload.Click += btn_levelreload1;
            // 
            // btn_reloadpallet
            // 
            btn_reloadpallet.BackColor = SystemColors.ControlLight;
            btn_reloadpallet.FlatStyle = FlatStyle.Flat;
            btn_reloadpallet.ForeColor = SystemColors.ControlText;
            btn_reloadpallet.Location = new Point(168, 22);
            btn_reloadpallet.Name = "btn_reloadpallet";
            btn_reloadpallet.Size = new Size(133, 23);
            btn_reloadpallet.TabIndex = 3;
            btn_reloadpallet.Text = "Reload Pallet";
            btn_reloadpallet.UseVisualStyleBackColor = false;
            btn_reloadpallet.Click += btn_reloadpallet1;
            // 
            // tb_reloadpallet
            // 
            tb_reloadpallet.BackColor = SystemColors.ControlLight;
            tb_reloadpallet.BorderStyle = BorderStyle.FixedSingle;
            tb_reloadpallet.Location = new Point(6, 22);
            tb_reloadpallet.Name = "tb_reloadpallet";
            tb_reloadpallet.PlaceholderText = "PalletAuthor.PalletName";
            tb_reloadpallet.Size = new Size(156, 23);
            tb_reloadpallet.TabIndex = 4;
            // 
            // tb_command
            // 
            tb_command.BackColor = SystemColors.ControlLight;
            tb_command.BorderStyle = BorderStyle.FixedSingle;
            tb_command.Location = new Point(6, 22);
            tb_command.Name = "tb_command";
            tb_command.PlaceholderText = "Command";
            tb_command.Size = new Size(156, 23);
            tb_command.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_command);
            groupBox1.Controls.Add(tb_command);
            groupBox1.ForeColor = SystemColors.ControlLightLight;
            groupBox1.Location = new Point(198, 90);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(311, 67);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Command";
            // 
            // btn_command
            // 
            btn_command.BackColor = SystemColors.ControlLight;
            btn_command.FlatStyle = FlatStyle.Flat;
            btn_command.ForeColor = SystemColors.ControlText;
            btn_command.Location = new Point(168, 22);
            btn_command.Name = "btn_command";
            btn_command.Size = new Size(133, 23);
            btn_command.TabIndex = 8;
            btn_command.Text = "Send";
            btn_command.UseVisualStyleBackColor = false;
            btn_command.Click += btn_command1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tb_reloadpallet);
            groupBox2.Controls.Add(btn_reloadpallet);
            groupBox2.ForeColor = SystemColors.ControlLightLight;
            groupBox2.Location = new Point(198, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(311, 67);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Pallet";
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.Black;
            groupBox3.Controls.Add(connectionStatus);
            groupBox3.Controls.Add(btn_connect);
            groupBox3.Controls.Add(btn_disconnect);
            groupBox3.ForeColor = SystemColors.ControlLightLight;
            groupBox3.Location = new Point(12, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(180, 67);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Connection";
            // 
            // connectionStatus
            // 
            connectionStatus.AutoSize = true;
            connectionStatus.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            connectionStatus.ForeColor = Color.Red;
            connectionStatus.Location = new Point(6, 48);
            connectionStatus.Name = "connectionStatus";
            connectionStatus.Size = new Size(83, 13);
            connectionStatus.TabIndex = 2;
            connectionStatus.Text = "Not connected";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btn_levelreload);
            groupBox4.ForeColor = SystemColors.ControlLightLight;
            groupBox4.Location = new Point(12, 90);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(186, 67);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "Level";
            // 
            // groupBox5
            // 
            groupBox5.BackColor = SystemColors.ActiveCaptionText;
            groupBox5.Controls.Add(btn_clearLogs);
            groupBox5.Controls.Add(logsListBox);
            groupBox5.ForeColor = SystemColors.ControlLightLight;
            groupBox5.Location = new Point(12, 163);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(497, 167);
            groupBox5.TabIndex = 11;
            groupBox5.TabStop = false;
            groupBox5.Text = "Logs";
            // 
            // btn_clearLogs
            // 
            btn_clearLogs.BackColor = SystemColors.ControlLight;
            btn_clearLogs.FlatStyle = FlatStyle.Flat;
            btn_clearLogs.ForeColor = SystemColors.ControlText;
            btn_clearLogs.Location = new Point(6, 138);
            btn_clearLogs.Name = "btn_clearLogs";
            btn_clearLogs.Size = new Size(481, 23);
            btn_clearLogs.TabIndex = 9;
            btn_clearLogs.Text = "Clear";
            btn_clearLogs.UseVisualStyleBackColor = false;
            btn_clearLogs.Click += btn_clearLogs1;
            // 
            // logsListBox
            // 
            logsListBox.CausesValidation = false;
            logsListBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            logsListBox.FormattingEnabled = true;
            logsListBox.HorizontalScrollbar = true;
            logsListBox.ItemHeight = 13;
            logsListBox.Location = new Point(6, 22);
            logsListBox.Name = "logsListBox";
            logsListBox.SelectionMode = SelectionMode.None;
            logsListBox.Size = new Size(481, 108);
            logsListBox.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(517, 342);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "BONELAB Developer Mode Tools";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btn_connect;
        private Button btn_disconnect;
        private Button btn_levelreload;
        private Button btn_reloadpallet;
        private TextBox tb_reloadpallet;
        private TextBox tb_command;
        private GroupBox groupBox1;
        private Button btn_command;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;

        private void btn_connect1(object sender, System.EventArgs e)
        {
            DevMode.ConnectButton();
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
            DevMode.ReloadPallet(tb_reloadpallet.Text);
        }

        private void btn_command1(object sender, System.EventArgs e)
        {
            DevMode.SendCommand(tb_command.Text);
        }

        private void btn_clearLogs1(object sender, System.EventArgs e)
        {
            logsListBox.Items.Clear();
        }

        private Label connectionStatus;
        private GroupBox groupBox5;
        private ListBox logsListBox;
        private Button btn_clearLogs;
    }
}
