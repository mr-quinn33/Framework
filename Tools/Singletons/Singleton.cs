using System;

namespace Framework.Tools.Singletons
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static volatile T instance;
        private static readonly object instanceLock = new();

        public static T Instance
        {
            get
            {
                T localInstance = instance;
                if (localInstance == null)
                {
                    lock (instanceLock)
                    {
                        localInstance = instance;
                        if (localInstance == null)
                        {
                            instance = localInstance = (T) Activator.CreateInstance(typeof(T), true);
                        }
                    }
                }

                return localInstance;
            }
        }
    }
}