using CppSharp.Generators;
using CppSharp.Generators.CLI;
using CppSharp.Generators.CSharp;
using CppSharp.Types;

namespace LibGD.CLI
{
    [TypeMap("_iobuf")]
    public class FILE : TypeMap
    {
        public override string CSharpSignature(CSharpTypePrinterContext ctx)
        {
            return "global::System.IntPtr";
        }

        public override void CSharpMarshalToManaged(MarshalContext ctx)
        {
            ctx.Return.Write(ctx.Parameter.Name);
        }

        public override void CSharpMarshalToNative(MarshalContext ctx)
        {
            ctx.Return.Write(ctx.Parameter.Name);
        }

        public override string CLISignature(CLITypePrinterContext ctx)
        {
            return "System::IntPtr";
        }

        public override void CLIMarshalToManaged(MarshalContext ctx)
        {
            ctx.Return.Write("(FILE*) {0}.ToPointer()", ctx.Parameter.Name);
        }

        public override void CLIMarshalToNative(MarshalContext ctx)
        {
            ctx.Return.Write("(FILE*) {0}.ToPointer()", ctx.Parameter.Name);
        }
    }
}
