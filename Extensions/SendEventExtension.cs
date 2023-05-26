using Framework.Rules;

namespace Framework.Extensions
{
    public static class SendEventExtension
    {
        public static void SendEvent<T>(this ISendEvent self, T t)
        {
            self.GetGameMode().SendEvent(t);
        }

        public static void SendEvent<T>(this ISendEvent self) where T : new()
        {
            self.GetGameMode().SendEvent<T>();
        }
    }
}