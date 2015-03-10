using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Codentia.Common.Helper
{
    /// <summary>
    /// Helper Methods for Sites
    /// </summary>
    public static class SiteHelper
    {
        private static string _siteEnvironment = ConfigurationManager.AppSettings["SiteEnvironment"];
      
        /// <summary>
        /// Gets the site environment.
        /// </summary>
        /// <value>The site environment.</value>
        public static string SiteEnvironment
        {
            get
            {
                string environment;

                if (_siteEnvironment == null)
                {
                    _siteEnvironment = string.Empty;
                }

                switch (_siteEnvironment.ToUpper())
                {
                    case "DEV":
                        environment = "DEV";
                        break;
                    case "TEST":
                        environment = "TEST";
                        break;
                    case "UAT":
                        environment = "UAT";
                        break;
                    case "LIVE":
                    case "DEMO":
                        environment = "LIVE";
                        break;
                    default:
                        environment = "DEV";
                        break;
                }

                return environment;
            }
        }

        /// <summary>
        /// Gets the payment environment.
        /// </summary>
        /// <value>The payment environment.</value>
        public static string PaymentEnvironment
        {
            get
            {
                string environment;

                if (_siteEnvironment == null)
                {
                    _siteEnvironment = string.Empty;
                }

                switch (_siteEnvironment.ToUpper())
                {
                    case "DEV":
                        environment = "DEV";
                        break;
                    case "TEST":
                        environment = "TEST";
                        break;
                    case "UAT":
                        environment = "UAT";
                        break;
                    case "LIVE":
                        environment = "LIVE";
                        break;
                    case "DEMO":
                        environment = "DEMO";
                        break;
                    default:
                        environment = "DEV";
                        break;
                }

                return environment;
            }
        }

        /// <summary>
        /// Sets the base environment.
        /// </summary>
        /// <value>The base environment.</value>
        public static string BaseEnvironment
        {
            set
            {
                _siteEnvironment = value;
            }
        }

        /// <summary>
        /// Builds the site map.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="basePath">The base path.</param>
        /// <param name="startPath">The start path.</param>
        /// <param name="extensions">The extensions.</param>
        /// <param name="fileExclusions">The file exclusions.</param>
        /// <param name="directoryExclusions">The directory exclusions.</param>
        /// <returns>string of sitemap</returns>
        public static string BuildSiteMap(string baseUrl, string basePath, string startPath, string[] extensions, string[] fileExclusions, string[] directoryExclusions)
        {
            StringBuilder output = new StringBuilder();

            output.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
            SiteHelper.BuildSiteMapRecursive(output, baseUrl, basePath, startPath, extensions, fileExclusions, directoryExclusions);
            output.Append("</urlset>");

            return output.ToString();
        }

        /// <summary>
        /// Builds the site map recursively.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="basePath">The base path.</param>
        /// <param name="startPath">The start path.</param>
        /// <param name="extensions">The extensions.</param>
        /// <param name="fileExclusions">The file exclusions.</param>
        /// <param name="directoryExclusions">The directory exclusions.</param>
        private static void BuildSiteMapRecursive(StringBuilder output, string baseUrl, string basePath, string startPath, string[] extensions, string[] fileExclusions, string[] directoryExclusions)
        {
            // output all files in base path that match the given pattern (aspx, html)
            List<string> fileList = new List<string>();
            for (int i = 0; i < extensions.Length; i++)
            {
                string[] newFiles = System.IO.Directory.GetFiles(startPath, extensions[i]);
                fileList.AddRange(newFiles);
            }

            string[] files = fileList.ToArray();

            string displayPath = startPath.Replace(basePath, string.Empty);
            if (!displayPath.EndsWith("/"))
            {
                displayPath = string.Format("{0}/", displayPath);
            }

            // handle separators
            displayPath = displayPath.Replace("\\", "/");

            for (int i = 0; i < files.Length; i++)
            {
                string name = Path.GetFileName(files[i]);
                string modified = File.GetLastWriteTime(files[i]).ToString("yyyy-MM-dd");

                // check exclusions
                bool excluded = false;

                for (int j = 0; j < fileExclusions.Length && !excluded; j++)
                {
                    excluded = name.ToLower().Contains(fileExclusions[j].ToLower());
                }

                if (!excluded)
                {
                    string entry = string.Format("<url><loc>{0}{1}{2}</loc><lastmod>{3}</lastmod></url>", baseUrl, displayPath, name, modified);
                    entry = entry.Replace("//", "/");
                    entry = entry.Replace("\\", string.Empty);
                    entry = entry.Replace(":/", "://");
                    output.Append(entry);
                }
            }

            // then do the same in all sub folders, excluding user
            string[] folders = System.IO.Directory.GetDirectories(startPath);
            for (int i = 0; i < folders.Length; i++)
            {
                bool excluded = false;
                for (int j = 0; j < directoryExclusions.Length && !excluded; j++)
                {
                    if (folders[i].ToLower().EndsWith(string.Format("{0}{1}", Path.DirectorySeparatorChar, directoryExclusions[j].ToLower())))
                    {
                        excluded = true;
                    }
                }

                if (!excluded)
                {
                    BuildSiteMapRecursive(output, baseUrl, basePath, folders[i], extensions, fileExclusions, directoryExclusions);
                }
            }
        }
    }
}
