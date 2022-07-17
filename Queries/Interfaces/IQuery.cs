using Framework.Rules.Interfaces;

namespace Framework.Queries.Interfaces
{
    public interface IQuery<out TResult> : ISetGameMode, IGetSystem, IGetModel, ISendQuery
    {
        TResult Execute();
    }
}