using Framework.Tools.StateMachines.States;

namespace Framework.Tools.StateMachines
{
    public abstract class StateMachine : IStateMachine
    {
        protected IState currentState;

        public void Update()
        {
            currentState.Update();
        }

        public void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        public void Transit(IState state)
        {
            currentState.OnExit();
            currentState = state;
            currentState.OnEnter();
        }
    }

    public abstract class StateMachine<T> : IStateMachine<T> where T : IState
    {
        protected T currentState;

        public void Update()
        {
            currentState.Update();
        }

        public void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        public void Transit(T state)
        {
            currentState.OnExit();
            currentState = state;
            currentState.OnEnter();
        }
    }
}