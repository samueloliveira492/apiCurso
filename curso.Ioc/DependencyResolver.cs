using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace curso.Ioc
{
    public static class DependencyResolver
    {
        public static void AddResolverDependencies(this IServiceCollection services)
        {
            var servicosInterface = AssemblyReflection.GetServicesInterfaces();
            var servicos = AssemblyReflection.GetServices();
            foreach (var servico in servicos)
            {
                var @interface = AssemblyReflection.FindInterface(servico, servicosInterface);

                if (@interface != null)
                    services.AddScoped(@interface, servico);
            }

            var domainInterfaces = AssemblyReflection.GetRepositoryInterfaces();
            var repositories = AssemblyReflection.GetRepositories();

            foreach (var repo in repositories)
            {
                var @interface = AssemblyReflection.FindInterface(repo, domainInterfaces);

                if (@interface != null)
                    services.AddScoped(@interface, repo);
            }
        }
    }
}
