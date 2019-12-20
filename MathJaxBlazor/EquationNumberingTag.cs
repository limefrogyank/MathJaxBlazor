using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MathJaxBlazor
{
    
    public enum EquationNumberingTag
    {
        [EnumMember(Value = "none")]
        None,
        [EnumMember(Value = "ams")]
        Ams,
        [EnumMember(Value = "all")]
        All
    }
}
