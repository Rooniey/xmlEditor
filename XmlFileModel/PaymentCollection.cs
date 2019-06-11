using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlFileModel
{
    [XmlRoot(ElementName = "monthlyPayment")]
    public class PaymentCollection
    {
        [XmlElement(ElementName = "payment")]
        public List<Payment> Collection { get; set; }
    }
}
