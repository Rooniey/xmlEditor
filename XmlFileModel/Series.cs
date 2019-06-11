using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "series")]
    public class Series
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "totalNumberOfEpisodes")]
        public string TotalNumberOfEpisodes { get; set; }

        [XmlElement(ElementName = "firstEpisodePremiere")]
        public string FirstEpisodePremiere { get; set; }

        [XmlElement(ElementName = "rottenTomatoes")]
        public RottenTomatoesScore RottenTomatoes { get; set; }

        [XmlAttribute(AttributeName = "provider")]
        public string Provider { get; set; }

        [XmlAttribute(AttributeName = "genre")]
        public string Genre { get; set; }
    }
}
