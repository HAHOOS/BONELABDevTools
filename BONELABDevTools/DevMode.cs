using static BonelabDevMode.Form1;

namespace BonelabDevMode
{
    internal class DevMode
    {
        private static WebSocketSharp.WebSocket websocket;
        public static WebSocketConnectionState state;
        public static string path = "ws://127.0.0.1:50152/console";
        public string command = "";
        public static bool DevModeEnabled;

        public static void ConnectButton()
        {
            Form1.Instance.SetEnabledState(Form1.WebSocketConnectionState.CONNECTING);
            state = WebSocketConnectionState.CONNECTING;
            if (websocket != null)
            {
                Form1.Instance.AddLog("An existing connection was found, disconnecting");
                ((IDisposable)websocket).Dispose();
                websocket = null;
            }

            try
            {
                Form1.Instance.AddLog("Connecting...");
                websocket = new WebSocketSharp.WebSocket(path);
                websocket.OnMessage += (sender, e) => Form1.Instance.AddLog($"[WEBSOCKET] {e.Data.Clone()}");

                websocket.OnOpen += (sender, e) =>
                {
                    Form1.Instance.SetEnabledState(Form1.WebSocketConnectionState.CONNECTED);
                    state = WebSocketConnectionState.CONNECTED;
                    Form1.Instance.AddLog($"[WEBSOCKET] Successfully connected!");
                };
                websocket.OnError += (sender, e) => Form1.Instance.AddLog($"[ERROR] [WEBSOCKET] The WebSocket threw an exception: {e}");
                websocket.OnClose += (sender, e) =>
                {
                    Form1.Instance.SetEnabledState(Form1.WebSocketConnectionState.DISCONNECTED);
                    state = WebSocketConnectionState.DISCONNECTED;
                    Form1.Instance.AddLog($"[WEBSOCKET] WebSocket closed/disconnected");
                };

                websocket.Connect();
            }
            catch (Exception e)
            {
                Form1.Instance.AddLog($"[ERROR] [WEBSOCKET] {e}");
                Form1.Instance.SetEnabledState(WebSocketConnectionState.DISCONNECTED);
                state = WebSocketConnectionState.DISCONNECTED;
            }
        }

        public static void DisconnectButton()
        {
            try
            {
                Form1.Instance.AddLog("Disconnecting...");
                state = WebSocketConnectionState.DISCONNECTING;
                Form1.Instance.SetEnabledState(WebSocketConnectionState.DISCONNECTING);
                websocket.Close();
                ((IDisposable)websocket).Dispose();
                websocket = null;
            }
            catch (Exception e)
            {
                Form1.Instance.AddLog($"[ERROR] [WEBSOCKET] {e}");
                Form1.Instance.SetEnabledState(WebSocketConnectionState.DISCONNECTED);
                state = WebSocketConnectionState.DISCONNECTED;
            }
        }

        public static void ReloadLevel()
        {
            websocket.Send($"level.reload");
        }

        public static void ReloadPallet(string palletBarcode)
        {
            websocket.Send($"assetwarehouse.reload {palletBarcode}");
        }

        public static void SendCommand(string command)
        {
            websocket.Send(command);
        }
    }
}