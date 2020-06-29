
using System;

namespace core.InjectionDependency
{
    public abstract class LifeTimeAttribute : Attribute
    {
        public abstract Type Interface { get; set; }
    }
}