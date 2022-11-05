using AnzRobotCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AnzRobotConsole 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // set this to true to show the executed commands
            var showCommands = false;

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRobotRunner, RobotRunner>()
                .BuildServiceProvider();

            var fileName = args.Count() > 0 ? args[0] : "commands.txt";

            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var path = $"{directory}/{fileName}";

            var commands = File.ReadAllText(path);

            var robotRunner = serviceProvider.GetService<IRobotRunner>();
            var outputs = robotRunner.RunCommands(commands, showCommands);

            foreach (var output in outputs)
                Console.WriteLine(output);
        }

    }
}