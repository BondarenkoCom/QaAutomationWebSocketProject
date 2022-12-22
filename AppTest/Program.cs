using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AWindowsUi.Installer;
using NUnitLite;

namespace AppTest
{
    public static class Program
    {
        private static readonly List<Assembly> TestAssemblies = new()
        {
            //Assembly.GetAssembly(typeof(TerminalInstall)),
            Assembly.GetAssembly(typeof(TerminalInstall)),
            Assembly.GetAssembly(typeof(WebSocketTests.Tests.Scenarios.TaxcomAgentTestsWithToken)),
            Assembly.GetAssembly(typeof(OldCryptoTests.OldWithNewComparatorTests)),
            Assembly.GetAssembly(typeof(WebSocketTests.Tests.CryptoMethod.Sign)),
            Assembly.GetAssembly(typeof(WebSocketTests.Tests.SupportMethod.VersionInfo)),
            Assembly.GetAssembly(typeof(WebSocketTests.Tests.SupportMethod.GetValue)),
            Assembly.GetAssembly(typeof(WebSocketPinCodeTests.PinCodeTests)),
            Assembly.GetAssembly(typeof(TestRestApi.RestTests))
        };

        static async Task Main(string[] args)
        {
            foreach (var testAssembly in TestAssemblies)
            {
                try
                {
                    Console.WriteLine($"Start new AssemblyTest [{testAssembly.FullName}]");
                    var res = new AutoRun(testAssembly).Execute(Array.Empty<string>());
                    Console.WriteLine($"End AssemblyTest [{testAssembly.FullName}] with code [{res}]");
                    await RenameResultFile(testAssembly.FullName);
                    Console.WriteLine("Work Install");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static async Task RenameResultFile(string nameOfAssembly)
        {
            const string sourceFileName = "TestResult.xml";

            var pathToSource = Path.Combine(Directory.GetCurrentDirectory(), sourceFileName);
            if (!File.Exists(pathToSource))
                throw new Exception($"Не найден файл результатов теста по пути [{pathToSource}]");

            var resPath = Path.Combine(Directory.GetCurrentDirectory(), "ResOfTest");
            if (!Directory.Exists(resPath))
                Directory.CreateDirectory(resPath);

            var pathToResFile = Path.Combine(resPath, $"{nameOfAssembly}_{sourceFileName}");
            if(File.Exists(pathToResFile))
                File.Delete(pathToResFile);

            await using (var resLastOriginal = new FileStream(pathToResFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                await using var resOriginal = File.OpenRead(pathToSource);
                await resOriginal.CopyToAsync(resLastOriginal);
            }

            Console.WriteLine($"FilePrepared [{pathToResFile}]");
            Console.WriteLine("Source file deleting");
            File.Delete(pathToSource);
        }
    }
}
