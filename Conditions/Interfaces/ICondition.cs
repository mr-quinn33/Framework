using Framework.Rules.Interfaces;

namespace Framework.Conditions.Interfaces
{
    public interface ICondition : ISetGameMode, ISendQuery
    {
        bool IsValid { get; }
    }
}