using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PHP.Core;
using PHP.Core.Utilities;
using System.Reflection;

namespace Devsense.WordPress.Plugins.WpDotNet
{
    public class AdminSectionUtils
    {
        public static string PlatformInfo
        {
            get
            {
                if (platformInfo == null)
                {
                    object[] attrsPhalangerVer = typeof(PHP.Core.ScriptContext).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                    string phalangerVer = attrsPhalangerVer.Length > 0 ? ((AssemblyFileVersionAttribute)attrsPhalangerVer[0]).Version : String.Empty;
                    phalangerVer = phalangerVer.Substring(0, 3);

                    string platformName;
                    string platfromVer;

                    Type t = Type.GetType("Mono.Runtime");
                    if (t != null)
                    {
                        platformName = "Mono";
                        platfromVer = GetMonoVersion(t);
                    }
                    else
                    {
                        platformName = ".NET";
                        platfromVer = GetNetVersion();
                    }

                    platformInfo = String.Format("Running on <b>{0} {1}</b> powered by <b>Phalanger</b> {2}.", platformName, platfromVer, phalangerVer);
                }
                return platformInfo;
            }
        }

        private static string GetNetVersion()
        {
            string platfromVer;
            object[] attrsVer = typeof(int).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
            platfromVer = attrsVer.Length > 0 ? ((AssemblyFileVersionAttribute)attrsVer[0]).Version : String.Empty;

            //platfromVer looks like 4.0.30319.261
            //change the version to known format
            return platfromVer.Substring(0, 3);
        }

        private static string platformInfo;


        public static bool IsMonoCompatible
        {
            get
            {
                Type t = Type.GetType("Mono.Runtime");

                if (t == null)
                    return true;//it is compatible, because it's .NET

                string version = GetMonoVersion(t);
                var parts = version.Split('.');

                int major = parts.Length > 0 ? System.Convert.ToInt32(parts[0]) : 0; 
                int minor = parts.Length > 1 ? System.Convert.ToInt32(parts[1]) : 0;
                int build = parts.Length > 2 ? System.Convert.ToInt32(parts[2]) : 0;

                var compatible = new Version(2,10,8);
                var actual = new Version(major,minor,build);

                if (actual < compatible)
                    return false;
                else
                    return true;
            }
        }

        private static string GetMonoVersion(Type t)
        {
            MethodInfo displayName = t.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);

            if (displayName == null)
                return String.Empty;

            object displayNameRes = displayName.Invoke(null, null);

            if (displayNameRes == null)
                return String.Empty;

            string s = displayNameRes.ToString();

            //s looks like this "2.6.7 (Debian 2.6.7-3ubuntu1~dhx1)"
            int pos = s.IndexOf(' ');

            if (pos == -1)
                return s;

            return s.Substring(0, pos);
        }

        public static void CleanDashboard(ScriptContext context)
        {
            dynamic wp = context.Globals;

            //wp.add_action( "wp_dashboard_setup", new Action(() => {
            
            //    wp.remove_meta_box( "dashboard_primary", "dashboard", "side" );
            //    wp.remove_meta_box( "dashboard_secondary", "dashboard", "side" );
            //    wp.remove_meta_box( "dashboard_plugins", "dashboard", "normal" );
            //    wp.remove_meta_box( "dashboard_incoming_links", "dashboard", "normal" );

            //}));

        }

    }
}
