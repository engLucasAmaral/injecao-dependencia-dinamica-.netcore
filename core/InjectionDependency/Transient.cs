
using System;

namespace core.InjectionDependency
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class Transient : LifeTimeAttribute
    {
        public override Type Interface { get; set; }
    }
}