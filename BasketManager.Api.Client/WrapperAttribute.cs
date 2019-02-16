using Microsoft.Extensions.DependencyInjection;
using System;

namespace BasketManager.Api.Client
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WrapperAttribute : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; set; }

        public Type Interface { get; set; }

        public WrapperAttribute(Type classInterface) : this(classInterface, ServiceLifetime.Scoped) { }

        public WrapperAttribute(Type classInterface, ServiceLifetime serviceLifetime)
        {
            Interface = classInterface;
            ServiceLifetime = serviceLifetime;
        }
    }
}
