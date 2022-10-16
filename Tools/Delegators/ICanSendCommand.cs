using Framework.Commands;

namespace Framework.Tools.Delegators
{
    public interface ICanSendCommand
    {
        void SendCommand<T>(T command) where T : ICommand;

        void SendCommand<T>() where T : ICommand, new();
    }
}