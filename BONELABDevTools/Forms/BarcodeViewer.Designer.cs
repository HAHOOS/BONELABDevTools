using PresentationControls;

namespace BonelabDevMode
{
    partial class BarcodeViewer
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
            components = new System.ComponentModel.Container();
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "com.stresslevelzero.bonerlab", "I love BONELAB", "SPAWNABLE", "1.0.0", "1.2.0" }, -1);
            ListViewItem listViewItem2 = new ListViewItem("Test2");
            CheckBoxProperties checkBoxProperties1 = new CheckBoxProperties();
            CheckBoxProperties checkBoxProperties2 = new CheckBoxProperties();
            lv_barcodes = new ListView();
            Barcode = new ColumnHeader();
            Title = new ColumnHeader();
            Type = new ColumnHeader();
            Version = new ColumnHeader();
            SDKVersion = new ColumnHeader();
            Author = new ColumnHeader();
            Tags = new ColumnHeader();
            gb_list = new GroupBox();
            groupBox2 = new GroupBox();
            label3 = new Label();
            cb_tags = new CheckBoxComboBox();
            tb_searchAuthor = new TextBox();
            label2 = new Label();
            tb_searchSDKVer = new TextBox();
            tb_searchVersion = new TextBox();
            cb_searchType = new CheckBoxComboBox();
            tb_searchTitle = new TextBox();
            tb_searchBarcode = new TextBox();
            btn_search = new Button();
            groupBox3 = new GroupBox();
            label1 = new Label();
            cb_autoSearch = new CheckBox();
            btn_refresh = new Button();
            cms_item = new ContextMenuStrip(components);
            openContainingFolderInExplorerToolStripMenuItem = new ToolStripMenuItem();
            openJSONFileToolStripMenuItem = new ToolStripMenuItem();
            editValuesToolStripMenuItem = new ToolStripMenuItem();
            palletToolStripMenuItem1 = new ToolStripMenuItem();
            objectToolStripMenuItem1 = new ToolStripMenuItem();
            checkChangelogsToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            palletToolStripMenuItem = new ToolStripMenuItem();
            objectToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            barcodeToolStripMenuItem = new ToolStripMenuItem();
            titleToolStripMenuItem = new ToolStripMenuItem();
            authorToolStripMenuItem = new ToolStripMenuItem();
            typeToolStripMenuItem = new ToolStripMenuItem();
            versionToolStripMenuItem = new ToolStripMenuItem();
            sDKVersionToolStripMenuItem = new ToolStripMenuItem();
            tagsToolStripMenuItem = new ToolStripMenuItem();
            gb_list.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            cms_item.SuspendLayout();
            SuspendLayout();
            // 
            // lv_barcodes
            // 
            lv_barcodes.Columns.AddRange(new ColumnHeader[] { Barcode, Title, Type, Version, SDKVersion, Author, Tags });
            lv_barcodes.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lv_barcodes.FullRowSelect = true;
            lv_barcodes.GridLines = true;
            lv_barcodes.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            lv_barcodes.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2 });
            lv_barcodes.Location = new Point(6, 14);
            lv_barcodes.Name = "lv_barcodes";
            lv_barcodes.ShowItemToolTips = true;
            lv_barcodes.Size = new Size(965, 382);
            lv_barcodes.TabIndex = 0;
            lv_barcodes.UseCompatibleStateImageBehavior = false;
            lv_barcodes.View = View.Details;
            lv_barcodes.MouseClick += Lv_barcodes_MouseClick;
            // 
            // Barcode
            // 
            Barcode.Text = "Barcode";
            Barcode.Width = 375;
            // 
            // Title
            // 
            Title.Text = "Title";
            Title.Width = 185;
            // 
            // Type
            // 
            Type.DisplayIndex = 3;
            Type.Text = "Type";
            Type.Width = 115;
            // 
            // Version
            // 
            Version.DisplayIndex = 4;
            Version.Text = "Version";
            Version.Width = 90;
            // 
            // SDKVersion
            // 
            SDKVersion.DisplayIndex = 5;
            SDKVersion.Text = "SDK Version";
            SDKVersion.Width = 90;
            // 
            // Author
            // 
            Author.DisplayIndex = 2;
            Author.Text = "Author";
            Author.Width = 115;
            // 
            // Tags
            // 
            Tags.Text = "Tags";
            Tags.Width = 146;
            // 
            // gb_list
            // 
            gb_list.Controls.Add(lv_barcodes);
            gb_list.ForeColor = SystemColors.ControlLightLight;
            gb_list.Location = new Point(12, 114);
            gb_list.Name = "gb_list";
            gb_list.Size = new Size(974, 402);
            gb_list.TabIndex = 1;
            gb_list.TabStop = false;
            gb_list.Text = "List";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(cb_tags);
            groupBox2.Controls.Add(tb_searchAuthor);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(tb_searchSDKVer);
            groupBox2.Controls.Add(tb_searchVersion);
            groupBox2.Controls.Add(cb_searchType);
            groupBox2.Controls.Add(tb_searchTitle);
            groupBox2.Controls.Add(tb_searchBarcode);
            groupBox2.ForeColor = SystemColors.ControlLightLight;
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1132, 96);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Filter";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(734, 44);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 6;
            label3.Text = "Type:";
            // 
            // cb_tags
            // 
            checkBoxProperties1.ForeColor = SystemColors.ControlText;
            cb_tags.CheckBoxProperties = checkBoxProperties1;
            cb_tags.DisplayMemberSingleItem = "";
            cb_tags.FormattingEnabled = true;
            cb_tags.Location = new Point(972, 62);
            cb_tags.Name = "cb_tags";
            cb_tags.Size = new Size(154, 23);
            cb_tags.TabIndex = 3;
            // 
            // tb_searchAuthor
            // 
            tb_searchAuthor.Location = new Point(621, 18);
            tb_searchAuthor.Name = "tb_searchAuthor";
            tb_searchAuthor.PlaceholderText = "Search (Author)";
            tb_searchAuthor.Size = new Size(270, 23);
            tb_searchAuthor.TabIndex = 5;
            tb_searchAuthor.TextChanged += AutoSearch;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(972, 44);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 4;
            label2.Text = "Tags:";
            // 
            // tb_searchSDKVer
            // 
            tb_searchSDKVer.Location = new Point(897, 18);
            tb_searchSDKVer.Name = "tb_searchSDKVer";
            tb_searchSDKVer.PlaceholderText = "Search (SDK Version)";
            tb_searchSDKVer.Size = new Size(229, 23);
            tb_searchSDKVer.TabIndex = 4;
            tb_searchSDKVer.TextChanged += AutoSearch;
            // 
            // tb_searchVersion
            // 
            tb_searchVersion.Location = new Point(458, 62);
            tb_searchVersion.Name = "tb_searchVersion";
            tb_searchVersion.PlaceholderText = "Search (Version)";
            tb_searchVersion.Size = new Size(270, 23);
            tb_searchVersion.TabIndex = 3;
            tb_searchVersion.TextChanged += AutoSearch;
            // 
            // cb_searchType
            // 
            checkBoxProperties2.ForeColor = SystemColors.ControlText;
            cb_searchType.CheckBoxProperties = checkBoxProperties2;
            cb_searchType.DisplayMemberSingleItem = "";
            cb_searchType.FormattingEnabled = true;
            cb_searchType.Location = new Point(734, 62);
            cb_searchType.Name = "cb_searchType";
            cb_searchType.Size = new Size(229, 23);
            cb_searchType.TabIndex = 2;
            cb_searchType.TextChanged += AutoSearch;
            // 
            // tb_searchTitle
            // 
            tb_searchTitle.Location = new Point(6, 62);
            tb_searchTitle.Name = "tb_searchTitle";
            tb_searchTitle.PlaceholderText = "Search (Title)";
            tb_searchTitle.Size = new Size(446, 23);
            tb_searchTitle.TabIndex = 1;
            tb_searchTitle.TextChanged += AutoSearch;
            // 
            // tb_searchBarcode
            // 
            tb_searchBarcode.Location = new Point(6, 18);
            tb_searchBarcode.Name = "tb_searchBarcode";
            tb_searchBarcode.PlaceholderText = "Search (Barcode)";
            tb_searchBarcode.Size = new Size(609, 23);
            tb_searchBarcode.TabIndex = 0;
            tb_searchBarcode.TextChanged += AutoSearch;
            // 
            // btn_search
            // 
            btn_search.ForeColor = SystemColors.WindowText;
            btn_search.Location = new Point(6, 331);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(140, 30);
            btn_search.TabIndex = 2;
            btn_search.Text = "Search";
            btn_search.UseVisualStyleBackColor = true;
            btn_search.Click += Search;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btn_search);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(cb_autoSearch);
            groupBox3.Controls.Add(btn_refresh);
            groupBox3.ForeColor = SystemColors.ControlLightLight;
            groupBox3.Location = new Point(992, 114);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(152, 413);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Other";
            // 
            // label1
            // 
            label1.Location = new Point(6, 44);
            label1.Name = "label1";
            label1.Size = new Size(140, 67);
            label1.TabIndex = 3;
            label1.Text = "Automatically searches after text changes in any of the text boxes/combo boxes\r\n";
            // 
            // cb_autoSearch
            // 
            cb_autoSearch.AutoSize = true;
            cb_autoSearch.ForeColor = SystemColors.ControlLightLight;
            cb_autoSearch.ImageAlign = ContentAlignment.TopCenter;
            cb_autoSearch.Location = new Point(6, 22);
            cb_autoSearch.Name = "cb_autoSearch";
            cb_autoSearch.Size = new Size(92, 19);
            cb_autoSearch.TabIndex = 2;
            cb_autoSearch.Text = "Auto-Search";
            cb_autoSearch.TextAlign = ContentAlignment.BottomCenter;
            cb_autoSearch.TextImageRelation = TextImageRelation.ImageAboveText;
            cb_autoSearch.UseVisualStyleBackColor = true;
            // 
            // btn_refresh
            // 
            btn_refresh.ForeColor = SystemColors.WindowText;
            btn_refresh.Location = new Point(6, 372);
            btn_refresh.Name = "btn_refresh";
            btn_refresh.Size = new Size(140, 30);
            btn_refresh.TabIndex = 1;
            btn_refresh.Text = "Refresh";
            btn_refresh.UseVisualStyleBackColor = true;
            btn_refresh.Click += Btn_refresh_Click;
            // 
            // cms_item
            // 
            cms_item.Items.AddRange(new ToolStripItem[] { copyToolStripMenuItem, openContainingFolderInExplorerToolStripMenuItem, openJSONFileToolStripMenuItem, editValuesToolStripMenuItem, checkChangelogsToolStripMenuItem, removeToolStripMenuItem });
            cms_item.Name = "cms_item";
            cms_item.Size = new Size(261, 158);
            // 
            // openContainingFolderInExplorerToolStripMenuItem
            // 
            openContainingFolderInExplorerToolStripMenuItem.Name = "openContainingFolderInExplorerToolStripMenuItem";
            openContainingFolderInExplorerToolStripMenuItem.Size = new Size(260, 22);
            openContainingFolderInExplorerToolStripMenuItem.Text = "Open Containing Folder in Explorer";
            openContainingFolderInExplorerToolStripMenuItem.Click += OpenContainingFolderInExplorerToolStripMenuItem_Click;
            // 
            // openJSONFileToolStripMenuItem
            // 
            openJSONFileToolStripMenuItem.Name = "openJSONFileToolStripMenuItem";
            openJSONFileToolStripMenuItem.Size = new Size(260, 22);
            openJSONFileToolStripMenuItem.Text = "Open JSON file";
            openJSONFileToolStripMenuItem.Click += OpenJSONFileToolStripMenuItem_Click;
            // 
            // editValuesToolStripMenuItem
            // 
            editValuesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { palletToolStripMenuItem1, objectToolStripMenuItem1 });
            editValuesToolStripMenuItem.Name = "editValuesToolStripMenuItem";
            editValuesToolStripMenuItem.Size = new Size(260, 22);
            editValuesToolStripMenuItem.Text = "Edit values";
            // 
            // palletToolStripMenuItem1
            // 
            palletToolStripMenuItem1.Name = "palletToolStripMenuItem1";
            palletToolStripMenuItem1.Size = new Size(109, 22);
            palletToolStripMenuItem1.Text = "Pallet";
            palletToolStripMenuItem1.Click += PalletToolStripMenuItem1_Click;
            // 
            // objectToolStripMenuItem1
            // 
            objectToolStripMenuItem1.Name = "objectToolStripMenuItem1";
            objectToolStripMenuItem1.Size = new Size(109, 22);
            objectToolStripMenuItem1.Text = "Object";
            objectToolStripMenuItem1.Click += ObjectToolStripMenuItem1_Click;
            // 
            // checkChangelogsToolStripMenuItem
            // 
            checkChangelogsToolStripMenuItem.Name = "checkChangelogsToolStripMenuItem";
            checkChangelogsToolStripMenuItem.Size = new Size(260, 22);
            checkChangelogsToolStripMenuItem.Text = "Check changelogs";
            checkChangelogsToolStripMenuItem.Click += CheckChangelogsToolStripMenuItem_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { palletToolStripMenuItem, objectToolStripMenuItem });
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(260, 22);
            removeToolStripMenuItem.Text = "Remove";
            // 
            // palletToolStripMenuItem
            // 
            palletToolStripMenuItem.Name = "palletToolStripMenuItem";
            palletToolStripMenuItem.Size = new Size(109, 22);
            palletToolStripMenuItem.Text = "Pallet";
            palletToolStripMenuItem.Click += PalletToolStripMenuItem_Click;
            // 
            // objectToolStripMenuItem
            // 
            objectToolStripMenuItem.Name = "objectToolStripMenuItem";
            objectToolStripMenuItem.Size = new Size(109, 22);
            objectToolStripMenuItem.Text = "Object";
            objectToolStripMenuItem.Click += ObjectToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { barcodeToolStripMenuItem, titleToolStripMenuItem, authorToolStripMenuItem, typeToolStripMenuItem, versionToolStripMenuItem, sDKVersionToolStripMenuItem, tagsToolStripMenuItem });
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(260, 22);
            copyToolStripMenuItem.Text = "Copy";
            // 
            // barcodeToolStripMenuItem
            // 
            barcodeToolStripMenuItem.Name = "barcodeToolStripMenuItem";
            barcodeToolStripMenuItem.Size = new Size(180, 22);
            barcodeToolStripMenuItem.Text = "Barcode";
            barcodeToolStripMenuItem.Click += Copy;
            // 
            // titleToolStripMenuItem
            // 
            titleToolStripMenuItem.Name = "titleToolStripMenuItem";
            titleToolStripMenuItem.Size = new Size(180, 22);
            titleToolStripMenuItem.Text = "Title";
            titleToolStripMenuItem.Click += Copy;
            // 
            // authorToolStripMenuItem
            // 
            authorToolStripMenuItem.Name = "authorToolStripMenuItem";
            authorToolStripMenuItem.Size = new Size(180, 22);
            authorToolStripMenuItem.Text = "Author";
            authorToolStripMenuItem.Click += Copy;
            // 
            // typeToolStripMenuItem
            // 
            typeToolStripMenuItem.Name = "typeToolStripMenuItem";
            typeToolStripMenuItem.Size = new Size(180, 22);
            typeToolStripMenuItem.Text = "Type";
            typeToolStripMenuItem.Click += Copy;
            // 
            // versionToolStripMenuItem
            // 
            versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            versionToolStripMenuItem.Size = new Size(180, 22);
            versionToolStripMenuItem.Text = "Version";
            versionToolStripMenuItem.Click += Copy;
            // 
            // sDKVersionToolStripMenuItem
            // 
            sDKVersionToolStripMenuItem.Name = "sDKVersionToolStripMenuItem";
            sDKVersionToolStripMenuItem.Size = new Size(180, 22);
            sDKVersionToolStripMenuItem.Text = "SDK Version";
            sDKVersionToolStripMenuItem.Click += Copy;
            // 
            // tagsToolStripMenuItem
            // 
            tagsToolStripMenuItem.Name = "tagsToolStripMenuItem";
            tagsToolStripMenuItem.Size = new Size(180, 22);
            tagsToolStripMenuItem.Text = "Tags";
            tagsToolStripMenuItem.Click += Copy;
            // 
            // BarcodeViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1156, 528);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(gb_list);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "BarcodeViewer";
            Text = "BLDMT | BarcodeViewer";
            gb_list.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            cms_item.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private void AutoSearch(object sender, EventArgs e)
        {
            if(cb_autoSearch.Checked) Search(sender , e);
        }
            
        private ListView lv_barcodes;
        private ColumnHeader Barcode;
        private ColumnHeader Title;
        private ColumnHeader Type;
        private ColumnHeader Version;
        private ColumnHeader SDKVersion;
        private GroupBox gb_list;
        private GroupBox groupBox2;
        private TextBox tb_searchSDKVer;
        private TextBox tb_searchVersion;
        private CheckBoxComboBox cb_searchType;
        private TextBox tb_searchTitle;
        private TextBox tb_searchBarcode;
        private GroupBox groupBox3;
        private Button btn_refresh;
        private ColumnHeader Author;
        private TextBox tb_searchAuthor;
        private Button btn_search;
        private Label label1;
        private CheckBox cb_autoSearch;
        private ContextMenuStrip cms_item;
        private ToolStripMenuItem openContainingFolderInExplorerToolStripMenuItem;
        private ToolStripMenuItem openJSONFileToolStripMenuItem;
        private ToolStripMenuItem checkChangelogsToolStripMenuItem;
        private CheckBoxComboBox cb_tags;
        private Label label2;
        private ColumnHeader Tags;
        private Label label3;
        private ToolStripMenuItem editValuesToolStripMenuItem;
        private ToolStripMenuItem palletToolStripMenuItem1;
        private ToolStripMenuItem objectToolStripMenuItem1;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem palletToolStripMenuItem;
        private ToolStripMenuItem objectToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem barcodeToolStripMenuItem;
        private ToolStripMenuItem titleToolStripMenuItem;
        private ToolStripMenuItem authorToolStripMenuItem;
        private ToolStripMenuItem versionToolStripMenuItem;
        private ToolStripMenuItem sDKVersionToolStripMenuItem;
        private ToolStripMenuItem tagsToolStripMenuItem;
        private ToolStripMenuItem typeToolStripMenuItem;
    }
}