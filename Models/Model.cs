using Framework.GameModes;
using Framework.Rules;

namespace Framework.Models
{
    public interface IModel : ISetGameMode, IGetUtility, ISendEvent
    {
        void Initialize();
    }

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