using ActiproSoftware.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HOFTools
{
    public class EditTreeViewModel : ObservableObjectBase
    {
        readonly private string dirPath = Config.DirPath;

        private DataStore store;
        private TreeNodeModel _root;
        private XmlTreeNode _selectedItem;

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
                ReadXml();
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new CommandHandler(() => Save()));
            }
        }

        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new CommandHandler(() => Refresh()));
            }
        }

        public EditTreeViewModel()
        {
            store = new DataStore(dirPath);
            Refresh();
        }

        private void ReadXml()
        {
            store.ReadXml(SelectedItem.XmlDataTable.TableName);
        }

        private void Save()
        {
            MessageBox.Show("저장 완료");
            store.WriteXml(SelectedItem.XmlDataTable);
        }

        private void Refresh()
        {
            store.ReadXsdAll();
            TreeCreater treeCreater = new TreeCreater();
            Root = treeCreater.Create(store.DataSet.Tables);
            Root.IsExpanded = true;
        }
    }
}
