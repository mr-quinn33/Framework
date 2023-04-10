using Framework.GameModes;
using Framework.Rules;

namespace Framework.Components
{
    public interface IComponent : ISendCommand, ISendCommandAsync, ISendQuery, IRegisterEvent, IUnregisterEvent, IRegisterDependency, IResolveDependency, IInjectDependency
    {
    }

    public interface IComponent<T> : IComponent where T : GameMode<T>, new()
    {
        /// <summary>
        /// This is the default implementation of <see cref="IGetGameMode.GetGameMode()"/>.
        /// </summary>
        /// <returns><see cref="IGameMode"/></returns>
        IGameMode IGetGameMode.GetGameMode() => GameMode<T>.Load();
    }
}