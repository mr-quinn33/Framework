using System;

namespace Framework.Interface
{
    public interface IArchitecture
    {
        void RegisterSystem<T>(T system) where T : class, ISystem;

        void RegisterModel<T>(T model) where T : class, IModel;

        void RegisterUtility<T>(T utility) where T : class, IUtility;

        T GetSystem<T>() where T : class, ISystem;

        T GetModel<T>() where T : class, IModel;

        T GetUtility<T>() where T : class, IUtility;

        void SendCommand<T>() where T : ICommand, new();

        void SendCommand<T>(T command) where T : ICommand;

        IUnregisterHandler RegisterEvent<T>(Action<T> action);

        IUnregisterHandler RegisterEvent<T, TResult>(Func<T, TResult> func);

        void UnregisterEvent<T>(Action<T> action);

        void UnregisterEvent<T, TResult>(Func<T, TResult> func);

        void InvokeEvent<T>() where T : new();

        void InvokeEvent<T, TResult>(out TResult result) where T : new();

        TResult InvokeEvent<T, TResult>() where T : new();

        void InvokeEvent<T>(T t);

        void InvokeEvent<T, TResult>(T t, out TResult result);

        TResult InvokeEvent<T, TResult>(T t);
    }
}