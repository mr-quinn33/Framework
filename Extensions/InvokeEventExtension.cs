using Framework.Rules.Interfaces;

namespace Framework.Extensions
{
    public static class InvokeEventExtension
    {
        public static void InvokeEvent<T>(this IInvokeEvent self, T t)
        {
            self.GetGameMode().InvokeEvent(t);
        }

        public static void InvokeEvent<T>(this IInvokeEvent self) where T : new()
        {
            self.GetGameMode().InvokeEvent<T>();
        }

        public static TResult InvokeEvent<T, TResult>(this IInvokeEvent self, T t)
        {
            return self.GetGameMode().InvokeEvent<T, TResult>(t);
        }

        public static TResult InvokeEvent<T, TResult>(this IInvokeEvent self) where T : new()
        {
            return self.GetGameMode().InvokeEvent<T, TResult>();
        }
    }
}