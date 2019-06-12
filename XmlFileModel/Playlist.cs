using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "playlist")]
    public class Playlist
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "positions")]
        public SeriesCollection Series { get; set; }
    }
}
