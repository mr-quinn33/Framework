using System;
using System.Reflection;
using UnityEngine;

namespace Framework.Scripts.Managers
{
    public abstract class SingletonManager<T> : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Initialize(typeof(T));
        }

        protected void Initialize(Type type)
        {
            if (type.GetProperty("Instance", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)?.GetValue(null) is Component component) component.transform.SetParent(transform);
        }
    }

    public abstract class SingletonManager<T1, T2> : SingletonManager<T1>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T2));
        }
    }

    public abstract class SingletonManager<T1, T2, T3> : SingletonManager<T1, T2>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T3));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4> : SingletonManager<T1, T2, T3>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T4));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5> : SingletonManager<T1, T2, T3, T4>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T5));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6> : SingletonManager<T1, T2, T3, T4, T5>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T6));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7> : SingletonManager<T1, T2, T3, T4, T5, T6>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T7));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8> : SingletonManager<T1, T2, T3, T4, T5, T6, T7>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T8));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T9));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T10));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T11));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T12));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T13));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T14));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T15));
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize(typeof(T16));
        }
    }
}