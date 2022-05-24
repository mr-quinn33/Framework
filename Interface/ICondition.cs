using Framework.Interface.Access;

namespace Framework.Interface
{
    public interface ICondition : ISetArchitecture, ISendQuery
    {
        bool IsValid { get; }
    }
}