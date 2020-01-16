using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BencodeEncodeDecode
{
    class BNode
    {
        public BNType type;
        public BNode parent = null;
        public Dictionary<string, BNode> ddata = null;
        public List<BNode> ldata = null;
        public long idata = 0;
        public string sdata = null;

        public BNode(Dictionary<string, BNode> _data, BNode _parent)
        {
            type = BNType.DICTIONARY;
            ddata = _data;
            parent = _parent;
        }

        public BNode(List<BNode> _data, BNode _parent)
        {
            type = BNType.LIST;
            ldata = _data;
            parent = _parent;
        }

        public BNode(long _data, BNode _parent)
        {
            type = BNType.INTEGER;
            idata = _data;
            parent = _parent;
        }

        public BNode(string _data, BNode _parent)
        {
            type = BNType.STRING;
            sdata = _data;
            parent = _parent;
        }
    }
}