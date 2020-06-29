
using System;

namespace core.InjectionDependency
{

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class Singleton : LifeTimeAttribute
    {
        public override Type Interface { get; set; }
    }

}