//using BonelabDevMode.Controls;
using BonelabDevMode.JSON;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static BonelabDevMode.Barcodes;

namespace BonelabDevMode
{
    public partial class Main : Form
    {
        internal static Main? Instance;

        internal static readonly string[] DefaultCmdAutoComplete = ["assetwarehouse.reload", "help", "aw.load", "assetwarehouse.load", "aw.reload", "assetwarehouse.unload", "spawn", "level.reload", "avatar", "aw.unload", "repo.add", "repo.del", "repo.delete", "repo.list", "save", "scene.list", "level", "teleport", "whereami"];

        internal static readonly List<ListViewItem> AllLogs = [];

        internal CustomBackgroundWorker bw_autoComplete;

        public Main()
        {
            InitializeComponent();
            Instance = this;
            lv_logs.Columns.Add(new ColumnHeader { Width = lv_logs.Width * 5 });
            SetEnabledState(WebSocketConnectionState.DISCONNECTED);

            // Auto scroll for ListView

            int count = 0;

            System.Timers.Timer timer = new();
            timer.Elapsed += (x, y) =>
            {
                if (lv_logs.Items?.Count > 0 && lv_logs.Items.Count != count)
                {
                    count = lv_logs.Items.Count;
                    lv_logs.Invoke(() => lv_logs.Items[^1].EnsureVisible());
                }
                void action()
                {
                    cb_logDebug.Text = $"Debug ({AllLogs.Count(x => x.Text.Contains("[DEBUG]"))})";
                    cb_logErrors.Text = $"Errors ({AllLogs.Count(x => x.Text.Contains("[ERROR]"))})";
                    cb_logMessage.Text = $"Messages ({AllLogs.Count(x => x.Text.Contains("[MESSAGE]"))})";
                    cb_logWarnings.Text = $"Warnings ({AllLogs.Count(x => x.Text.Contains("[WARNING]"))})";
                }
                cb_logDebug.Invoke(action);
            };
            timer.Interval = 10;
            timer.AutoReset = true;
            timer.Start();

            // Background worker

            bw_autoComplete = new CustomBackgroundWorker
            {
                Start = () =>
            {
                DevMode.Update = false;
                void action2() { btn_openBarcodeViewer.Enabled = false; }
                if (btn_openBarcodeViewer.InvokeRequired) btn_openBarcodeViewer.Invoke(action2);
                else action2();
                bool autoCompleteBarcodes = cb_autoCompleteBarcodes.Checked;
                if (autoCompleteBarcodes)
                {
                    AddLog("Updating barcode suggestions");
                    var roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string? roaming_parent = Directory.GetParent(roaming)?.FullName;
                    if (roaming_parent == null)
                    {
                        AddLog("Roaming folder does not have a parent!", LogType.ERROR);
                        DevMode.Update = true;
                        void action3() { btn_openBarcodeViewer.Enabled = true; }
                        if (btn_openBarcodeViewer.InvokeRequired) btn_openBarcodeViewer.Invoke(action3);
                        else action3();
                        return;
                    }
                    var locallow = Path.Combine(roaming_parent, "LocalLow");
                    var mods = new DirectoryInfo(Path.Combine(locallow, "Stress Level Zero", "BONELAB", "Mods"));
                    if (mods.Exists)
                    {
                        AddLog("Found mod directory");
                        Barcodes.barcodes.Clear();
                        Barcodes.AddDefaults();
                        int success = 0;
                        int fail = 0;
                        AddLog("Retrieving all barcodes");
                        Dictionary<BarcodeType, int> results = [];
                        int expected = 0;
                        foreach (var mod in mods.GetDirectories())
                        {
                            try
                            {
                                if (mod.GetFiles().Any(x => x.Name == $"{mod.Name}.pallet.json" || x.Name == "pallet.json"))
                                {
                                    var palletFile = mod.GetFiles().First(x => x.Name == $"{mod.Name}.pallet.json" || x.Name == "pallet.json");
                                    var json = JsonConvert.DeserializeObject<Pallet>(File.ReadAllText(palletFile.FullName)) ?? throw new JsonException("JSON format was invalid!");

                                    expected++;
                                    json.DirectoryPath = mod.FullName;
                                    json.FilePath = palletFile.FullName;
                                    foreach (var item in json.Objects)
                                    {
                                        var return_ = AddBarcodeObject(json, item.Value, item.Key);
                                        if (return_.success) success++;
                                        else fail++;

                                        if (return_.success && return_.type != null)
                                        {
                                            BarcodeType bType = (BarcodeType)return_.type;
                                            if (results.TryGetValue(bType, out int value))
                                            {
                                                results[bType] = ++value;
                                            }
                                            else
                                            {
                                                results[bType] = 1;
                                            }
                                        }
                                    }
                                    foreach (var item in json.Objects)
                                    {
                                        var type = DetermineBarcodeType(json, item.Value);
                                        if (type == BarcodeType.PALLET && (item.Value.Crates != null || item.Value.DataCards != null))
                                        {
                                            var _all = new List<Crate>();
                                            if (item.Value.Crates != null) _all.AddRange(item.Value.Crates);
                                            if (item.Value.DataCards != null) _all.AddRange(item.Value.DataCards);
                                            foreach (var _Ref in _all)
                                            {
                                                if (json.Objects.TryGetValue(_Ref.Ref, out PalletObject? value) && value != null)
                                                {
                                                    var dictionary = Barcodes.barcodes.Where(x => x != null && x.Barcode == value.Barcode).ToList();
                                                    lock (dictionary)
                                                    {
                                                        lock (Barcodes.barcodes)
                                                        {
                                                            for (int i = 0; i < dictionary.Count; i++)
                                                            {
                                                                if (dictionary[i].Barcode == value.Barcode)
                                                                {
                                                                    var obj = Barcodes.barcodes.Find(x => x.Barcode == value.Barcode);
                                                                    if (obj != null)
                                                                        obj.Origin = item.Value;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                AddLog($"An exception was thrown while checking for barcodes in {mod.Name}: {ex}", LogType.ERROR);
                                fail++;
                            }
                        }
                        AddLog($"Successfully retrieved {success} barcode(s) with {fail} fail(s):");
                        foreach (var result in results)
                        {
                            AddLog($"{result.Key}: {result.Value} found");
                        }

                        AddLog("Adding barcodes to auto-complete");

                        #region Pallet

                        SetAutoComplete(ref tb_pallet, "Pallet", (obj) =>
                        {
                            if (obj.Type == BarcodeType.PALLET) return (true, [obj.Barcode]);
                            else return (false, [obj.Barcode]);
                        });

                        #endregion Pallet

                        #region Cmd

                        SetAutoComplete(ref tb_command, "Command", (obj) =>
                        {
                            if (obj.Type == BarcodeType.AVATAR)
                            {
                                return (true, [$"avatar {obj.Barcode}"]);
                            }
                            else if (obj.Type == BarcodeType.SPAWNABLE)
                            {
                                return (true, [$"spawn {obj.Barcode}"]);
                            }
                            else if (obj.Type == BarcodeType.LEVEL)
                            {
                                return (true, [$"level {obj.Barcode}"]);
                            }
                            else if (obj.Type == BarcodeType.PALLET)
                            {
                                return (true,
                                        cb_optimizeAutoComplete.Checked
                                        ?

                                            [
                                            $"aw.load {obj.Barcode}",
                                        $"aw.unload {obj.Barcode}",
                                        $"aw.reload {obj.Barcode}",
                                            ]
                                        :
                                            [
                                            $"aw.load {obj.Barcode}",
                                        $"aw.unload {obj.Barcode}",
                                        $"aw.reload {obj.Barcode}",
                                        $"assetwarehouse.load {obj.Barcode}",
                                        $"assetwarehouse.load {obj.Barcode}",
                                        $"assetwarehouse.unload {obj.Barcode}",
                                            ]);
                            }
                            else
                            {
                                return (false, [obj.Barcode]);
                            }
                        }, DefaultCmdAutoComplete);

                        #endregion Cmd

                        #region Level

                        SetAutoComplete(ref tb_level, "Level", (obj) =>
                        {
                            if (obj.Type == BarcodeType.LEVEL) return (true, [obj.Barcode]);
                            else return (false, [obj.Barcode]);
                        });

                        #endregion Level

                        #region Avatar

                        SetAutoComplete(ref tb_avatar, "Avatar", (obj) =>
                        {
                            if (obj.Type == BarcodeType.AVATAR) return (true, [obj.Barcode]);
                            else return (false, [obj.Barcode]);
                        });

                        #endregion Avatar

                        #region Spawn

                        SetAutoComplete(ref tb_spawn, "Spawn", (obj) =>
                        {
                            if (obj.Type == BarcodeType.SPAWNABLE) return (true, [obj.Barcode]);
                            else return (false, [obj.Barcode]);
                        });

                        #endregion Spawn
                    }
                    else
                    {
                        AddLog("Could not find the Mods folder for BONELAB", LogType.ERROR);
                    }
                }
                else
                {
                    AddLog("Updating auto-complete");
                    tb_command.AutoCompleteMode = AutoCompleteMode.Suggest;
                    tb_command.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    tb_command.AutoCompleteCustomSource = [.. DefaultCmdAutoComplete];
                    AddLog("Added default auto complete for commands");

                    tb_pallet.AutoCompleteCustomSource = null;
                    AddLog("Removed auto-complete from Pallet text box");
                    tb_level.AutoCompleteCustomSource = null;
                    AddLog("Removed auto-complete from Level text box");
                    tb_avatar.AutoCompleteCustomSource = null;
                    AddLog("Removed auto-complete from Avatar text box");
                    tb_spawn.AutoCompleteCustomSource = null;
                    AddLog("Removed auto-complete from Spawn text box");
                }
                void action() { btn_openBarcodeViewer.Enabled = true; }
                if (btn_openBarcodeViewer.InvokeRequired) btn_openBarcodeViewer.Invoke(action);
                else action();
                DevMode.Update = true;
            }
            };
            //bw_autoComplete.Work();
        }

        internal void SetAutoComplete(ref TextBox textBox, string name, Func<PalletObject, (bool meetsCondition, string[] acString)> condition, string[]? defaultCollection = null)
        {
            List<string> acsc = [];
            if (defaultCollection != null) acsc = [.. defaultCollection];
            foreach (var item in Barcodes.barcodes)
            {
                var (meetsCondition, acString) = condition.Invoke(item);
                if (meetsCondition)
                {
                    acsc.AddRange(acString);
                }
            }
            void action(ref TextBox tb)
            {
                tb.AutoCompleteCustomSource = [.. acsc];
                tb.AutoCompleteMode = AutoCompleteMode.Suggest;
                tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }

            if (textBox.InvokeRequired) textBox.Invoke(action, textBox);
            else action(ref textBox);

            AddLog($"Added {acsc.Count} to {name} text box auto-complete");
        }

        internal static BarcodeType? DetermineBarcodeType(Pallet pallet, PalletObject obj)
        {
            Dictionary<string, BarcodeType> types = [];

            // Add types

            types = new Dictionary<string, BarcodeType>()
                {
                    {"pallet#0", BarcodeType.PALLET },
                    {"crate-level#0", BarcodeType.LEVEL },
                    {"crate-avatar#0", BarcodeType.AVATAR },
                    {"crate-spawnable#0", BarcodeType.SPAWNABLE },
                    {"crate-vfx#0", BarcodeType.VFX },
                    {"datacard-monodisc#0", BarcodeType.MONODISC },
                    {"datacard-bonetag#0", BarcodeType.BONETAG },
                };

            if (pallet.Types?.Count > 0)
            {
                var refs = new Dictionary<string, BarcodeType>()
                    {
                        {"SLZ.Marrow.Warehouse.Pallet", BarcodeType.PALLET },
                      {"SLZ.Marrow.Warehouse.AvatarCrate", BarcodeType.AVATAR },
                      {"SLZ.Marrow.Warehouse.SpawnableCrate", BarcodeType.SPAWNABLE },
                      {"SLZ.Marrow.Warehouse.LevelCrate", BarcodeType.LEVEL },
                      {"SLZ.Marrow.Warehouse.VFXCrate", BarcodeType.VFX },
                      {"SLZ.Marrow.Warehouse.MonoDisc", BarcodeType.MONODISC },
                      {"SLZ.Marrow.Warehouse.BoneTag", BarcodeType.BONETAG },
                        {"SLZ.Marrow.Warehouse.SurfaceDataCard", BarcodeType.SURFACEDATACARD }
                    };

                foreach (var type in pallet.Types)
                {
                    KeyValuePair<string, BarcodeType> first = refs.FirstOrDefault(x => type.Value.FullName.StartsWith(x.Key));
                    if (first.Key != null)
                    {
                        types.Add(type.Value.Type, first.Value);
                    }
                }
            }

            var _type = types.FirstOrDefault(x => obj.ISA != null && x.Key == obj.ISA.Type);
            if (_type.Key != null)
            {
                return _type.Value;
            }
            else
            {
                return null;
            }
        }

        internal (bool success, BarcodeType? type) AddBarcodeObject(Pallet pallet, PalletObject obj, string key)
        {
            var _type = DetermineBarcodeType(pallet, obj);
            if (_type != null)
            {
                obj.Type = _type.Value;
                obj.Key = key;
                Barcodes.AddObject(obj, pallet, (BarcodeType)_type);
                AddLog($"{obj.Barcode} ({obj.Title}) is a{(_type == BarcodeType.AVATAR ? "n" : string.Empty)} {_type}", LogType.DEBUG);
                return (true, _type);
            }
            else
            {
                AddLog($"Object has an unsupported type, but this will be ignored: {obj.ISA?.Type ?? "N/A"} ({pallet.DirectoryPath})", LogType.WARNING);
                return (true, null);
            }
        }

        internal void UpdateAutoComplete(bool _ = true)
        {
            bw_autoComplete.Work();
        }

        internal void CheckBox_autoCompleteBarcodes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAutoComplete(cb_autoCompleteBarcodes.Checked);
        }

        internal bool CanShow(string message)
        {
            bool errors = cb_logErrors.Checked;
            bool warnings = cb_logWarnings.Checked;
            bool msgs = cb_logMessage.Checked;
            bool debug = cb_logDebug.Checked;

            if (message.Contains("[MESSAGE]") && !msgs) return false;
            if (message.Contains("[WARNING]") && !warnings) return false;
            if (message.Contains("[ERROR]") && !errors) return false;
            if (message.Contains("[DEBUG]") && !debug) return false;
            return true;
        }

        internal void Filter(object sender, EventArgs e)
        {
            List<ListViewItem> items = [];
            items.AddRange(from ListViewItem item in AllLogs
                           where CanShow(item.Text)
                           select (ListViewItem)item.Clone());
            lv_logs.Items.Clear();
            lv_logs.Items.AddRange([.. items]);
        }

        internal void AddLog(string message, LogType logType = LogType.MESSAGE, Color? backColor = null, Color? foregroundColor = null)
        {
            var item = new ListViewItem($"[{DateTime.Now:T}] [{logType}] {message}");

            if (backColor != null)
            {
                item.BackColor = (Color)backColor;
            }
            else
            {
                item.BackColor = SystemColors.ActiveCaptionText;
            }

            if (foregroundColor != null)
            {
                item.ForeColor = (Color)foregroundColor;
            }
            else if (logType != LogType.MESSAGE && logType != LogType.DEBUG)
            {
                item.ForeColor = (logType == LogType.WARNING ? Color.Yellow : Color.Red);
            }
            else
            {
                item.ForeColor = SystemColors.ControlLightLight;
            }

            ListViewItem lvItem() => lv_logs.Items.Add(item);
            if (CanShow(item.Text))
            {
                if (lv_logs.InvokeRequired) item = lv_logs.Invoke<ListViewItem>(lvItem);
                else item = lvItem();
            }

            ListViewItem lvItemClone() => (ListViewItem)item.Clone();

            if (lv_logs.InvokeRequired) lv_logs.Invoke(AllLogs.Add, lv_logs.Invoke(lvItemClone));
            else AllLogs.Add(lvItemClone());
        }

        internal static void LabelText(Label label, string text)
        {
            if (label.InvokeRequired) label.Invoke(() => label.Text = text);
            else label.Text = text;
        }

        internal static void LabelForeColor(Label label, Color color)
        {
            if (label.InvokeRequired) label.Invoke(() => label.ForeColor = color);
            else label.ForeColor = color;
        }

        internal void SetStatus(string message, Color color)
        {
            LabelText(connectionStatus, message);
            LabelForeColor(connectionStatus, color);
        }

        internal void UpdateCoordinates(string coordinates, ref WebSocketEvents? webSocketEvents, ref EventHandler<CustomMessageEventArgs>? _event)
        {
            LabelText(label_coordinates, "Player Coordinates: " + coordinates);
            if (_event != null && webSocketEvents != null) webSocketEvents.OnMessage -= _event;
        }

        internal void UpdateCurrentLevel(string levelName, ref WebSocketEvents? webSocketEvents, ref EventHandler<CustomMessageEventArgs>? _event)
        {
            LabelText(label_currentLevel, "Current Scene/Level: " + levelName);
            if (_event != null && webSocketEvents != null) webSocketEvents.OnMessage -= _event;
        }

        internal void UpdateCoordinates(string coordinates)
        {
            LabelText(label_coordinates, "Player Coordinates: " + coordinates);
        }

        internal void UpdateCurrentLevel(string levelName)
        {
            LabelText(label_currentLevel, "Current Scene/Level: " + levelName);
        }

        internal static void SetButtonEnabled(Button btn, bool enabled)
        {
            if (btn.InvokeRequired) btn.Invoke(() => btn.Enabled = enabled);
            else btn.Enabled = enabled;
        }

        internal void SetCmdButtonState(bool enabled)
        {
            SetButtonEnabled(btn_reloadpallet, enabled);
            SetButtonEnabled(btn_unloadpallet, enabled);
            SetButtonEnabled(btn_loadpallet, enabled);

            SetButtonEnabled(btn_levelreload, enabled);
            SetButtonEnabled(btn_loadLevel, enabled);

            SetButtonEnabled(btn_command, enabled);

            SetButtonEnabled(btn_setAvatar, enabled);
            SetButtonEnabled(btn_spawn, enabled);
        }

        internal void SetEnabledState(WebSocketConnectionState state)
        {
            switch (state)
            {
                case WebSocketConnectionState.CONNECTING:
                    SetButtonEnabled(btn_connect, false);
                    SetButtonEnabled(btn_disconnect, false);
                    SetCmdButtonState(false);

                    SetStatus("Connecting...", Color.Yellow);
                    UpdateCoordinates("N/A");
                    UpdateCurrentLevel("N/A");

                    break;

                case WebSocketConnectionState.DISCONNECTING:
                    SetButtonEnabled(btn_connect, false);
                    SetButtonEnabled(btn_disconnect, false);
                    SetCmdButtonState(false);

                    SetStatus("Disconnecting...", Color.Yellow);
                    UpdateCoordinates("N/A");
                    UpdateCurrentLevel("N/A");

                    break;

                case WebSocketConnectionState.CONNECTED:
                    SetButtonEnabled(btn_connect, false);
                    SetButtonEnabled(btn_disconnect, true);
                    SetCmdButtonState(true);

                    SetStatus("Connected!", Color.Green);
                    UpdateCoordinates("N/A");
                    UpdateCurrentLevel("N/A");

                    break;

                case WebSocketConnectionState.DISCONNECTED:
                    SetButtonEnabled(btn_connect, true);
                    SetButtonEnabled(btn_disconnect, false);
                    SetCmdButtonState(false);

                    SetStatus("Disconnected!", Color.Red);
                    UpdateCoordinates("N/A");
                    UpdateCurrentLevel("N/A");

                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetEnabledState(WebSocketConnectionState.DISCONNECTED);
            UpdateAutoComplete(cb_autoCompleteBarcodes.Checked);
        }

        private void Btn_updateAC_Click(object sender, EventArgs e)
        {
            UpdateAutoComplete(cb_autoCompleteBarcodes.Checked);
        }

        private void Cb_optimizeAutoComplete_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAutoComplete(cb_autoCompleteBarcodes.Checked);
        }

        public enum WebSocketConnectionState
        {
            CONNECTING,
            DISCONNECTING,
            CONNECTED,
            DISCONNECTED
        }

        public enum LogType
        {
            MESSAGE,
            DEBUG,
            WARNING,
            ERROR
        }

        [GeneratedRegex("(<(.*?)>)")]
        internal static partial Regex AC_HTMLRemove();

        private void Btn_openBarcodeViewer_Click(object sender, EventArgs e)
        {
            var window = new BarcodeViewer();
            window.Show();
        }
    }
}