namespace Framework.Tools.StateMachines.States
{
    public interface IState
    {
        void OnEnter(IStateMachine stateMachine);

        void Update(IStateMachine stateMachine);

        void FixedUpdate(IStateMachine stateMachine);

        void OnExit(IStateMachine stateMachine);
    }
}