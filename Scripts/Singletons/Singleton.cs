using System;

namespace Framework.Scripts.Singletons
{
    public abstract class Singleton<T> : IDisposable where T : Singleton<T>, new()
    {
        private static T instance;

        private static readonly object Lock = new();

        public static T Instance
        {
            get
            {
                lock (Lock)
                {
                    instance ??= new T();
                }

                return instance;
            }
        }

        public void Dispose()
        {
            instance = null;
        }

        ~Singleton()
        {
            Dispose();
        }
    }
}