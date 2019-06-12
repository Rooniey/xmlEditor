using GUI.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using XmlFileModel;

namespace GUI.ViewModels
{
    public class ProvidersViewModel : BindableBase
    {
        private Document _document;

        public ObservableCollection<Provider> Providers { get; }
        public ObservableCollection<Payment> Payments { get; } = new ObservableCollection<Payment>();

        private Provider _chosenProvider;

        public Provider ChosenProvider
        {
            get => _chosenProvider;
            set { SetProperty(ref _chosenProvider, value);
                OnChosenProviderChanged(value);
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _cost;

        public string Cost
        {
            get => _cost;
            set => SetProperty(ref _cost, value);
        }

        private string _currency;

        public string Currency
        {
            get => _currency;
            set => SetProperty(ref _currency, value);
        }


        public RelayCommand AddProvider { get; }
        public RelayCommand DeleteProvider { get; }
        public RelayCommand AddPaymentToProvider { get; }
        public RelayCommand DeletePaymentFromProvider { get; }



        public ProvidersViewModel(Document document)
        {
            _document = document;
            Providers = new ObservableCollection<Provider>(document.Providers.Collection);

            DeleteProvider = new RelayCommand(
                (provider) => DeleteProviderFromDocument((Provider)provider));
            AddProvider = new RelayCommand(
                (_) => AddProviderToDocument());

            AddPaymentToProvider = new RelayCommand(
                (_) => AddPaymentToChosenProvider(),
                (_) => ChosenProvider != null);
            DeletePaymentFromProvider = new RelayCommand(
                (payment) => DeletePaymentFromChosenProvider((Payment)payment));
        }


        public void DeleteProviderFromDocument(Provider provider)
        {
            _document.Providers.Collection.Remove(provider);
            Providers.Remove(provider);

            foreach (var playlist in _document.Playlists.Collection)
            {
                playlist.Series.Collection.RemoveAll(s => s.Provider == provider.Id);
            }

        }

        public void AddProviderToDocument()
        {
            if (string.IsNullOrEmpty(Name)) return;
            Provider provider = new Provider()
            {
                Id = GetUniqueProviderId(),
                Name = Name,
                MonthlyPayment = new PaymentCollection()
                {
                    Collection = new List<Payment>()
                }
            };
            Providers.Add(provider);
            _document.Providers.Collection.Add(provider);
        }

        public string GetUniqueProviderId()
        {
            string providerId = Name.Trim().Split(' ')[0].ToUpper();
            if (!_document.Providers.Collection.Exists(p => p.Id == providerId)) return providerId;
            int i = 1;
            while (_document.Providers.Collection.Exists(p => p.Id == (providerId + i++))) ;

            return providerId + --i;
        }

        private void OnChosenProviderChanged(Provider value)
        {
            Payments.Clear();
            foreach(var payment in value.MonthlyPayment.Collection)
            {
                Payments.Add(payment);
            }
        }

        public void AddPaymentToChosenProvider()
        {
            Payment payment = new Payment()
            {
                Cost = Cost,
                Currency = Currency
            };
            ChosenProvider.MonthlyPayment.Collection.Add(payment);
            Payments.Add(payment);
        }

        public void DeletePaymentFromChosenProvider(Payment payment)
        {
            ChosenProvider.MonthlyPayment.Collection.Remove(payment);
            Payments.Remove(payment);
        }

    }
}
