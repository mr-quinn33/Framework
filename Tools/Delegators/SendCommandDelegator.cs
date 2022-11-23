using System;
using System.Threading.Tasks;
using Framework.Commands;
using Framework.Interfaces;

namespace Framework.Tools.Delegators
{
    public interface ISendCommandDelegator : IDelegateSendCommand, IDelegateSendCommandAsync
    {
        IUnregisterHandler RegisterOnSendCommand(Action<ICommand> action);

        IUnregisterHandler RegisterOnSendCommandAsync(Func<ICommandAsync, Task> func);

        void UnregisterOnSendCommand(Action<ICommand> action);

        void UnregisterOnSendCommandAsync(Func<ICommandAsync, Task> func);
    }

    public class SendCommandDelegator : ISendCommandDelegator
    {
        IUnregisterHandler ISendCommandDelegator.RegisterOnSendCommand(Action<ICommand> action)
        {
            OnSendCommand += action;
            return new SendCommandUnregisterHandler(this, action);
        }

        IUnregisterHandler ISendCommandDelegator.RegisterOnSendCommandAsync(Func<ICommandAsync, Task> func)
        {
            OnSendCommandAsync += func;
            return new SendCommandAsyncUnregisterHandler(this, func);
        }

        void ISendCommandDelegator.UnregisterOnSendCommand(Action<ICommand> action)
        {
            OnSendCommand -= action;
        }

        void ISendCommandDelegator.UnregisterOnSendCommandAsync(Func<ICommandAsync, Task> func)
        {
            OnSendCommandAsync -= func;
        }

        void IDelegateSendCommand.SendCommand<T>(T command)
        {
            OnSendCommand?.Invoke(command);
        }

        void IDelegateSendCommand.SendCommand<T>()
        {
            OnSendCommand?.Invoke(new T());
        }

        async Task IDelegateSendCommandAsync.SendCommandAsync<T>(T command)
        {
            if (OnSendCommandAsync == null) await Task.CompletedTask;
            else await OnSendCommandAsync.Invoke(command);
        }

        async Task IDelegateSendCommandAsync.SendCommandAsync<T>()
        {
            if (OnSendCommandAsync == null) await Task.CompletedTask;
            else await OnSendCommandAsync.Invoke(new T());
        }

        private event Action<ICommand> OnSendCommand;

        private event Func<ICommandAsync, Task> OnSendCommandAsync;

        private class SendCommandUnregisterHandler : IUnregisterHandler
        {
            private Action<ICommand> action;
            private ISendCommandDelegator sendCommandDelegator;

            public SendCommandUnregisterHandler(ISendCommandDelegator sendCommandDelegator, Action<ICommand> action)
            {
                this.sendCommandDelegator = sendCommandDelegator;
                this.action = action;
            }

            public void Unregister()
            {
                sendCommandDelegator.UnregisterOnSendCommand(action);
                sendCommandDelegator = null;
                action = null;
            }
        }

        private class SendCommandAsyncUnregisterHandler : IUnregisterHandler
        {
            private Func<ICommandAsync, Task> func;
            private ISendCommandDelegator sendCommandDelegator;

            public SendCommandAsyncUnregisterHandler(ISendCommandDelegator sendCommandDelegator, Func<ICommandAsync, Task> func)
            {
                this.sendCommandDelegator = sendCommandDelegator;
                this.func = func;
            }

            public void Unregister()
            {
                sendCommandDelegator.UnregisterOnSendCommandAsync(func);
                sendCommandDelegator = null;
                func = null;
            }
        }
    }
}