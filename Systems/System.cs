using Framework.GameModes.Interfaces;
using Framework.Rules.Interfaces;
using Framework.Systems.Interfaces;

namespace Framework.Systems
{
    public abstract class System : ISystem
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

        void ISystem.Initialize()
        {
            Initialize();
        }

        protected abstract void Initialize();
    }
}