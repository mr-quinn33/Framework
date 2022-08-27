using Framework.Rules.Interfaces;

namespace Framework.Components.Interfaces
{
    public interface IView : IComponent, ISendQuery, IRegisterEvent, IUnregisterEvent
    {
    }
}