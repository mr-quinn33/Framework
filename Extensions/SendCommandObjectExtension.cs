using Framework.CommandObjects.Interfaces;
using Framework.Rules.Interfaces;

namespace Framework.Extensions
{
    public static class SendCommandObjectExtension
    {
        public static void SendCommandObject(this ISendCommandObject self, ICommandObject commandObject)
        {
            self.GetGameMode().SendCommandObject(commandObject);
        }
    }
}