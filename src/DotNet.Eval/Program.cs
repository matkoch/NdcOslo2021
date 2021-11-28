using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

do
{
    Console.Write("> ");
    var expression = Console.ReadLine();
    try
    {
        var options = ScriptOptions.Default
            .AddImports("System")
            .AddImports("System.Collections.Generic");
        var result = CSharpScript.EvaluateAsync(expression, options);
        Console.WriteLine(result.Result);
    }
    catch
    {
        Console.Error.WriteLine("Couldn't evaluate expression");
    }
} while (true);
