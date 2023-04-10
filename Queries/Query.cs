using Framework.GameModes;
using Framework.Rules;

namespace Framework.Queries
{
    public interface IQuery<out TResult> : ISetGameMode, IGetSystem, IGetModel, ISendQuery
    {
        TResult Execute();
    }

    public abstract class Query<T> : IQuery<T>
    {
        private IGameMode gameMode;

        IGameMode IGetGameMode.GetGameMode() => gameMode;

        void ISetGameMode.SetGameMode(IGameMode gameMode) => this.gameMode = gameMode;

        T IQuery<T>.Execute() => Execute();

        protected abstract T Execute();
    }
}