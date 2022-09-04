using Framework.GameModes;
using Framework.Rules;

namespace Framework.Systems
{
    public interface ISystem : ISetGameMode, IGetSystem, IGetModel, IGetUtility, IRegisterEvent, IUnregisterEvent, ISendEvent
    {
        void Initialize();
    }

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