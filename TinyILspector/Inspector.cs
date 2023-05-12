using AsmResolver.PE.DotNet.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmResolver.DotNet;

namespace TinyILspector
{
    internal class Inspector
    {
        internal void Inspect(FileInfo fileInfo)
        {
            var module = ModuleDefinition.FromFile(fileInfo.FullName);
            Inspect(module.GetAllMethods());
        }


        internal void Inspect(FileInfo fileInfo, MetadataToken metadataToken)
        {
            if (metadataToken == MetadataToken.Zero)
            {
                Console.Error.WriteLine($"Metadata token cannot be zero");
                return;
            }

            var module = ModuleDefinition.FromFile(fileInfo.FullName);

            var method = module.GetAllMethods().SingleOrDefault(method => method.MetadataToken == metadataToken);
            if (method != null)
            {
                Inspect(method);
            }
            else
            {
                Console.Error.WriteLine($"There is no method with {metadataToken} metadata token");
            }
        }

        internal void Inspect(FileInfo fileInfo, string functionName)
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                Console.Error.WriteLine($"Function name cannot be empty or white space");
                return;
            }

            var module = ModuleDefinition.FromFile(fileInfo.FullName);

            var method = module.GetAllMethods().SingleOrDefault(method => method.Name == functionName);
            if (method != null)
            {
                Inspect(method);
            }
            else
            {
                Console.Error.WriteLine($"There is no method with {functionName} name");
            }
        }

        private void Inspect(MethodDefinition methodDefinitions)
        {
            methodDefinitions.Print();
        }

        private void Inspect(IEnumerable<MethodDefinition> methodDefinitions)
        {
            foreach (var method in methodDefinitions)
            {
                Inspect(method);
            }
        }
    }
}
