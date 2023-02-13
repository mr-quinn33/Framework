using System;
using System.Reflection;
using UnityEngine;

namespace Framework.Scripts.Managers
{
    public abstract class SingletonManager<T1> : MonoBehaviour
    {
        protected T1 t1;

        protected virtual void Awake()
        {
            if (TryGetInstance(typeof(T1), out var instance)) t1 = (T1) instance;
        }

        protected bool TryGetInstance(Type type, out object instance)
        {
            var propertyInfo = type.GetProperty("Instance", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static);
            if (propertyInfo != null)
            {
                instance = propertyInfo.GetValue(null);
                return true;
            }

            instance = null;
            return false;
        }
    }

    public abstract class SingletonManager<T1, T2> : SingletonManager<T1>
    {
        protected T2 t2;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T2), out var instance)) t2 = (T2) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3> : SingletonManager<T1, T2>
    {
        protected T3 t3;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T3), out var instance)) t3 = (T3) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4> : SingletonManager<T1, T2, T3>
    {
        protected T4 t4;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T4), out var instance)) t4 = (T4) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5> : SingletonManager<T1, T2, T3, T4>
    {
        protected T5 t5;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T5), out var instance)) t5 = (T5) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6> : SingletonManager<T1, T2, T3, T4, T5>
    {
        protected T6 t6;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T6), out var instance)) t6 = (T6) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7> : SingletonManager<T1, T2, T3, T4, T5, T6>
    {
        protected T7 t7;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T7), out var instance)) t7 = (T7) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8> : SingletonManager<T1, T2, T3, T4, T5, T6, T7>
    {
        protected T8 t8;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T8), out var instance)) t8 = (T8) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        protected T9 t9;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T9), out var instance)) t9 = (T9) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        protected T10 t10;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T10), out var instance)) t10 = (T10) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        protected T11 t11;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T11), out var instance)) t11 = (T11) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        protected T12 t12;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T12), out var instance)) t12 = (T12) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        protected T13 t13;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T13), out var instance)) t13 = (T13) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        protected T14 t14;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T14), out var instance)) t14 = (T14) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        protected T15 t15;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T15), out var instance)) t15 = (T15) instance;
        }
    }

    public abstract class SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : SingletonManager<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        protected T16 t16;

        protected override void Awake()
        {
            base.Awake();
            if (TryGetInstance(typeof(T16), out var instance)) t16 = (T16) instance;
        }
    }
}