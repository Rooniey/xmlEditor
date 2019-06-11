using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "rottenTomatoes")]
    public class RottenTomatoesScore
    {
        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }
        [XmlText]
        public string Score { get; set; }
    }
}
