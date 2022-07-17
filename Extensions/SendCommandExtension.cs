using Framework.Commands.Interfaces;
using Framework.Rules.Interfaces;

namespace Framework.Extensions
{
    public static class SendCommandExtension
    {
        public static void SendCommand(this ISendCommand self, ICommand command)
        {
            self.GetGameMode().SendCommand(command);
        }

        public static void SendCommand<T>(this ISendCommand self) where T : ICommand, new()
        {
            self.GetGameMode().SendCommand<T>();
        }
    }
}