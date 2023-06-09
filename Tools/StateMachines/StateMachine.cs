using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Commands;
using Framework.Extensions;
using Framework.Interfaces;
using Framework.Rules;
using Framework.Tools.StateMachines.States;

namespace Framework.Tools.StateMachines
{
    public interface IStateMachine
    {
        void Update(float deltaTime);

        void FixedUpdate(float fixedDeltaTime);

        void Transit(IState state);

        void Transit<T>() where T : IState;

        void SendCommand<T>() where T : ICommand, new();

        void SendCommand<T>(T command) where T : ICommand;

        Task SendCommandAsync<T>() where T : ICommandAsync, new();

        Task SendCommandAsync<T>(T command) where T : ICommandAsync;

        Task SendCommandAsync<T>(CancellationTokenSource source) where T : ICommandCancellableAsync, new();

        Task SendCommandAsync<T>(T command, CancellationTokenSource source) where T : ICommandCancellableAsync;

        IUnregisterHandler RegisterOnGetSendCommand(Func<ISendCommand> callback);
        
        void RegisterOnGetSendCommandNonAlloc(Func<ISendCommand> callback);

        IUnregisterHandler RegisterOnGetSendCommandAsync(Func<ISendCommandAsync> callback);

        void RegisterOnGetSendCommandAsyncNonAlloc(Func<ISendCommandAsync> callback);

        void UnregisterOnGetSendCommand(Func<ISendCommand> callback);

        void UnregisterOnGetSendCommandAsync(Func<ISendCommandAsync> callback);
    }

    public abstract class StateMachine : IStateMachine
    {
        private IState currentState;
        private readonly IEnumerable<IState> states;

        private event Func<ISendCommand> OnGetSendCommand;
        private event Func<ISendCommandAsync> OnGetSendCommandAsync;

        protected StateMachine(IState currentState, params IState[] states)
        {
            this.currentState = currentState;
            this.states = states;
        }

        public void Update(float deltaTime)
        {
            currentState.Update(this, deltaTime);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            currentState.FixedUpdate(this, fixedDeltaTime);
        }

        public void Transit(IState state)
        {
            currentState.OnExit(this);
            currentState = state;
            currentState.OnEnter(this);
        }

        public void Transit<T>() where T : IState
        {
            Transit(states.First(x => x is T));
        }

        public void SendCommand<T>() where T : ICommand, new()
        {
            OnGetSendCommand?.Invoke().SendCommand<T>();
        }

        public void SendCommand<T>(T command) where T : ICommand
        {
            OnGetSendCommand?.Invoke().SendCommand(command);
        }

        public async Task SendCommandAsync<T>() where T : ICommandAsync, new()
        {
            if (OnGetSendCommandAsync != null)
            {
                await OnGetSendCommandAsync().SendCommandAsync<T>();
            }
        }

        public async Task SendCommandAsync<T>(T command) where T : ICommandAsync
        {
            if (OnGetSendCommandAsync != null)
            {
                await OnGetSendCommandAsync().SendCommandAsync(command);
            }
        }

        public async Task SendCommandAsync<T>(CancellationTokenSource source) where T : ICommandCancellableAsync, new()
        {
            if (OnGetSendCommandAsync != null)
            {
                await OnGetSendCommandAsync().SendCommandAsync<T>(source);
            }
        }

        public async Task SendCommandAsync<T>(T command, CancellationTokenSource source) where T : ICommandCancellableAsync
        {
            if (OnGetSendCommandAsync != null)
            {
                await OnGetSendCommandAsync().SendCommandAsync(command, source);
            }
        }

        public IUnregisterHandler RegisterOnGetSendCommand(Func<ISendCommand> callback)
        {
            OnGetSendCommand += callback;
            return new StateMachineOnGetSendCommandUnregisterHandler(this, callback);
        }

        public void RegisterOnGetSendCommandNonAlloc(Func<ISendCommand> callback)
        {
            OnGetSendCommand += callback;
        }

        public IUnregisterHandler RegisterOnGetSendCommandAsync(Func<ISendCommandAsync> callback)
        {
            OnGetSendCommandAsync += callback;
            return new StateMachineOnGetSendCommandAsyncUnregisterHandler(this, callback);
        }

        public void RegisterOnGetSendCommandAsyncNonAlloc(Func<ISendCommandAsync> callback)
        {
            OnGetSendCommandAsync += callback;
        }

        public void UnregisterOnGetSendCommand(Func<ISendCommand> callback)
        {
            OnGetSendCommand -= callback;
        }

        public void UnregisterOnGetSendCommandAsync(Func<ISendCommandAsync> callback)
        {
            OnGetSendCommandAsync -= callback;
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

        private sealed class StateMachineOnGetSendCommandAsyncUnregisterHandler : IUnregisterHandler
        {
            private IStateMachine stateMachine;
            private Func<ISendCommandAsync> callback;

            public StateMachineOnGetSendCommandAsyncUnregisterHandler(IStateMachine stateMachine, Func<ISendCommandAsync> callback)
            {
                this.stateMachine = stateMachine;
                this.callback = callback;
            }

            public void Unregister()
            {
                stateMachine.UnregisterOnGetSendCommandAsync(callback);
                stateMachine = null;
                callback = null;
            }
        }
    }
}