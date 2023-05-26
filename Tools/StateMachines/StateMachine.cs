using System;
using Framework.Commands;
using Framework.Extensions;
using Framework.Interfaces;
using Framework.Rules;
using Framework.Tools.StateMachines.States;

namespace Framework.Tools.StateMachines
{
    public interface IStateMachine
    {
        void Update();

        void FixedUpdate();

        void Transit(IState state);

        void SendCommand<T>() where T : ICommand, new();

        void SendCommand<T>(T command) where T : ICommand;

        IUnregisterHandler RegisterOnGetSendCommand(Func<ISendCommand> callback);

        void UnregisterOnGetSendCommand(Func<ISendCommand> callback);
    }

    public abstract class StateMachine : IStateMachine
    {
        private IState currentState;

        private event Func<ISendCommand> OnGetSendCommand;

        protected StateMachine(IState currentState)
        {
            this.currentState = currentState;
        }

        public void Update()
        {
            currentState.Update(this);
        }

        public void FixedUpdate()
        {
            currentState.FixedUpdate(this);
        }

        public void Transit(IState state)
        {
            currentState.OnExit(this);
            currentState = state;
            currentState.OnEnter(this);
        }

        public void SendCommand<T>() where T : ICommand, new()
        {
            OnGetSendCommand?.Invoke().SendCommand<T>();
        }

        public void SendCommand<T>(T command) where T : ICommand
        {
            OnGetSendCommand?.Invoke().SendCommand(command);
        }

        public IUnregisterHandler RegisterOnGetSendCommand(Func<ISendCommand> callback)
        {
            OnGetSendCommand += callback;
            return new StateMachineOnGetSendCommandUnregisterHandler(this, callback);
        }

        public void UnregisterOnGetSendCommand(Func<ISendCommand> callback)
        {
            OnGetSendCommand -= callback;
        }

        private sealed class StateMachineOnGetSendCommandUnregisterHandler : IUnregisterHandler
        {
            private IStateMachine stateMachine;
            private Func<ISendCommand> callback;

            public StateMachineOnGetSendCommandUnregisterHandler(IStateMachine stateMachine, Func<ISendCommand> callback)
            {
                this.stateMachine = stateMachine;
                this.callback = callback;
            }

            public void Unregister()
            {
                stateMachine.UnregisterOnGetSendCommand(callback);
                stateMachine = null;
                callback = null;
            }
        }
    }
}