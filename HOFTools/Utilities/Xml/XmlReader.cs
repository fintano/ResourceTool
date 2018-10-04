using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFTools
{
    public class XmlReader
    {
        public DataTable ReadSchema(string filePath)
        {
            DataTable dataTable = new DataTable();
            dataTable.ReadXmlSchema(filePath);
            return dataTable;
        }

        public void Read(string filePath, DataTable dataTable)
        {
            dataTable.Clear();
            if (File.Exists(filePath))
                dataTable.ReadXml(filePath);
        }
    }

    public class XmlWriter
    {
        public void WriteSchema(string filePath, DataTable dataTable)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                dataTable.WriteXmlSchema(stream);
            }
        }

        public void Write(string filePath, DataTable dataTable)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                dataTable.WriteXml(stream);
            }
        }
    }
}
