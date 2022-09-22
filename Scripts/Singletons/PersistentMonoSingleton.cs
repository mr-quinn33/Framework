using UnityEngine;

namespace Framework.Scripts.Singletons
{
    public abstract class PersistentMonoSingleton<T> : MonoBehaviour where T : PersistentMonoSingleton<T>
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

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T) this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this) Destroy(gameObject);
            }
        }
    }
}