using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtonitRestApi.Annotation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DatabasePrimaryKeyAttribute : Attribute
    {
    }
}
