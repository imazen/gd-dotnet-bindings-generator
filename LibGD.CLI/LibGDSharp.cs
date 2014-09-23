using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Parser;
using CppAbi = CppSharp.Parser.AST.CppAbi;

namespace LibGD.CLI
{
    public class LibGDSharp : ILibrary
    {
        private readonly string includeDir;
        private readonly string make;
        private readonly string libraryFile;

        public LibGDSharp(string includeDir, string make, string libraryFile)
        {
            this.includeDir = includeDir;
            this.make = make;
            this.libraryFile = libraryFile;
        }

        public void Preprocess(Driver driver, ASTContext ctx)
        {
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
        }

        public void Setup(Driver driver)
        {
            string error;
            string output = ProcessHelper.Run(Path.Combine(Path.GetDirectoryName(this.make), "gcc"), "-v", out error);
            if (string.IsNullOrEmpty(output))
            {
                output = error;
            }
            string target = Regex.Match(output, @"Target:\s*(?<target>[^\r\n]+)").Groups["target"].Value;
            string compilerVersion = Regex.Match(output, @"gcc\s+version\s+(?<version>\S+)").Groups["version"].Value;

            driver.Options.addDefines("_WIN32");
            driver.Options.GeneratorKind = GeneratorKind.CSharp;
            driver.Options.LanguageVersion = LanguageVersion.C;
            driver.Options.NoBuiltinIncludes = true;
            driver.Options.MicrosoftMode = false;
            driver.Options.TargetTriple = target;
            driver.Options.Abi = CppAbi.Itanium;
            driver.Options.LibraryName = "LibGDSharp";
            driver.Options.OutputNamespace = "LibGD";
            driver.Options.Verbose = true;
            driver.Options.IgnoreParseWarnings = true;
            driver.Options.CompileCode = true;
            driver.Options.CheckSymbols = true;
            driver.Options.StripLibPrefix = false;
            driver.Options.GenerateSingleCSharpFile = true;
            driver.Options.Headers.AddRange(Directory.EnumerateFiles(this.includeDir, "*.h").Where(f => Path.GetFileNameWithoutExtension(f) != "gdpp"));
            string gccPath = Path.GetDirectoryName(Path.GetDirectoryName(this.make));
            driver.Options.addSystemIncludeDirs(Path.Combine(gccPath, target, "include"));
            driver.Options.addSystemIncludeDirs(Path.Combine(gccPath, target, "include", "c++"));
            driver.Options.addSystemIncludeDirs(Path.Combine(gccPath, target, "include", "c++", target));
            driver.Options.addSystemIncludeDirs(Path.Combine(gccPath, "lib", "gcc", target, compilerVersion, "include"));
            driver.Options.addIncludeDirs(includeDir);
            driver.Options.addLibraryDirs(Path.GetDirectoryName(this.libraryFile));
            driver.Options.Libraries.Add(Path.GetFileName(this.libraryFile));
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            driver.Options.CodeFiles.Add(Path.Combine(dir, "_iobuf.cs"));
        }

        public void SetupPasses(Driver driver)
        {
        }
    }
}
