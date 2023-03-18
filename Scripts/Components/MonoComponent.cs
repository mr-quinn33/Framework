using System.Reflection;
using Framework.Components;
using Framework.GameModes;
using Framework.Rules;
using UnityEngine;

namespace Framework.Scripts.Components
{
    public abstract class MonoComponent<T> : MonoBehaviour, IComponent where T : GameMode<T>, new()
    {
        IGameMode IGetGameMode.GetGameMode()
        {
            return GameMode<T>.Load();
        }
    }
}