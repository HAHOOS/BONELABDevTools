namespace BonelabDevMode
{
    partial class Changelogs
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
            TreeNode treeNode1 = new TreeNode("- Added children");
            TreeNode treeNode2 = new TreeNode("V1.0", new TreeNode[] { treeNode1 });
            label_changelogs = new Label();
            tv_changelogs = new TreeView();
            cb_split = new CheckBox();
            SuspendLayout();
            // 
            // label_changelogs
            // 
            label_changelogs.AutoSize = true;
            label_changelogs.Location = new Point(12, 9);
            label_changelogs.Name = "label_changelogs";
            label_changelogs.Size = new Size(113, 15);
            label_changelogs.TabIndex = 0;
            label_changelogs.Text = "Changelogs for N/A";
            // 
            // tv_changelogs
            // 
            tv_changelogs.BackColor = SystemColors.WindowText;
            tv_changelogs.ForeColor = SystemColors.ControlLightLight;
            tv_changelogs.ImeMode = ImeMode.Off;
            tv_changelogs.LineColor = Color.White;
            tv_changelogs.Location = new Point(12, 62);
            tv_changelogs.Name = "tv_changelogs";
            treeNode1.ForeColor = Color.Black;
            treeNode1.Name = "Node1";
            treeNode1.Text = "- Added children";
            treeNode2.BackColor = SystemColors.WindowText;
            treeNode2.ForeColor = SystemColors.ControlLightLight;
            treeNode2.Name = "Node0";
            treeNode2.Text = "V1.0";
            tv_changelogs.Nodes.AddRange(new TreeNode[] { treeNode2 });
            tv_changelogs.ShowLines = false;
            tv_changelogs.Size = new Size(493, 359);
            tv_changelogs.TabIndex = 1;
            // 
            // cb_split
            // 
            cb_split.AutoSize = true;
            cb_split.Location = new Point(12, 27);
            cb_split.Name = "cb_split";
            cb_split.Size = new Size(197, 19);
            cb_split.TabIndex = 2;
            cb_split.Text = "Add to new line when \"- \" found";
            cb_split.UseVisualStyleBackColor = true;
            cb_split.CheckedChanged += cb_split_CheckedChanged;
            // 
            // Changelogs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowText;
            ClientSize = new Size(517, 433);
            Controls.Add(cb_split);
            Controls.Add(tv_changelogs);
            Controls.Add(label_changelogs);
            ForeColor = SystemColors.ControlLightLight;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Changelogs";
            Text = "BLDMT | Pallet Changelogs";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_changelogs;
        private TreeView tv_changelogs;
        private CheckBox cb_split;
    }
}