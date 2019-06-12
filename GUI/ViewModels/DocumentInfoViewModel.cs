using GUI.Base;
using System.Collections.ObjectModel;
using XmlFileModel;

namespace GUI.ViewModels
{
    public class DocumentInfoViewModel : BindableBase
    {

        private DocumentInfo _documentInfo;

        public ObservableCollection<Author> Authors { get; }

        private string _currentTitle;

        public string CurrentTitle
        {
            get => _currentTitle;
            set => SetProperty(ref _currentTitle, value);
        }


        private string _title = "";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }


        private string _firstName = "";

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName = "";

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _index = "";

        public string Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }


        public RelayCommand AddAuthor { get; }
        public RelayCommand DeleteAuthor { get; }

        public RelayCommand SaveTitle { get; }

        public DocumentInfoViewModel(DocumentInfo docInfo)
        {
            _documentInfo = docInfo;
            CurrentTitle = _documentInfo.Title;
            Authors = new ObservableCollection<Author>(_documentInfo.Authors.Collection);

            AddAuthor = new RelayCommand(
                (_) => AddAuthorToDocument());
            DeleteAuthor = new RelayCommand(
                (author) => DeleteAuthorFromDocument((Author)author));
            SaveTitle = new RelayCommand(
                (_) => SaveDocumentTitle());
        }


        public void AddAuthorToDocument()
        {
            Author author = new Author()
            {
                FirstName = FirstName,
                LastName = LastName,
                Index = Index
            };

            _documentInfo.Authors.Collection.Add(author);
            Authors.Add(author);
        }

        public void DeleteAuthorFromDocument(Author author)
        {
            _documentInfo.Authors.Collection.Remove(author);
            Authors.Remove(author);
        }   

        public void SaveDocumentTitle()
        {
            _documentInfo.Title = Title;
            CurrentTitle = _documentInfo.Title;
        }
    }
}
