using System;
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
                Console.WriteLine("Usage: LibGD.CLI.exe [include_dir_of_libd] [GCC/MinGW_make_exe (optional) - skip for VC++] [library(.dll)_file]");
                return;
            }
            LibGDSharp libGdSharp = null;
            switch (args.Length)
            {
                case 2:
                    libGdSharp = new LibGDSharp(args[0], args[1]);
                    break;
                default:
                    libGdSharp = new LibGDSharp(args[0], args[1], args[2]);
                    break;
            }
            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine("{0} does not exist or is not a directory.", args[0]);
                return;
            }
            if (!File.Exists(args[1]))
            {
                Console.WriteLine(args.Length > 2 ? "{0} does not exist or is not a directory." : "{0} does not exist.", args[1]);
                return;
            }
            if (args.Length > 2 && !File.Exists(args[2]))
            {
                Console.WriteLine("{0} does not exist.", args[2]);
                return;
            }
            using (new ConsoleCopy("gd-cppsharp-log.txt"))
            {
                ConsoleDriver.Run(libGdSharp);
            }
        }
    }
}
