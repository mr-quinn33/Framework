using Framework.Rules.Interfaces;

namespace Framework.Components.Interfaces
{
    public interface IComponent : IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IRegisterEvent, IUnregisterEvent, IRegisterDependency, IResolveDependency, IInjectDependency
    {
    }
}