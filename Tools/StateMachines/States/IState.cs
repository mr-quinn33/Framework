namespace Framework.Tools.StateMachines.States
{
    public interface IState
    {
        void OnEnter(IStateMachine stateMachine);

        void Update(IStateMachine stateMachine, float deltaTime);

        void FixedUpdate(IStateMachine stateMachine, float fixedDeltaTime);

        void OnExit(IStateMachine stateMachine);
    }
}