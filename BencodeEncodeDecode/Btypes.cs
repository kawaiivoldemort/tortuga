using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BencodeEncodeDecode
{
    enum BNType
    {
        STRING, DICTIONARY, INTEGER, LIST
    }

    enum ParentType
    {
        LIST, DICTKEY, DICTVALUE
    }
}
