using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HOFTools
{
    class SchemaDataGridViewModel
    {
        readonly private string dirPath = Config.DirPath;

        private ObservableCollection<SchemaSpecification> _schema = new ObservableCollection<SchemaSpecification>();
        public ObservableCollection<SchemaSpecification> ColumnSchemas
        {
            get { return this._schema; }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new CommandHandler(() => Save()));
            }
        }

        public SchemaDataGridViewModel()
        {
            ColumnSchemas.Add(new SchemaSpecification()
            {
                ColumnName = "Id",
                Type = ColumnType.Int32,
                PrimaryKey = true,
            });
        }

        private void Save()
        {
            string tableName = string.Empty;
            while (tableName == "")
            {
                tableName = Microsoft.VisualBasic.Interaction.InputBox("테이블명을 입력하세요",
                                             "스키마",
                                             "",
                                             -1, -1);
            }

            DataTable dataTable = new DataTable(tableName);
            foreach(var columnSchema in ColumnSchemas)
            {
                dataTable.Columns.Add(new DataColumn(columnSchema.ColumnName, Type.GetType("System." + columnSchema.Type.ToString())));
            }

            DataStore store = new DataStore(dirPath);
            store.WriteXsd(dataTable);
            MessageBox.Show("저장 완료");
        }
    }
}
