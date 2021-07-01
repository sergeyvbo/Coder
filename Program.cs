using Coder.Scenarios;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;

namespace Coder
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Any(s => s.Contains("help") || s.Contains("?") || s.Contains("/h")
            ))
            {
                ShowHelp();
                Console.ReadLine();
                return;
            }

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddCommandLine(args)
                .Build();
            
            var scenarioManager = new ScenarioManager(configuration);

            if (args.Any(s => s == "/scenarios"))
            {
                Console.WriteLine("Scenarios:");
                ShowScenarios(scenarioManager);
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"Next scenario: {scenarioManager.GetNextScenarioName()}");
            Console.WriteLine("Press Enter to start");
            Console.WriteLine("Press Ctrl-Brake to stop");
            Console.ReadLine();
            Thread.Sleep(2000);

            while (true)
            {
                scenarioManager.GetNextScenario().Execute();
            }

            // ожидаемое максимальное время работы ~40 минут.
            // выбирать не рандомные сценари, а вперемешку: большие и маленькие

            //сценарии
            //1. набор кода в VS
            //2. меланхоличный скроллинг текста
            //3. чтение конфлюенса
            //4. просмотр видео
            //5. чтение metanit (как перейти на следующую страницу?)
            //6. переписка в linq
            //7. просмотр почты
            //8. редкие нажатия для поддержания соединения

        }

        private static void ShowScenarios(ScenarioManager scenarioManager)
        {
            foreach (var s in scenarioManager.Scenarios)
            {
                Console.WriteLine(s);
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Command-line arguments:");
            Console.WriteLine("/help | /h | /? - show this message.");
            Console.WriteLine("/scenario <scenarioName> - select scenario");
            Console.WriteLine("/scenarios - get list of scenarios");
            Console.WriteLine("Press any key to exit.");
        }
    }
}