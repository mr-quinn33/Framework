using System.Threading.Tasks;
using Framework.Commands;

namespace Framework.Tools.StateMachines.States
{
    public abstract class State : IState
    {
        private readonly IStateMachine stateMachine;

        protected State(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void SendCommand<T>(T command) where T : ICommand
        {
            stateMachine.SendCommand(command);
        }

        public void SendCommand<T>() where T : ICommand, new()
        {
            stateMachine.SendCommand<T>();
        }

        public async Task SendCommandAsync<T>(T command) where T : ICommandAsync
        {
            await stateMachine.SendCommandAsync(command);
        }

        public async Task SendCommandAsync<T>() where T : ICommandAsync, new()
        {
            await stateMachine.SendCommandAsync<T>();
        }

        public abstract void OnEnter();

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void OnExit();
    }

    public abstract class State<T> : IState where T : IState
    {
        private readonly IStateMachine<T> stateMachine;

        protected State(IStateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void SendCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            stateMachine.SendCommand(command);
        }

        public void SendCommand<TCommand>() where TCommand : ICommand, new()
        {
            stateMachine.SendCommand<TCommand>();
        }

        public async Task SendCommandAsync<TCommand>(TCommand command) where TCommand : ICommandAsync
        {
            await stateMachine.SendCommandAsync(command);
        }

        public async Task SendCommandAsync<TCommand>() where TCommand : ICommandAsync, new()
        {
            await stateMachine.SendCommandAsync<TCommand>();
        }

        public abstract void OnEnter();

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void OnExit();
    }
}