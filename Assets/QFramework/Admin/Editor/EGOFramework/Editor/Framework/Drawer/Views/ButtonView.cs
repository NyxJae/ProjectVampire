using System;
using QFramework;
using UnityEngine;

namespace EGO.Framework
{
    public class ButtonView : View
    {
        public ButtonView(string text, Action onClickEvent)
        {
            Text = text;
            OnClickEvent = onClickEvent;
            Style = new FluentGUIStyle(()=>new GUIStyle(GUI.skin.button));
        }

        public string Text { get; set; }

        public Action OnClickEvent { get; set; }

        protected override void OnGUI()
        {
            if (GUILayout.Button(Text, Style.Value, LayoutStyles))
            {
                OnClickEvent.Invoke();
            }
        }
    }
}