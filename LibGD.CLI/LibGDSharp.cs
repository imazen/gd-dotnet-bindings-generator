using System.IO;
using System.Reflection;
using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Parser;

namespace LibGD.CLI
{
    public class LibGDSharp : ILibrary
    {
        private readonly string includeDir;
        private readonly string libraryFile;

        public LibGDSharp(string includeDir, string libraryFile)
        {
            this.includeDir = includeDir;
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
            driver.Options.addDefines("_WIN32");
            driver.Options.LanguageVersion = LanguageVersion.C;
            driver.Options.GeneratorKind = GeneratorKind.CSharp;
            driver.Options.TargetTriple = "i686-pc-win64";
            driver.Options.LibraryName = "LibGDSharp";
            driver.Options.OutputNamespace = "LibGD";
            driver.Options.Verbose = true;
            driver.Options.IgnoreParseWarnings = true;
            driver.Options.CompileCode = true;
            driver.Options.CheckSymbols = true;
            driver.Options.Headers.AddRange(Directory.EnumerateFiles(this.includeDir, "*.h"));
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
