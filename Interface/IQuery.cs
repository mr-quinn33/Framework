using Framework.Interface.Restriction;

namespace Framework.Interface
{
    public interface IQuery<out TResult> : ISetArchitecture, IGetSystem, IGetModel, ISendQuery
    {
        TResult Execute();
    }
}