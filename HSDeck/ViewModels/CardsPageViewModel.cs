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
    public class CardsPageViewModel : ViewModelBase
    {
        private readonly ICardApi _cardApi;
        private readonly IImageLoader _imageLoader;

        // static permet de garder le tableau en mémoire au changement de page
        private static ObservableCollection<Card> _cards = new ObservableCollection<Card>();
        public ObservableCollection<Card> Cards
        {
            get { return _cards; }
            set { Set(ref _cards, value); }
        }

        // static permet de garder le tableau en mémoire au changement de page
        private static ObservableCollection<Card> _deckCards = new ObservableCollection<Card>();
        public ObservableCollection<Card> DeckCards
        {
            get { return _deckCards; }
            set { Set(ref _deckCards, value); }
        }

        private Card _selectedCardInDeck;
        public Card SelectedCardInDeck
        {
            get { return _selectedCardInDeck; }
            set { Set(ref _selectedCardInDeck, value); RaisePropertyChanged(); }
        }

        public void Remove(int index)
        {
            DeckCards.RemoveAt(index);
        }

        public CardsPageViewModel()
        {
            _cardApi = new CardApi();
            _imageLoader = new ImageLoader();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (Cards.Count == 0)
            {
                Views.Shell.SetBusy(true, "Loading cards data, please wait ...");

                var response = await _cardApi.GetAll();
                foreach (var basic in response.Basic)
                {
                    if (!String.IsNullOrEmpty(basic.img))
                    {
                        basic.Image = await _imageLoader.GetFromUrl(basic.img);
                        Cards.Add(basic);

                        Views.Shell.SetBusyText("Loading cards data, please wait ... (" + ((int)( ((double)Cards.Count / 171) * 100 )).ToString() + "%)");
                    }
                }
                
                Views.Shell.SetBusy(false);
            }

            /** A utiliser en cas de crash de l'endpoint de l'API pour avoir quelques cartes
            Card Leroy = new Card()
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
            Card Ysera = new Card()
            {
                cardId = "EX1_572",
                name = "Ysera",
                cardSet = "Classic",
                type = "Minion",
                faction = "Neutral",
                rarity = "Legendary",
                cost = 9,
                attack = 4,
                health = 12,
                text = "At the end of your turn, add a Dream Card to your hand.",
                flavor = "Ysera rules the Emerald Dream.  Which is some kind of green-mirror-version of the real world, or something?",
                artist = "Gabor Szikszai",
                collectible = true,
                elite = true,
                img = "http://wow.zamimg.com/images/hearthstone/cards/enus/original/EX1_572.png",
                imgGold = "http://wow.zamimg.com/images/hearthstone/cards/enus/animated/EX1_572_premium.gif",
                locale = "enUS",

                Image = await _imageLoader.GetFromUrl("http://wow.zamimg.com/images/hearthstone/cards/enus/original/EX1_575.png")
            };
            Card SunfuryProtector = new Card()
            {
                cardId = "EX1_058",
                name = "Sunfury Protector",
                cardSet = "Classic",
                type = "Minion",
                faction = "Alliance",
                rarity = "Rare",
                cost = 2,
                attack = 2,
                health = 3,
                text = "<b>Battlecry:</b> Give adjacent minions <b>Taunt</b>.",
                flavor = "She carries a shield,but only so she can give it to someone she can stand behind.",
                artist = "James Ryman",
                collectible = true,
                img = "http://wow.zamimg.com/images/hearthstone/cards/enus/original/EX1_058.png",
                imgGold = "http://wow.zamimg.com/images/hearthstone/cards/enus/animated/EX1_058_premium.gif",
                locale = "enUS",
                mechanics = new Mechanic11[] { new Mechanic11() { name = "Battlecry" } },

                Image = await _imageLoader.GetFromUrl("http://wow.zamimg.com/images/hearthstone/cards/enus/original/EX1_058.png")
            };
            Card AmaniBerserker = new Card()
            {
                cardId = "EX1_393",
                name = "Amani Berserker",
                cardSet = "Classic",
                type = "Minion",
                faction = "Neutral",
                rarity = "Common",
                cost = 2,
                attack = 2,
                health = 3,
                text = "<b>Enrage:</b> +3 Attack",
                flavor = "If an Amani berserker asks \"Joo lookin' at me?!\", the correct response is \"Nah, mon\".",
                artist = "Chippy",
                collectible = true,
                img = "http://wow.zamimg.com/images/hearthstone/cards/enus/original/EX1_393.png",
                imgGold = "http://wow.zamimg.com/images/hearthstone/cards/enus/animated/EX1_393_premium.gif",
                locale = "enUS",
                mechanics = new Mechanic11[] { new Mechanic11() { name = "Enrage" } },
                
                Image = await _imageLoader.GetFromUrl("http://wow.zamimg.com/images/hearthstone/cards/enus/original/EX1_393.png")
            };
            
            Cards.Add(Leroy);
            Cards.Add(Ysera);
            Cards.Add(SunfuryProtector);
            Cards.Add(AmaniBerserker);*/
        }
        
        public void GotoCardDetailsPage(Card selectedCard) =>
            NavigationService.Navigate(typeof(Views.CardDetailsPage), selectedCard.cardId);
    }
}