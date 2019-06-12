using GUI.Base;
using GUI.ViewUtility;
using System;
using System.IO;
using XmlFileModel;
using XmlFileModel.Utility;

namespace GUI.ViewModels
{
    public class MainViewModel : BindableBase
    {

        private object _chosenViewModel;
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
                (_) => SaveXmlFile(),
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

        public void SaveXmlFile()
        {
            Console.WriteLine("Saving to file");
        }

        public void GenerateSpecificTypeReport(string type)
        {
            Console.WriteLine($"Generating report with extension {type}");
        }
    }
}
