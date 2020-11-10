using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DFS.Views
{
    public partial class StripeSelectionPage : ContentPage
    {
        public StripeSelectionPage()
        {
            InitializeComponent();
        }

        void Handle_StripeAsync(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new StripeWebPage());
        }

        void Handle_CancelAsync(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new RootPage(App.SelectedView);
        }
    }
}
