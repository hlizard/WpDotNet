using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PHP.Core;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace Devsense.WordPress.Plugins.WpDotNet
{
    public class PluginUpdate
    {
        /// <summary>
        /// API url used for WpDotNet plugin updates.
        /// </summary>
#if DEBUG
        public const string PluginApiUrl = "http://localhost/wp-update-api/api.php";
#else
        public const string PluginApiUrl = "http://www.wpdotnet.com/api.php";
#endif

        public static void Load(ScriptContext/*!*/context, string/*!!*/plugin_slug)
        {
            Debug.Assert(context != null);
            Debug.Assert(!string.IsNullOrEmpty(plugin_slug));

            dynamic wp = context.Globals;

#if DEBUG
            //// Enable update check on every request. This is for testing only!
            //wp.set_site_transient("update_plugins", null);

            //// Show which variables are being requested when query plugin API
            //wp.add_filter("plugins_api_result", new Func<object, object, object, object>((res, action, args) =>
            //{
            //    return res;
            //}), 10, 3);
#endif
        }


        public static void UpdateMuPlugin(string plugin_dir)
        {
            var muPluginFolder = new DirectoryInfo(Path.Combine(plugin_dir, "wpdotnet"));
            if (!muPluginFolder.Exists)
                return;

            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "wp-content", "mu-plugins");
            //TODO: create if it doesn't exist

            MoveFiles(muPluginFolder, path, "*.*");

        }

        private static void MoveFiles(DirectoryInfo destinationFolder, string targetFolder, string searchPattern)
        {
            foreach (var f in destinationFolder.GetFiles(searchPattern))
            {
                try
                {
                    f.CopyTo(Path.Combine(targetFolder, f.Name), true);
                    f.Delete();
                }
                catch
                {
                    // TODO: die('Update wp-content\mu-plugins permission'); // ?
                    Debug.Fail("");
                }
            }

            try
            {
                //delete mu-plugin folder from updater directory
                destinationFolder.Delete();
            }
            catch
            {

            }
        }


        public static void Update(string plugin_dir)
        {
            UpdateMuPlugin(plugin_dir);
            UpdateBin(plugin_dir);
            ForceRecompile();
        }

        public static void UpdateBin(string plugin_dir)
        {
            var binsrc = new DirectoryInfo(Path.Combine(plugin_dir, "Bin"));
            if (!binsrc.Exists)
                return;

            MoveFiles(binsrc, HttpRuntime.BinDirectory, "*.dll");
                        
        }

        private static void ForceRecompile()
        {

            // touch web.config to force Phalanger to recompile SSAs
            try
            {
                var webconfig_fi = new FileInfo(Path.Combine(HttpRuntime.AppDomainAppPath, "web.config"));
                if (webconfig_fi.Exists)
                    webconfig_fi.LastWriteTimeUtc = DateTime.UtcNow;
            }
            catch
            {
                // TODO: die('Delete ASP.NET Temp folder manually');
            }
        }
    }
}
