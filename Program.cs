using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        PlayAudio("Audio2.m4a"); // Replace with your audio file name if needed
        Console.WriteLine("Program started.");
    }

    static void PlayAudio(string fileName)
    {
        string fullPath = Path.GetFullPath(fileName);

        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"Audio file not found: {fullPath}");
            return;
        }

        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Console.WriteLine("Running on macOS... playing audio with afplay.");
                Process.Start("afplay", $"\"{fullPath}\"");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("Running on Windows... trying to play audio.");

                try
                {
                    Process.Start("wmplayer", $"\"{fullPath}\"");
                }
                catch
                {
                    Console.WriteLine("wmplayer not found. Using PowerShell fallback.");
                    Process.Start("powershell", $"-c (New-Object Media.SoundPlayer \"{fullPath}\").PlaySync();");
                }
            }
            else
            {
                Console.WriteLine("Unsupported OS. Cannot play audio.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error playing audio: {ex.Message}");
        }
    }
}
