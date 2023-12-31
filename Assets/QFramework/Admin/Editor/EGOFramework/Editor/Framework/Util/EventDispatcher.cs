using System;
using System.Collections.Generic;

namespace EGO.Util
{
    public static class EventDispatcher
    {
        private static Dictionary<int,Action<object>> mRegisteredEvents = new Dictionary<int, Action<object>>();
        
        public static void Register<T>(T key,Action<object> onEvent) where T : IConvertible
        {
            int intKey = key.ToInt32(null);

            
            if (!mRegisteredEvents.TryGetValue(intKey, out var registerdEvent))
            {
                registerdEvent = (_) => { };
                registerdEvent += onEvent;
                mRegisteredEvents.Add(intKey, registerdEvent);
            }
            else
            {
                mRegisteredEvents[intKey] += onEvent;
            }
        }

        public static void UnRegister<T>(T key,Action<object> onEvent) where T : IConvertible
        {
            int intKey = key.ToInt32(null);

            if (!mRegisteredEvents.TryGetValue(intKey, out var registerdEvent))
            {
                
            }
            else
            {
                registerdEvent -= onEvent;
            }
        }
        
        public static void UnRegisterAll<T>(T key) where T : IConvertible
        {
            int intKey = key.ToInt32(null);
            mRegisteredEvents.Remove(intKey);
        }

        public static void Send<T>(T key,object arg = null) where T : IConvertible
        {
            int intKey = key.ToInt32(null);

            if (mRegisteredEvents.TryGetValue(intKey,out var registeredEvent))
            {
                registeredEvent.Invoke(arg);
            }
        }
    }
}