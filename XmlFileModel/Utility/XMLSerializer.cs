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
    }
}
