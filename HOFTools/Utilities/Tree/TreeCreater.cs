using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFTools
{
    public class TreeCreater
    {
        public XmlTreeNode Create(DataTableCollection dataTables)
        {
            XmlTreeNode root = new XmlTreeNode()
            {
                Name = "Root",
                IsExpanded = true
            };

            foreach (DataTable table in dataTables)
            {
                root.Children.Add(new XmlTreeNode()
                {
                    Name = table.TableName,
                    XmlDataTable = table,
                    IsExpanded = true
                });
            }

            return root;
        }
    }
}
