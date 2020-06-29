
using System;

namespace core.InjectionDependency
{

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RequestScoped : LifeTimeAttribute
    {
        public override Type Interface { get; set; }
    }
}