using Framework.Rules;

namespace Framework.Components
{
    public interface IView : IComponent, ISendQuery, IRegisterEvent, IUnregisterEvent
    {
    }
}