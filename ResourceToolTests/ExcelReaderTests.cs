using System;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using ResourceTool;

namespace ResourceToolTests
{
    [TestFixture]
    public class ExcelReaderTests
    {   
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Read_GivenFourByFourArrayExcelFile_ReadCells()
        {
            ExcelReader excelReader = new ExcelReader(@"D:\HandOfFate2\Excels\Test.xlsx");
            Worksheet worksheet = excelReader.GetWorkSheet(0);
            int oneByOne = worksheet.Cells[1, 1];
            Assert.AreEqual(100, oneByOne);
        }
    }
}
