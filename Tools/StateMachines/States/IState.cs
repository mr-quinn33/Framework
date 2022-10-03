using Framework.Tools.Delegations.Commands;

namespace Framework.Tools.StateMachines.States
{
    public interface IState : IDelegateSendCommand
    {
        void OnEnter();

        void Update();

        void FixedUpdate();

        void OnExit();
    }
}