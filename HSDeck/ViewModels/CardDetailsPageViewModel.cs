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
    public class CardDetailsPageViewModel : ViewModelBase
    {
        private readonly ICardApi _cardApi;
        private readonly IImageLoader _imageLoader;

        private Card _card;
        public Card CardToDisplay
        {
            get { return _card; }
            set { Set(ref _card, value); }
        }
        
        private string _cardName;
        public string CardName { get { return _cardName; } set { Set(ref _cardName, value); } }


        public CardDetailsPageViewModel()
        {
            _cardApi = new CardApi();
            _imageLoader = new ImageLoader();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Views.Shell.SetBusy(true, "Loading card data, please wait ... ");

            if (state.ContainsKey(nameof(CardName)))
            {
                CardName = state[nameof(CardName)]?.ToString();
                state.Clear();
            }
            else CardName = parameter?.ToString();
                        
            var response = await _cardApi.GetSingle(CardName);
           
            if (!String.IsNullOrEmpty(response.img))
            {
                response.Image = await _imageLoader.GetFromUrl(response.img);
                CardToDisplay = response;
            }

            /*Card Leroy = new Card()
            {
                cardId = "EX1_116",
                name = "Leeroy Jenkins",
                cardSet = "Classic",
                type = "Minion",
                faction = "Alliance",
                rarity = "Legendary",
                cost = 5,
                attack = 6,
                health = 2,
                text = "<b>Charge</b>. <b>Battlecry:</b> Summon two 1/1 Whelps for your opponent.",
                flavor = "At least he has Angry Chicken.",
                artist = "Gabe from Penny Arcade",
                collectible = true,
                elite = true,
                img = "http://wow.zamimg.com/images/hearthstone/cards/enus/original/EX1_116.png",
                imgGold = "http://wow.zamimg.com/images/hearthstone/cards/enus/animated/EX1_116_premium.gif",
                locale = "enUS",
                mechanics = new Mechanic11[] {
                    new Mechanic11() { name = "Battlecry" },
                    new Mechanic11() { name = "Charge" }
                },

                Image = await _imageLoader.GetFromUrl("http://wow.zamimg.com/images/hearthstone/cards/enus/original/EX1_116.png")
            };

            Cards.Add(Leroy);*/


            Views.Shell.SetBusy(false);
        }
    }
}
