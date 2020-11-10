using System;
using System.Collections.Generic;
using DFS.Utils;
using DFS.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace DFS.Views
{
    public partial class TraineeProfilePage : ContentPage
    {
        RootPage root;
        TraineeProfileViewModel traineeProfileViewModel;
        public TraineeProfilePage(RootPage root)
        {
            InitializeComponent();

            this.root = root;

            BindingContext = traineeProfileViewModel = new TraineeProfileViewModel();
        }

        private async void GalleryListView_FlowItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as string;
            await PopupNavigation.Instance.PushAsync(new ImagePopupPage(item));
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            //this.Navigation.PushAsync(new UserInformationPage(new ViewModels.SignupViewModel()));

            ViewModels.SignupViewModel signupViewModel = new SignupViewModel();
            signupViewModel.UserIcon = App.LoginResponse.basicInfo.ImageUrl;

            App.Current.MainPage = new HanselmanNavigationPage(new UserInformationPage(signupViewModel));
        }

        void Handle_TraineMe(object sender, System.EventArgs e)
        {
            this.root.NavigateAsync((int)MenuType.CoachList);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            traineeProfileViewModel.UpdateInstagramMedia();

            MessagingCenter.Subscribe<TraineeProfileViewModel>(this, "CalenderPage", async (sender) =>
            {

                await this.Navigation.PushAsync(new TraineeCalanderPage());
            });


        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Object, string>(this, "InstagramMedia");
            MessagingCenter.Unsubscribe<TraineeProfileViewModel>(this, "CalenderPage");
        }

        async void Insta_Disconnect(object sender, System.EventArgs e)
        {
            traineeProfileViewModel.IsServiceInProgress = true;
            var response = await App.TodoManager.DeleteInstaImages();

            if (response == "Success")
            {
                App.InstagramMedia = null;
                App.InstaAccessToken = null;
                //traineeProfileViewModel.UpdateInstagramMedia();
                traineeProfileViewModel.GalleryVisible = false;
                traineeProfileViewModel.InstaVisible = true;
                traineeProfileViewModel.DisconnectVisible = false;
            }
            else
            {
                await DisplayAlert("Alert", "Something went wrong. Please try again later.", "Ok");
            }

            traineeProfileViewModel.IsServiceInProgress = false;
        }

        async void Handle_Tapped_1(object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new InstagramLoginPage(false));

            MessagingCenter.Subscribe<Object, string>(this, "InstagramMedia", async (arg1, arg2) =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(arg2))
                    {
                        traineeProfileViewModel.IsServiceInProgress = true;

                        var instaResult = await App.TodoManager.GetInstaToken(arg2);

                        if (instaResult == "Success")
                        {

                            var result = await App.TodoManager.GetInstagramMedia(App.InstaAccessToken);

                            
                            CredentialsService.SaveCredentials(instagramMedia: App.InstagramMedia);
                            Application.Current.MainPage = new RootPage(App.SelectedView);
                            
                        }
                        else
                        {
                            await DisplayAlert("Alert", instaResult, "Ok");
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    await DisplayAlert("Alert", "something went wrong", "Ok");
                }
                traineeProfileViewModel.IsServiceInProgress = false;
            });

        }
    }
}
