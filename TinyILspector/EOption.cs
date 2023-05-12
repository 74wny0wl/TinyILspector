using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyILspector
{
    [Flags]
    internal enum EOption
    {
        None = 0,
        MetadataToken = 1,
        FunctionName = 2,
        Ambiguous = 4,
    }
}
