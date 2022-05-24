using System.Collections.Generic;
using Framework.Interface;
using UnityEngine;

namespace Framework.Script
{
    public class UnregisterOnDestroy : MonoBehaviour
    {
        private readonly ICollection<IUnregisterHandler> unregisterHandlers = new HashSet<IUnregisterHandler>();

        private void OnDestroy()
        {
            foreach (var unregisterHandler in unregisterHandlers) unregisterHandler.Unregister();
            unregisterHandlers.Clear();
        }

        public void Add(IUnregisterHandler unregisterHandler)
        {
            unregisterHandlers.Add(unregisterHandler);
        }
    }
}