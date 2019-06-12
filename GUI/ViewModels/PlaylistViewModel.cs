using GUI.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using XmlFileModel;

namespace GUI.ViewModels
{
    public class PlaylistViewModel : BindableBase
    {
        public List<string> GENRES { get; }  = new List<string> { "superhero", "action", "fantasy", "drama", "sci-fi", "crime", "gangster", "legaldrama" };

        public Document Document { get; set; }

        public ObservableCollection<Playlist> Playlists{ get; private set;}

        public ObservableCollection<Series> Series { get; private set; } = new ObservableCollection<Series>();

        public List<string> Providers { get;  }


        private Playlist _chosenPlaylist;

        public Playlist ChosenPlaylist
        {
            get => _chosenPlaylist;
            set {
                SetProperty(ref _chosenPlaylist, value);
                OnChangeSelectedPlaylist(value);
            }
        }


        private string _newName = "";

        public string NewName
        {
            get => _newName;
            set => SetProperty(ref _newName, value);
        }


        private string _title = "";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _nrOfEpisodes = "";

        public string NrOfEpisodes
        {
            get => _nrOfEpisodes;
            set => SetProperty(ref _nrOfEpisodes, value);
        }


        private string _premiere = "";

        public string Premiere
        {
            get => _premiere;
            set => SetProperty(ref _premiere, value);
        }

        private string _rottenTomatoesScore = "";

        public string RottenTomatoesScore
        {
            get => _rottenTomatoesScore;
            set => SetProperty(ref _rottenTomatoesScore, value);
        }

        private string _genre = "";

        public string Genre
        {
            get => _genre;
            set => SetProperty(ref _genre, value);
        }

        private string _provider = "";

        public string Provider
        {
            get => _provider;
            set => SetProperty(ref _provider, value);
        }


        public RelayCommand DeletePlaylist { get; }
        public RelayCommand AddNewPlaylist { get; }
        public RelayCommand ChangeSelectedPlaylist { get; }

        public RelayCommand DeleteSeriesFromPlaylist { get; }
        public RelayCommand AddSeriesToPlaylist { get; }



        public PlaylistViewModel(Document data)
        {
            Document = data;
            Providers = Document.Providers.Collection.Select(p => p.Id).ToList();
            Playlists = new ObservableCollection<Playlist>(data.Playlists.Collection);

            DeletePlaylist = new RelayCommand(
                (name) => DeletePlaylistFromDocument((Playlist)name));
            AddNewPlaylist = new RelayCommand(
                (_) => AddPlaylistToDocument());
            DeleteSeriesFromPlaylist = new RelayCommand(
                (name) => DeleteSeriesFromChosenPlaylist((Series)name));
            AddSeriesToPlaylist = new RelayCommand(
                (_) => AddSeriesToChosenPlaylist(),
                (_) => ChosenPlaylist != null);

        }


        public void DeletePlaylistFromDocument(Playlist playlist)
        {
            Document.Playlists.Collection.Remove(playlist);
            Playlists.Remove(playlist);
        }

        public void AddPlaylistToDocument()
        {
            Playlist playlist = new Playlist() { Name = NewName, Series = new SeriesCollection() { Collection = new List<Series>() } };
            Document.Playlists.Collection.Add(playlist);
            Playlists.Add(playlist);
        }

        public void DeleteSeriesFromChosenPlaylist(Series series)
        {
            ChosenPlaylist.Series.Collection.Remove(series);
            Series.Remove(series);
        }

        private void AddSeriesToChosenPlaylist()
        {
            if (string.IsNullOrEmpty(Provider)) return;
            string providerId = Document.Providers.Collection.First(p => p.Id == Provider).Id;
            Series series = new Series()
            {
                Title = Title,
                FirstEpisodePremiere = Premiere,
                TotalNumberOfEpisodes = NrOfEpisodes,
                Genre = Genre,
                Provider = providerId,
                RottenTomatoes = new RottenTomatoesScore() { Score = RottenTomatoesScore }
            };
            ChosenPlaylist.Series.Collection.Add(series);
            Series.Add(series);
        }


        public void OnChangeSelectedPlaylist(Playlist playlist)
        {
            Series.Clear();
            if (playlist == null) return;
            foreach (var series in playlist.Series.Collection)
            {
                Series.Add(series);
            }
        }
    }
}
