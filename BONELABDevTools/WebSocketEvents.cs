using WebSocketSharp;

namespace BonelabDevMode
{
    internal class WebSocketEvents
    {
        internal WebSocket webSocket;

        public int MessageID = 0;

        public event EventHandler<CustomMessageEventArgs> OnLateMessage;

        public event EventHandler<CustomMessageEventArgs> OnMessage;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public WebSocketEvents(WebSocket webSocket)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            this.webSocket = webSocket;
            webSocket.OnMessage += (sender, e) =>
            {
                MessageID++;
                var args = new CustomMessageEventArgs(e, MessageID);
                OnMessage?.Invoke(this, args);
                OnLateMessage?.Invoke(this, args);
            };
        }
    }

    public class CustomMessageEventArgs(MessageEventArgs args, int messageID) : EventArgs
    {
        public int MessageID { get; private set; } = messageID;

        public MessageEventArgs EventArgs { get; private set; } = args;
    }
}