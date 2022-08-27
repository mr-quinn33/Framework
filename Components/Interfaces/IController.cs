using Framework.Rules.Interfaces;

namespace Framework.Components.Interfaces
{
    public interface IController : IComponent, ISendCommand, ISendCommandAsync
    {
    }
}