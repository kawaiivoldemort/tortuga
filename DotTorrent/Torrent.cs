using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotTorrent
{
    class Torrent
    {
        public Link announce                    { get; set; }
        public List<List<Link>> announceList    { get; set; }
        public Date creationDate                { get; set; }
        public string comment                   { get; set; }
        public string createdBy                 { get; set; }
        public string encoding                  { get; set; }

        public long pieceLength                 { get; set; }
        public string pieces                    { get; set; }                         //check datatype
        public string rootPath                  { get; set; }                       //path of the directory in which to store all the files . Confusion for multi and single file modes.CHECK!!
        public List<DataFile> files             { get; set; }

        public FileMode fileMode                { get; set; }

        public Boolean announceListExists       { get; set; }
        public Boolean creationDateExists       { get; set; }
        public Boolean commentExists            { get; set; }
        public Boolean createdByExists          { get; set; }
        public Boolean encodingExists           { get; set; }
        public Boolean ispublic                 { get; set; }        //this is different from publicExists...CHECK
        public Boolean rootPathExists           { get; set; }

        public Torrent()
        {
            announce = new Link();
            announceList = new List<List<Link>>();
            creationDate = new Date();
            files = new List<DataFile>();
            fileMode = new FileMode();
        }

        public void display()
        {
            if (announce != null)
                Console.WriteLine("Annonce : \n{0}", announce);
            else
                Console.WriteLine("Annonce : NULL");
            Console.WriteLine();

            if (announceList != null)
            {
                Console.WriteLine("Announce List : ");
                foreach (List<Link> outer in announceList)
                    foreach (Link inner in outer)
                        Console.WriteLine(inner);
            }
            else
                Console.WriteLine("AnnonceList : NULL");
            Console.WriteLine();

            if (creationDate != null)
                Console.WriteLine("Creation Date : \n{0}", creationDate);
            else
                Console.WriteLine("Creation Date : NULL");
            Console.WriteLine();

            if (comment != null)
                Console.WriteLine("Comment : \n{0}", comment);
            else
                Console.WriteLine("Comment : NULL");
            Console.WriteLine();

            if (createdBy != null)
                Console.WriteLine("Created By : \n{0}", createdBy);
            else
                Console.WriteLine("Created By : NULL");
            Console.WriteLine();

            if (encoding != null)
                Console.WriteLine("Encoding : \n{0}", encoding);
            else
                Console.WriteLine("Encoding : NULL");
            Console.WriteLine();

            if (pieces != null)
                Console.WriteLine("Pieces : \n{0}", pieces);
            else
                Console.WriteLine("Pieces : NULL");
            Console.WriteLine();

            if (rootPath != null)
                Console.WriteLine("Root Path : \n{0}", rootPath);
            else
                Console.WriteLine("Root Path : NULL");
            Console.WriteLine();

            if (files != null)
            {
                Console.WriteLine("Files : ");
                foreach (DataFile fd in files)
                    Console.WriteLine(fd + "\n");
            }
            else
                Console.WriteLine("Files : NULL");
            Console.WriteLine();

            Console.WriteLine("File Mode : \n{0}", fileMode);
            Console.WriteLine();
            Console.WriteLine("Piece Length : \n{0}", pieceLength);
            Console.WriteLine();
        }
    }
}