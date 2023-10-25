using QFramework;
using UnityEngine;

namespace EGO.Framework
{
    public class BoxView : View
    {
        public string Text;

        public BoxView(string text)
        {
            Text = text;
            Style = new FluentGUIStyle(()=>new GUIStyle(GUI.skin.box));
        }
        
        protected override void OnGUI()
        {
            GUILayout.Box(Text,Style.Value,LayoutStyles);
        }
    }
}