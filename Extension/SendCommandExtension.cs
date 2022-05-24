using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.Extension
{
    public static class SendCommandExtension
    {
        public static void SendCommand(this ISendCommand self, ICommand command)
        {
            self.GetArchitecture().SendCommand(command);
        }

        public static void SendCommand<T>(this ISendCommand self) where T : ICommand, new()
        {
            self.GetArchitecture().SendCommand<T>();
        }
    }
}