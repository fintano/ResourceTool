using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ResourceTool
{
    public class ExcelReader
    {
        private readonly string m_path;
        private Workbook m_workbook;

        public ExcelReader(string path)
        {
            m_path = path;
            Application app = new Application();
            m_workbook = app.Workbooks.Open(Filename: path);
        }

        public Worksheet GetWorkSheet(int index)
        {
            return m_workbook.Worksheets.Item[index];
        }
    }
}
