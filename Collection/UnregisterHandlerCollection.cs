using System.Collections.Generic;
using Framework.Interface;

namespace Framework.Collection
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