/****************************************************************************
 * Copyright (c) 2016 - 2023 liangxiegame UNDER MIT License
 *
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

using System;
using UnityEngine;

namespace QFramework.Experimental
{
    public abstract class ScriptableProperty : ScriptableObject{}
    public abstract class ScriptableProperty<T> : ScriptableProperty,IBindableProperty<T> 
    {
        public ScriptableProperty()
        {
            mValue = InitValue;
        }

        [SerializeField]
        public T InitValue;
        
        
        protected T mValue;

        public T Value
        {
            get => GetValue();
            set
            {
                if (value == null && mValue == null) return;
                if (value != null && value.Equals(mValue)) return;

                SetValue(value);
                mOnValueChanged?.Invoke(value);
            }
        }

        protected virtual void SetValue(T newValue) => mValue = newValue;

        protected virtual T GetValue() => mValue;

        public void SetValueWithoutEvent(T newValue) => mValue = newValue;

        private Action<T> mOnValueChanged = (v) => { };

        public IUnRegister Register(Action<T> onValueChanged)
        {
            mOnValueChanged += onValueChanged;
            return new ScriptablePropertyUnRegister<T>(this, onValueChanged);
        }

        public IUnRegister RegisterWithInitValue(Action<T> onValueChanged)
        {
            onValueChanged(mValue);
            return Register(onValueChanged);
        }
        
        public void UnRegister(Action<T> onValueChanged) => mOnValueChanged -= onValueChanged;
        
        IUnRegister IEasyEvent.Register(Action onEvent)
        {
            return Register(Action);
            void Action(T _) => onEvent();
        }

        public abstract void ResetValue();
    }
    
    
    public class ScriptablePropertyUnRegister<T> : IUnRegister
    {
        public ScriptablePropertyUnRegister(ScriptableProperty<T> scriptableProperty, Action<T> onValueChanged)
        {
            ScriptableProperty = scriptableProperty;
            OnValueChanged = onValueChanged;
        }

        public ScriptableProperty<T> ScriptableProperty { get; set; }

        public Action<T> OnValueChanged { get; set; }

        public void UnRegister()
        {
            ScriptableProperty.UnRegister(OnValueChanged);
            ScriptableProperty = null;
            OnValueChanged = null;
        }
    }
}
