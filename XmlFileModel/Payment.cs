using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "payment")]
    public class Payment
    {
        [XmlText]
        public string Cost { get; set; }

        [XmlAttribute(AttributeName = "currency")]
        public string Currency { get; set; }
    }
}
