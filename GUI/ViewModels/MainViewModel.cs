using GUI.Base;
using GUI.ViewUtility;
using System;
using System.IO;
using System.Text;
using XmlFileModel;
using XmlFileModel.Utility;

namespace GUI.ViewModels
{
    public class MainViewModel : BindableBase
    {

        private object _chosenViewModel;
        private string _readXmlFilePath;
        public object ChosenViewModel
        {
            get => _chosenViewModel;
            private set => SetProperty(ref _chosenViewModel, value);
        }

        private Document _document;

        public RelayCommand Navigate { get; }
        public RelayCommand ReadXml { get; }
        public RelayCommand WriteXml { get; }
        public RelayCommand GenerateReport { get; }

        public MainViewModel()
        {
            Navigate = new RelayCommand(
                (dest) => NavigateTo((string)dest), 
                (_) => _document != null);

            ReadXml = new RelayCommand(
                (_) => ReadXmlFile());

            WriteXml = new RelayCommand(
                (mode) => SaveXmlFile((string)mode),
                (_) => _document != null);

            GenerateReport = new RelayCommand(
                (type) => GenerateSpecificTypeReport((string)type),
                (_) => _document != null);
        }

        public void NavigateTo(string dest)
        {
            switch(dest)
            {
                case "PLAYLIST":
                    ChosenViewModel = new PlaylistViewModel(_document);
                    break;
                case "PROVIDERS":
                    ChosenViewModel = new ProvidersViewModel(_document);
                    break;
                case "DOCUMENT_INFO":
                    ChosenViewModel = new DocumentInfoViewModel(_document.Info);
                    break;
            }
        }

        public void ReadXmlFile()
        {
            string xmlFilePath = FileSystemHelper.GetXmlFilePath();
            if (string.IsNullOrEmpty(xmlFilePath) || !File.Exists(xmlFilePath)) return;
            _readXmlFilePath = xmlFilePath;
            try
            {
                ValidationResult valRes = XMLValidator.Validate(xmlFilePath);

                if (!valRes.IsValid)
                {
                    Messager.DisplayValidationErrors(valRes.Errors);
                    return;
                }

                _document = XMLSerializer.Deserialize(xmlFilePath);
                NavigateTo("PLAYLIST");

            } catch(Exception ex)
            {
                Messager.DisplayError(ex.Message);
            }
        }

        public void SaveXmlFile(string mode)
        {
            Console.WriteLine("Saving to file");

            string saveFileName = mode == "SAVEAS" ? FileSystemHelper.GetXmlSaveFilePath() : _readXmlFilePath;

            if (string.IsNullOrEmpty(saveFileName)) return;

            using (MemoryStream ms = new MemoryStream())
            {
                XMLSerializer.Serialize(_document, ms);

                var str = Encoding.ASCII.GetString(ms.ToArray());

                ValidationResult valRes = XMLValidator.ValidateString(str);

                if (!valRes.IsValid)
                {
                    Messager.DisplayValidationErrors(valRes.Errors);
                    return;
                }

                using (FileStream fs = new FileStream(saveFileName, FileMode.Create))
                {
                    ms.WriteTo(fs);
                }
            }
            
        }

        public void GenerateSpecificTypeReport(string type)
        {
            string fileName = FileSystemHelper.GetSaveFilePath();

            if (String.IsNullOrEmpty(fileName)) return;

            XMLTransformer transformer = new XMLTransformer(_readXmlFilePath);
            transformer.Transform($"{fileName}.{type.ToLowerInvariant()}", type == "TXT" ? XsltOutputType.Txt : XsltOutputType.Svg);
            Console.WriteLine($"Generating report with extension {type}");
        }
    }
}
