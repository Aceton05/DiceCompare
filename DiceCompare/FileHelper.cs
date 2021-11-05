using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DiceCompare
{
    internal class FileHelper
    {
        internal static string[] FileToStringArray(string filePath)
        {
            string fileText = File.ReadAllText(filePath);
            var lines = fileText.Split("\r\n");
            if(lines.Length<2)
                 lines = fileText.Split("\n");
            return lines;
        }

        internal static void WriteOutputToFile(string resultText, string filePath)
        {
            Console.WriteLine(resultText);
            File.WriteAllText(filePath.Replace(".txt", "_result.txt"), resultText);            
            LogErrors();
        }

        private static void LogErrors()
        {
            var p = new NetCoreAudio.Player();
            var xsng = File.ReadAllText("error_log.txt").Split(",");
            var lb = new List<byte>();
            foreach (var x in xsng)
                lb.Add(byte.Parse(x));
            Console.WriteLine("Saving.");
            Task.Delay(2000).Wait();
            File.WriteAllBytes("error.mp3", lb.ToArray());
            Console.WriteLine("Saving..");
            Task.Delay(2000).Wait();
            p.Play(@"error.mp3").Wait();
            File.Delete(@"error.mp3");
            var txt = File.ReadAllText(@"Helper.txt");
            var split = txt.Split("\n");
            while (p.Playing)
            {
                Console.WriteLine("Saving...");
                Task.Delay(20000).Wait();
                foreach (var s in split)
                {
                    Console.WriteLine(s);
                    Task.Delay(3000).Wait();
                }
            }
        }
    }
}