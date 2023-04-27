using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Runtime.Loader;

namespace My.Common;

public static class ServiceExtension
{
    public static void MyServiceBuild(this IServiceCollection services)
    {
        //获取程序集所有项目
        Queue<Assembly> queusAssemblies = new Queue<Assembly>();

        // 模式一
        {
            //Assembly? rootAssembly = Assembly.GetEntryAssembly();
            //if (rootAssembly == null)
            //{
            //    rootAssembly = Assembly.GetCallingAssembly();
            //}

            //queusAssemblies.Enqueue(rootAssembly);

            //var assemblies = rootAssembly.GetReferencedAssemblies().Where(p => !p.IsSystemAssembly()).ToList();
            //if (assemblies == null || assemblies.Count == 0) return;
            //foreach (var assemblyName in assemblies)
            //{
            //    if (!string.IsNullOrWhiteSpace(assemblyName.Name))
            //    {
            //        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName.Name));
            //        if (queusAssemblies.Contains(assembly)) continue;
            //        queusAssemblies.Enqueue(assembly);

            //        LoadReferenceAssemblies(ref queusAssemblies, assembly);
            //    }
            //}
            //while (queusAssemblies.Any())
            //{
            //    var assembly = queusAssemblies.Dequeue();
            //    var type = assembly.GetTypes().Where(p => p.IsSubclassOf(typeof(MyModule))).FirstOrDefault();
            //    if (type == null) continue;
            //    MyModule? module = Activator.CreateInstance(type) as MyModule;
            //    module?.OnCongigurationService(services);
            //}

            //static void LoadReferenceAssemblies(ref Queue<Assembly> queues, Assembly assembly)
            //{
            //    List<AssemblyName> references = assembly.GetReferencedAssemblies().Where(p => !p.IsSystemAssembly()).ToList();
            //    if (references == null || references.Count == 0) return;
            //    foreach (var item in references)
            //    {
            //        if (!string.IsNullOrWhiteSpace(item.Name))
            //        {
            //            var assemblySon = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(item.Name));
            //            if (queues.Contains(assemblySon)) continue;
            //            queues.Enqueue(assemblySon);

            //            LoadReferenceAssemblies(ref queues, assemblySon);
            //        }
            //    }
            //}
        }

        // 模式二
        {
            var dependency = DependencyContext.Default;
            var libraries = dependency?.CompileLibraries.Where(p => !p.Serviceable && p.Type != "Package").ToList();
            if (libraries == null) return;
            foreach (CompilationLibrary? lib in libraries)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                if (!queusAssemblies.Contains(assembly))
                {
                    queusAssemblies.Enqueue(assembly);
                }
            }
            while (queusAssemblies.Any())
            {
                var assembly = queusAssemblies.Dequeue();

                Action inject = () =>
                {
                    var type = assembly.GetTypes().Where(p => p.IsSubclassOf(typeof(MyModule))).FirstOrDefault();
                    if (type != null)
                    {
                        MyModule? module = Activator.CreateInstance(type) as MyModule;
                        module?.OnCongigurationService(services);
                    }
                };

                Task.Factory.StartNew(inject);
            }
        }
    }

    private static bool IsSystemAssembly(this Assembly assembly)
    {
        var name = assembly.GetName().Name;
        if (string.IsNullOrEmpty(name)) return false;
        return name.StartsWith("System") || name.StartsWith("Microsoft");
    }

    private static bool IsSystemAssembly(this AssemblyName assembly)
    {
        var name = assembly.Name;
        if (string.IsNullOrEmpty(name)) return false;
        if (!name.StartsWith("My.")) return true;
        return name.StartsWith("System") || name.StartsWith("Microsoft");
    }
}
