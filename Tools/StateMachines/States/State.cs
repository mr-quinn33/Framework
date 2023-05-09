using Framework.GameModes;
using Framework.Rules;

namespace Framework.Tools.StateMachines.States
{
    public abstract class State<T> : IState, ISendCommand, ISendCommandAsync where T : GameMode<T>, new()
    {
        public abstract void OnEnter();

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void OnExit();

        IGameMode IGetGameMode.GetGameMode() => GameMode<T>.Load();
    }
}