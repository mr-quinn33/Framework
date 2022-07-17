using Framework.GameModes.Interfaces;
using Framework.Models.Interfaces;
using Framework.Rules.Interfaces;

namespace Framework.Models
{
    public abstract class Model : IModel
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

        void IModel.Initialize()
        {
            Initialize();
        }

        protected abstract void Initialize();
    }
}