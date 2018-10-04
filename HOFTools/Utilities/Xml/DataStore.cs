using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFTools
{
    public class DataStore
    {
        public DataSet DataSet { get; set; }

        private XmlReader xmlReader;
        private XmlWriter xmlWriter;
        private string xmlDirPath;
        private string xsdDirPath;

        public DataStore(string dirPath)
        {
            xmlDirPath = string.Format("{0}\\{1}", dirPath, "Xml");
            xsdDirPath = string.Format("{0}\\{1}", dirPath, "Xsd");
            xmlReader = new XmlReader();
            xmlWriter = new XmlWriter();
            DataSet = new DataSet();
        }

        public void ReadXsdAll()
        {
            string[] filePaths = Directory.GetFiles(xsdDirPath, "*.xsd",
                                         SearchOption.AllDirectories);

            foreach (string filePath in filePaths)
            {
                DataTable newTable = xmlReader.ReadSchema(filePath);
                if (!DataSet.Tables.Contains(newTable.TableName))
                    DataSet.Tables.Add(newTable);
            }
        }

        public void ReadXml(string name)
        {
            DataTable dataTable = DataSet.Tables[name];
            string filePath = string.Format("{0}\\{1}.xml", xmlDirPath, name);
            xmlReader.Read(filePath, dataTable);
        }

        internal void WriteXsd(DataTable dataTable)
        {
            string filePath = string.Format("{0}\\{1}.xsd", xsdDirPath, dataTable.TableName);
            xmlWriter.WriteSchema(filePath, dataTable);
        }

        public void WriteXml(DataTable dataTable)
        {
            string filePath = string.Format("{0}\\{1}.xml", xmlDirPath, dataTable.TableName);
            xmlWriter.Write(filePath, dataTable);
        }
    }
}
