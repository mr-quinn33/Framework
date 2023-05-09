namespace Framework.Tools.StateMachines.States
{
    public interface IState
    {
        void OnEnter();

        void Update();

        void FixedUpdate();

        void OnExit();
    }
}