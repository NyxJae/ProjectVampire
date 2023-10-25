using System;
using QFramework;
using UnityEditor;
using UnityEngine;

namespace EGO.Framework
{
    public class EnumPopupView<T> : View where T : Enum
    {
        public Property<T> ValueProperty { get; }

        public EnumPopupView(T initValue)
        {
            ValueProperty = new Property<T>(initValue);
            ValueProperty.Value = initValue;

            Style = new FluentGUIStyle(()=>new GUIStyle(EditorStyles.popup));
        }

        protected override void OnGUI()
        {
                            
            ValueProperty.Value = (T) EditorGUILayout.EnumPopup(ValueProperty.Value,Style.Value, LayoutStyles);
        }
    }
}