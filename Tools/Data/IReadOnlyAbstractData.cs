using System;

namespace Framework.Tools.Data
{
    public interface IReadOnlyAbstractData<out T> where T : struct, IEquatable<T>, IComparable, IComparable<T>
    {
        T Value { get; }

        T MaxValue { get; }

        T DefaultValue { get; }
    }
}