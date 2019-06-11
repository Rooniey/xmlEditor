using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XmlFileModel;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "../../../files/series_list.xml";
            string dest = "../../../files/output.xml";

            Document doc = ReadModel(source);
            doc.Info.Authors.Collection[0].FirstName = "Kupa";
            WriteModel(dest, doc);

            Console.WriteLine("DONE");
            Console.ReadKey();

        }

        public static Document ReadModel(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Document));

            using (StreamReader sr = new StreamReader(path))
            {
                return (Document)serializer.Deserialize(sr);

            }
        }

        public static void WriteModel(string path, Document doc)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Document));

            using (StreamWriter sw = new StreamWriter(path))
            {
                serializer.Serialize(sw, doc);
            }
        }
    }

    
}
