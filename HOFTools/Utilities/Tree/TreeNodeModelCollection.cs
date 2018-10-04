using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFTools
{
    public class TreeNodeModelCollection : ObservableCollection<TreeNodeModel>
    {
        public TreeNodeModel this[string name]
        {
            get { return Items.Where(i => i.Name.Equals(name)).FirstOrDefault(); }
        }
    }
}
