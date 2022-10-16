using Framework.Tools.Delegators;
using Framework.Tools.StateMachines.States;

namespace Framework.Tools.StateMachines
{
    public abstract class StateMachine : SendCommandDelegator, IStateMachine
    {
        private IState currentState;

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
            currentState?.OnExit();
            currentState = state;
            currentState.OnEnter();
        }
    }

    public abstract class StateMachine<T> : SendCommandDelegator, IStateMachine<T> where T : IState
    {
        private T currentState;

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
            currentState?.OnExit();
            currentState = state;
            currentState.OnEnter();
        }
    }
}