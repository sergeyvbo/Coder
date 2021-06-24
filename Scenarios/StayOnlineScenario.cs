using System;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace Coder.Scenarios
{
    public class StayOnlineScenario : IScenario
    {
        private InputSimulator _inputSimulator = new();
        public void Execute()
        {
            var random = new Random();
            const int MinDelay = 30000;
            const int MaxDelay = 120000;
            while (true)
            {
                _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.CONTROL);
                Thread.Sleep(random.Next(MinDelay, MaxDelay));
            }
        }
    }
}
