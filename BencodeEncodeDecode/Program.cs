using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BencodeEncodeDecode
{ 
    class Program
    {
        static void Main(string[] args)
        {
            FileStream file = new FileStream("test.torrent", FileMode.Open);
            DotTorrent.Torrent t1 = new DotTorrent.Torrent();
            Bencode b = new Bencode(file);
            b.Parse();
            b.createDotTorrent();
            b.display();
            b.t.display();
        }
    }
}