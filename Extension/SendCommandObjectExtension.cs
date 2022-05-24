using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.Extension
{
    public static class SendCommandObjectExtension
    {
        public static void SendCommandObject(this ISendCommandObject self, ICommandObject commandObject)
        {
            self.GetArchitecture().SendCommandObject(commandObject);
        }
    }
}