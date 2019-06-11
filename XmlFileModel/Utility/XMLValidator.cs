using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace XmlFileModel.Utility
{
    public class XMLValidator
    {
        public static string XmlFilePath = "../../../xml_files/series_list.xml";
        public static string XsdFilePath = "../../../xml_files/series_list.xsd";
        public static void Validate()
        {
            Validate(XmlFilePath, XsdFilePath);
        }

        public static void Validate(string xmlPath, string xsdPath)
        {
            // Create the XmlSchemaSet class.
            XmlSchemaSet sc = new XmlSchemaSet();

            // Add the schema to the collection.
            sc.Add("http://tempuri.org/SeriesPlaylist", xsdPath);

            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;
            settings.ValidationEventHandler += ValidationCallBack;

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create(xmlPath, settings);

            // Parse the file. 
            while (reader.Read()) ;

        }

        // Display any validation errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            Console.WriteLine($"Validation Error:\n   {e.Message}\n");
        }

    }
}
