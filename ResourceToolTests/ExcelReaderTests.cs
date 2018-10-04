using System;
using NUnit.Framework;
using System.Data;
using System.IO;
using HOFTools;

namespace ResourceToolTests
{
    [TestFixture]
    public class ExcelReaderTests
    {
        readonly string dirPath = @"D:\HandOfFate2\";
        readonly string xsdFilePath = @"D:\HandOfFate2\Xsd\Test.xsd";
        readonly string xsdDirPath = @"D:\HandOfFate2\Xsd\";
        readonly string xmlFilePath = @"D:\HandOfFate2\Xml\Test.xml";

        XmlReader reader;
        XmlWriter writer;

        public ExcelReaderTests()
        {
            reader = new XmlReader();
            writer = new XmlWriter();
        }

        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Write_GivenDataTable_WriteXmlSchema()
        {
            DataTable dataTable = MakeDataTable();
            writer.WriteSchema(xsdFilePath, dataTable);

            Assert.True(File.Exists(xsdFilePath));
        }

        [Test]
        public void Write_GivenData_WriteXml()
        {
            DataTable dataTable = MakeDataTable();
            writer.Write(xmlFilePath, dataTable);
            Assert.True(File.Exists(xmlFilePath));
        }

        [Test]
        public void Read_GivenXsdFile_ReadXmlSchema()
        {
            DataTable dataTable = reader.ReadSchema(xsdFilePath);
            DataColumn firstCol = dataTable.Columns[0];

            Assert.AreEqual("CustomerId", firstCol.ColumnName);
        }

        [Test]
        public void Read_GivenData_ReadXml()
        {
            DataTable dataTable = reader.ReadSchema(xsdFilePath);
            reader.Read(xmlFilePath, dataTable);
            int firstCustomerId = (int)dataTable.Rows[0]["CustomerId"];

            Assert.AreEqual(1, firstCustomerId);
        }

        [Test]
        public void ReadAll_GivenXsdFiles_ReadXmlAll()
        {
            DataStore store = new DataStore(dirPath);
            store.ReadXsdAll();
            DataTableCollection dataTables = store.DataSet.Tables;
            Assert.AreEqual(1, dataTables.Count);
        }

        [Test]
        public void ReadXml_GivenName_ReadSpecificXmlFile()
        {
            DataStore store = new DataStore(dirPath);
            store.ReadXsdAll();
            store.ReadXml("Test");
            int firstCustomerId = (int)store.DataSet.Tables["Test"].Rows[0]["CustomerId"];

            Assert.AreEqual(1, firstCustomerId);
        }

        [Test]
        public void WriteXml_GivenDataTable_WriteSpecificXmlFile()
        {
            DataStore store = new DataStore(dirPath);
            DataTable dataTable = MakeDataTable();
            store.WriteXml(dataTable);
            Assert.True(File.Exists(xmlFilePath));
        }

        DataTable MakeDataTable()
        {
            DataTable dataTable = new DataTable
            {
                TableName = "Test",
                Columns =
                {
                new DataColumn {
                    ColumnName = "CustomerId",
                    DataType = typeof(int),
                    AutoIncrement = true,
                    AutoIncrementStep = 10,
                    AutoIncrementSeed = 1,
                    Unique = true
                    },
                new DataColumn {
                    ColumnName = "Firstname",
                    DataType = typeof(string)
                    },
                new DataColumn {
                    ColumnName = "Lastname",
                    DataType = typeof(string)
                    }
                }
            };

            DataRow row = dataTable.NewRow();
            row["CustomerId"] = 1;
            row["Firstname"] = "JH";
            row["Lastname"] = "Lee";
            dataTable.Rows.Add(row);

            return dataTable;
        }
    }
}
