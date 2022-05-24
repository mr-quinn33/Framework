using Framework.Interface.Access;

namespace Framework.Extension
{
    public static class InvokeEventExtension
    {
        public static void InvokeEvent<T>(this IInvokeEvent self, T t)
        {
            self.GetArchitecture().InvokeEvent(t);
        }

        public static void InvokeEvent<T>(this IInvokeEvent self) where T : new()
        {
            self.GetArchitecture().InvokeEvent<T>();
        }

        public static TResult InvokeEvent<T, TResult>(this IInvokeEvent self, T t)
        {
            return self.GetArchitecture().InvokeEvent<T, TResult>(t);
        }

        public static TResult InvokeEvent<T, TResult>(this IInvokeEvent self) where T : new()
        {
            return self.GetArchitecture().InvokeEvent<T, TResult>();
        }
    }
}