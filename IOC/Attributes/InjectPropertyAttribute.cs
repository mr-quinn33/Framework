using System;

namespace Framework.IOC.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class InjectPropertyAttribute : Attribute
    {
    }
}