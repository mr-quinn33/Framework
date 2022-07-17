using System;
using System.Collections.Generic;
using Framework.GameModes.Interfaces;

namespace Framework.GameModes
{
    public abstract class GameModeBase
    {
        private protected static readonly IDictionary<Type, IGameMode> Instances = new Dictionary<Type, IGameMode>();
    }
}