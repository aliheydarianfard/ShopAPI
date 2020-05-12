using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Sell.Core.Extension
{
    public static class CommonExtension
    {
        public static List<string> GetAllClassNames(this Type type)
        {
            var _lista = new List<Assembly>();
            foreach (string dllPath in Directory.GetFiles(System.AppContext.BaseDirectory, "Sell.*.dll"))
            {
                var shadowCopiedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
                _lista.Add(shadowCopiedAssembly);
            }
            return _lista.SelectMany(x => x.GetTypes())
                 .Where(x => type.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .Select(x => x.FullName).ToList();
        }

        public static List<Type> GetAllClassTypes(this Type type)
        {
            var _lista = new List<Assembly>();
            foreach (string dllPath in Directory.GetFiles(System.AppContext.BaseDirectory, "Sell.*.dll"))
            {
                var shadowCopiedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
                _lista.Add(shadowCopiedAssembly);
            }

            return _lista.SelectMany(x => x.GetTypes())
                 .Where(x => type.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .ToList();
        }
    }
}
