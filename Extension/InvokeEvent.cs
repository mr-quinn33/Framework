using Framework.Interface.Restriction;

namespace Framework.Extension
{
    public static class InvokeEventExtension
    {
        public static void InvokeEvent<T>(this IInvokeEvent self) where T : new()
        {
            self.GetArchitecture().InvokeEvent<T>();
        }

        public static void InvokeEvent<T, TResult>(this IInvokeEvent self, out TResult result) where T : new()
        {
            self.GetArchitecture().InvokeEvent<T, TResult>(out result);
        }

        public static TResult InvokeEvent<T, TResult>(this IInvokeEvent self) where T : new()
        {
            return self.GetArchitecture().InvokeEvent<T, TResult>();
        }

        public static void InvokeEvent<T>(this IInvokeEvent self, T t)
        {
            self.GetArchitecture().InvokeEvent(t);
        }

        public static void InvokeEvent<T, TResult>(this IInvokeEvent self, T t, out TResult result)
        {
            self.GetArchitecture().InvokeEvent(t, out result);
        }

        public static TResult InvokeEvent<T, TResult>(this IInvokeEvent self, T t)
        {
            return self.GetArchitecture().InvokeEvent<T, TResult>(t);
        }
    }
}