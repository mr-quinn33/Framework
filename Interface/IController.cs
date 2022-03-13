using Framework.Interface.Access;

namespace Framework.Interface
{
    public interface IController : IGetSystem, IGetModel, ISendCommand, ISendCommandAsync, ISendQuery, IRegisterEvent,
        IUnregisterEvent, IRegisterDependency, IInjectDependency
    {
    }
}