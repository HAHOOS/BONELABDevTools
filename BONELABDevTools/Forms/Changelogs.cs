using BonelabDevMode.JSON;
using System.Text.RegularExpressions;

namespace BonelabDevMode
{
    public partial class Changelogs : Form
    {
        public static PalletObject? _object { get; private set; }
        public static List<ChangeLog>? _list { get; private set; }

        public Changelogs()
        {
            InitializeComponent();
        }

        public static TreeNode CreateNode(string text)
        {
            return new TreeNode(text)
            {
                BackColor = SystemColors.WindowText,
                ForeColor = SystemColors.ControlLightLight
            };
        }

        public void Setup(PalletObject palletObj, List<ChangeLog>? changeLogs)
        {
            List<TreeNode> names_expanded = tv_changelogs.Nodes.Cast<TreeNode>().Where(x => x.IsExpanded).ToList();

            tv_changelogs.Nodes.Clear();

            ArgumentNullException.ThrowIfNull(palletObj, nameof(palletObj));

            _object = palletObj;
            _list = changeLogs;

            if (changeLogs == null || changeLogs.Count == 0)
            {
                tv_changelogs.Nodes.Add(CreateNode("There are no changelogs in this pallet"));
            }
            else
            {
                foreach (ChangeLog changeLog in changeLogs)
                {
                    var node = CreateNode(Main.AC_HTMLRemove().Replace($"{changeLog.Title} (v{changeLog.Version})", string.Empty));
                    string[] lines_1st = changeLog.Text.Split(Environment.NewLine);
                    List<string> lines_2nd = [];
                    if (cb_split.Checked)
                    {
                        foreach (string line in lines_1st)
                        {
                            lines_2nd.AddRange(line.Split("- "));
                        }
                    }
                    else
                    {
                        lines_2nd = [.. lines_1st];
                    }
                    foreach (string line in lines_2nd)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        string fixedLine = line;
                        fixedLine = Main.AC_HTMLRemove().Replace(line, string.Empty);
                        fixedLine = RemoveWhiteSpaceOnBeginning().Replace(line, string.Empty);

                        node.Nodes.Add(CreateNode(fixedLine));
                    }
                    if (names_expanded.Any(x => x.Text == node.Text)) node.Expand();
                    tv_changelogs.Nodes.Add(node);
                }
            }
            label_changelogs.Text = $"Changelogs for {palletObj.Barcode} ({Main.AC_HTMLRemove().Replace(palletObj.Title, string.Empty)})";
        }

        private void cb_split_CheckedChanged(object sender, EventArgs e)
        {
            Setup(_object, _list);
        }

        [GeneratedRegex(@"^\s+|\s+$|\s+(?=\s)")]
        internal static partial Regex RemoveWhiteSpaceOnBeginning();
    }
}