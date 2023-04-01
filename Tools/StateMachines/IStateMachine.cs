using Framework.Tools.StateMachines.Delegators;
using Framework.Tools.StateMachines.States;

namespace Framework.Tools.StateMachines
{
    public interface IStateMachine : ISendCommandDelegator
    {
        void Update();

        void FixedUpdate();

        void Transit(IState state);
    }

    public interface IStateMachine<in T> : ISendCommandDelegator where T : IState
    {
        void Update();

        void FixedUpdate();

        void Transit(T state);
    }
}