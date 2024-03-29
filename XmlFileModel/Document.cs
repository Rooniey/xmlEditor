﻿using System.Xml.Serialization;

namespace XmlFileModel
{

    //    [XmlRoot(ElementName = "seriesPlaylists")]
    [XmlRoot(ElementName = "seriesPlaylists", Namespace = "http://tempuri.org/SeriesPlaylist")]
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
