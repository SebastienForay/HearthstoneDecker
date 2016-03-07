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
    public class CardsByClassPageViewModel : ViewModelBase
    {
        private readonly ICardApi _cardApi;
        private readonly IImageLoader _imageLoader;

        private ObservableCollection<Card> _cards = new ObservableCollection<Card>();
        public ObservableCollection<Card> Cards
        {
            get { return _cards; }
            set { Set(ref _cards, value); }
        }

        private string _className;
        public string ClassName
        {
            get { return _className; }
            set { Set(ref _className, value); }
        }


        public CardsByClassPageViewModel()
        {
            _cardApi = new CardApi();
            _imageLoader = new ImageLoader();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (state.ContainsKey(nameof(ClassName)))
            {
                ClassName = state[nameof(ClassName)]?.ToString();
                state.Clear();
            }
            else ClassName = parameter?.ToString();

            if (Cards.Count == 0)
            {
                Views.Shell.SetBusy(true, "Loading cards data, please wait ...");

                var response = await _cardApi.GetAllByClass(ClassName);
                foreach (var card in response)
                {
                    if (!String.IsNullOrEmpty(card.img))
                    {
                        card.Image = await _imageLoader.GetFromUrl(card.img);
                        Cards.Add(card);

                        Views.Shell.SetBusyText("Loading cards data, please wait ... (" + ((int)(((double)Cards.Count / response.Count()) * 100)).ToString() + "%)");
                    }
                }
                Views.Shell.SetBusy(false);
            }
            Views.Shell.SetBusy(false);
        }

        public void GotoCardDetailsPage(Card selectedCard) =>
            NavigationService.Navigate(typeof(Views.CardDetailsPage), selectedCard.cardId);
    }
}
