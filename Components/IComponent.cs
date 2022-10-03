using Framework.Rules;

namespace Framework.Components
{
    public interface IComponent : ISendCommand, ISendCommandAsync, ISendQuery, IRegisterEvent, IUnregisterEvent, IRegisterDependency, IResolveDependency, IInjectDependency
    {
    }
}