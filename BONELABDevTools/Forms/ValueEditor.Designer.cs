namespace BonelabDevMode.Forms
{
    partial class ValueEditor
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
            groupBox1 = new GroupBox();
            gb_pallet = new GroupBox();
            tb_author = new TextBox();
            cb_sdkVersion = new ComboBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            tb_version = new TextBox();
            label5 = new Label();
            cb_redacted = new CheckBox();
            tb_tags = new TextBox();
            label4 = new Label();
            cb_unlockable = new CheckBox();
            tb_description = new TextBox();
            label3 = new Label();
            tb_title = new TextBox();
            label2 = new Label();
            tb_barcode = new TextBox();
            label1 = new Label();
            btn_saveAndExit = new Button();
            btn_save = new Button();
            groupBox1.SuspendLayout();
            gb_pallet.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(gb_pallet);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cb_redacted);
            groupBox1.Controls.Add(tb_tags);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(cb_unlockable);
            groupBox1.Controls.Add(tb_description);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(tb_title);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tb_barcode);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.ControlLightLight;
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(405, 471);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Values";
            // 
            // gb_pallet
            // 
            gb_pallet.Controls.Add(tb_author);
            gb_pallet.Controls.Add(cb_sdkVersion);
            gb_pallet.Controls.Add(label8);
            gb_pallet.Controls.Add(label7);
            gb_pallet.Controls.Add(label6);
            gb_pallet.Controls.Add(tb_version);
            gb_pallet.ForeColor = SystemColors.ControlLightLight;
            gb_pallet.Location = new Point(6, 308);
            gb_pallet.Name = "gb_pallet";
            gb_pallet.Size = new Size(393, 142);
            gb_pallet.TabIndex = 12;
            gb_pallet.TabStop = false;
            gb_pallet.Text = "Pallet Specific";
            // 
            // tb_author
            // 
            tb_author.Location = new Point(6, 81);
            tb_author.Name = "tb_author";
            tb_author.Size = new Size(369, 23);
            tb_author.TabIndex = 14;
            // 
            // cb_sdkVersion
            // 
            cb_sdkVersion.FormattingEnabled = true;
            cb_sdkVersion.Items.AddRange(new object[] { "0.6.0", "1.1.0", "1.2.0" });
            cb_sdkVersion.Location = new Point(228, 37);
            cb_sdkVersion.Name = "cb_sdkVersion";
            cb_sdkVersion.Size = new Size(147, 23);
            cb_sdkVersion.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 63);
            label8.Name = "label8";
            label8.Size = new Size(44, 15);
            label8.TabIndex = 13;
            label8.Text = "Author";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(228, 19);
            label7.Name = "label7";
            label7.Size = new Size(69, 15);
            label7.TabIndex = 14;
            label7.Text = "SDK Version";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 19);
            label6.Name = "label6";
            label6.Size = new Size(45, 15);
            label6.TabIndex = 13;
            label6.Text = "Version";
            // 
            // tb_version
            // 
            tb_version.Location = new Point(6, 37);
            tb_version.Name = "tb_version";
            tb_version.Size = new Size(153, 23);
            tb_version.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 453);
            label5.Name = "label5";
            label5.Size = new Size(375, 15);
            label5.TabIndex = 11;
            label5.Text = "Currently the editor does not allow editing values like Type, Assets etc.";
            // 
            // cb_redacted
            // 
            cb_redacted.AutoSize = true;
            cb_redacted.Location = new Point(6, 258);
            cb_redacted.Name = "cb_redacted";
            cb_redacted.Size = new Size(75, 19);
            cb_redacted.TabIndex = 10;
            cb_redacted.Text = "Redacted";
            cb_redacted.UseVisualStyleBackColor = true;
            // 
            // tb_tags
            // 
            tb_tags.Location = new Point(6, 229);
            tb_tags.Name = "tb_tags";
            tb_tags.PlaceholderText = "Divide tags by \",\"";
            tb_tags.Size = new Size(393, 23);
            tb_tags.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 211);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 8;
            label4.Text = "Tags";
            // 
            // cb_unlockable
            // 
            cb_unlockable.AutoSize = true;
            cb_unlockable.Location = new Point(6, 283);
            cb_unlockable.Name = "cb_unlockable";
            cb_unlockable.Size = new Size(85, 19);
            cb_unlockable.TabIndex = 6;
            cb_unlockable.Text = "Unlockable";
            cb_unlockable.UseVisualStyleBackColor = true;
            // 
            // tb_description
            // 
            tb_description.Location = new Point(6, 125);
            tb_description.Multiline = true;
            tb_description.Name = "tb_description";
            tb_description.Size = new Size(393, 83);
            tb_description.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 107);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 4;
            label3.Text = "Description";
            // 
            // tb_title
            // 
            tb_title.Location = new Point(6, 81);
            tb_title.Name = "tb_title";
            tb_title.Size = new Size(393, 23);
            tb_title.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 63);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 2;
            label2.Text = "Title";
            // 
            // tb_barcode
            // 
            tb_barcode.Location = new Point(6, 37);
            tb_barcode.Name = "tb_barcode";
            tb_barcode.ReadOnly = true;
            tb_barcode.Size = new Size(393, 23);
            tb_barcode.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "Barcode";
            // 
            // btn_saveAndExit
            // 
            btn_saveAndExit.ForeColor = SystemColors.ActiveCaptionText;
            btn_saveAndExit.Location = new Point(12, 489);
            btn_saveAndExit.Name = "btn_saveAndExit";
            btn_saveAndExit.Size = new Size(405, 23);
            btn_saveAndExit.TabIndex = 1;
            btn_saveAndExit.Text = "Save And Exit";
            btn_saveAndExit.UseVisualStyleBackColor = true;
            btn_saveAndExit.Click += btn_saveAndExit_Click;
            // 
            // btn_save
            // 
            btn_save.ForeColor = SystemColors.ActiveCaptionText;
            btn_save.Location = new Point(12, 518);
            btn_save.Name = "btn_save";
            btn_save.Size = new Size(405, 23);
            btn_save.TabIndex = 2;
            btn_save.Text = "Save";
            btn_save.UseVisualStyleBackColor = true;
            btn_save.Click += btn_save_Click;
            // 
            // ValueEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowText;
            ClientSize = new Size(429, 547);
            Controls.Add(btn_save);
            Controls.Add(btn_saveAndExit);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.ControlLightLight;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ValueEditor";
            Text = "BLDMT | Value Editor";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            gb_pallet.ResumeLayout(false);
            gb_pallet.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label4;
        private CheckBox cb_unlockable;
        private TextBox tb_description;
        private Label label3;
        private TextBox tb_title;
        private Label label2;
        private TextBox tb_barcode;
        private Label label1;
        private CheckBox cb_redacted;
        private TextBox tb_tags;
        private GroupBox gb_pallet;
        private TextBox tb_author;
        private ComboBox cb_sdkVersion;
        private Label label8;
        private Label label7;
        private Label label6;
        private TextBox tb_version;
        private Label label5;
        private Button btn_saveAndExit;
        private Button btn_save;
    }
}