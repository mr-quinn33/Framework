using Framework.Interface.Access;

namespace Framework.Interface
{
    public interface IController : IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IRegisterEvent, IUnregisterEvent, IRegisterDependency, IResolveDependency, IInjectDependency
    {
    }
}