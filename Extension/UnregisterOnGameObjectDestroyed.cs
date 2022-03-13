using Framework.Interface;
using Framework.Script;
using UnityEngine;

namespace Framework.Extension
{
    public static class UnregisterHandlerExtension
    {
        public static void UnregisterOnGameObjectDestroyed(this IUnregisterHandler unregisterHandler,
            GameObject gameObject)
        {
            if (!gameObject.TryGetComponent(out UnregisterOnDestroy unregisterOnDestroy))
                unregisterOnDestroy = gameObject.AddComponent<UnregisterOnDestroy>();
            unregisterOnDestroy.Add(unregisterHandler);
        }
    }
}