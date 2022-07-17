using System.Collections.Generic;
using Framework.Collections.Interfaces;
using Framework.Interfaces;

namespace Framework.Collections
{
    public class UnregisterHandlerList : IUnregisterHandlerCollection
    {
        ICollection<IUnregisterHandler> IUnregisterHandlerCollection.UnregisterHandlers { get; } = new List<IUnregisterHandler>();
    }

    public class UnregisterHandlerHashSet : IUnregisterHandlerCollection
    {
        ICollection<IUnregisterHandler> IUnregisterHandlerCollection.UnregisterHandlers { get; } = new HashSet<IUnregisterHandler>();
    }
}