using Framework.Interface.Restriction;

namespace Framework.Interface
{
    public interface IController : IGetSystem, IGetModel, ISendCommand, ISendCommandAsync, ISendQuery, IRegisterEvent, IUnregisterEvent
    {
    }
}