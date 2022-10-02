namespace Framework.Tools.StateMachines.States
{
    public abstract class State : IState
    {
        protected readonly IStateMachine stateMachine;

        protected State(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public abstract void OnEnter();

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void OnExit();
    }

    public abstract class State<T> : IState where T : IState
    {
        protected readonly IStateMachine<T> stateMachine;

        protected State(IStateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public abstract void OnEnter();

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void OnExit();
    }
}