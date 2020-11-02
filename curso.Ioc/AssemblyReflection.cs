using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace curso.Ioc
{
    public static class AssemblyReflection
    {
        public static IEnumerable<Type> GetApplicationClasses()
        {
            return Assembly.Load("curso.application").GetTypes().Where(
                type => type.IsClass
                && !type.IsAbstract
                && type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
        }

        public static IEnumerable<Type> GetRepositoryInterfaces()
        {
            return Assembly.Load("curso.Domain").GetTypes().Where(
                type => type.IsInterface
                && type.Namespace != null
                && type.Namespace.StartsWith("curso.domain.Interfaces.Repository"));
        }

        public static IEnumerable<Type> GetRepositories()
        {
            return Assembly.Load("curso.infra.data").GetTypes().Where(
                type => type.IsClass
                && !type.IsAbstract
                && type.Namespace != null
                && type.Namespace.StartsWith("curso.infra.data")
                && type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
        }

        public static IEnumerable<Type> GetServicesInterfaces()
        {
            return Assembly.Load("curso.service").GetTypes().Where(
                type => type.IsInterface
                && type.Namespace != null
                && type.Namespace.StartsWith("curso.service.Interfaces"));
        }

        public static IEnumerable<Type> GetServices()
        {
            return Assembly.Load("curso.service").GetTypes().Where(
                type => type.IsClass
                && !type.IsAbstract
                && type.Namespace != null
                && type.Namespace.StartsWith("curso.service")
                && type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
        }

        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("curso.application"),
                Assembly.Load("curso.domain"),
                Assembly.Load("curso.service"),
                Assembly.Load("curso.infra.data"),
                Assembly.Load("curso.Ioc")
            };
        }

        public static Type FindType(Type @interface, IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(@interface))
                {
                    return type;
                }
            }

            return null;
        }

        public static Type FindInterface(Type type, IEnumerable<Type> interfaces)
        {
            foreach (var @interface in interfaces)
            {
                if (type.GetInterfaces().Contains(@interface))
                {
                    return @interface;
                }
            }

            return null;
        }
    }
}
