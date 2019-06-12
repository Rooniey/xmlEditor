using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "seriesPlaylists")]
    public class Document
    {
        [XmlElement(ElementName = "documentInfo")]
        public DocumentInfo Info { get; set; }

        [XmlElement(ElementName = "providers")]
        public ProviderCollection Providers { get; set; }

        [XmlElement(ElementName = "playlists")]
        public PlaylistCollection Playlists { get; set; }
    }
}
