using Framework.Interface.Restriction;

namespace Framework.Interface
{
    public interface ISystem : ISetArchitecture, IGetSystem, IGetModel, IGetUtility, IRegisterEvent, IUnregisterEvent, IInvokeEvent
    {
        void Initialize();
    }
}