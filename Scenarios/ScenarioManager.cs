using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Coder.Scenarios
{
    public class ScenarioManager
    {
        private readonly IConfiguration _configuration;
        

        public string[] Scenarios { get; set; }
        public Queue<string> ScenarioQueue;

        public ScenarioManager(IConfiguration configuration)
        {
            _configuration = configuration;
            Scenarios = InitScenarios();
            ScenarioQueue = InitScenarioQueue();
        }

        private string[] InitScenarios()
        {
            var scenarios = new string[]
            {
                "CodingScenario",
                "ScrollingScenario",
                "StayOnlineScenario"
            };

            return scenarios;
        }

        private Queue<string> InitScenarioQueue()
        {
            var queue = new Queue<string>();
            var confScenario = _configuration["defaultScenario"];
            if (confScenario is not null)
            {
                queue.Enqueue(confScenario);
                return queue;
            }

            foreach (var s in Shuffle(Scenarios))
            {
                queue.Enqueue(s);
            }

            return queue;
        }

        public string GetNextScenarioName()
        {
            return ScenarioQueue.Peek();
        }

        private string[] Shuffle(string[] array)
        {
            int n = array.Length;
            var result = new string[n];
            array.CopyTo(result, 0);
            Random rnd = new();
            
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                var value = result[k];
                result[k] = result[n];
                result[n] = value;
            }

            return result;
        }


        public IScenario GetNextScenario()
        {
            var scenario = ScenarioQueue.Dequeue();
            ScenarioQueue.Enqueue(scenario);
            return CreateInstance(scenario);
        }

        private IScenario CreateInstance(string name)
        {
            try
            {
                Type type = Type.GetType($"Coder.Scenarios.{name}");
                object instance = Activator.CreateInstance(type);
                return (IScenario)instance;
            }
            catch (Exception)
            {
                Console.WriteLine($"Invalid scenario name: {name}");
            }
            return new StayOnlineScenario(); // в случае проблем возвращаем самый безопасный и ненавязчивый сценарий
        }
    }
}
