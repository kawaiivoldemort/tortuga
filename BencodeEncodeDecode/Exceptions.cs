using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BencodeEncodeDecode
{
    public class CorruptTorrentFileException : Exception
    {
        public CorruptTorrentFileException()
            : base()
        { }

        public CorruptTorrentFileException(string message)
            : base(message)
        { }

        public CorruptTorrentFileException(string format, params object[] args)
            : base(string.Format(format, args))
        { }

        public CorruptTorrentFileException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public CorruptTorrentFileException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        { }

        public CorruptTorrentFileException(string message,FileStream file)
            : base(message)
        {
            int maxLengthOfChars = 25;
            int character = -1;
            Console.WriteLine("\nName : " + file.Name);
            Console.WriteLine("Position : " + file.Position);
            Console.WriteLine("At : " + (char)file.ReadByte());
            Console.Write("Near : ");
            if (file.Position > (int)(maxLengthOfChars / 2))
                file.Seek(-(int)(maxLengthOfChars / 2), SeekOrigin.Current);
            else
                file.Seek(0, SeekOrigin.Begin);
            for (int i = 1; (i <= maxLengthOfChars) && (character = file.ReadByte()) != -1; i++)
                Console.Write((char)character);
            Console.WriteLine();
        }
    }
}
