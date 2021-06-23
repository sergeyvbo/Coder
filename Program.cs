using System;
using System.IO;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace Coder
{
    class Program
    {
        static InputSimulator inputSimulator = new();
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to start");
            Console.ReadLine();
            Thread.Sleep(2000);
            NewFile();
            TypeText();
        }

        static void TypeText()
        {
            var files = GetAllFiles(@"C:\temp\");

            foreach (var file in files)
            {
                ; string filename = file;
                var random = new Random();
                const int MinDelay = 10;
                const int MaxDelay = 400;

                if (!File.Exists(filename))
                {
                    return;
                }

                foreach (char ch in File.ReadAllText(filename))
                {
                    Thread.Sleep(random.Next(MinDelay, MaxDelay));
                    inputSimulator.Keyboard.TextEntry(ch);

                }
                NewFile();
            }
        }

        static string[] GetAllFiles(string path)
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

        static void NewFile()
        {
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_A);
            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
        }
        
    }
}