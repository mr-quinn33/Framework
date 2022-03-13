using Framework.Interface;

namespace Framework.Extension
{
    public static class AddToUnregisterHandlerCollectionExtension
    {
        public static void AddToUnregisterHandlerCollection(this IUnregisterHandler self,
            IUnregisterHandlerCollection collection)
        {
            collection.UnregisterHandlers.Add(self);
        }
    }
}