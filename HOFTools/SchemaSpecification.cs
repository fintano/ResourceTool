using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFTools
{
    class SchemaSpecification
    {
        public string ColumnName { get; set; }
        public ColumnType Type { get; set; }
        public bool PrimaryKey { get; set; }
    }

    public enum ColumnType
    {
        Char,
        Int16,
        Int32,
        Int64,
        Double,
        String,
        Boolean,
    }
}
