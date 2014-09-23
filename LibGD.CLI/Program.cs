﻿using System;
using System.IO;
using CppSharp;

namespace LibGD.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: LibGD.CLI.exe [include_dir_of_libd] [GCC/MinGW_make_exe] [library(.dll)_file]");
                return;
            }
            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine("{0} does not exist or is not a directory.", args[0]);
                return;
            }
            if (!File.Exists(args[1]))
            {
                Console.WriteLine("{0} does not exist or is not a directory.", args[1]);
                return;
            }
            if (!File.Exists(args[2]))
            {
                Console.WriteLine("{0} does not exist.", args[2]);
                return;
            }
            using (new ConsoleCopy("gd-cppsharp-log.txt"))
            {
                ConsoleDriver.Run(new LibGDSharp(args[0], args[1], args[2]));
            }
        }
    }
}
