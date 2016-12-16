using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public interface IColumn
    {
        String Identifier { get; }

        ColumnPropertyAttribute Column { get; }
        ColumnAllowNullAttribute AllowNull { get; }
        ColumnAutoValueAttribute AutoValue { get; }
        ColumnIndexAttribute Index { get; }
        ValueTypeAttribute ValueType { get; }

        Plink.PropertyDefinition Property { get; }
        ITable Model { get; }
    }
}
