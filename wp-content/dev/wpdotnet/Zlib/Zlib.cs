using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using PHP.Core;
using System.Diagnostics;

namespace Devsense.WordPress.Plugins.WpDotNet.Zlib
{
    /// <summary>
    /// Simulates behaviour of zlib extension with required zlib functions.
    /// </summary>
    internal class Zlib
    {
        /// <summary>
        /// Load required zlib functions into the <paramref name="context"/>.
        /// </summary>
        public static void Load(ScriptContext/*!*/context)
        {
            Debug.Assert(context != null);
            Debug.Assert(!context.ApplicationContext.GetLoadedExtensions().Contains("zlib"));

            // - zlib.output_compression (ignore, always "")
            if (PHP.Library.IniOptions.GetOption("zlib.output_compression") == null)
            {
                lock (typeof(Zlib)) try
                    {
                        PHP.Library.IniOptions.Register("zlib.output_compression", PHP.Library.IniFlags.Supported, (config, option, value, action) => string.Empty, "zlib");
                    }
                    catch { }
            }
            
            // - gzopen, gzclose, gzread, gzinflate
            DeclareFunction(context, "gzopen", NotSupportedFunction);
            DeclareFunction(context, "gzclose", NotSupportedFunction);
            DeclareFunction(context, "gzread", NotSupportedFunction);
            DeclareFunction(context, "gzinflate", (_, stack) =>
            {
                if (stack.ArgCount != 1) return null;
                if (!(stack.PeekValue(1) is PhpBytes)) return false;

                var bytes = ((PhpBytes)stack.PeekValue(1)).ReadonlyData;

                try
                {
                    List<byte>/*!*/inflate = new List<byte>(bytes.Length);
                    
                    using (var stream = new DeflateStream(new System.IO.MemoryStream(bytes), CompressionMode.Decompress))
                    {
                        byte[] buffer = new byte[1024];
                        int n;
                        while ((n = stream.Read(buffer, 0, buffer.Length)) > 0)
                            inflate.AddRange(buffer.Take(n));
                    }

                    return new PhpBytes(inflate.ToArray());
                }
                catch (Exception ex)
                {
                    PhpException.Throw(PhpError.Warning, "Error while decompressing gz stream: " + ex.Message);
                    return false;
                }
            });

            // - NS: gzdeflate, gzputs, gzwrite
            DeclareFunction(context, "gzdeflate", NotSupportedFunction);
            DeclareFunction(context, "gzputs", NotSupportedFunction);
            DeclareFunction(context, "gzwrite", NotSupportedFunction);
        }

        /// <summary>
        /// Declare function <paramref name="name"/> into the PHP <paramref name="context"/>.
        /// </summary>
        private static void DeclareFunction(ScriptContext/*!*/context, string name, RoutineDelegate func)
        {
            context.DeclaredFunctions.Add(name, new PHP.Core.Reflection.PhpRoutineDesc(PHP.Core.PhpMemberAttributes.Public | PHP.Core.PhpMemberAttributes.Static, func, false));
        }

        private static object NotSupportedFunction(object instance, PhpStack/*!*/stack)
        {
            PhpException.FunctionNotSupported();
            return null;
        }
    }
}
