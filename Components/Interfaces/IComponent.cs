using Framework.Rules.Interfaces;

namespace Framework.Components.Interfaces
{
    public interface IComponent : IRegisterDependency, IResolveDependency, IInjectDependency
    {
    }
}