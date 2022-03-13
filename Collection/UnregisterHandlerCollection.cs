using System.Collections.Generic;
using Framework.Interface;

namespace Framework.Collection
{
    public class UnregisterHandlerList : IUnregisterHandlerCollection
    {
        public ICollection<IUnregisterHandler> UnregisterHandlers { get; } = new List<IUnregisterHandler>();
    }
    
    public class UnregisterHandlerHashSet : IUnregisterHandlerCollection
    {
        public ICollection<IUnregisterHandler> UnregisterHandlers { get; } = new HashSet<IUnregisterHandler>();
    }
}