using UnityEngine;

namespace Framework.Scripts.MonoSingletons
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = FindObjectOfType<T>();
                if (instance != null) return instance;
                var gameObject = new GameObject(typeof(T).Name);
                instance = gameObject.AddComponent<T>();
                return instance;
            }
        }

        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }
}