using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Configuration.Helpers;
using Configuration.Test.Fixtures;

namespace Configuration.Test
{
    public class AssemblyExtensionsTest
    {
        [Fact]
        public void GetTypesFromDefaultAssembly()
        {
            IEnumerable<Type> currentTypes = Helpers.AssemblyExtensions.GetTypes<FooAttribute>();
            ValidateTypes(currentTypes);
        }

        [Fact]
        public void GetTypesFromSpecificAssembly()
        {
            IEnumerable<Type> currentTypes = Assembly.GetAssembly(typeof(IScopedBar)).GetTypes<FooAttribute>();
            ValidateTypes(currentTypes);
        }

        [Fact]
        public void GetTypesAndAttributesFromDefaultAssembly()
        {
            IEnumerable<Tuple<Type, FooAttribute>> typesAndAttributes =
                Helpers.AssemblyExtensions.GetTypesAndAttributes<FooAttribute>();

            ValidateTypesAndAttributes(typesAndAttributes);
        }

        [Fact]
        public void GetTypesAndAttributesFromSpecificAssembly()
        {
            IEnumerable<Tuple<Type, FooAttribute>> typesAndAttributes =
                Assembly.GetAssembly(typeof(ITransientBar)).GetTypesAndAttributes<FooAttribute>();

            ValidateTypesAndAttributes(typesAndAttributes);
        }

        private static void ValidateTypes(IEnumerable<Type> currentTypes)
        {
            IEnumerable<Type> expectedTypes = new List<Type>()
            {
               typeof(ScopedBar),
               typeof(TransientBar),
               typeof(SingletonBar)
            };

            Assert.Subset(currentTypes.ToHashSet(), expectedTypes.ToHashSet());
        }

        private static void ValidateTypesAndAttributes(IEnumerable<Tuple<Type, FooAttribute>> tuples)
        {
            var scopedTuple = tuples.SingleOrDefault(tuple => tuple.Item1 == typeof(ScopedBar));
            Assert.NotNull(scopedTuple);
            Assert.Equal(nameof(ServiceLifetime.Scoped), scopedTuple.Item2.ServiceLifetime.ToString());

            var singletonTuple = tuples.SingleOrDefault(tuple => tuple.Item1 == typeof(SingletonBar));
            Assert.NotNull(singletonTuple);
            Assert.Equal(nameof(ServiceLifetime.Singleton), singletonTuple.Item2.ServiceLifetime.ToString());

            var transientTuple = tuples.SingleOrDefault(tuple => tuple.Item1 == typeof(TransientBar));
            Assert.NotNull(transientTuple);
            Assert.Equal(nameof(ServiceLifetime.Transient), transientTuple.Item2.ServiceLifetime.ToString());
        }
    }
}
