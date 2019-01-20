﻿using System;
using Scripts.Services;
using Scripts.UI.Windows;
using Scripts.Utils;
using UI.Manager;

namespace Scripts.UI.Controllers
{
    public class ChatController : MonoSingleton<ChatController>
    {
        public event Action<string> MessageSent; 

        private ChatWindow chatWindow;

        protected override void OnAwake()
        {
            base.OnAwake();

            chatWindow = UIElementsCreator.GetInstance().Create<ChatWindow>();
            chatWindow.MessageAdded += OnMessageAdded;

            // TODO: Find a better solution, lol...
            DontDestroyOnLoad(chatWindow.transform.root);
        }

        private void Start()
        {
            ChatConnectionProvider.GetInstance().Connect();
        }

        protected override void OnDestroying()
        {
            base.OnDestroying();

            if (chatWindow != null)
            {
                chatWindow.MessageAdded -= OnMessageAdded;

                Destroy(chatWindow.gameObject);
            }
        }

        public void SetCharacterName(string name)
        {
            if (chatWindow != null)
            {
                chatWindow.CharacterName = name;
            }
        }

        public void OnMessageReceived(string message)
        {
            if (chatWindow != null)
            {
                chatWindow.AddMessage(message);
            }
        }

        private void OnMessageAdded(string message)
        {
            MessageSent?.Invoke(message);
        }
    }
}