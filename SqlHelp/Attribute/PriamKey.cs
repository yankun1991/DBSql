using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelp.Attr
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class PriamKeyAttribute: Attribute
    {
      
    }
}
