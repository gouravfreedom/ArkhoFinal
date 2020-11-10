using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Xamarin.Auth;
using System.Linq;
using DFS.Utils;
using DFS.Dependency;
using DFS.Views;

namespace DFS
{
    public partial class LoginPage : ContentPage
    {
        UserProfileViewModel userProfileViewModel;
        public LoginPage(String _selectedView)
        {
            InitializeComponent();

            BindingContext = userProfileViewModel = new UserProfileViewModel();
            userProfileViewModel.SelectedView = _selectedView;
        }

        async void Forgot_Tapped(object sender, EventArgs e)
        {
            //string result = await App.Current.MainPage.DisplayPromptAsync("Question 2", "What's 5 + 5?", maxLength: 2, keyboard: Keyboard.Numeric);
            await this.Navigation.PushAsync(new Views.ResetPasswordPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.access_code != null)
            {
                userProfileViewModel.FacebookData();
            }

            MessagingCenter.Subscribe<Object, string>(this, "InstagramLogin", async (arg1, arg2) =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(arg2))
                    {
                        var instaResult = await App.TodoManager.GetInstaToken(arg2);

                        if (instaResult == "Success")
                        {

                            userProfileViewModel.IsServiceInProgress = true;
                            //var result = await App.TodoManager.GetInstagramInfo(App.InstaAccessToken);
                            await App.TodoManager.GetInstagramMedia(App.InstaAccessToken);

                            var loginRequestModel = new Models.LoginRequestModel("Fb", App.InstagramUser.data.username, App.SelectedView, "fb@trainme");
                            var message = await App.TodoManager.Login(loginRequestModel);

                            if (message.Equals("Success"))
                            {

                                ViewModels.SignupViewModel signupViewModel = new ViewModels.SignupViewModel();
                                signupViewModel.EmailAddress = App.InstagramUser.data.username;
                                signupViewModel.Password = "fb@trainme";
                                //signupViewModel.Name = App.InstagramUser.data.full_name;
                                signupViewModel.UserIcon = App.InstagramUser.data.profile_picture;
                                signupViewModel.SelectedView = App.SelectedView;

                                var data = App.LoginResponse;

                                if (data.Profile == null || data.Profile == "")
                                {
                                    await this.Navigation.PushAsync(new Views.UserInformationPage(signupViewModel));
                                }
                                else
                                {
                                    //var member = App.SelectedView == "Trainee" ? App.LoginResponse : App.TrainerData;
                                    CredentialsService.SaveCredentials(App.InstagramUser.data.username, "fb@trainme", App.LoginResponse, instagramUser: App.InstagramUser, instagramMedia: App.InstagramMedia, userType: App.SelectedView);
                                    Application.Current.MainPage = new RootPage(App.SelectedView);
                                }
                            }
                            else
                            {
                                await DisplayAlert("Alert", message, "Ok");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    await DisplayAlert("Alert", "something went wrong", "Ok");
                }
                userProfileViewModel.IsServiceInProgress = false;
            });

            MessagingCenter.Subscribe<UserProfileViewModel, String>(this, "LoginSuccess", async (sender, message) =>
            {
                MessagingCenter.Unsubscribe<UserProfileViewModel>(this, "LoginSuccess");
                MessagingCenter.Unsubscribe<UserProfileViewModel>(this, "LoginFailure");
                if (message == "NAV")
                {
                    if (userProfileViewModel.UserPassword == "fb@trainme")
                    {
                        ViewModels.SignupViewModel signupViewModel = new ViewModels.SignupViewModel();
                        signupViewModel.EmailAddress = userProfileViewModel.Username;
                        signupViewModel.Password = userProfileViewModel.UserPassword;
                        //signupViewModel.Name = App.FacebookProfile.Name;
                        //signupViewModel.UserIcon = App.FacebookProfile.Picture.Data.Url;
                        signupViewModel.SelectedView = App.SelectedView;


                        await this.Navigation.PushAsync(new Views.UserInformationPage(signupViewModel));
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Incorrect Username or Password. Please try again.", "OK");
                    }
                }
                else
                {
                    if (userProfileViewModel.IsRememberMe)
                    {
                        var member = App.LoginResponse;
                        CredentialsService.SaveCredentials(userName: App.LoginResponse.Email, password: App.LoginResponse.Password, member: member, userType: App.SelectedView);
                    }

                    Application.Current.MainPage = new NavigationPage(new RootPage(userProfileViewModel.SelectedView));
                }


            });

            MessagingCenter.Subscribe<UserProfileViewModel, String>(this, "LoginFailure", async (sender, message) =>
            {

                await DisplayAlert("Alert", message, "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<UserProfileViewModel, String>(this, "LoginSuccess");
            MessagingCenter.Unsubscribe<UserProfileViewModel, String>(this, "LoginFailure");
            MessagingCenter.Unsubscribe<InstagramLoginPage, string>(this, "InstagramLogin");
        }

        void Handle_Facebook(object sender, System.EventArgs e)
        {
            var iFacebookManger = DependencyService.Get<IFacebookManager>();
            iFacebookManger.Login(HandleAction);
            //await this.Navigation.PushAsync(new Views.FacebookProfileCsPage());
        }

        async void Handle_Instagram(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new InstagramLoginPage(true));
            //DependencyService.Get<IInstagramManager>().Login();
        }

        async void HandleAction(Models.FacebookUser arg1, string arg2)
        {
            try
            {
                App.FacebookUser = arg1;
                App.access_code = arg1.Token;

                var loginRequestModel = new Models.LoginRequestModel("App", arg1.Email, App.SelectedView, "fb@trainme");
                var message = await App.TodoManager.Login(loginRequestModel);

                if (message == "Success")
                {

                    ViewModels.SignupViewModel signupViewModel = new ViewModels.SignupViewModel();
                    signupViewModel.EmailAddress = App.FacebookUser.Email;
                    signupViewModel.Password = "fb@trainme";
                    //signupViewModel.Name = arg1.FirstName + " " + arg1.LastName;
                    signupViewModel.UserIcon = arg1.Picture;
                    signupViewModel.SelectedView = App.SelectedView;

                    var data = App.LoginResponse;

                    if (data.Profile == null || data.Profile == "")
                    {
                        await this.Navigation.PushAsync(new Views.UserInformationPage(signupViewModel));
                    }
                    else
                    {
                        var member = App.LoginResponse;
                        CredentialsService.SaveCredentials(App.FacebookUser.Email, "fb@trainme", member, App.FacebookUser, userType: App.SelectedView);
                        Application.Current.MainPage = new RootPage(App.SelectedView);
                    }
                }
                else
                {
                    await DisplayAlert("Alert", message, "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "Ok");
            }
        }


        async void Handle_SignUpClickedAsync(object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new Views.SignUp(userProfileViewModel.SelectedView));
        }
    }
}
