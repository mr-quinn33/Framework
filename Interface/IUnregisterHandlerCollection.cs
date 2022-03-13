using System.Collections.Generic;

namespace Framework.Interface
{
    public interface IUnregisterHandlerCollection
    {
        ICollection<IUnregisterHandler> UnregisterHandlers { get; }
    }
}