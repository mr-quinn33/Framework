using Framework.Tools.Delegations.Commands;
using Framework.Tools.StateMachines.States;

namespace Framework.Tools.StateMachines
{
    public interface IStateMachine : ICommandDelegation
    {
        void Update();

        void FixedUpdate();

        void Transit(IState state);
    }

    public interface IStateMachine<in T> : ICommandDelegation where T : IState
    {
        void Update();

        void FixedUpdate();

        void Transit(T state);
    }
}