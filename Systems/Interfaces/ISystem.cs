using Framework.Rules.Interfaces;

namespace Framework.Systems.Interfaces
{
    public interface ISystem : ISetGameMode, IGetSystem, IGetModel, IGetUtility, IRegisterEvent, IUnregisterEvent, IInvokeEvent
    {
        void Initialize();
    }
}