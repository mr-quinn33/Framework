using System.Collections.Generic;
using Framework.Interfaces;

namespace Framework.Collections.Interfaces
{
    public interface IUnregisterHandlerCollection
    {
        ICollection<IUnregisterHandler> UnregisterHandlers { get; }
    }
}