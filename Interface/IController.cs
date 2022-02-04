using Framework.Interface.Restriction;

namespace Framework.Interface
{
    public interface IController : IGetSystem, IGetModel, ISendCommand, ISendQuery, IRegisterEvent, IUnregisterEvent
    {
    }
}