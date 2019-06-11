using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "authors")]
    public class AuthorCollection
    {
        [XmlElement(ElementName = "author")]
        public List<Author> Collection { get; set; }
    }
}
