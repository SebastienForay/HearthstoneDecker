using Template10.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using HSDeck.Services.InfosAPI;
using HSDeck.Models;
using Windows.UI.Xaml.Controls;

namespace HSDeck.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IInfosAPI _infosAPI;

        public MainPageViewModel()
        {
            _infosAPI = new InfosAPI();

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        string _Value = string.Empty;
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        string _cardName = string.Empty;
        public string CardName { get { return _cardName; } set { Set(ref _cardName, value); } }

        private bool _apiInfosLoaded = false;
        public bool APIInfosLoaded { get { return _apiInfosLoaded; } set { Set(ref _apiInfosLoaded, value); } }

        private static Infos _patchInfos;
        public Infos PatchInfos { get { return _patchInfos; } set { Set(ref _patchInfos, value); RaisePropertyChanged(); } }

        private string _selectedClassName;
        public string SelectedClasseName
        {
            get { return _selectedClassName; }
            set { _selectedClassName = value; RaisePropertyChanged(); }
        }

        private int[] _defaultSelectedIndex;
        public int[] DefaultSelectedIndex
        {
            get { return _defaultSelectedIndex; }
            set
            {
                Set(ref _defaultSelectedIndex, value);
                RaisePropertyChanged();
            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (state.Any())
            {
                Value = state[nameof(Value)]?.ToString();
                state.Clear();
            }

            if (!APIInfosLoaded)
            {
                PatchInfos = await _infosAPI.GetAllInfos();
                APIInfosLoaded = true;
                DefaultSelectedIndex = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            }
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
            {
                state[nameof(Value)] = Value;
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }

        
        public void GoToClassCards() =>
            NavigationService.Navigate(typeof(Views.CardsByClass), SelectedClasseName);

        public void GotoCardDetailsPage() =>
            NavigationService.Navigate(typeof(Views.CardDetailsPage), CardName);

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

