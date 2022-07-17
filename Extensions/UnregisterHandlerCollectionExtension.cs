using Framework.Collections.Interfaces;

namespace Framework.Extensions
{
    public static class UnregisterHandlerCollectionExtension
    {
        public static void UnregisterAll(this IUnregisterHandlerCollection self)
        {
            foreach (var unregisterHandler in self.UnregisterHandlers) unregisterHandler.Unregister();
            self.UnregisterHandlers.Clear();
        }
    }
}