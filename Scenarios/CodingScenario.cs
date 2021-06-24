using System;
using System.IO;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace Coder.Scenarios
{
    public class CodingScenario : IScenario
    {
        private InputSimulator _inputSimulator = new();
        public void Execute()
        {
            var files = GetAllFiles(@"C:\temp\");

            foreach (var file in files)
            {
                string filename = file;
                var random = new Random();
                const int MinDelay = 60;
                const int MaxDelay = 120;
                const int MinBetweenLinesDelay = 600;
                const int MaxBetweenLinesDelay = 1200;

                if (!File.Exists(filename))
                {
                    return;
                }

                foreach (var line in File.ReadAllLines(filename))
                {
                    var curLine = line.Trim();
                    Thread.Sleep(random.Next(MinBetweenLinesDelay, MaxBetweenLinesDelay));
                    foreach (char ch in curLine)
                    {
                        if (ch == '\n')
                        {
                            continue;
                        }
                        Thread.Sleep(random.Next(MinDelay, MaxDelay));
                        _inputSimulator.Keyboard.TextEntry(ch);
                    }
                    _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                }


                NewFile();
            }
        }

        private string[] GetAllFiles(string path)
        {
            var random = new Random();
            var files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                var nf = random.Next(0, files.Length);
                var temp = files[nf];
                files[nf] = files[i];
                files[i] = temp;
            }
            return files;
        }

        private void NewFile()
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_A);
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
            _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
        }
    }
}
