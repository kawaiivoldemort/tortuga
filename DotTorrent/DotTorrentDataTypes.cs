using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DotTorrent
{
    class Link
    {
        public string link          { get; set; }
        public string info          { get; set; }

        public Link(string link,string info)
        {
            this.link = link;
            this.info = info;
        }

        public Link(string link)
        {
            this.link = link;
        }

        public Link()
        {
        }

        public override string ToString()
        {
            string str = null;
            string l;
            string i;
            l = (link == null) ? "NULL" : link;
            i = (info == null) ? "NULL" : info;
            str = "Link : " + l + "\nInfo : " + i;
            return str;
        }
    }

    class Date
    {
        public long epochTime       { get; set; }
        public DateTime date        { get; set; }

        public Date(long epochTime, DateTime date)
        {
            this.epochTime = epochTime;
            this.date = date;
        }

        public Date(long epochTime)
        {
            this.epochTime = epochTime;
            date = new DateTime(1970, 1, 1, 0, 0, 0);
            date = date.AddSeconds(epochTime);
        }

        public Date(DateTime date)
        {
            this.date = date;
            this.epochTime = date.Ticks;
        }

        public Date()
        {
        }

        public override string ToString()
        {
            string str = null;
            string d;
            if (date == null)
                d = "NULL";
            else
                d = date.ToString();
            str = "EpochTime : " + epochTime + "\nDate : " + d;
            return str;
        }

    }

    class DataFile
    {
        public string fileName      { get; set; }
        public long length          { get; set; }
        public string md5sum        { get; set; }

        private string _path;
        public string path          
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                _path = _path.TrimEnd(Path.DirectorySeparatorChar);
                if (fileName == null)
                    fileName = path.Substring(_path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            }

        }

        public Boolean md5sumExists  { get; set; }
        public Boolean pathExists    { get; set; }          //may not exist in single-file-mode

        public DataFile(string fileName,int length,string md5sum,string path)
        {
            this.fileName = fileName;
            this.length = length;
            this.md5sum = md5sum;
            this.path = path;
        }

        public DataFile(string fileName, int length, string path)
        {
            this.fileName = fileName;
            this.length = length;
            this.path = path;
        }

        public DataFile(string fileName, int length)
        {
            this.fileName = fileName;
            this.length = length;
        }

        public DataFile()
        {
        }

        public override string ToString()
        {
            string str = null;
            string f;
            long l;
            string m;
            string p;

            f = (fileName == null) ? "NULL" : fileName;
            l = length;
            m = (md5sum == null) ? "NULL" : md5sum;
            p = (path == null) ? "NULL" : path;

            str = "File Name : " + f + "\nLength : " + l + "\nmd5sum : " + m + "\nPath : " + p;
            return str;
        }
    }

    //class SHAHashs
    //{
    //    public List<string> hash            { get; set; }
    //    public int dataSize                 { get; set; }

    //    SHAHash()
    //    {
    //        hash = new List<string>();
    //        dataSize = 20;
    //    }

    //    SHAHash(string hashes)
    //    {
    //        dataSize = 20;
    //        for(i)
    //    }

    //    public Boolean validate(string other)
    //    {

    //    }
    //}


    enum FileMode
    {
        SingleFileMode,MultiFileMode
    }
}
