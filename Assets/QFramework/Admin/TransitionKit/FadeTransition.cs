using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace QFramework
{
    public class FadeTransition : IAction
    {
        private float mFrom;
        private float mTo;
        private float mDuration;
        private Color mColor;
        private Action mOnFinish;
        IUnRegister onGUI = null;
        IActionController lerp = null;
        Texture2D texture = null;

        private IAction mLerpAction;

        public FadeTransition From(float from)
        {
            mFrom = from;
            return this;
        }

        public FadeTransition To(float to)
        {
            mTo = to;
            return this;
        }

        public FadeTransition Color(Color color)
        {
            mColor = color;
            return this;
        }

        public FadeTransition Duration(float duration)
        {
            mDuration = duration;
            return this;
        }

        public FadeTransition OnFinish(Action onFinish)
        {
            mOnFinish = onFinish;
            return this;
        }

        public ulong ActionID { get; set; }
        public ActionStatus Status { get; set; }

        public void OnStart()
        {
            texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, mColor);
            onGUI = ActionKit.OnGUI.Register(() =>
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture,
                    ScaleMode.StretchToFill,
                    false, 0,
                    mColor, Vector4.zero, 0);
            });
            ActionKit.Lerp(mFrom, mTo, mDuration, p => { mColor.a = p; }, () =>
                {
                    mColor.a = mTo;
                    mOnFinish?.Invoke();
                    this.Finish();
                    
                    ActionKit.NextFrame(() =>
                    {
                        Object.Destroy(texture);
                        texture = null;
                        onGUI.UnRegister();
                        onGUI = null;
                    }).StartGlobal();
                })
                .StartGlobal();
        }

        public void OnExecute(float dt)
        {
        }

        public void OnFinish()
        {
        }

        public bool Deinited { get; set; }
        public bool Paused { get; set; }

        public void Reset()
        {
        }

        public void Deinit()
        {
        }
    }
}