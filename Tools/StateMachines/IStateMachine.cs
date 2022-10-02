using Framework.Tools.StateMachines.States;

namespace Framework.Tools.StateMachines
{
    public interface IStateMachine
    {
        void Update();

        void FixedUpdate();

        void Transit(IState state);
    }

    public interface IStateMachine<in T> where T : IState
    {
        void Update();

        void FixedUpdate();

        void Transit(T state);
    }
}