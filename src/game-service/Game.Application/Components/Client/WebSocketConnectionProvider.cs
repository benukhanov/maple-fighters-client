using System;
using Fleck;

namespace Game.Application.Components
{
    public class WebSocketConnectionProvider : ComponentBase, IWebSocketConnectionProvider
    {
        public event Action ConnectionEstablished;

        public event Action ConnectionClosed;

        public event Action<Exception> ErrorOccurred;

        public event Action<string> MessageReceived;

        private readonly int id;
        private readonly IWebSocketConnection connection;

        public WebSocketConnectionProvider(int id, IWebSocketConnection connection)
        {
            this.id = id;
            this.connection = connection;
            this.connection.OnOpen += OnConnectionEstablished;
            this.connection.OnClose += OnConnectionClosed;
            this.connection.OnError += OnErrorOccurred;
            this.connection.OnMessage += OnMessageReceived;
        }

        private void OnConnectionEstablished()
        {
            ConnectionEstablished?.Invoke();
        }

        private void OnConnectionClosed()
        {
            ConnectionClosed?.Invoke();
        }

        private void OnErrorOccurred(Exception exception)
        {
            ErrorOccurred?.Invoke(exception);
        }

        private void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(message);
        }

        public int ProvideId()
        {
            return id;
        }

        public IWebSocketConnection ProvideConnection()
        {
            return connection;
        }
    }
}