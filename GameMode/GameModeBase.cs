using System;
using System.Collections.Generic;
using Framework.Interface;

namespace Framework.GameMode
{
    public abstract class GameModeBase
    {
        private protected static readonly IDictionary<Type, IArchitecture> Instances = new Dictionary<Type, IArchitecture>();
    }
}