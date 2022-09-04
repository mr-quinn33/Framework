using Framework.Rules;

namespace Framework.Components
{
    public interface IController : IComponent, ISendCommand, ISendCommandAsync
    {
    }
}