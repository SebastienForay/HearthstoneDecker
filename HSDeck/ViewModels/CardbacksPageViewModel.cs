using Template10.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using HSDeck.Models;
using HSDeck.Services.CardAPI;
using System;
using HSDeck.Services.ImageLoader;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using HSDeck.Views;

namespace HSDeck.ViewModels
{
    public class CardbacksPageViewModel : ViewModelBase
    {
        private readonly ICardApi _cardApi;
        private readonly IImageLoader _imageLoader;

        private static ObservableCollection<Cardback> _cardBacks = new ObservableCollection<Cardback>();
        public ObservableCollection<Cardback> Cardbacks
        {
            get { return _cardBacks; }
            set { Set(ref _cardBacks, value); }
        }

        public CardbacksPageViewModel()
        {
            _cardApi = new CardApi();
            _imageLoader = new ImageLoader();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (Cardbacks.Count == 0)
            {
                Views.Shell.SetBusy(true, "Loading cards data, please wait ...");

                var response = await _cardApi.GetAllBacks();
                foreach (var cardback in response)
                {
                    if (!String.IsNullOrEmpty(cardback.img))
                    {
                        cardback.Image = await _imageLoader.GetFromUrl(cardback.img);
                        Cardbacks.Add(cardback);

                        Views.Shell.SetBusyText("Loading cards data, please wait ... (" + ((int)(((double)Cardbacks.Count / response.Count()) * 100)).ToString() + "%)");
                    }
                }
                Views.Shell.SetBusy(false);
            }
            Views.Shell.SetBusy(false);
        }
    }
}
