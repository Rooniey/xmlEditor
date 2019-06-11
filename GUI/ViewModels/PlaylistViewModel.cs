using GUI.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using XmlFileModel;

namespace GUI.ViewModels
{
    public class PlaylistViewModel : BindableBase
    {
        public List<string> GENRES { get; }  = new List<string> { "superhero", "action", "fantasy", "drama", "sci-fi", "crime", "gangster", "legaldrama" };

        public PlaylistCollection Data { get; set; }

        public ObservableCollection<Playlist> Playlists{ get; private set;}

        public ObservableCollection<Series> Series { get; private set; } = new ObservableCollection<Series>();


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

        public RelayCommand DeletePlaylist { get; }
        public RelayCommand AddNewPlaylist { get; }
        public RelayCommand ChangeSelectedPlaylist { get; }

        public RelayCommand DeleteSeriesFromPlaylist { get; }


        public PlaylistViewModel(PlaylistCollection data)
        {
            Data = data;
            Playlists = new ObservableCollection<Playlist>(data.Collection); 
            DeletePlaylist = new RelayCommand((name) => DeleteP((Playlist)name));
            AddNewPlaylist = new RelayCommand((_) => AddP());

            DeleteSeriesFromPlaylist = new RelayCommand((name) => DeleteS((Series)name));
        }


        public void DeleteP(Playlist playlist)
        {
            Data.Collection.Remove(playlist);
            Playlists.Remove(playlist);
        }

        public void AddP()
        {
            Playlist playlist = new Playlist() { Name = NewName, Series = new SeriesCollection() };
            Data.Collection.Add(playlist);
            Playlists.Add(playlist);
        }

        public void DeleteS(Series series)
        {
            ChosenPlaylist.Series.Collection.Remove(series);
            Series.Remove(series);
        }

        public void OnChangeSelectedPlaylist(Playlist playlist)
        {
            //Add null checking
            Console.WriteLine(playlist.Name);
            Series.Clear();
            foreach (var series in playlist.Series.Collection)
            {
                Series.Add(series);
            }
        }
    }
}
