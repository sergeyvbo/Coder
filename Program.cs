using Desktop.Robot;
using Desktop.Robot.Extensions;
using System;
using System.IO;

string filename = GetRandomFile(@"C:\temp\");
var random = new Random();
const int MinDelay = 10;
const int MaxDelay = 300;

if (!File.Exists(filename))
{
    return;
}
Console.WriteLine(filename);
Console.ReadLine();
var robot = new Robot();

foreach (char ch in File.ReadAllText(filename))
{
    robot.Type(ch.ToString(),(uint)random.Next(MinDelay, MaxDelay));
}

string GetRandomFile(string v)
{
    return @"C:\temp\AzureStorageService.cs";
}