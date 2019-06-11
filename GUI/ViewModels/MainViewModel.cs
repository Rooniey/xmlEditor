using GUI.Base;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;
using System.Xml.Serialization;
using XmlFileModel;

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

        public Document Document
        {
            get => _document;
            private set => SetProperty(ref _document, value);
        }

        public RelayCommand Navigate { get; }
        public RelayCommand ReadXml { get; }

        public MainViewModel()
        {
            Navigate = new RelayCommand((dest) => NavigateTo((string)dest), (_) => Document != null);
            ReadXml = new RelayCommand((_) => ReadXmlFile());
        }

        public void NavigateTo(string dest)
        {
            switch(dest)
            {
                case "PLAYLIST":
                    ChosenViewModel = new PlaylistViewModel(Document.Playlists);
                    break;
            }
        }

        public void ReadXmlFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".xml"; 
            dlg.Filter = "xml (.xml)|*.xml";
            bool? result = dlg.ShowDialog();

            if (result == false) return;
            string xmlFile = dlg.FileName;

            XmlSerializer serializer = new XmlSerializer(typeof(Document));

            using (StreamReader sr = new StreamReader(xmlFile))
            {
                Document = (Document)serializer.Deserialize(sr);
            }
        }


    }
}
