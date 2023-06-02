using Framework.Collections;
using Framework.Interfaces;
using Framework.Scripts;
using UnityEngine;

namespace Framework.Extensions
{
    public static class UnregisterHandlerCollectionExtension
    {
        public static void UnregisterOnGameObjectDestroyed(this IUnregisterHandlerCollection self, GameObject gameObject)
        {
            if (!gameObject.TryGetComponent(out IUnregisterOnDestroy unregisterOnDestroy)) unregisterOnDestroy = gameObject.AddComponent<UnregisterOnDestroy>();
            unregisterOnDestroy.Add(self);
        }

        public static void UnregisterAll(this IUnregisterHandlerCollection self)
        {
            foreach (IUnregisterHandler unregisterHandler in self.UnregisterHandlers) unregisterHandler.Unregister();
            self.UnregisterHandlers.Clear();
        }
    }
}