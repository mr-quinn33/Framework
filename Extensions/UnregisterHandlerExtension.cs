using Framework.Collections;
using Framework.Interfaces;
using Framework.Scripts;
using UnityEngine;

namespace Framework.Extensions
{
    public static class UnregisterHandlerExtension
    {
        public static void UnregisterOnGameObjectDestroyed(this IUnregisterHandler self, GameObject gameObject)
        {
            if (!gameObject.TryGetComponent(out IUnregisterOnDestroy unregisterOnDestroy)) unregisterOnDestroy = gameObject.AddComponent<UnregisterOnDestroy>();
            unregisterOnDestroy.Add(self);
        }

        public static void AddToUnregisterHandlerCollection(this IUnregisterHandler self, IUnregisterHandlerCollection collection)
        {
            collection.UnregisterHandlers.Add(self);
        }
    }
}