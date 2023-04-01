using System.Collections.Generic;
using Framework.Interfaces;

namespace Framework.Collections
{
    public interface IUnregisterHandlerCollection
    {
        ICollection<IUnregisterHandler> UnregisterHandlers { get; }
    }

    public sealed class UnregisterHandlerList : IUnregisterHandlerCollection
    {
        ICollection<IUnregisterHandler> IUnregisterHandlerCollection.UnregisterHandlers { get; } = new List<IUnregisterHandler>();
    }

    public sealed class UnregisterHandlerHashSet : IUnregisterHandlerCollection
    {
        ICollection<IUnregisterHandler> IUnregisterHandlerCollection.UnregisterHandlers { get; } = new HashSet<IUnregisterHandler>();
    }
}