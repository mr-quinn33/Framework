using System;
using Framework.Commands;
using Framework.Interfaces;

namespace Framework.Tools.Delegators
{
    public interface ISendCommandDelegator : ICanSendCommand
    {
        IUnregisterHandler RegisterOnSendCommand(Action<ICommand> action);

        void UnregisterOnSendCommand(Action<ICommand> action);
    }

    public class SendCommandDelegator : ISendCommandDelegator
    {
        IUnregisterHandler ISendCommandDelegator.RegisterOnSendCommand(Action<ICommand> action)
        {
            OnSendCommand += action;
            return new SendCommandDelegatorUnregisterHandler(this, action);
        }

        void ISendCommandDelegator.UnregisterOnSendCommand(Action<ICommand> action)
        {
            OnSendCommand -= action;
        }

        void ICanSendCommand.SendCommand<T>(T command)
        {
            OnSendCommand?.Invoke(command);
        }

        void ICanSendCommand.SendCommand<T>()
        {
            OnSendCommand?.Invoke(new T());
        }

        private event Action<ICommand> OnSendCommand;

        private class SendCommandDelegatorUnregisterHandler : IUnregisterHandler
        {
            private Action<ICommand> action;
            private ISendCommandDelegator sendCommandDelegator;

            public SendCommandDelegatorUnregisterHandler(ISendCommandDelegator sendCommandDelegator, Action<ICommand> action)
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
    }
}