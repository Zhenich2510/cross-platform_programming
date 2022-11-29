using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;

using LabsClassLibrary;

using McMaster.Extensions.CommandLineUtils;

using static System.Net.Mime.MediaTypeNames;

[Command(Name = "labs", Description = "Labs runner"),
    Subcommand(typeof(Run), typeof(Version), typeof(SetPath))]
class LabRunner
{
    const string LAB_PATH = "LAB_PATH";
    public static void Main(string[] args) => CommandLineApplication.Execute<LabRunner>(args);


    private int OnExecute(CommandLineApplication app, IConsole console)
    {
        console.WriteLine("You must specify at a command.");

        console.WriteLine($"Platform={Environment.OSVersion.Platform}");
        app.ShowHelp();
        return 1;
    }

    [Command("version", Description = "Show version")]
    private class Version
    {
        private int OnExecute(IConsole console)
        {
            Console.WriteLine("Author: Zhenich");
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            console.WriteLine($"Version: {ver}");
            var labpath = SetPath.GetLabPathEnv();
            console.WriteLine($"Variable LAB_PATH: {(string.IsNullOrWhiteSpace(labpath) ? "not set" : labpath)}");
            return 1;
        }
    }

    [Command("run", Description = "Run the lab"),
     Subcommand(typeof(Lab1)),
     Subcommand(typeof(Lab2)),
     Subcommand(typeof(Lab3))
        ]
    private class Run
    {

        private int OnExecute(IConsole console)
        {
            console.Error.WriteLine("You must specify a lab. See run --help for more details.");

            return 1;
        }

        abstract class LabBase
        {
            [Option("--input -i", Description = "Specify a name for input file")]
            public string Input { get; } = null!;

            [Option("--output -o", Description = "Specify a name for output file")]
            public string Output { get; } = null!;

            protected string GetInputPath()
            {
                if (File.Exists(Input))
                {
                    return Input;
                }

                var labpath = SetPath.GetLabPathEnv();
                if (string.IsNullOrWhiteSpace(labpath))
                {
                    labpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
                labpath = Path.Combine(labpath, "INPUT.TXT");

                if (File.Exists(labpath))
                {
                    return labpath;
                }

                return "";
            }
            protected string GetOutputPath()
            {
                if (!string.IsNullOrWhiteSpace(Output))
                {
                    return Output;
                }

                var labpath = SetPath.GetLabPathEnv();
                if (string.IsNullOrWhiteSpace(labpath))
                {
                    labpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }

                if (Directory.Exists(labpath))
                {
                    return Path.Combine(labpath, "OUTPUT.TXT");
                }

                return "OUTPUT.TXT";
            }

            protected string ReadInputFile()
            {
                string inputPath = GetInputPath();
                if (string.IsNullOrWhiteSpace(inputPath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: INPUT.TXT not found.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return string.Empty;
                }
                Console.WriteLine($"Read input file: {inputPath}");

                return File.ReadAllText(inputPath);
            }
            protected void WriteOuputFile(string outputData)
            {
                string outputPath = GetOutputPath();

                Console.WriteLine($"Write output file: {outputPath}");

                File.WriteAllText(outputPath, outputData);
            }

            protected virtual int OnExecute(IConsole console)
            {
                console.WriteLine($"Run a lab Input:{Input} Output:{Output}");
                return 1;
            }
        }

        [Command("lab1", Description = "Run lab1 - Combinations")]
        private class Lab1 : LabBase
        {
            protected override int OnExecute(IConsole console)
            {
                string inputData = ReadInputFile();
                if (string.IsNullOrWhiteSpace(inputData))
                {
                    return -1;
                }
                console.WriteLine($"Running lab1 - Combinations");
                string inputCombination = inputData.Split('\n', 1)[0].Trim();
                console.WriteLine($"Start combination: {inputCombination}");
                List<string> resultCombinations = Lab1Combination.CombineString(inputCombination);
                console.WriteLine($"Result combinations: ");
                string outputData = "";
                foreach (var item in resultCombinations)
                {
                    outputData += $"{item}\n";
                    console.WriteLine($"{item}");
                }

                WriteOuputFile(outputData);
                return 1;
            }
        }

        [Command("lab2", Description = "Run lab2 - Sequence")]
        private class Lab2 : LabBase
        {
            protected override int OnExecute(IConsole console)
            {
                string inputData = ReadInputFile();
                if (string.IsNullOrWhiteSpace(inputData))
                {
                    return -1;
                }
                console.WriteLine($"Running lab2 - Sequence");
                string inputN = inputData.Split('\n', 1)[0].Trim();
                console.WriteLine($"N: {inputN}");

                if (!uint.TryParse(inputN, out uint n) || n <= 1 || n > 1000)
                {
                    console.WriteLine("number must be between 1 and 1000");
                    return -1;
                }

                var result = LabSequence.CountCombines(n);
                console.WriteLine($"Count of combinations: {result}");
                string outputData = $"{result}";

                WriteOuputFile(outputData);
                return 1;
            }
        }

        [Command("lab3", Description = "Run lab3 - Pyatnashki")]
        private class Lab3 : LabBase
        {
            protected override int OnExecute(IConsole console)
            {
                string inputData = ReadInputFile();
                if (string.IsNullOrWhiteSpace(inputData))
                {
                    return -1;
                }
                console.WriteLine($"Running lab3 - Pyatnashki");
                var splited = inputData.Split('\n');
                if (splited.Length < 4)
                {
                    Console.WriteLine("Wrong input");
                    return -1;
                }

                string start = splited[0].Trim() + splited[1].Trim();
                string finish = splited[2].Trim() + splited[3].Trim();
                if (start.Length != 8 || start.Count(c => c == '#') != 1
                    || finish.Length != 8 || finish.Count(c => c == '#') != 1)
                {
                    Console.WriteLine("Wrong input");
                    return -1;
                }

                console.WriteLine($"input: ");
                console.WriteLine($"{splited[0]}");
                console.WriteLine($"{splited[1]}");
                console.WriteLine($"{splited[2]}");
                console.WriteLine($"{splited[3]}");


                var result = LabPyatnashki.ComputeStepsCount(start, finish);
                console.WriteLine($"Steps Count: {result}");
                string outputData = $"{result}";

                WriteOuputFile(outputData);
                return 1;
            }
        }
    }
    [Command("set-path", Description = "Set LAB_PATH for INPUT.TXT and OUTPUT.TXT")]
    private class SetPath
    {
        [Option("--path -p", Description = "path to dir of input/output files")]
        [Required(ErrorMessage = "You must specify the path")]
        public string Path { get; } = null!;
        private int OnExecute(IConsole console)
        {
            Environment.SetEnvironmentVariable(LAB_PATH, Path);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                File.WriteAllText(".env", $"{LAB_PATH}={Path}");
            }

            return 1;
        }
        public static string GetLabPathEnv()
        {
            var path = Environment.GetEnvironmentVariable(LAB_PATH);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && File.Exists(".env"))
            {
                var text = File.ReadAllText(".env");
                var keyValue = text.Split('=');
                if (keyValue.Length == 2)
                    path = keyValue[1];
            }
            return path ?? "";
        }
    }
}