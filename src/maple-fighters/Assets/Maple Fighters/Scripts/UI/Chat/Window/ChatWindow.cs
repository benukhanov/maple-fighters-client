﻿using System;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.UI.Chat
{
    [RequireComponent(typeof(UICanvasGroup))]
    public class ChatWindow : UIElement, IChatView
    {
        public event Action<bool> FocusChanged;

        public event Action<string> MessageAdded;

        public bool IsTypingBlocked
        {
            get => isTypingBlocked;
        }

        private const KeyCode SendMessageKeyCode = KeyCode.Return;
        private const KeyCode SecondarySendMessageKeyCode = KeyCode.KeypadEnter;
        private const KeyCode CloseMessageKeyCode = KeyCode.Escape;

        [Header("Texts")]
        [SerializeField]
        private Text chatText;

        [SerializeField]
        private InputField chatInputField;

        private bool IsTypingMessage
        {
            get => isTypingMessage;

            set
            {
                isTypingMessage = value;

                FocusChanged?.Invoke(isTypingMessage);
            }
        }

        private bool isTypingBlocked;
        private bool isTypingMessage;

        private void Update()
        {
            if (isTypingBlocked)
            {
                return;
            }

            if (IsTypingMessage)
            {
                FocusableState();
            }
            else
            {
                UnFocusableState();
            }
        }

        private void FocusableState()
        {
            if (IsAnySendKeyPressed() || IsEscapeKeyPressed())
            {
                if (IsAnySendKeyPressed())
                {
                    SendMessage();
                }

                ActivateOrDeactivateInputField();
                SelectOrDeselectChatInputField();

                IsTypingMessage = false;
            }
        }

        private void UnFocusableState()
        {
            if (IsAnySendKeyPressed())
            {
                IsTypingMessage = true;

                ActivateOrDeactivateInputField();
                SelectOrDeselectChatInputField();
            }
        }

        public void AddMessage(string message, UIChatMessageColor color = UIChatMessageColor.None)
        {
            if (chatText != null)
            {
                if (color != UIChatMessageColor.None)
                {
                    var colorName = color.ToString().ToLower();
                    message = $"<color={colorName}>{message}</color>";
                }

                var isEmpty = chatText.text.Length == 0;
                chatText.text += !isEmpty ? $"\n{message}" : $"{message}";
            }
        }

        public void BlockOrUnblockTyping(bool block)
        {
            if (block)
            {
                if (chatInputField != null)
                {
                    chatInputField.text = string.Empty;
                    chatInputField.gameObject.SetActive(false);
                }

                if (EventSystem.current != null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }

                isTypingBlocked = true;
                isTypingMessage = false;
            }
            else
            {
                isTypingBlocked = false;
            }
        }

        private void SendMessage()
        {
            if (chatInputField != null)
            {
                var text = chatInputField.text;

                if (!string.IsNullOrWhiteSpace(text))
                {
                    MessageAdded?.Invoke(text);
                }
            }
        }

        private void ActivateOrDeactivateInputField()
        {
            if (chatInputField != null)
            {
                var isActive = chatInputField.gameObject.activeSelf;

                chatInputField.text = string.Empty;
                chatInputField.gameObject.SetActive(!isActive);
            }
        }

        private void SelectOrDeselectChatInputField()
        {
            if (chatInputField != null)
            {
                var active = chatInputField.gameObject.activeSelf;
                var selected = active ? chatInputField.gameObject : null;

                if (EventSystem.current != null)
                {
                    EventSystem.current.SetSelectedGameObject(selected);
                }
            }
        }

        private bool IsAnySendKeyPressed()
        {
            return Input.GetKeyDown(SendMessageKeyCode)
                   || Input.GetKeyDown(SecondarySendMessageKeyCode);
        }

        private bool IsEscapeKeyPressed()
        {
            return Input.GetKeyDown(CloseMessageKeyCode);
        }
    }
}