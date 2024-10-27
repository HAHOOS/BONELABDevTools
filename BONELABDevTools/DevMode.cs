using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace BonelabDevMode
{
    internal class DevMode
    {
        private static WebSocketSharp.WebSocket websocket;
        public static string path = "ws://127.0.0.1:50152/console";
        public string command = "";
        public static bool DevModeEnabled;

        public static void ConnectButton(string ipppath)
        {
            if (websocket != null)
            {
                ((IDisposable)websocket).Dispose();
                websocket = null;
            }

            try
            {
                websocket = new WebSocketSharp.WebSocket(ipppath);
                websocket.OnMessage += (sender, e) => Console.WriteLine(e.Data);
                websocket.Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void DisconnectButton()
        {
            try
            {
                websocket.Close();
                ((IDisposable)websocket).Dispose();
                websocket = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
