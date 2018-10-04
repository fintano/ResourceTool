using NUnit.Framework;
using ResourceTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResourceToolTests
{
    [TestFixture]
    class TreeCreaterTests
    {

        [Test]
        public void Create_GivenDataTables_MakeTree()
        {
            TreeCreater treeCreater = new TreeCreater();
            TreeNode Root = treeCreater.Create();
            Assert.NotNull(Root.Nodes["Test"]);
        }
    }
}
