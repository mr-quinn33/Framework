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

        public abstract void OnEnter();

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void OnExit();
    }
}