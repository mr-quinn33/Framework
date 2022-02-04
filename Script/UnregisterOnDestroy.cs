using System.Collections.Generic;
using Framework.Interface;
using UnityEngine;

namespace Framework.Script
{
    public class UnregisterOnDestroy : MonoBehaviour
    {
        private readonly HashSet<IUnregisterHandler> _unregisterHandlers = new();

        private void OnDestroy()
        {
            foreach (var unregisterHandler in _unregisterHandlers) unregisterHandler.Unregister();

            _unregisterHandlers.Clear();
        }

        public void Add(IUnregisterHandler unregisterHandler)
        {
            _unregisterHandlers.Add(unregisterHandler);
        }

        public void Add(IEnumerable<IUnregisterHandler> unregisterHandlers)
        {
            foreach (var handler in unregisterHandlers) Add(handler);
        }
    }
}