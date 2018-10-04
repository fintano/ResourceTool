using HOFTools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResourceToolTests
{
    [TestFixture]
    class TreeTests
    {
        readonly string dirPath = @"D:\HandOfFate2\";

        [Test]
        public void Create_GivenDataTables_CreateTree()
        {
            TreeCreater treeCreater = new TreeCreater();
            DataStore store = new DataStore(dirPath);
            store.ReadXsdAll();
            XmlTreeNode root = treeCreater.Create(store.DataSet.Tables);
            Assert.NotNull(root.Children["Test"]);
        }
    }
}
