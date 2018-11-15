using ActiproSoftware.Windows;
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
    class SchemaDataGridViewModel : ObservableObjectBase
    {
        readonly private string dirPath = Config.DirPath;

        private DataStore store;
        private TreeNodeModel _root;
        private XmlTreeNode _selectedItem;
        private string tableName;

        public TreeNodeModel Root
        {
            get { return _root; }
            set { _root = value; NotifyPropertyChanged("Root"); }
        }

        public XmlTreeNode SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
                ReadXsd();
            }
        }

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

        private ICommand _newBtnCommand;
        public ICommand NewBtnCommand
        {
            get
            {
                return _newBtnCommand ?? (_newBtnCommand = new CommandHandler(() => New()));
            }
        }

        public SchemaDataGridViewModel()
        {
            store = new DataStore(dirPath);
            Refresh();
            Init();
        }

        private void Init()
        {
            tableName = "";
            ColumnSchemas.Clear();
            ColumnSchemas.Add(new SchemaSpecification()
            {
                ColumnName = "Id",
                Type = ColumnType.Int32,
                PrimaryKey = true,
            });
        }

        public void ReadXsd()
        {
            tableName = SelectedItem.XmlDataTable.TableName;
            var table = store.DataSet.Tables[tableName];

            ColumnSchemas.Clear();

            foreach(DataColumn column in table.Columns)
            {
                var tokens = column.DataType.ToString().Split('.');
                ColumnSchemas.Add(new SchemaSpecification
                {
                    ColumnName = column.ColumnName,
                    Type = (ColumnType)Enum.Parse(typeof(ColumnType), tokens[1])
                });
            }
        }

        private void Save()
        {
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

            store.WriteXsd(dataTable);
            MessageBox.Show("저장 완료");
        }

        private void Refresh()
        {
            store.ReadXsdAll();
            TreeCreater treeCreater = new TreeCreater();
            Root = treeCreater.Create(store.DataSet.Tables);
            Root.IsExpanded = true;
        }

        private void New()
        {
            Init();
        }
    }
}
