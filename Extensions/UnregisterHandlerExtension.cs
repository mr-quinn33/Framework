using Framework.Collections.Interfaces;
using Framework.Interfaces;
using Framework.Scripts;
using UnityEngine;

namespace Framework.Extensions
{
    public static class UnregisterHandlerExtension
    {
        public static void UnregisterOnGameObjectDestroyed(this IUnregisterHandler unregisterHandler, GameObject gameObject)
        {
            if (!gameObject.TryGetComponent(out UnregisterOnDestroy unregisterOnDestroy)) unregisterOnDestroy = gameObject.AddComponent<UnregisterOnDestroy>();
            unregisterOnDestroy.Add(unregisterHandler);
        }

        public static void AddToUnregisterHandlerCollection(this IUnregisterHandler self, IUnregisterHandlerCollection collection)
        {
            collection.UnregisterHandlers.Add(self);
        }
    }
}