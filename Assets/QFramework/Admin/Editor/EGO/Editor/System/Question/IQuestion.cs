using System;
using EGO.Framework;

namespace EGO.System
{
    public interface IQuestion : IView
    {
        void OnProcess(Action onProcess);

        void OnChoice(Action<string> onChoice);

    }
    
    public interface IQuestion<TParent> : IQuestion where TParent : IQuectionContainer 
    {
        TParent Container { set; }

        TParent End();
    }
}