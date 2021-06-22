using System;
using System.IO;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

InputSimulator inputSimulator = new();
var files = GetAllFile(@"C:\temp\");
Console.ReadLine();
Thread.Sleep(2000);

foreach (var file in files)
{
    ; string filename = file;
    var random = new Random();
    const int MinDelay = 10;
    const int MaxDelay = 1000;

    if (!File.Exists(filename))
    {
        return;
    }


    foreach (char ch in File.ReadAllText(filename))
    {
        Thread.Sleep(random.Next(MinDelay, MaxDelay));
        inputSimulator.Keyboard.TextEntry(ch);

    }
    NewFile(filename);

}


string[] GetAllFile(string path)
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

void NewFile(string file)
{

    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
    //---------------

}