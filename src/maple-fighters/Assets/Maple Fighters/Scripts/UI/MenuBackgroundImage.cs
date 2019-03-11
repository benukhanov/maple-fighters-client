﻿using System;
using UI.Manager;
using UnityEngine.EventSystems;

namespace Scripts.UI
{
    public class MenuBackgroundImage : UIElement,
                                       IPointerClickHandler,
                                       IMenuBackgroundView
    {
        public event Action PointerClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClicked?.Invoke();
        }
    }
}