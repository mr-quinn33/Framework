using Framework.GameModes.Interfaces;
using Framework.Queries.Interfaces;
using Framework.Rules.Interfaces;

namespace Framework.Queries
{
    public abstract class Query<T> : IQuery<T>
    {
        private IGameMode gameMode;

        IGameMode IGetGameMode.GetGameMode()
        {
            return gameMode;
        }

        void ISetGameMode.SetGameMode(IGameMode gameMode)
        {
            this.gameMode = gameMode;
        }

        T IQuery<T>.Execute()
        {
            return Execute();
        }

        protected abstract T Execute();
    }
}