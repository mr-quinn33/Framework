#if UNITY_NETCODE_GAMEOBJECTS
using Unity.Netcode;
#endif
using UnityEngine;

namespace Framework.Scripts.Netcode.GameObjects
{
#if UNITY_NETCODE_GAMEOBJECTS
    public abstract class NetworkSingleton<T> : NetworkBehaviour where T : NetworkSingleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = FindObjectOfType<T>();
                if (instance != null) return instance;
                var gameObject = new GameObject(typeof(T).Name, typeof(NetworkObject));
                instance = gameObject.AddComponent<T>();
                return instance;
            }
        }

        public override void OnNetworkDespawn()
        {
            instance = null;
        }
    }
#endif
}