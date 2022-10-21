using Framework.Commands;

namespace Framework.Tools.Delegators
{
    public interface IDelegateSendCommand
    {
        void SendCommand<T>(T command) where T : ICommand;

        void SendCommand<T>() where T : ICommand, new();
    }
}