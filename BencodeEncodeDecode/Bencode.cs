using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BencodeEncodeDecode
{
    class Bencode
    {
        public FileStream file             { get; set; }
        public DotTorrent.Torrent t        { get; set; }
        public BNode root                  { get; set; }

        public Bencode(FileStream _file)
        {
            file = _file;
            t = new DotTorrent.Torrent();
        }

        public void Parse()
        {
            root = new BNode(new List<BNode>(), null);
            int readByte = -1;
            char character;
            BNode temp = root;
            ParentType tempType = ParentType.LIST;
            string tempKey = null;
            while ((readByte = file.ReadByte()) != -1)
            {
                character = (char)readByte;
                switch (tempType)
                {
                    case (ParentType.LIST):
                        if (character == 'd')
                        {
                            //Console.WriteLine("Dictionary Made");
                            BNode dictionary = new BNode(new Dictionary<string, BNode>(), temp);
                            temp.ldata.Add(dictionary);
                            temp = dictionary;
                            tempType = ParentType.DICTKEY;
                        }
                        else if (character == 'l')
                        {
                            ////Console.WriteLine("List Made");
                            BNode list = new BNode(new List<BNode>(), temp);
                            temp.ldata.Add(list);
                            temp = list;
                            tempType = ParentType.LIST;
                        }
                        else if (character == 'i')
                        {
                            long integer = 0;
                            bool negative = false;
                            readByte = file.ReadByte();
                            character = (char)readByte;
                            if (character == '-')
                            {
                                negative = true;
                                readByte = file.ReadByte();
                                character = (char)readByte;
                            }
                            else if (character == '0')
                            {
                                readByte = file.ReadByte();
                                character = (char)readByte;
                                if (character != 'e') ;
                                //Throw exception
                                else
                                {
                                    temp.ldata.Add(new BNode(0, temp));
                                    break;
                                }
                            }
                            do
                            {
                                integer += character - '0';
                                integer *= 10;
                                readByte = file.ReadByte();
                                character = (char)readByte;
                            }
                            while (character != 'e');
                            integer /= 10;
                            if (negative)
                                integer *= -1;
                            ////Console.WriteLine("Integer Made : {0}", integer);
                            temp.ldata.Add(new BNode(integer, temp));
                        }
                        else if (character == 'e')
                        {
                            temp = temp.parent;
                            ////Console.WriteLine("List Closed");
                            tempType = (temp.type == BNType.DICTIONARY) ? ParentType.DICTKEY : ParentType.LIST;
                        }
                        else if (char.IsNumber(character))
                        {
                            int length = 0;
                            do
                            {
                                length += character - '0';
                                length *= 10;
                                readByte = file.ReadByte();
                                character = (char)readByte;
                            }
                            while (character != ':');
                            length /= 10;
                            char[] array = new char[length];
                            for (int count = 0; count < length; count++)
                            {
                                readByte = file.ReadByte();
                                array[count] = (char)readByte;
                            }
                            string str = new string(array);
                            //Console.WriteLine("String Made : {0}", str);
                            temp.ldata.Add(new BNode(str, temp));
                        }
                        else
                        {
                            //Throw exception
                        }
                        break;
                    case (ParentType.DICTKEY):
                        if (char.IsNumber(character))
                        {
                            int length = 0;
                            do
                            {
                                length += character - '0';
                                length *= 10;
                                readByte = file.ReadByte();
                                character = (char)readByte;
                            }
                            while (character != ':');
                            length /= 10;
                            char[] array = new char[length];
                            for (int count = 0; count < length; count++)
                            {
                                readByte = file.ReadByte();
                                array[count] = (char)readByte;
                            }
                            tempKey = new string(array);
                            ////Console.WriteLine("String Made : {0}", tempKey);
                            tempType = ParentType.DICTVALUE;
                        }
                        else if (character == 'e')
                        {
                            temp = temp.parent;
                            //Console.WriteLine("Dictionary Closed");
                            tempType = (temp.type == BNType.DICTIONARY) ? ParentType.DICTKEY : ParentType.LIST;
                        }
                        else
                        {
                            //Throw exception
                        }
                        break;
                    case (ParentType.DICTVALUE):
                        if (character == 'd')
                        {
                            //Console.WriteLine("Dictionary Paired");
                            BNode dictionary = new BNode(new Dictionary<string, BNode>(), temp);
                            temp.ddata.Add(tempKey, dictionary);
                            tempKey = null;
                            temp = dictionary;
                            tempType = ParentType.DICTKEY;
                        }
                        else if (character == 'l')
                        {
                            //Console.WriteLine("List Paired");
                            BNode list = new BNode(new List<BNode>(), temp);
                            temp.ddata.Add(tempKey, list);
                            tempKey = null;
                            temp = list;
                            tempType = ParentType.LIST;
                        }
                        else if (character == 'i')
                        {
                            long integer = 0;
                            bool negative = false;
                            readByte = file.ReadByte();
                            character = (char)readByte;
                            if (character == '-')
                            {
                                negative = true;
                                readByte = file.ReadByte();
                                character = (char)readByte;
                            }
                            else if (character == '0')
                            {
                                readByte = file.ReadByte();
                                character = (char)readByte;
                                if (character != 'e') ;
                                //Throw exception
                                else
                                {
                                    temp.ddata.Add(tempKey, new BNode(0, temp));
                                    tempKey = null;
                                    tempType = ParentType.DICTKEY;
                                    break;
                                }
                            }
                            do
                            {
                                integer += character - '0';
                                integer *= 10;
                                readByte = file.ReadByte();
                                character = (char)readByte;
                            }
                            while (character != 'e');
                            integer /= 10;
                            if (negative)
                                integer *= -1;
                            temp.ddata.Add(tempKey, new BNode(integer, temp));
                            //Console.WriteLine("Integer Paired : {0}", integer);
                            tempKey = null;
                            tempType = ParentType.DICTKEY;
                        }
                        else if (char.IsNumber(character))
                        {
                            int length = 0;
                            do
                            {
                                length += character - '0';
                                length *= 10;
                                readByte = file.ReadByte();
                                character = (char)readByte;
                            }
                            while (character != ':');
                            length /= 10;
                            char[] array = new char[length];
                            for (int count = 0; count < length; count++)
                            {
                                readByte = file.ReadByte();
                                array[count] = (char)readByte;
                            }
                            string str = new string(array);
                            //Console.WriteLine("String Paired : {0}", str);
                            temp.ddata.Add(tempKey, new BNode(str, temp));
                            tempKey = null;
                            tempType = ParentType.DICTKEY;
                        }
                        else
                        {
                            //Throw exception
                        }
                        break;
                    default:
                        //Throw Exception
                        break;
                }
            }
            //Console.WriteLine();
            //Console.ReadKey();
        }

        public void createDotTorrent()
        {
            BNode head = root.ldata[0];
            foreach (string key in head.ddata.Keys)
            {
                switch (key)
                {
                    case "announce":
                        t.announce.link = (head.ddata[key].sdata);
                        break;

                    case "announce-list":
                        foreach (BNode outer in head.ddata[key].ldata)
                        {
                            if (outer.type == BNType.LIST)
                            {
                                List<DotTorrent.Link> temp = new List<DotTorrent.Link>();
                                foreach (BNode element in outer.ldata)
                                    temp.Add(new DotTorrent.Link(element.sdata));
                                t.announceList.Add(temp);
                            }
                        }
                        t.announceListExists = true;
                        break;

                    case "comment":
                        t.comment = head.ddata[key].sdata;
                        t.commentExists = true;
                        break;

                    case "created by":
                        t.createdBy = head.ddata[key].sdata;
                        t.createdByExists = true;
                        break;

                    case "creation date":
                        t.creationDate = new DotTorrent.Date(head.ddata[key].idata);
                        t.creationDateExists = true;
                        break;

                    case "encoding":
                        t.createdBy = head.ddata[key].sdata;
                        t.encodingExists = true;
                        break;

                    case "info":
                        BNode inf = head.ddata[key];

                        if (inf.ddata.ContainsKey("files"))
                            t.fileMode = DotTorrent.FileMode.MultiFileMode;
                        else
                            t.fileMode = DotTorrent.FileMode.SingleFileMode;

                        foreach (string k in inf.ddata.Keys)
                        {
                            switch (k)
                            {
                                case "name":
                                    if (t.fileMode == DotTorrent.FileMode.MultiFileMode)
                                        t.rootPath = inf.ddata[k].sdata;
                                    else
                                        t.rootPath = "SIngle file mode Path goes here!!";
                                    break;

                                case "piece length":
                                    t.pieceLength = inf.ddata[k].idata;
                                    break;

                                case "pieces":
                                    t.pieces = inf.ddata[k].sdata;
                                    break;

                                case "files":
                                    foreach (BNode fl in inf.ddata[k].ldata)
                                    {
                                        DotTorrent.DataFile df = new DotTorrent.DataFile();
                                        foreach (string ki in fl.ddata.Keys)
                                        {
                                            switch (ki)
                                            {
                                                case "length":
                                                    df.length = fl.ddata[ki].idata;
                                                    break;

                                                case "path":
                                                    string fullpath = null;
                                                    foreach (BNode s in fl.ddata[ki].ldata)
                                                        fullpath += ("\\" + s.sdata);
                                                    df.path = fullpath;
                                                    df.pathExists = true;
                                                    break;
                                            }
                                        }
                                        t.files.Add(df);
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        public void display()
        {
            displayWrapped(root, 0);
        }

        private void displayWrapped(BNode root, int level = 0)
        {
            Console.Write(string.Concat(Enumerable.Repeat("\t", level)));
            switch (root.type)
            {
                case BNType.DICTIONARY:
                    Console.WriteLine("Dictionary");
                    Console.Write(string.Concat(Enumerable.Repeat("\t", level)));
                    Console.WriteLine("{");
                    Console.Write(string.Concat(Enumerable.Repeat("\t", ++level)));
                    foreach (string key in root.ddata.Keys)
                    {
                        Console.WriteLine("Key : {0}", key);
                        Console.Write(string.Concat(Enumerable.Repeat("\t", level)));
                        Console.WriteLine("Value : ");
                        displayWrapped(root.ddata[key], level + 1);
                        Console.Write(string.Concat(Enumerable.Repeat("\t", level)));
                    }
                    Console.WriteLine();
                    Console.Write(string.Concat(Enumerable.Repeat("\t", level-1)));
                    Console.WriteLine("}");
                    break;
                case BNType.LIST:
                    Console.WriteLine("List");
                    Console.Write(string.Concat(Enumerable.Repeat("\t", level)));
                    Console.WriteLine("{");
                    foreach (BNode element in root.ldata)
                        displayWrapped(element, level + 1);
                    Console.Write(string.Concat(Enumerable.Repeat("\t", level)));
                    Console.WriteLine("}");
                    break;
                case BNType.INTEGER:
                    Console.WriteLine("Int : {0}", root.idata);
                    break;
                case BNType.STRING:
                    Console.WriteLine("String : {0}", root.sdata);
                    break;
                default:
                    Console.WriteLine("Error Unknown type!!");
                    break;
            }
        }
    }
}