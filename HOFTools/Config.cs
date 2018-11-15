using System.Configuration;
using System.Windows;

namespace HOFTools
{
    public static class Config
    {
        public static string DirPath { get; set; }

        public static void GetConfigs()
        {
            DirPath = ConfigurationManager.AppSettings["root"];

            if (DirPath == null)
                MessageBox.Show("HOFRoot 설정이 되어있지 않습니다. Config 파일에 root 키를 설정해주세요.");
        }
    }
}
