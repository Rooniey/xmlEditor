using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "positions")]
    public class SeriesCollection
    {
        [XmlElement(ElementName = "series")]
        public List<Series> Collection { get; set; }
    }
}
