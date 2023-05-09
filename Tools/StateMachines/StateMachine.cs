using Framework.Tools.StateMachines.States;

namespace Framework.Tools.StateMachines
{
    public abstract class StateMachine : IStateMachine
    {
        private IState currentState;

        protected StateMachine(IState currentState)
        {
            this.currentState = currentState;
        }

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
}