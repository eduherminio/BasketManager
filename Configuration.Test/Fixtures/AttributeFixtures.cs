using Microsoft.Extensions.DependencyInjection;
using System;

namespace Configuration.Test.Fixtures
{
    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = true)]
    internal class FooAttribute : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; set; }

        public Type Interface { get; set; }

        public FooAttribute(Type classInterface, ServiceLifetime serviceLifetime)
        {
            Interface = classInterface;
            ServiceLifetime = serviceLifetime;
        }
    }

    internal interface IScopedBar { }

    [Foo(typeof(IScopedBar), ServiceLifetime.Scoped)]
    internal class ScopedBar : IScopedBar { }

    internal interface ITransientBar { }

    [Foo(typeof(ITransientBar), ServiceLifetime.Transient)]
    internal class TransientBar : ITransientBar { }

    internal interface ISingletonBar { }

    [Foo(typeof(ISingletonBar), ServiceLifetime.Singleton)]
    internal class SingletonBar : ISingletonBar { }
}
