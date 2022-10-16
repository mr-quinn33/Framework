using Framework.Tools.Delegators;

namespace Framework.Tools.StateMachines.States
{
    public interface IState : ICanSendCommand
    {
        void OnEnter();

        void Update();

        void FixedUpdate();

        void OnExit();
    }
}