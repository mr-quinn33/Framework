using Framework.Components;
using Framework.GameModes;
using Framework.Rules;
using UnityEngine;

namespace Framework.Scripts.Components
{
    public abstract class MonoComponent<T> : MonoBehaviour, IComponent where T : GameMode<T>, new()
    {
        IGameMode IGetGameMode.GetGameMode() => GameMode<T>.Load();
    }
}