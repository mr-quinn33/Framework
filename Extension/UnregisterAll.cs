using Framework.Interface;

namespace Framework.Extension
{
    public static class UnregisterAllExtension
    {
        public static void UnregisterAll(this IUnregisterHandlerCollection self)
        {
            foreach (var unregisterHandler in self.UnregisterHandlers)
            {
                unregisterHandler.Unregister();
            }
            
            self.UnregisterHandlers.Clear();
        }
    }
}