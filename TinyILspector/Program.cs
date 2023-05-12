using AsmResolver.PE.DotNet.Metadata.Tables;
using System.CommandLine;
using TinyILspector;

internal class Program
{
    static async Task Main(string[] args)
    {
        RootCommand rootCommand = new RootCommand(description: "Inspects MS IL in .NET Assembly");

        var inputOption = new Option<FileInfo>(
            aliases: new string[] { "--input", "-i" },
            description: "The path to the executable that is to be inspected.");
        var metadataTokenOption = new Option<string>(
            aliases: new string[] { "--metadata-token", "-mt" },
            description: "Requested metadata token");
        var functionNameOption = new Option<string>(
            aliases: new string[] { "--function-name", "-fn" },
            description: "Requested function name");
        rootCommand.Add(inputOption);
        rootCommand.Add(metadataTokenOption);
        rootCommand.Add(functionNameOption);
        rootCommand.SetHandler(
            (fileInfo, mt, functionName) => { RunOption(mt, functionName, fileInfo); },
            inputOption, metadataTokenOption, functionNameOption);
        await rootCommand.InvokeAsync(args);
    }

    private static void RunOption(string mt, string functionName, FileInfo fileInfo)
    {
        var metadataToken = Convert.ToUInt32(mt, 16);
        var option = new OptionFactory().GetOption(metadataToken, functionName);
        var inspector = new Inspector();
        switch (option)
        {
            case EOption.Ambiguous:
                Console.Error.WriteLine($"Metadata token and function name cannot be set concurrently");
                break;
            case EOption.None:
                inspector.Inspect(fileInfo);
                break;
            case EOption.MetadataToken:
                inspector.Inspect(fileInfo, new MetadataToken(metadataToken));
                break;
            case EOption.FunctionName:
                inspector.Inspect(fileInfo, functionName);
                break;
            default:
                Console.Error.WriteLine($"I don't know to do");
                break;
        }
    }

}