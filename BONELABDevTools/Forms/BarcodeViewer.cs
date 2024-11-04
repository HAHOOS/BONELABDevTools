using BonelabDevMode.Controls;
using BonelabDevMode.Forms;
using BonelabDevMode.JSON;
using Newtonsoft.Json;
using PresentationControls;
using System.Diagnostics;
using static BonelabDevMode.Barcodes;
using static System.Windows.Forms.ListViewItem;

namespace BonelabDevMode
{
    public partial class BarcodeViewer : Form
    {
        public static Dictionary<PalletObject, ListViewItem> Items { get; private set; } = [];

        public static ListViewItem? CmsItem { get; private set; }

#pragma warning disable IDE1006 // Naming Styles
        public static List<string> tags { get; private set; } = [];
#pragma warning restore IDE1006 // Naming Styles

        private readonly CustomBackgroundWorker bw_search = new();
        private readonly CustomBackgroundWorker bw_list = new();

        private bool HandleCBCB(CheckBoxComboBox cbcb, KeyValuePair<PalletObject, ListViewItem> item, params string[]? validate)
        {
            List<string> _checked = [];
            cbcb.CheckBoxItems.ForEach(x =>
            {
                if (x.Checked) _checked.Add(x.Name);
            });
            if (_checked.Count > 0)
            {
                if (validate == null || validate.Length == 0)
                {
                    if (lv_barcodes.Items.Contains(item.Value))
                    {
                        void action() { lv_barcodes.Items.Remove(item.Value); }
                        InvokeSafe(lv_barcodes, action);
                    }
                    return true;
                }
                foreach (var check in _checked)
                {
                    if (validate.Contains(check))
                    {
                        return false;
                    }
                }
                if (lv_barcodes.Items.Contains(item.Value))
                {
                    void action() { lv_barcodes.Items.Remove(item.Value); }
                    InvokeSafe(lv_barcodes, action);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public BarcodeViewer()
        {
            InitializeComponent();
            bw_search.Start = () =>
            {
                //_Update();
                if (gb_list.InvokeRequired) gb_list.Invoke(() => gb_list.Text = "List (Searching with filters...)");
                else gb_list.Text = "List (Searching with filters...)";
                void suspend() { DrawingControl.SuspendDrawing(lv_barcodes); }
                if (lv_barcodes.InvokeRequired) lv_barcodes.Invoke(suspend);
                else suspend();
                foreach (var item in Items)
                {
                    Dictionary<int, string> tbs = new() {
                        { -1, tb_searchBarcode.Text },
                        { 1, tb_searchTitle.Text },
                        { 3, tb_searchVersion.Text },
                        { 4, tb_searchSDKVer.Text },
                        { 5, tb_searchAuthor.Text },
                    };

                    bool instructionsNotMet = false;
                    foreach (var text in tbs)
                    {
                        if (!string.IsNullOrWhiteSpace(text.Value))
                        {
                            ListViewSubItem? subItem = null;
                            if (text.Key != -1) subItem = item.Value.SubItems[(int)text.Key];
                            if (subItem != null)
                            {
                                if (!subItem.Text.Contains(text.Value))
                                {
                                    if (lv_barcodes?.Items?.Contains(item.Value) == true)
                                    {
                                        instructionsNotMet = true;
                                        void action() { lv_barcodes?.Items.Remove(item.Value); }
                                        InvokeSafe(lv_barcodes, action);
                                    }
                                }
                            }
                            else
                            {
                                if (!item.Value.Text.Contains(text.Value))
                                {
                                    instructionsNotMet = true;
                                    if (lv_barcodes?.Items?.Contains(item.Value) == true)
                                    {
                                        void action() { lv_barcodes?.Items.Remove(item.Value); }
                                        InvokeSafe(lv_barcodes, action);
                                    }
                                }
                            }
                        }
                    }

                    if (HandleCBCB(cb_tags, item, item.Key.Tags != null ? [.. item.Key.Tags] : null)) instructionsNotMet = true;
                    if (HandleCBCB(cb_searchType, item, item.Key.Type.ToString())) instructionsNotMet = true;

                    if (!instructionsNotMet)
                    {
                        if (lv_barcodes?.Items?.Contains(item.Value) == false)
                        {
                            void action() { lv_barcodes?.Items.Add(item.Value); }
                            InvokeSafe(lv_barcodes, action);
                            AssignGroup(item.Value);
                        }
                    }
                }
                void resume() { DrawingControl.ResumeDrawing(lv_barcodes!); }
                if (lv_barcodes?.InvokeRequired == true) lv_barcodes.Invoke(resume);
                else resume();
                if (gb_list.InvokeRequired) gb_list.Invoke(() => gb_list.Text = $"List ({lv_barcodes?.Items.Count} found)");
                else gb_list.Text = $"List ({lv_barcodes?.Items.Count} found)";
            };

            bw_list.Start = () =>
            {
                if (gb_list.InvokeRequired) gb_list.Invoke(() => gb_list.Text = "List (Updating, please wait...)");
                else gb_list.Text = "List (Updating, please wait...)";

                Items.Clear();
                tags.Clear();

                InvokeSafe(lv_barcodes, lv_barcodes.Items.Clear);
                InvokeSafe(lv_barcodes, lv_barcodes.Groups.Clear);
                InvokeSafe(cb_tags, cb_tags.Items.Clear);
                InvokeSafe(cb_searchType, cb_searchType.Items.Clear);
                InvokeSafe(cb_tags, cb_tags.CheckBoxItems.Clear);
                InvokeSafe(cb_searchType, cb_searchType.CheckBoxItems.Clear);

                var pallets = Barcodes.GetPOByType(BarcodeType.PALLET);

                DrawingControl.SuspendDrawing(lv_barcodes);

                foreach (var pallet in pallets)
                {
                    ListViewGroup group = new(pallet.Barcode, $"{pallet.Barcode} ({pallet.Title})")
                    {
                        Tag = pallet.Barcode,
                    };
                    int func() => lv_barcodes.Groups.Add(group);
                    InvokeSafe<int>(lv_barcodes, func);
                    Console.WriteLine(group.Tag);
                }

                ListViewGroup contentFunc() => lv_barcodes.Groups.Add("BONELAB_Content", "BONELAB Content");
                ListViewGroup otherFunc() => lv_barcodes.Groups.Add("N/A", "Other");
                var contentGroup = InvokeSafe<ListViewGroup>(lv_barcodes, contentFunc);
                var otherGroup = InvokeSafe<ListViewGroup>(lv_barcodes, otherFunc);

                Dictionary<string, int> TagCount = [];

                Dictionary<BarcodeType, int> TypeCount = [];

                int _index = 0;

                var barcodes = Barcodes.barcodes.Count - Barcodes.GetPOByType(BarcodeType.PALLET).Length;

                foreach (var obj in Barcodes.barcodes)
                {
                    if (obj.Type == BarcodeType.PALLET) continue;
                    if (gb_list.InvokeRequired) gb_list.Invoke(() => gb_list.Text = $"List (Updating, please wait... {_index++}/{barcodes})");
                    else gb_list.Text = $"List (Updating, please wait... {_index}/{barcodes}))";
                    var item = new ListViewItem(obj.Barcode)
                    {
                        ToolTipText = obj.Barcode,
                        Name = "Barcode"
                    };
                    string tagText = string.Empty;

                    if (TypeCount.TryGetValue(obj.Type, out int type_value))
                    {
                        TypeCount[obj.Type] = ++type_value;
                    }
                    else
                    {
                        TypeCount.Add(obj.Type, 1);
                    }

                    obj.Tags?.ForEach((x) =>
                    {
                        var tag = Main.AC_HTMLRemove().Replace(x, string.Empty);
                        if (!tags.Contains(x)) tags.Add(tag);
                        tagText += (tagText?.Length == 0 ? tag : ", " + tag);

                        if (TagCount.TryGetValue(tag, out int value))
                        {
                            TagCount[tag] = ++value;
                        }
                        else
                        {
                            TagCount.Add(tag, 1);
                        }
                    });
                    if (obj.Origin == null) item.Tag = obj.isBONELABContent ? "BONELAB_Content" : "N/A";
                    else item.Tag = !string.IsNullOrWhiteSpace(obj.Origin.Barcode) ? obj.Origin.Barcode : obj.isBONELABContent ? "BONELAB_Content" : "N/A";
                    item.SubItems.Add(new ListViewSubItem() { Text = Main.AC_HTMLRemove().Replace(obj.Title, string.Empty), Name = "Title" });
                    item.SubItems.Add(new ListViewSubItem() { Text = obj.Type.ToString(), Name = "Type" });
                    item.SubItems.Add(new ListViewSubItem() { Text = obj.Origin != null ? EmptyDefault(obj.Origin.Version) : "N/A", Name = "Version" });
                    item.SubItems.Add(new ListViewSubItem() { Text = obj.Origin != null ? EmptyDefault(obj.Origin.SDKVersion) : "N/A", Name = "SDKVersion" });
                    item.SubItems.Add(new ListViewSubItem() { Text = obj.Origin != null ? EmptyDefault(obj.Origin.Author) : "N/A", Name = "Author" });
                    item.SubItems.Add(new ListViewSubItem() { Text = Main.AC_HTMLRemove().Replace(EmptyDefault(tagText), string.Empty), Name = "Tags" });
                    ListViewItem func() => lv_barcodes.Items.Add(item);
                    InvokeSafe(lv_barcodes, func);

                    ListViewItem func2() => contentGroup.Items.Add(item);
                    if (obj.isBONELABContent) InvokeSafe(lv_barcodes, func2);

                    bool added = AssignGroup(item);

                    ListViewItem func3() => contentGroup.Items.Add(item);
                    if (!added) InvokeSafe(lv_barcodes, func3);
                    Items.Add(obj, item);
                }
                var list = TagCount.ToList();
                list = [.. list.OrderByDescending((x) => x.Value)];
                for (int i = 0; i < list.Count; i++)
                {
                    var tag = list[i];
                    int func() => cb_tags.Items.Add($"{tag.Key} [{tag.Value}]");
                    InvokeSafe(cb_tags, func);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    ((CheckBoxComboBoxItem?)cb_tags.CheckBoxItems[i]).Name = tag.Key;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }

                var list2 = TypeCount.ToList();
                list2 = [.. list2.OrderByDescending((x) => x.Value)];
                for (int i = 0; i < list2.Count; i++)
                {
                    var tag = list2[i];
                    int func() => cb_searchType.Items.Add($"{tag.Key} [{tag.Value}]");
                    InvokeSafe(cb_searchType, func);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    ((CheckBoxComboBoxItem?)cb_searchType.CheckBoxItems[i]).Name = tag.Key.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }

                DrawingControl.ResumeDrawing(lv_barcodes);

                if (gb_list.InvokeRequired) gb_list.Invoke(() => gb_list.Text = $"List ({lv_barcodes?.Items.Count} found)");
                else gb_list.Text = $"List ({lv_barcodes?.Items.Count} found)";
                bw_search.Work();
            };

            UpdateList();
        }

        public static string EmptyDefault(string? text)
        {
            if (text == null || string.IsNullOrWhiteSpace(text)) return "N/A";
            else return text;
        }

        public static void InvokeSafe(Control control, Action action)
        {
            if (control.InvokeRequired) control.Invoke(action);
            else action.Invoke();
        }

        public static void InvokeSafe(Control control, Action action, params object[] _params)
        {
            if (control.InvokeRequired) control.Invoke(action, _params);
            else action.DynamicInvoke(_params);
        }

        public static TResult InvokeSafe<TResult>(Control control, Func<TResult> func)
        {
            if (control.InvokeRequired) return control.Invoke<TResult>(func);
            else return func.Invoke();
        }

        public bool AssignGroup(ListViewItem item)
        {
            foreach (ListViewGroup group in lv_barcodes.Groups)
            {
                if (group.Tag == item.Tag)
                {
                    ListViewItem func() => group.Items.Add(item);
                    InvokeSafe(lv_barcodes, func);
                    return true;
                }
            }
            return false;
        }

        public void UpdateList()
        {
            bw_list.Work();
        }

        private void Btn_refresh_Click(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void Search(object sender, EventArgs e)
        {
            bw_search.Work();
        }

        private void Lv_barcodes_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focused = lv_barcodes.FocusedItem;
                if (focused?.Bounds.Contains(e.Location) == true)
                {
                    CmsItem = focused;
                    var palletObj = Items.FirstOrDefault(x => x.Value == CmsItem);
                    if (palletObj.Value == null) return;
                    if (palletObj.Key.isBONELABContent)
                    {
                        Invoke(() =>
                        {
                            editValuesToolStripMenuItem.Enabled = false;
                            removeToolStripMenuItem.Enabled = false;

                            palletToolStripMenuItem.Enabled = false;
                            palletToolStripMenuItem1.Enabled = false;

                            objectToolStripMenuItem.Enabled = false;
                            objectToolStripMenuItem1.Enabled = false;

                            openContainingFolderInExplorerToolStripMenuItem.Enabled = false;
                            openJSONFileToolStripMenuItem.Enabled = false;

                            checkChangelogsToolStripMenuItem.Enabled = false;
                        });
                    }
                    else
                    {
                        Invoke(() =>
                        {
                            editValuesToolStripMenuItem.Enabled = true;
                            removeToolStripMenuItem.Enabled = true;

                            palletToolStripMenuItem.Enabled = true;
                            palletToolStripMenuItem1.Enabled = true;

                            objectToolStripMenuItem.Enabled = true;
                            objectToolStripMenuItem1.Enabled = true;

                            openContainingFolderInExplorerToolStripMenuItem.Enabled = true;
                            openJSONFileToolStripMenuItem.Enabled = true;

                            checkChangelogsToolStripMenuItem.Enabled = true;
                        });
                    }
                    cms_item.Show(Cursor.Position);
                }
            }
        }

        private void OpenContainingFolderInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CmsItem != null)
            {
                var palletObj = Items.FirstOrDefault(x => x.Value == CmsItem);
                if (palletObj.Value == null) return;
                if (palletObj.Key.isBONELABContent)
                {
                    MessageBox.Show("Cannot open folder for BONELAB Content", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(palletObj.Key.Pallet?.DirectoryPath))
                {
                    MessageBox.Show("Pallet is not specified in the item or pallet does not have a directory path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Process.Start(new ProcessStartInfo()
                {
                    FileName = palletObj.Key.Pallet.DirectoryPath,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
        }

        private void OpenJSONFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CmsItem != null)
            {
                var palletObj = Items.FirstOrDefault(x => x.Value == CmsItem);
                if (palletObj.Value == null) return;
                if (palletObj.Key.isBONELABContent)
                {
                    MessageBox.Show("Cannot open pallet file for BONELAB Content", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(palletObj.Key.Pallet?.DirectoryPath))
                {
                    MessageBox.Show("Pallet is not specified in the item or pallet does not have a directory path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Process.Start(new ProcessStartInfo()
                {
                    FileName = palletObj.Key.Pallet.FilePath,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
        }

        private void CheckChangelogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CmsItem != null)
            {
                var palletObj = Items.FirstOrDefault(x => x.Value == CmsItem);
                if (palletObj.Value == null) return;

                if (palletObj.Key.isBONELABContent)
                {
                    MessageBox.Show("Cannot check changelogs for BONELAB Content", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (palletObj.Key.Origin == null)
                {
                    MessageBox.Show("Origin is not specified in the item ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (palletObj.Key.Origin.ChangeLogs?.Count == 0)
                {
                    MessageBox.Show("This pallet has no changelogs!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var popup = new Changelogs();
                    popup.Setup(palletObj.Key.Origin, palletObj.Key.Origin.ChangeLogs);
                    popup.ShowDialog();
                }
            }
        }

        private void PalletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CmsItem != null)
            {
                var palletObj = Items.FirstOrDefault(x => x.Value == CmsItem);
                if (palletObj.Value == null) return;

                if (palletObj.Key.isBONELABContent)
                {
                    MessageBox.Show("BONELAB Content cannot be removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (palletObj.Key.Pallet == null)
                {
                    MessageBox.Show("Pallet is not specified in the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = MessageBox.Show("WARNING: This is a destructive action and cannot be reverted unless you install the pallet back. This will remove the folder containing all the information for the mod, therefore removing the mod from the game. Do you want to continue?", "Destructive Action", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Directory.Delete(palletObj.Key.Pallet.DirectoryPath, true);
                        MessageBox.Show("Successfully removed pallet from the mods directory!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Main.Instance?.bw_autoComplete.Work();
                        Main.Instance!.bw_autoComplete.Finish += () => bw_list.Work();
                    }
                    catch (Exception ex)
                    {
                        Main.Instance?.AddLog($"Unable to remove directory, exception: \n{ex}");
                        MessageBox.Show("Unable to remove the directory. Go back to the main window (where you opened the barcode viewer) to see logs. Press retry to try again", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private static string GetFileName(string name)
        {
            List<string> disallowed = [" ", "<", ">", ":", "\"", "/", "\\", "|", "?", "*"];
            disallowed.ForEach(x => name = name.Replace(x, string.Empty));
            return name.ToLower();
        }

        private static string[] GetFilePaths(FileInfo[] fileInfos)
        {
            List<string> _return = [];
            foreach (var item in fileInfos)
            {
                _return.Add(item.FullName);
            }
            return [.. _return];
        }

        private static string[] GetDirectoryPaths(DirectoryInfo[] fileInfos)
        {
            List<string> _return = [];
            foreach (var item in fileInfos)
            {
                _return.Add(item.FullName);
            }
            return [.. _return];
        }

        private void ObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CmsItem != null)
            {
                var palletObj = Items.FirstOrDefault(x => x.Value == CmsItem);
                if (palletObj.Value == null) return;

                if (palletObj.Key.isBONELABContent)
                {
                    MessageBox.Show("BONELAB Content cannot be removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (palletObj.Key.Pallet == null)
                {
                    MessageBox.Show("Pallet is not specified in the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (palletObj.Key.Origin == null)
                {
                    MessageBox.Show("Origin is not specified in the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = MessageBox.Show("WARNING: This is a destructive action and cannot be reverted unless you reinstall the pallet. This will remove data and files related to the object and may cause issues with other mods or the mod the object is from. The issues could be for example: Items on the same Pallet as the removed object not spawning. Do you want to continue?", "Destructive Action", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var pallet = palletObj.Key.Pallet;
                        pallet.Objects.Remove(palletObj.Key.Key);
                        File.WriteAllText(palletObj.Key.Pallet.FilePath, JsonConvert.SerializeObject(palletObj.Key.Pallet, Formatting.Indented));

                        string name = (string)palletObj.Key.Title.Clone();
                        name = GetFileName(name);

                        Dictionary<BarcodeType, string[]> directoryNames = new()
                        {
                            { BarcodeType.SPAWNABLE , ["%replace%_spawnables_assets_spawnable", "%replace%_spawnables_assets_previewmesh"] },
                            { BarcodeType.LEVEL, ["%replace%_levels_scenes_level", "%replace%_levels_scenes_chunkscenes"] },
                            { BarcodeType.MONODISC, ["%replace%_monodiscs_assets_monodisc", "%replace%_monodiscs_assets_audioclip"] },
                            { BarcodeType.AVATAR , ["%replace%_avatars_assets_avatar", "%replace%_avatars_assets_previewmesh"] },
                            { BarcodeType.VFX, ["%replace%_vfxs_assets_vfx", "%replace%_vfxs_assets_previewmesh"] },
                            { BarcodeType.SURFACEDATACARD, ["%replace%_surfacedatacards_assets_physicsmaterialstatic", "%replace%_surfacedatacards_assets_physicsmaterial", "%replace%_surfacedatacards_assets_surfacedata", "%replace%_surfacedatacards_assets_surfacedatacard"] }
                        };

                        string palletTitle = (string)palletObj.Key.Origin.Title.Clone();
                        palletTitle = GetFileName(palletTitle);

                        if (directoryNames.TryGetValue(palletObj.Key.Type, out string[]? names))
                        {
                            foreach (var n in names)
                            {
                                string _n = n.Replace("%replace%", palletTitle);
                                string directory = Path.Combine(palletObj.Key.Pallet.DirectoryPath, _n);
                                DirectoryInfo info = new(directory);
                                if (info.Exists)
                                {
                                    List<string> remove = [];
                                    remove.AddRange(GetFilePaths(info.GetFiles($"{name}.bundle")));
                                    remove.AddRange(GetDirectoryPaths(info.GetDirectories($"{name}")));
                                    remove.ForEach(x =>
                                    {
                                        Main.Instance?.AddLog($"Deleting {Path.GetFileName(x)} ({x})");
                                        FileAttributes attr = File.GetAttributes(x);

                                        if (attr.HasFlag(FileAttributes.Directory))
                                            Directory.Delete(x, true);
                                        else
                                            File.Delete(x);

                                        Main.Instance?.AddLog($"Successfully deleted {Path.GetFileName(x)} ({x})");
                                    });
                                }
                            }
                        }

                        MessageBox.Show("Successfully removed object from the pallet", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Main.Instance?.bw_autoComplete.Work();
                        Main.Instance!.bw_autoComplete.Finish += () => bw_list.Work();
                    }
                    catch (Exception ex)
                    {
                        Main.Instance?.AddLog($"Unable to remove object, exception: \n{ex}");
                        MessageBox.Show("Unable to remove the object. Go back to the main window (where you opened the barcode viewer) to see logs. Press retry to try again", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private void PalletToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CmsItem != null)
            {
                var palletObj = Items.FirstOrDefault(x => x.Value == CmsItem);
                if (palletObj.Value == null) return;

                if (palletObj.Key.isBONELABContent)
                {
                    MessageBox.Show("BONELAB Content cannot be edited", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (palletObj.Key.Origin == null)
                {
                    MessageBox.Show("Origin is not specified in the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (palletObj.Key.Pallet == null)
                {
                    MessageBox.Show("Pallet is not specified in the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var form = new ValueEditor();
                form.Edit(palletObj.Key.Origin);
                form.Save += (generated) =>
                {
                    palletObj.Key.Origin = generated;
                    File.WriteAllText(palletObj.Key.Pallet.FilePath, JsonConvert.SerializeObject(palletObj.Key.Pallet, Formatting.Indented));
                    Main.Instance?.bw_autoComplete.Work();
                    Main.Instance!.bw_autoComplete.Finish += () => bw_list.Work();
                };
                form.ShowDialog();
            }
        }

        private void ObjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CmsItem != null)
            {
                var palletObj = Items.FirstOrDefault(x => x.Value == CmsItem);
                if (palletObj.Value == null) return;

                if (palletObj.Key.isBONELABContent)
                {
                    MessageBox.Show("BONELAB Content cannot be edited", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (palletObj.Key.Origin == null)
                {
                    MessageBox.Show("Origin is not specified in the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (palletObj.Key.Pallet == null)
                {
                    MessageBox.Show("Pallet is not specified in the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var form = new ValueEditor();
                form.Edit(palletObj.Key);
                form.Save += (generated) =>
                {
                    var pallet = palletObj.Key.Pallet;
                    pallet.Objects[palletObj.Key.Key] = generated;
                    File.WriteAllText(palletObj.Key.Pallet.FilePath, JsonConvert.SerializeObject(palletObj.Key.Pallet, Formatting.Indented));
                    Main.Instance?.bw_autoComplete.Work();
                    Main.Instance!.bw_autoComplete.Finish += () => bw_list.Work();
                };
                form.ShowDialog();
            }
        }

        private void Copy(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            if (CmsItem != null)
            {
                Dictionary<string, int> index = new() {
                        { "Barcode", -1 },
                        { "Title", 1 },
                        { "Type", 2 },
                        { "Version", 3 },
                        { "SDK Version", 4 },
                        { "Author", 5 },
                    { "Tags", 6 }
                    };
                var _index = index.FirstOrDefault(x => x.Key == item.Text);
                if (!string.IsNullOrWhiteSpace(_index.Key))
                {
                    string value = string.Empty;
                    if (_index.Value != -1)
                    {
                        value = CmsItem.SubItems[_index.Value].Text;
                    }
                    else
                    {
                        value = CmsItem.Text;
                    }
                    Clipboard.SetText(value);
                }
            }
        }
    }
}