using HOFTools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceToolTests
{
    [TestFixture]
    class ConfigTests
    {
        [Test]
        public void Config_AtStarting_GetRootFolder()
        {
            Config.GetConfigs();
            string dirPath = Config.DirPath;
            Assert.AreEqual(@"D:\HandOfFate2", dirPath);
        }
    }
}
