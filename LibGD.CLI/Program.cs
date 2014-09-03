﻿using System;
using System.IO;
using CppSharp;

namespace LibGD.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: LibGD.CLI.exe [include_dir_of_libd] [library(.dll/.lib)_file]");
                return;
            }
            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine("{0} does not exist or is not a directory.", args[0]);
                return;
            }
            if (!File.Exists(args[1]))
            {
                Console.WriteLine("{0} does not exist.", args[0]);
                return;
            }
            ConsoleDriver.Run(new LibGDSharp(args[0], args[1]));
        }
    }
}
