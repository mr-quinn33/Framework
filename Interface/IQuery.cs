using Framework.Interface.Access;

namespace Framework.Interface
{
    public interface IQuery<out TResult> : ISetArchitecture, IGetSystem, IGetModel, ISendQuery
    {
        TResult Execute();
    }
}