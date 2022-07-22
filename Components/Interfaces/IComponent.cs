using Framework.Rules.Interfaces;

namespace Framework.Components.Interfaces
{
    public interface IComponent : ISendCommand, ISendCommandAsync, ISendQuery, IRegisterEvent, IUnregisterEvent, IRegisterDependency, IResolveDependency, IInjectDependency
    {
    }
}