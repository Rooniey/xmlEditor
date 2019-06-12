using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "provider")]
    public class Provider
    {
        [XmlAttribute(AttributeName = "provider_id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "monthlyPayment")]
        public PaymentCollection MonthlyPayment { get; set; }

    }
}
