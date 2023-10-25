using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace EGO.Framework
{
    public interface IView : IDisposable
    {
        void Show();

        void Hide();
        
        void DrawGUI();        

        ILayout Parent { get; set; }
        
        FluentGUIStyle Style { get; }
        
        Color BackgroundColor { get; set; }

        void RefreshNextFrame();

        void AddLayoutOption(GUILayoutOption option);
        
        void RemoveFromParent();

        void Refresh();
    }
}