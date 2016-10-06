using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PHP.Core;
using System.Diagnostics;

namespace Devsense.WordPress.Plugins.WpDotNet.Mbstring
{
    class Mbstring
    {
         /// <summary>
        /// Load required Mbstring stuff into the <paramref name="context"/>.
        /// </summary>
        public static void Load(ScriptContext/*!*/context)
        {
            Debug.Assert(context != null);
            Debug.Assert(!context.ApplicationContext.GetLoadedExtensions().Contains("mbstring"));

            // mbstring.func_overload just return 0, so Phalanger won't throw warning(unlike PHP)
            if (PHP.Library.IniOptions.GetOption("mbstring.func_overload") == null)
            {
                lock (typeof(Mbstring)) try
                    {
                        PHP.Library.IniOptions.Register("mbstring.func_overload", PHP.Library.IniFlags.Supported, (config, option, value, action) => 0, "mbstring");
                    }
                    catch { }
            }
        }
    }
}
