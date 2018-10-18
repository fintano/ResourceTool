using System.Configuration;

namespace HOFTools
{
    public static class Config
    {
        public static string DirPath { get; set; }

        public static void GetConfigs()
        {
            DirPath = ConfigurationManager.AppSettings["root"];
        }
    }
}
