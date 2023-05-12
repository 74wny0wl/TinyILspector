using AsmResolver.PE.DotNet.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyILspector
{
    internal class OptionFactory
    {
        internal EOption GetOption(MetadataToken metadataToken, string functionName)
        {
            if (metadataToken != MetadataToken.Zero && !string.IsNullOrWhiteSpace(functionName))
            {
                return EOption.Ambiguous;
            }

            if (metadataToken == MetadataToken.Zero && string.IsNullOrWhiteSpace(functionName))
            {
                return EOption.None;
            }

            if (metadataToken != MetadataToken.Zero && string.IsNullOrWhiteSpace(functionName))
            {
                return EOption.MetadataToken;
            }

            if (metadataToken == MetadataToken.Zero && !string.IsNullOrWhiteSpace(functionName))
            {
                return EOption.FunctionName;
            }

            return default;
        }
    }
}
