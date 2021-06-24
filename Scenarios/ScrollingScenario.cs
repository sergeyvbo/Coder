using System;
using System.Threading;
using WindowsInput;

namespace Coder.Scenarios
{
    public class ScrollingScenario : IScenario
    {
        private InputSimulator _inputSimulator = new();
        public void Execute()
        {
            var random = new Random();
            const int MinDelay = 30000;
            const int MaxDelay = 120000;
            while (true)
            {
                _inputSimulator.Mouse.VerticalScroll(-1);
                Thread.Sleep(random.Next(MinDelay, MaxDelay));
            }
        }
    }
}
