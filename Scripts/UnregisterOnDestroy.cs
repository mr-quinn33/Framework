using System.Collections.Generic;
using Framework.Collections;
using Framework.Interfaces;
using UnityEngine;

namespace Framework.Scripts
{
    public interface IUnregisterOnDestroy
    {
        void Add(IUnregisterHandler unregisterHandler);

        void Add(IUnregisterHandlerCollection unregisterHandlerCollection);
    }

    public class UnregisterOnDestroy : MonoBehaviour, IUnregisterOnDestroy
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

        public void Add(IUnregisterHandlerCollection unregisterHandlerCollection)
        {
            foreach (var unregisterHandler in unregisterHandlerCollection.UnregisterHandlers) Add(unregisterHandler);
        }
    }
}