namespace BonelabDevMode
{
    public partial class Form1 : Form
    {
        internal static Form1 Instance;

        public Form1()
        {
            InitializeComponent();
            Instance = this;
            SetEnabledState(WebSocketConnectionState.DISCONNECTED);
        }

        internal void AddLog(string message)
        {
            logsListBox.Invoke(new Action(() => { logsListBox.Items.Add($"[{DateTime.Now:T}] {message}"); }));
        }

        internal void SetStatus(string message, Color color)
        {
            connectionStatus.Text = message;
            connectionStatus.ForeColor = color;
        }

        internal void SetEnabledState(WebSocketConnectionState state)
        {
            switch (state)
            {
                case WebSocketConnectionState.CONNECTING:
                    btn_connect.Enabled = false;
                    btn_disconnect.Enabled = false;
                    btn_reloadpallet.Enabled = false;
                    btn_levelreload.Enabled = false;
                    btn_command.Enabled = false;

                    SetStatus("Connecting...", Color.Yellow);

                    break;

                case WebSocketConnectionState.DISCONNECTING:
                    btn_connect.Enabled = false;
                    btn_disconnect.Enabled = false;
                    btn_reloadpallet.Enabled = false;
                    btn_levelreload.Enabled = false;
                    btn_command.Enabled = false;

                    SetStatus("Disconnecting...", Color.Yellow);

                    break;

                case WebSocketConnectionState.CONNECTED:
                    btn_connect.Enabled = false;
                    btn_disconnect.Enabled = true;
                    btn_reloadpallet.Enabled = true;
                    btn_levelreload.Enabled = true;
                    btn_command.Enabled = true;

                    SetStatus("Connected!", Color.Green);

                    break;

                case WebSocketConnectionState.DISCONNECTED:
                    btn_connect.Enabled = true;
                    btn_disconnect.Enabled = false;
                    btn_reloadpallet.Enabled = false;
                    btn_levelreload.Enabled = false;
                    btn_command.Enabled = false;

                    SetStatus("Disconnected!", Color.Red);

                    break;
            }
        }

        public enum WebSocketConnectionState
        {
            CONNECTING,
            DISCONNECTING,
            CONNECTED,
            DISCONNECTED
        }
    }
}