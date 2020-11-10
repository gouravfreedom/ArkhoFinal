using System;
using System.Collections.Generic;
using System.Web;
using Xamarin.Forms;

namespace DFS.Views
{
    public partial class StripeWebPage : ContentPage
    {
        public StripeWebPage()
        {
            InitializeComponent();

            webView.Source = App.TrainerStripeUrl;
            App.TrainerStripeUrl = "";
            webView.Navigated += WebView_Navigated;
        }

        async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.StartsWith("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/stripe/account/creation", StringComparison.CurrentCultureIgnoreCase))
            {
                string param1 = HttpUtility.ParseQueryString(new Uri(e.Url).Query).Get("error");

                if (param1 != null && param1 != "" && param1 != "access_denied")
                {

                    await DisplayAlert("Alert", "Account Creation successfull!", "OK");
                }
                else
                {
                    await DisplayAlert("Alert", "Account Creation Unsuccessful!", "OK");
                }
                App.Current.MainPage = new RootPage(App.SelectedView);

            }
        }
    }
}
