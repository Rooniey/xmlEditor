using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Schema;

namespace XmlFileModel.Utility
{
    public class XmlSchemaValidator
    {
        public XmlSchemaSet XmlSchemaSet { get; }

        public XmlSchemaValidator(string schemaFile)
        {
            XmlSchemaSet = new XmlSchemaSet();
            XmlSchemaSet.Add("http://exampleNamespace.org/XMLTech", schemaFile);
        }

        public ValidationResult Validate(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            List<string> errors = new List<string>();
            doc.Validate(
                XmlSchemaSet,
                (o, e) => { errors.Add(e.Message); },
                true);


            return new ValidationResult(errors.Count > 0, errors);
        }

    }

}
