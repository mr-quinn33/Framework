using System;
using System.Collections.Generic;
using Framework.Interface;

namespace Framework.GameMode
{
    public abstract class GameModeBase
    {
        private protected static readonly Dictionary<Type, IArchitecture> Instances = new();
    }
}