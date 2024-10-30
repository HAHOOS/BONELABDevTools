using BonelabDevMode.JSON;
using Newtonsoft.Json;

namespace BonelabDevMode
{
    public partial class Form1 : Form
    {
        internal static Form1? Instance;

        internal static Dictionary<string, BarcodeType> BarcodesFound = [];

        internal static string[] DefaultCmdAutoComplete = ["assetwarehouse.reload", "help", "aw.load", "assetwarehouse.load", "aw.reload", "assetwarehouse.unload", "spawn", "level.reload", "avatar", "aw.unload", "repo.add", "repo.del", "repo.delete", "repo.list", "save", "scene.list", "level", "teleport", "whereami"];

        internal static List<ListViewItem> AllLogs = [];

        public Form1()
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
                if (lv_logs.Items != null && lv_logs.Items.Count > 0 && lv_logs.Items.Count != count)
                {
                    count = lv_logs.Items.Count;
                    lv_logs.Invoke(() => lv_logs.Items[^1].EnsureVisible());
                }
            };
            timer.Interval = 10;
            timer.AutoReset = true;
            timer.Start();
        }

        internal void SetAutoComplete(ref TextBox textBox, string name, Func<string, BarcodeType, (bool meetsCondition, string[] acString)> condition, AutoCompleteStringCollection? defaultCollection = null)
        {
            AutoCompleteStringCollection acsc = [];
            if (defaultCollection != null) acsc = defaultCollection;
            foreach (var item in BarcodesFound)
            {
                var (meetsCondition, acString) = condition.Invoke(item.Key, item.Value);
                if (meetsCondition)
                {
                    acsc.AddRange(acString);
                }
            }
            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox.AutoCompleteCustomSource = acsc;
            AddLog($"Added {acsc.Count} to {name} text box auto-complete");
        }

        internal (bool success, BarcodeType? type) DetermineBarcodeType(Pallet pallet, PalletObject obj)
        {
            Dictionary<string, BarcodeType> types = [];

            // Add types

            if (pallet.version == 2)
            {
                types = new Dictionary<string, BarcodeType>()
                {
                    {"pallet#0", BarcodeType.PALLET },
                    {"crate-level#0", BarcodeType.LEVEL },
                    {"crate-avatar#0", BarcodeType.AVATAR },
                    {"crate-spawnable#0", BarcodeType.SPAWNABLE },
                };
            }
            else if (pallet.version == 1)
            {
                if (pallet.types != null && pallet.types.Count > 0)
                {
                    foreach (var type in pallet.types)
                    {
                        if (type.Value.fullname.StartsWith("SLZ.Marrow.Warehouse.Pallet"))
                        {
                            types.Add(type.Value.type, BarcodeType.PALLET);
                        }
                        else if (type.Value.fullname.StartsWith("SLZ.Marrow.Warehouse.AvatarCrate"))
                        {
                            types.Add(type.Value.type, BarcodeType.AVATAR);
                        }
                        else if (type.Value.fullname.StartsWith("SLZ.Marrow.Warehouse.SpawnableCrate"))
                        {
                            types.Add(type.Value.type, BarcodeType.SPAWNABLE);
                        }
                        else if (type.Value.fullname.StartsWith("SLZ.Marrow.Warehouse.LevelCrate"))
                        {
                            types.Add(type.Value.type, BarcodeType.LEVEL);
                        }
                    }
                }
                else
                {
                    throw new Exception("There are no declared types!");
                }
            }
            else
            {
                throw new Exception("Unsupported pallet.json version!");
            }

            // Check if object matches any of the types

            var _type = types.Where(x => x.Key == obj.isa.type).FirstOrDefault();
            if (_type.Key != null)
            {
                BarcodesFound.Add(obj.barcode, _type.Value);
                AddLog($"{obj.barcode} ({obj.title}) is a{(_type.Value == BarcodeType.AVATAR ? "n" : string.Empty)} {_type.Value}", LogType.DEBUG);
                return (true, _type.Value);
            }
            else
            {
                AddLog("Object has an unsupported type, but this will be ignored", LogType.WARNING);
                return (true, null);
            }
        }

        internal void UpdateAutoComplete(bool autoCompleteBarcodes = true)
        {
            DevMode.Update = false;
            if (autoCompleteBarcodes)
            {
                AddLog("Updating barcode suggestions");
                var roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string? roaming_parent = Directory.GetParent(roaming)?.FullName;
                if (roaming_parent == null)
                {
                    AddLog("Roaming folder does not have a parent!", LogType.ERROR);
                    DevMode.Update = true;
                    return;
                }
                var locallow = Path.Combine(roaming_parent, "LocalLow");
                var mods = new DirectoryInfo(Path.Combine(locallow, "Stress Level Zero", "BONELAB", "Mods"));
                if (mods.Exists)
                {
                    AddLog("Found mod directory");
                    BarcodesFound.Clear();
                    int success = 0;
                    int fail = 0;
                    AddLog("Retrieving all barcodes");
                    Dictionary<BarcodeType, int> results = [];
                    foreach (var mod in mods.GetDirectories())
                    {
                        try
                        {
                            if (mod.GetFiles().Where(x => x.Name == $"{mod.Name}.pallet.json" || x.Name == "pallet.json").Any())
                            {
                                var palletFile = mod.GetFiles().Where(x => x.Name == $"{mod.Name}.pallet.json" || x.Name == "pallet.json").First();
                                var json = JsonConvert.DeserializeObject<Pallet>(File.ReadAllText(palletFile.FullName)) ?? throw new JsonException("JSON format was invalid!");
                                foreach (var item in json.objects)
                                {
                                    var return_ = DetermineBarcodeType(json, item.Value);
                                    if (return_.success) success++;
                                    else fail++;

                                    if (return_.success && return_.type != null)
                                    {
                                        BarcodeType bType = (BarcodeType)return_.type;
                                        if (results.ContainsKey(bType))
                                        {
                                            results[bType] += 1;
                                        }
                                        else
                                        {
                                            results[bType] = 1;
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

                    SetAutoComplete(ref tb_pallet, "Pallet", (barcode, type) =>
                    {
                        if (type == BarcodeType.PALLET) return (true, new string[] { barcode });
                        else return (false, new string[] { barcode });
                    });

                    #endregion Pallet

                    #region Cmd

                    AutoCompleteStringCollection acsc_cmd = [.. DefaultCmdAutoComplete];

                    SetAutoComplete(ref tb_command, "Command", (barcode, type) =>
                    {
                        if (type == BarcodeType.AVATAR) return (true, new string[] { $"avatar {barcode}" });
                        else if (type == BarcodeType.SPAWNABLE) return (true, new string[] { $"spawn {barcode}" });
                        else if (type == BarcodeType.LEVEL) return (true, new string[] { $"level {barcode}" });
                        else if (type == BarcodeType.PALLET) return (true, new string[] { $"aw.load {barcode}", $"aw.unload {barcode}", $"aw.reload {barcode}", $"assetwarehouse.load {barcode}", $"assetwarehouse.unload {barcode}", $"assetwarehouse.reload {barcode}" });
                        else return (false, new string[] { barcode });
                    }, acsc_cmd);

                    #endregion Cmd

                    #region Level

                    SetAutoComplete(ref tb_level, "Level", (barcode, type) =>
                    {
                        if (type == BarcodeType.LEVEL) return (true, new string[] { barcode });
                        else return (false, new string[] { barcode });
                    });

                    #endregion Level

                    #region Avatar

                    SetAutoComplete(ref tb_avatar, "Avatar", (barcode, type) =>
                    {
                        if (type == BarcodeType.AVATAR) return (true, new string[] { barcode });
                        else return (false, new string[] { barcode });
                    });

                    #endregion Avatar

                    #region Spawn

                    SetAutoComplete(ref tb_spawn, "Spawn", (barcode, type) =>
                    {
                        if (type == BarcodeType.SPAWNABLE) return (true, new string[] { barcode });
                        else return (false, new string[] { barcode });
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
            DevMode.Update = true;
        }

        internal void CheckBox_autoCompleteBarcodes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAutoComplete(checkBox_autoCompleteBarcodes.Checked);
        }

        internal bool CanShow(string message)
        {
            bool errors = cb_logErrors.Checked;
            bool warnings = cb_logDebug.Checked;
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

            foreach (ListViewItem item in AllLogs)
            {
                if (CanShow(item.Text))
                {
                    items.Add((ListViewItem)item.Clone());
                }
            }

            lv_logs.Items.Clear();
            lv_logs.Items.AddRange([.. items]);
        }

        internal void AddLog(string message, LogType logType = LogType.MESSAGE, Color? backColor = null, Color? foregroundColor = null)
        {
            var action = (Action<string, LogType, Color?, Color?>)_AddLog;
            if (lv_logs.InvokeRequired) lv_logs.Invoke(action, message, logType, backColor, foregroundColor);
            else _AddLog(message, logType, backColor, foregroundColor);
        }

        internal void _AddLog(string message, LogType logType = LogType.MESSAGE, Color? backColor = null, Color? foregroundColor = null)
        {
            var item = new ListViewItem($"[{DateTime.Now:T}] [{logType}] {message}");

            if (backColor != null)
            {
                item.BackColor = (Color)backColor;
            }

            if (foregroundColor != null)
            {
                item.ForeColor = (Color)foregroundColor;
            }
            else if (logType != LogType.MESSAGE && logType != LogType.DEBUG)
            {
                item.ForeColor = (logType == LogType.WARNING ? Color.Yellow : Color.Red);
            }

            if (CanShow(item.Text)) item = lv_logs.Items.Add(item);

            AllLogs.Add((ListViewItem)item.Clone());
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
            UpdateAutoComplete(checkBox_autoCompleteBarcodes.Checked);
        }

        private void Btn_updateAC_Click(object sender, EventArgs e)
        {
            UpdateAutoComplete(checkBox_autoCompleteBarcodes.Checked);
        }

        public enum WebSocketConnectionState
        {
            CONNECTING,
            DISCONNECTING,
            CONNECTED,
            DISCONNECTED
        }

        public enum BarcodeType
        {
            LEVEL,
            SPAWNABLE,
            AVATAR,
            PALLET
        }

        public enum LogType
        {
            MESSAGE,
            DEBUG,
            WARNING,
            ERROR
        }
    }
}