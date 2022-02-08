using System;

namespace Framework.Interface
{
    public interface IEventSystem
    {
        IUnregisterHandler Register<T>(Action<T> action);

        IUnregisterHandler Register<T, TResult>(Func<T, TResult> func);

        void Unregister<T>(Action<T> action);

        void Unregister<T, TResult>(Func<T, TResult> func);

        void Invoke<T>(T t);
        
        void Invoke<T>() where T : new();

        TResult Invoke<T, TResult>(T t);
        
        TResult Invoke<T, TResult>() where T : new();
    }
}