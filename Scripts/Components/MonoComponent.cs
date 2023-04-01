using Framework.Components;
using Framework.GameModes;
using UnityEngine;

namespace Framework.Scripts.Components
{
    public abstract class MonoComponent<T> : MonoBehaviour, IComponent<T> where T : GameMode<T>, new()
    {
    }
}