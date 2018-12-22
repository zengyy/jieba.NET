using System;
using System.Configuration;
using System.IO;

namespace JiebaNet.Analyser
{
    public class ConfigManager
    {
        // TODO: duplicate codes.
        public static string ConfigFileBaseDir
        {
            get
            {
#if !(NETSTANDARD1_0 || NETSTANDARD2_0)
                var configFileDir = ConfigurationManager.AppSettings["JiebaConfigFileDir"];
                if (configFileDir == null)
                {
                    // 如果没有配置词库的目录则取当前dll所在的目录下的Dict_Jieba为词库目录
                    var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                    var uri = new UriBuilder(codeBase);
                    var path = Uri.UnescapeDataString(uri.Path);
                    configFileDir = Path.Combine(Path.GetDirectoryName(path), "Dict_Jieba");
                }

                if (!Path.IsPathRooted(configFileDir))
                {
                    var domainDir = AppDomain.CurrentDomain.BaseDirectory;
                    configFileDir = Path.GetFullPath(Path.Combine(domainDir, configFileDir));
                }
#else
                var configFileDir = "Resources";
#endif
                return configFileDir;
            }
        }

        public static string IdfFile
        {
            get { return Path.Combine(ConfigFileBaseDir, "idf.txt"); }
        }

        public static string StopWordsFile
        {
            get { return Path.Combine(ConfigFileBaseDir, "stopwords.txt"); }
        }
    }
}
