using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "documentInfo")]
    public class DocumentInfo
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "authors")]
        public AuthorCollection Authors { get; set; }

    }
}
