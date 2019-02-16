using System;
using System.Collections.Generic;
using System.Reflection;

namespace Configuration.Helpers
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Returns types marked with TAttribute within its assembly
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes<TAttribute>()
            where TAttribute : Attribute
        {
            return GetTypes<TAttribute>(Assembly.GetAssembly(typeof(TAttribute)));
        }

        /// <summary>
        /// Returns types marked with TAttribute within a given assembly
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes<TAttribute>(this Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsDefined(typeof(TAttribute), true))
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// Returns types marked with TAttribute and TAttribute value within its assembly
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Tuple<Type, TAttribute>> GetTypesAndAttributes<TAttribute>()
            where TAttribute : Attribute
        {
            return GetTypesAndAttributes<TAttribute>(Assembly.GetAssembly(typeof(TAttribute)));
        }

        /// <summary>
        /// Returns types marked with TAttribute and TAttribute value within a given assembly
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<Type, TAttribute>> GetTypesAndAttributes<TAttribute>(this Assembly assembly)
            where TAttribute : Attribute
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsDefined(typeof(TAttribute), true))
                {
                    yield return Tuple.Create(type, type.GetCustomAttribute<TAttribute>());
                }
            }
        }
    }
}
