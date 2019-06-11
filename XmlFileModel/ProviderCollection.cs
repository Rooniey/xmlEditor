using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "providers")]
    public class ProviderCollection
    {
        [XmlElement(ElementName = "provider")]
        public List<Provider> Collection { get; set; }
    }
}
