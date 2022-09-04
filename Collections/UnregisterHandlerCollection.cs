using System.Collections.Generic;
using Framework.Interfaces;

namespace Framework.Collections
{
    public interface IUnregisterHandlerCollection
    {
        ICollection<IUnregisterHandler> UnregisterHandlers { get; }
    }

    public class UnregisterHandlerList : IUnregisterHandlerCollection
    {
        ICollection<IUnregisterHandler> IUnregisterHandlerCollection.UnregisterHandlers { get; } = new List<IUnregisterHandler>();
    }

    public class UnregisterHandlerHashSet : IUnregisterHandlerCollection
    {
        ICollection<IUnregisterHandler> IUnregisterHandlerCollection.UnregisterHandlers { get; } = new HashSet<IUnregisterHandler>();
    }
}