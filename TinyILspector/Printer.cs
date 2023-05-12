using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmResolver.DotNet;

namespace TinyILspector
{
    internal static class Printer
    {
        public static bool Print(this MethodDefinition md)
        {
            var canBeParsed = true;
            try
            {
                md.CilMethodBody.ComputeMaxStack();
                Console.WriteLine("###################");
                Console.WriteLine($"{md.FullName}@{md.MetadataToken}");
                foreach (var instruction in md.CilMethodBody.Instructions)
                {
                    Console.WriteLine(instruction);
                }
            }
            catch (Exception)
            {
                canBeParsed = false;
            }
            return canBeParsed;
        }
    }
}
