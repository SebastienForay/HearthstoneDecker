using HSDeck.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace HSDeck.Views
{
    public sealed partial class CardsByClass : Page
    {
        public CardsByClass()
        {
            this.InitializeComponent();
        }

        private void Card_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            CardsByClassPageViewModel.GotoCardDetailsPage(CardsListView.SelectedItem as Card);
        }
    }
}
