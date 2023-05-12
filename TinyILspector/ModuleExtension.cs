using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmResolver.DotNet;

namespace TinyILspector
{
    internal static class ModuleExtension
    {
        internal static IEnumerable<MethodDefinition> GetAllMethods(this ModuleDefinition module)
        {
            return module.GetAllTypes()
                .Where(p => p.IsClass)
                .SelectMany(t => t.Methods);
        }
    }
}
