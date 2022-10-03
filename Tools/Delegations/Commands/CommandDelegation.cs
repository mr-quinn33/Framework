using System;
using Framework.Commands;
using Framework.Interfaces;

namespace Framework.Tools.Delegations.Commands
{
    public interface IDelegateSendCommand
    {
        void SendCommand<T>(T command) where T : ICommand;

        void SendCommand<T>() where T : ICommand, new();
    }

    public interface ICommandDelegation : IDelegateSendCommand
    {
        IUnregisterHandler RegisterOnSendCommand(Action<ICommand> action);

        void UnregisterOnSendCommand(Action<ICommand> action);
    }

    public class CommandDelegation : ICommandDelegation
    {
        IUnregisterHandler ICommandDelegation.RegisterOnSendCommand(Action<ICommand> action)
        {
            OnSendCommand += action;
            return new CommandDelegationUnregisterHandler(this, action);
        }

        void ICommandDelegation.UnregisterOnSendCommand(Action<ICommand> action)
        {
            OnSendCommand -= action;
        }

        void IDelegateSendCommand.SendCommand<T>(T command)
        {
            OnSendCommand?.Invoke(command);
        }

        void IDelegateSendCommand.SendCommand<T>()
        {
            OnSendCommand?.Invoke(new T());
        }

        private event Action<ICommand> OnSendCommand;

        private class CommandDelegationUnregisterHandler : IUnregisterHandler
        {
            private Action<ICommand> action;
            private ICommandDelegation commandDelegation;

            public CommandDelegationUnregisterHandler(ICommandDelegation commandDelegation, Action<ICommand> action)
            {
                this.commandDelegation = commandDelegation;
                this.action = action;
            }

            public void Unregister()
            {
                commandDelegation.UnregisterOnSendCommand(action);
                commandDelegation = null;
                action = null;
            }
        }
    }
}