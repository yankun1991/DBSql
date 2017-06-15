using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelp.Attr
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class TableAttribute: Attribute
    {
        internal string TableName;

        public TableAttribute(string tableName)
        {
            this.TableName = tableName;    
        }
    }
}
