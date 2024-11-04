using BonelabDevMode.JSON;

namespace BonelabDevMode.Forms
{
    public partial class ValueEditor : Form
    {
        public PalletObject palletObject;

        public event Action<PalletObject> Save;

        public ValueEditor()
        {
            InitializeComponent();
        }

        public void Edit(PalletObject _object)
        {
            palletObject = _object;
            gb_pallet.Visible = (_object.Type == Barcodes.BarcodeType.PALLET);

            tb_barcode.Text = _object.Barcode;
            tb_title.Text = _object.Title;
            tb_description.Text = _object.Description;

            cb_unlockable.Checked = _object.Unlockable != null && (bool)_object.Unlockable;
            cb_redacted.Checked = _object.Redacted != null && (bool)_object.Redacted;

            if (_object.Type == Barcodes.BarcodeType.PALLET)
            {
                tb_author.Text = _object.Author;
                tb_version.Text = _object.Version;
                cb_sdkVersion.SelectedItem = _object.SDKVersion;
                string tags = string.Empty;
                if (_object.Tags != null)
                {
                    foreach (var tag in _object.Tags)
                    {
                        if (string.IsNullOrEmpty(tags)) tags = tag;
                        else tags += $", {tag}";
                    }
                }
                tb_tags.Text = tags;
            }
        }

        public string[]? GetTags()
        {
            if (string.IsNullOrWhiteSpace(tb_tags.Text)) return null;
            var split = tb_tags.Text.Split(",");
            for (int i = 0; i < split.Length; i++)
            {
                split[i] = Changelogs.RemoveWhiteSpaceOnBeginning().Replace(split[i], string.Empty);
            }
            return split;
        }

        public PalletObject? Generate(PalletObject _ref)
        {
            // Text boxes

            if (!string.IsNullOrWhiteSpace(tb_title.Text)) _ref.Title = tb_title.Text;
            if (!string.IsNullOrWhiteSpace(tb_description.Text)) _ref.Description = tb_description.Text;
            if (!string.IsNullOrWhiteSpace(tb_author.Text) && (_ref.Type == Barcodes.BarcodeType.PALLET)) _ref.Author = tb_author.Text;
            if (!string.IsNullOrWhiteSpace(tb_version.Text) && (_ref.Type == Barcodes.BarcodeType.PALLET)) _ref.Version = tb_version.Text;

            // Tags

            string[]? tags = GetTags();
            if (tags?.Length > 0) _ref.Tags = [.. tags];

            // SDK Version

            var text = cb_sdkVersion.Text;
            if (!cb_sdkVersion.Items.Contains(text) && (_ref.Type == Barcodes.BarcodeType.PALLET))
            {
                MessageBox.Show("Incorrect SDK Version! You can only choose between 0.6.0, 1.1.0 & 1.2.0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else if (cb_sdkVersion.Items.Contains(text) && (_ref.Type == Barcodes.BarcodeType.PALLET))
            {
                _ref.SDKVersion = text;
            }

            // Checkboxes

            if (_ref.Type != Barcodes.BarcodeType.MONODISC && _ref.Type != Barcodes.BarcodeType.VFX && _ref.Type != Barcodes.BarcodeType.BONETAG && _ref.Type != Barcodes.BarcodeType.SURFACEDATACARD)
            {
                _ref.Unlockable = cb_unlockable.Checked;
                _ref.Redacted = cb_redacted.Checked;
            }

            // Return generated

            return _ref;
        }

        private void btn_saveAndExit_Click(object sender, EventArgs e)
        {
            var generated = Generate(palletObject);
            if (generated != null)
            {
                try
                {
                    Save(generated);
                    MessageBox.Show("Successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Main.Instance.AddLog($"Error occured while saving data: \n{ex}", Main.LogType.ERROR);
                    MessageBox.Show("An error occured while saving, check logs in the main window (where you opened barcode viewer) for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Unable to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var generated = Generate(palletObject);
            if (generated != null)
            {
                try
                {
                    Save(generated);
                    MessageBox.Show("Successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Main.Instance.AddLog($"Error occured while saving data: \n{ex}", Main.LogType.ERROR);
                    MessageBox.Show("An error occured while saving, check logs in the main window (where you opened barcode viewer) for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Unable to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}