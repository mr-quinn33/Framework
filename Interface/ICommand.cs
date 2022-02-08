using Framework.Interface.Restriction;

namespace Framework.Interface
{
    public interface ICommand : ISetArchitecture, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        void Execute();
    }
}