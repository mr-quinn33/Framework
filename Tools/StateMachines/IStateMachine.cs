using Framework.Tools.StateMachines.States;

namespace Framework.Tools.StateMachines
{
    public interface IStateMachine
    {
        void Update();

        void FixedUpdate();

        void Transit(IState state);
    }
}