using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace XmlFileModel.Utility
{
    public static class XMLValidator
    {
        public static string XmlFilePath = "../../../xml_files/series_list.xml";
        public static string XsdFilePath = "../../../xml_files/series_list.xsd";
        public static ValidationResult Validate()
        {
            return Validate(XmlFilePath, XsdFilePath);
        }

        public static ValidationResult Validate(string xmlPath)
        {
            return Validate(xmlPath, XsdFilePath);
        }

        public static ValidationResult ValidateString(string xmlString)
        {
            return ValidateString(xmlString, XsdFilePath);
        }

        public static ValidationResult Validate(Stream xmlStream)
        {
            return Validate(xmlStream, XsdFilePath);
        }

        public static ValidationResult Validate(string xmlPath, string xsdPath)
        {
            // Create the XmlSchemaSet class.
            XmlSchemaSet sc = new XmlSchemaSet();

            // Add the schema to the collection.
            sc.Add("http://tempuri.org/SeriesPlaylist", xsdPath);

            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;

            List<string> errors = new List<string>();
            settings.ValidationEventHandler += (object sender, ValidationEventArgs e) => {
                errors.Add(e.Message);
                System.Console.WriteLine(e.Message);
                };

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create(xmlPath, settings);

            // Parse the file. 
            while (reader.Read()) ;
            

            return new ValidationResult(errors.Count == 0, errors);
        }

        public static ValidationResult Validate(Stream xmlStream, string xsdPath)
        {
            // Create the XmlSchemaSet class.
            XmlSchemaSet sc = new XmlSchemaSet();

            // Add the schema to the collection.
            sc.Add("http://tempuri.org/SeriesPlaylist", xsdPath);

            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;

            List<string> errors = new List<string>();
            settings.ValidationEventHandler += (object sender, ValidationEventArgs e) => {
                errors.Add(e.Message);
                System.Console.WriteLine(e.Message);
            };

            // Create the XmlReader object.
            using (StreamReader sr = new StreamReader(xmlStream))
            {
                XmlReader reader = XmlReader.Create(sr, settings);
                // Parse the file. 
                while (reader.Read()) ;


                return new ValidationResult(errors.Count == 0, errors);
            }
        }

        public static ValidationResult ValidateString(string xmlString, string xsdPath)
        {
            // Create the XmlSchemaSet class.
            XmlSchemaSet sc = new XmlSchemaSet();

            // Add the schema to the collection.
            sc.Add("http://tempuri.org/SeriesPlaylist", xsdPath);

            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;

            List<string> errors = new List<string>();
            settings.ValidationEventHandler += (object sender, ValidationEventArgs e) => {
                errors.Add(e.Message);
                System.Console.WriteLine(e.Message);
            };

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create(new StringReader(xmlString), settings);

            // Parse the file. 
            while (reader.Read()) ;


            return new ValidationResult(errors.Count == 0, errors);
        }
    }
}
