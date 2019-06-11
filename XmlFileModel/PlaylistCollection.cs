using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "playlists")]
    public class PlaylistCollection
    {
        [XmlElement(ElementName = "playlist")]
        public List<Playlist> Collection { get; set; }
    }
}
