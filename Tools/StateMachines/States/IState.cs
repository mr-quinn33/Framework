using Framework.Tools.StateMachines.Delegators;

namespace Framework.Tools.StateMachines.States
{
    public interface IState : IDelegateSendCommand, IDelegateSendCommandAsync
    {
        void OnEnter();

        void Update();

        void FixedUpdate();

        void OnExit();
    }
}