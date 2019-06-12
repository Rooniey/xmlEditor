using System.IO;
using System.Xml.Serialization;

namespace XmlFileModel.Utility
{
    public static class XMLSerializer
    {
        public static Document Deserialize(string xmlFilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Document));

            using (StreamReader sr = new StreamReader(xmlFilePath))
            {
                return (Document)serializer.Deserialize(sr);
            }
        }

        public static void Serialize(Document xmlDocument, Stream outputStream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Document));
            serializer.Serialize(outputStream, xmlDocument);
        }
    }
}
