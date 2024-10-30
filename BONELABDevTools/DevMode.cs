using BonelabDevMode.JSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using static BonelabDevMode.Form1;

namespace BonelabDevMode
{
    internal static class DevMode
    {
        private static WebSocketSharp.WebSocket? websocket;
        public static WebSocketConnectionState state;
        public static string path = "ws://127.0.0.1:50152/console";

        internal static WebSocketEvents? events;

        private static string lastCommand = string.Empty;
        private static long? lastCommand_ExecutionDate;

        public static List<int> DontLog = [];

        public static bool Update = true;

        public static void UpdateCoordinates()
        {
            Update = true;
            var timer = new System.Timers.Timer();
            timer.Elapsed += (s, e) =>
            {
                if (websocket == null || websocket.ReadyState == WebSocketSharp.WebSocketState.Closing || websocket.ReadyState == WebSocketSharp.WebSocketState.Closed || websocket.ReadyState == WebSocketSharp.WebSocketState.Connecting || events == null || state != WebSocketConnectionState.CONNECTED)
                {
                    timer.Stop();
                    return;
                }

                if (!Update) return;

                // HACK: Couldn't in any other way use the _event variable inside the eventhandler action
                EventHandler<CustomMessageEventArgs>? _event = null;
                _event = (sender, msg) =>
                {
                    if (msg.EventArgs != null && msg.EventArgs.Data?.StartsWith("whereami: teleport") == true)
                    {
                        var coordinates = msg.EventArgs.Data.Replace("whereami: teleport ", string.Empty);
                        DontLog.Add(msg.MessageID);
                        Form1.Instance?.UpdateCoordinates(coordinates, ref events, ref _event);
                    }
                };
                events.OnMessage += _event;
                websocket.Send("whereami");
            };
            timer.Interval = 175;
            timer.AutoReset = true;
            timer.Start();
        }

        public static void UpdateCurrentLevel()
        {
            Update = true;
            var timer = new System.Timers.Timer();
            timer.Elapsed += (s, e) =>
            {
                if (websocket == null || websocket.ReadyState == WebSocketSharp.WebSocketState.Closing || websocket.ReadyState == WebSocketSharp.WebSocketState.Closed || websocket.ReadyState == WebSocketSharp.WebSocketState.Connecting || events == null || state != WebSocketConnectionState.CONNECTED)
                {
                    timer.Stop();
                    return;
                }

                if (!Update) return;

                // HACK: Couldn't in any other way use the _event variable inside the eventhandler action
                EventHandler<CustomMessageEventArgs>? _event = null;
                bool awaitNext = false;
                _event = (sender, msg) =>
                {
                    if (msg.EventArgs.Data == "Active Scene")
                    {
                        awaitNext = true;
                    }
                    else
                    {
                        if (IsSceneSchemaValid(msg.EventArgs.Data) && awaitNext)
                        {
                            Scene? scene = JsonConvert.DeserializeObject<Scene>(msg.EventArgs.Data);
                            if (scene != null)
                            {
                                Form1.Instance?.UpdateCurrentLevel(scene.name, ref events, ref _event);
                            }
                        }
                    }
                };

                events.OnMessage += _event;
                websocket.Send("scene.list");
            };
            timer.Interval = 2500;
            timer.AutoReset = true;
            timer.Start();
        }

        public static void ConnectButton(string ipppath)
        {
            Form1.Instance?.SetEnabledState(Form1.WebSocketConnectionState.CONNECTING);
            state = WebSocketConnectionState.CONNECTING;
            if (websocket != null)
            {
                Form1.Instance?.AddLog("An existing connection was found, disconnecting");
                ((IDisposable)websocket).Dispose();
                websocket = null;
            }

            try
            {
                Form1.Instance?.AddLog("Connecting...");
                websocket = new WebSocketSharp.WebSocket(ipppath);
                events = new WebSocketEvents(websocket);
                events.OnLateMessage += (sender, e) => { if (!DontLog.Contains(e.MessageID)) Form1.Instance?.AddLog($"[WEBSOCKET] {e.EventArgs.Data}"); };
                events.OnMessage += UpdateCurrentLevel_HideMessages;

                websocket.OnOpen += (sender, e) =>
                {
                    Form1.Instance?.SetEnabledState(Form1.WebSocketConnectionState.CONNECTED);
                    state = WebSocketConnectionState.CONNECTED;
                    Form1.Instance?.AddLog("[WEBSOCKET] Successfully connected!");
                    UpdateCurrentLevel();
                    UpdateCoordinates();
                };
                websocket.OnError += (sender, e) => Form1.Instance?.AddLog($"[ERROR] [WEBSOCKET] The WebSocket threw an exception: {e}", LogType.ERROR);
                websocket.OnClose += (sender, e) =>
                {
                    Form1.Instance?.SetEnabledState(Form1.WebSocketConnectionState.DISCONNECTED);
                    state = WebSocketConnectionState.DISCONNECTED;
                    Form1.Instance?.AddLog("[WEBSOCKET] WebSocket closed/disconnected");
                };

                websocket.Connect();
            }
            catch (Exception e)
            {
                Form1.Instance?.AddLog($"[ERROR] [WEBSOCKET] {e}");
                Form1.Instance?.SetEnabledState(Form1.WebSocketConnectionState.DISCONNECTED);
                state = WebSocketConnectionState.DISCONNECTED;
            }
        }

        internal static void AddProperty(ref JSchema schema, string name, JSchemaType type)
        {
            JSchema jSchema = new()
            {
                Type = type
            };
            schema.Properties.Add(name, jSchema);
            schema.Required.Add(name);
        }

        internal static bool IsSceneSchemaValid(string message)
        {
            JSchema schema = new()
            {
                Type = JSchemaType.Object
            };

            AddProperty(ref schema, "buildIndex", JSchemaType.Integer);
            AddProperty(ref schema, "isDirty", JSchemaType.Boolean);
            AddProperty(ref schema, "isLoaded", JSchemaType.Boolean);
            AddProperty(ref schema, "name", JSchemaType.String);
            AddProperty(ref schema, "path", JSchemaType.String);
            AddProperty(ref schema, "rootCount", JSchemaType.Integer);
            try
            {
                JObject json = JObject.Parse(message);
                return json.IsValid(schema);
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }

        private static void UpdateCurrentLevel_HideMessages(object? sender, CustomMessageEventArgs e)
        {
            if ((e.EventArgs.Data.StartsWith("Scene") || e.EventArgs.Data.StartsWith("Active Scene")) && (!lastCommand.StartsWith("scene.list") || (lastCommand_ExecutionDate == null || DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastCommand_ExecutionDate >= 100))) DontLog.Add(e.MessageID);
            else if (IsSceneSchemaValid(e.EventArgs.Data) && (!lastCommand.StartsWith("scene.list") || (lastCommand_ExecutionDate == null || DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastCommand_ExecutionDate >= 100))) DontLog.Add(e.MessageID);
        }

        public static void DisconnectButton()
        {
            try
            {
                Form1.Instance?.AddLog("Disconnecting...");
                state = WebSocketConnectionState.DISCONNECTING;
                Form1.Instance?.SetEnabledState(WebSocketConnectionState.DISCONNECTING);
                websocket?.Close();
                (websocket as IDisposable)?.Dispose();
                websocket = null;
            }
            catch (Exception e)
            {
                Form1.Instance?.AddLog($"[ERROR] [WEBSOCKET] {e}");
                Form1.Instance?.SetEnabledState(WebSocketConnectionState.DISCONNECTED);
                state = WebSocketConnectionState.DISCONNECTED;
            }
        }

        public static void SendCommand(string command)
        {
            lastCommand = command;
            lastCommand_ExecutionDate = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            websocket?.Send(command);
        }
    }
}