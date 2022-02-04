using System;
using System.Collections.Generic;
using Framework.Interface;

namespace Framework.GameMode
{
    public abstract class GameModeBase : IArchitecture
    {
        private protected static readonly Dictionary<Type, IArchitecture> Instances = new();

        public abstract void RegisterSystem<T>(T system) where T : class, ISystem;

        public abstract void RegisterModel<T>(T model) where T : class, IModel;

        public abstract void RegisterUtility<T>(T utility) where T : class, IUtility;

        public abstract T GetSystem<T>() where T : class, ISystem;

        public abstract T GetModel<T>() where T : class, IModel;

        public abstract T GetUtility<T>() where T : class, IUtility;

        public abstract void SendCommand<T>() where T : ICommand, new();

        public abstract void SendCommand<T>(T command) where T : ICommand;

        public abstract TResult SendQuery<TResult>(IQuery<TResult> query);

        public abstract IUnregisterHandler RegisterEvent<T>(Action<T> action);

        public abstract IUnregisterHandler[] RegisterEvent<T>(params Action<T>[] actions);

        public abstract IUnregisterHandler RegisterEvent<T, TResult>(Func<T, TResult> func);

        public abstract IUnregisterHandler[] RegisterEvent<T, TResult>(params Func<T, TResult>[] functions);

        public abstract void UnregisterEvent<T>(Action<T> action);

        public abstract void UnregisterEvent<T>(params Action<T>[] actions);

        public abstract void UnregisterEvent<T, TResult>(Func<T, TResult> func);

        public abstract void UnregisterEvent<T, TResult>(params Func<T, TResult>[] functions);

        public abstract void InvokeEvent<T>() where T : new();

        public abstract void InvokeEvent<T, TResult>(out TResult result) where T : new();

        public abstract TResult InvokeEvent<T, TResult>() where T : new();

        public abstract void InvokeEvent<T>(T t);

        public abstract void InvokeEvent<T, TResult>(T t, out TResult result);

        public abstract TResult InvokeEvent<T, TResult>(T t);
    }
}