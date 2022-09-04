using Framework.Rules;

namespace Framework.Components
{
    public interface IComponent : IRegisterDependency, IResolveDependency, IInjectDependency
    {
    }
}