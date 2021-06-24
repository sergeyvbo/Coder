using Coder.Scenarios;
using System;
using System.Threading;
using WindowsInput;

namespace Coder
{
    class Program
    {
        static InputSimulator inputSimulator = new();
        static void Main(string[] args)
        {
            //TODO чтение конфига
            //TODO парсинг параметров командной строки и оверрайд конфига


            Console.WriteLine("Press Enter to start");
            Console.ReadLine();
            Thread.Sleep(2000);

            var scenarioManager = new ScenarioManager();

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
    }
}