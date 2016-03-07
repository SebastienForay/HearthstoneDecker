using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HSDeck.Models;

namespace HSDeck.Views
{
    public sealed partial class CardsPage : Page
    {
        public CardsPage()
        {
            this.InitializeComponent();
        }

        private void Cards_OnDragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            var items = String.Join(",", e.Items.Cast<Card>().Select(c => c.cardId));
            e.Data.Properties.Add("sourceList", sender as ListView);
            e.Data.SetText(items);
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }

        private void Deck_OnDragOver(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
                e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void Deck_OnDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var ids = await e.DataView.GetTextAsync();

                var destinationListView = sender as ListView;
                var destinationCollection = destinationListView?.ItemsSource as ObservableCollection<Card>;
                object sourceListView;
                if (!e.Data.Properties.TryGetValue("sourceList", out sourceListView))
                    return;

                var sourceCollection = ((ListView)sourceListView).ItemsSource as ObservableCollection<Card>;
                if (sourceCollection != null && destinationCollection != null)
                {
                    // Nombre de cartes dans le deck
                    var nbCardsInDeck = destinationCollection.Count();
                    // Liste des cartes uniques en double
                    var listCardsDouble = destinationCollection.Where(c => c.counter == "2");

                    // S'il y a des cartes en doubles
                    if (listCardsDouble.Count() > 0)
                        // Nombre de cartes du deck += nombre de cartes uniques en double
                        nbCardsInDeck += listCardsDouble.Count();

                    // Vérifie qu'il y a moins de 30 cartes dans le deck avant d'ajouter la nouvelle
                    if (nbCardsInDeck < 30)
                    {
                        foreach (var cardId in ids.Split(','))
                        {
                            var itemToMove = sourceCollection.First(c => c.cardId == cardId);

                            var listItems = destinationCollection.Where(c => c.cardId == cardId);
                            // Limite à 2 fois une carte identique dans le deck
                            if (listItems.Count() == 1)
                            {
                                var tmp = listItems.ElementAt(0);
                                destinationCollection.Remove(tmp);
                                destinationCollection.Add(itemToMove);
                                itemToMove.counter = "2";
                            }
                            else if (listItems.Count() == 0)
                            {
                                itemToMove.counter = "";
                                destinationCollection.Add(itemToMove);
                            }
                        }
                    }
                }
            }
        }

        private void DeckCard_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var tb = sender as TextBlock;
            var tmpCard = CardsPageViewModel.DeckCards.Single(c => c.name == tb.Text);

            if (tmpCard.counter == "2")
            {
                tmpCard.counter = "";
                CardsPageViewModel.Remove(DeckListView.SelectedIndex);
                CardsPageViewModel.DeckCards.Add(tmpCard);
            }
            else
                CardsPageViewModel.Remove(DeckListView.SelectedIndex);
        }

        private void Card_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            CardsPageViewModel.GotoCardDetailsPage(CardsListView.SelectedItem as Card);
        }
    }
}
