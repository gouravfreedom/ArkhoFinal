using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DFS.Models;
using DFS.Utils;
using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace DFS
{
    public class HTTPService : IRestService
    {
        HttpClient client;

        public HTTPService()
        {

            client = new HttpClient(new NativeMessageHandler());
            client.MaxResponseContentBufferSize = 256000;

        }


        public async Task<string> GetInstagramInfo(string accessToken)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var url = "https://api.instagram.com/v1/users/self/?access_token=" + accessToken;

                var uri = new Uri(url);

                try
                {

                    HttpResponseMessage response = null;

                    response = await client.GetAsync(uri);


                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;

                        var instagramUser = JsonConvert.DeserializeObject<InstagramUser>(responseJson);
                        App.InstagramUser = instagramUser;

                        return "Success";
                    }
                    else
                    {
                        return "Internal Server Error. Please try again.";
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                    return "Internal Server Error. Please try again.";
                }
            }
            else
            {
                return "Internet Connectivity error. Please try again.";
            }
        }

        public async Task<string> DeleteInstaImages()
        {
            TraineeSignupModel signupModel = new TraineeSignupModel();
            signupModel.email = App.LoginResponse.Email;
            signupModel.imagePayload = App.LoginResponse.ImagePayload;
            signupModel.password = App.LoginResponse.Password;
            signupModel.paypalId = App.LoginResponse.PaypalId;
            signupModel.profile = App.LoginResponse.Profile;
            signupModel.signUpMetod = App.LoginResponse.SignUpMetod;
            TraineeSignupModel.BasicInfo basicInfo = new TraineeSignupModel.BasicInfo();


            basicInfo.address = App.LoginResponse.basicInfo.Address;
            basicInfo.anyMedicalCondition = App.LoginResponse.basicInfo.AnyMedicalCondition;
            basicInfo.country = App.LoginResponse.basicInfo.Country;
            basicInfo.dateOfBirth = App.LoginResponse.basicInfo.DateOfBirth;
            basicInfo.gender = App.LoginResponse.basicInfo.Gender;
            basicInfo.height = App.LoginResponse.basicInfo.Height;
            basicInfo.imageUrl = App.LoginResponse.basicInfo.ImageUrl;
            basicInfo.instaGramId = "";
            basicInfo.latitude = App.LoginResponse.basicInfo.Latitude;
            basicInfo.longitude = App.LoginResponse.basicInfo.Longitude;
            basicInfo.mobileNumber = App.LoginResponse.basicInfo.PhoneNumber;
            basicInfo.name = App.LoginResponse.basicInfo.Name;
            basicInfo.phoneNumber = App.LoginResponse.basicInfo.PhoneNumber;
            basicInfo.sportsInterest = App.LoginResponse.basicInfo.SportsInterest;
            basicInfo.state = App.LoginResponse.basicInfo.State;
            basicInfo.title = App.LoginResponse.basicInfo.Title;
            basicInfo.weight = App.LoginResponse.basicInfo.Weight;
            basicInfo.instaGramImages = "";

            signupModel.basicInfo = basicInfo;

            TraineeSignupModel.ProfessionalInfo professionalInfo = new TraineeSignupModel.ProfessionalInfo();

            if (App.SelectedView == "Trainer")
            {
                professionalInfo.accolades = App.LoginResponse.professionalInfo.Accolades;
                professionalInfo.experience = App.LoginResponse.professionalInfo.Experience;
                professionalInfo.speciality = App.LoginResponse.professionalInfo.Speciality;
                professionalInfo.services = new List<TraineeSignupModel.Services>();
                professionalInfo.certifications = new List<TraineeSignupModel.Certifications>();

                foreach (var item in App.LoginResponse.professionalInfo.services)
                {
                    TraineeSignupModel.Services services = new TraineeSignupModel.Services();

                    services.charges = Convert.ToDouble(item.Charges);
                    services.chargingPeriod = item.ChargingPeriod;
                    services.serviceName = item.ServiceName;
                    services.teamSize = item.TeamSize;
                    services.workLocaton = item.WorkLocaton;
                    services.schedule = new List<TraineeSignupModel.Schedule>();
                    foreach (var scheduleItem in item.schedules)
                    {
                        TraineeSignupModel.Schedule schedule = new TraineeSignupModel.Schedule();

                        schedule.day = scheduleItem.Day;
                        schedule.endTime = scheduleItem.EndTime;
                        schedule.month = scheduleItem.Month;
                        schedule.scheduleType = scheduleItem.ScheduleType;
                        schedule.startTime = scheduleItem.StartTime;
                        schedule.weekDay = scheduleItem.WeekDay;
                        schedule.year = scheduleItem.Year;

                        services.schedule.Add(schedule);
                    }

                    professionalInfo.services.Add(services);
                }


                foreach (var certItem in App.LoginResponse.professionalInfo.certifications)
                {
                    TraineeSignupModel.Certifications certifications = new TraineeSignupModel.Certifications();
                    certifications.certification = certItem.Certification;

                    professionalInfo.certifications.Add(certifications);
                }


                //signupModel.professionalInfo.certifications = App.LoginResponse.professionalInfo.certifications;
                //signupModel.professionalInfo.services = App.LoginResponse.professionalInfo.services;
            }

            signupModel.professionalInfo = professionalInfo;

            String signupSuccess = await SignUpAsync(signupModel);
            await CredentialsService.SaveCredentials(userName: App.LoginResponse.Email, password: App.LoginResponse.Password, member: App.LoginResponse, userType: App.SelectedView);
            return signupSuccess;
        }

        public async Task<string> GetInstagramMedia(string accessToken)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                var url = "https://graph.instagram.com/me/media?fields=id,media_type,media_url,username,timestamp&access_token=" + accessToken;

                var uri = new Uri(url);

                try
                {

                    HttpResponseMessage response = null;

                    response = await client.GetAsync(uri);


                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;

                        var instagramMedia = JsonConvert.DeserializeObject<InstagramMedia>(responseJson);
                        App.InstagramMedia = instagramMedia;

                        TraineeSignupModel signupModel = new TraineeSignupModel();
                        signupModel.email = App.LoginResponse.Email;
                        signupModel.imagePayload = App.LoginResponse.ImagePayload;
                        signupModel.password = App.LoginResponse.Password;
                        signupModel.paypalId = App.LoginResponse.PaypalId;
                        signupModel.profile = App.LoginResponse.Profile;
                        signupModel.signUpMetod = App.LoginResponse.SignUpMetod;
                        TraineeSignupModel.BasicInfo basicInfo = new TraineeSignupModel.BasicInfo();


                        basicInfo.address = App.LoginResponse.basicInfo.Address;
                        basicInfo.anyMedicalCondition = App.LoginResponse.basicInfo.AnyMedicalCondition;
                        basicInfo.country = App.LoginResponse.basicInfo.Country;
                        basicInfo.dateOfBirth = App.LoginResponse.basicInfo.DateOfBirth;
                        basicInfo.gender = App.LoginResponse.basicInfo.Gender;
                        basicInfo.height = App.LoginResponse.basicInfo.Height;
                        basicInfo.imageUrl = App.LoginResponse.basicInfo.ImageUrl;
                        basicInfo.instaGramId = "";
                        basicInfo.latitude = App.LoginResponse.basicInfo.Latitude;
                        basicInfo.longitude = App.LoginResponse.basicInfo.Longitude;
                        basicInfo.mobileNumber = App.LoginResponse.basicInfo.PhoneNumber;
                        basicInfo.name = App.LoginResponse.basicInfo.Name;
                        basicInfo.phoneNumber = App.LoginResponse.basicInfo.PhoneNumber;
                        basicInfo.sportsInterest = App.LoginResponse.basicInfo.SportsInterest;
                        basicInfo.state = App.LoginResponse.basicInfo.State;
                        basicInfo.title = App.LoginResponse.basicInfo.Title;
                        basicInfo.weight = App.LoginResponse.basicInfo.Weight;
                        basicInfo.instaGramImages = "";


                        foreach (var item in App.InstagramMedia.data)
                        {
                            var media = item.media_url;
                            basicInfo.instaGramImages = basicInfo.instaGramImages == "" ? media : basicInfo.instaGramImages + "," + media;
                        }

                        signupModel.basicInfo = basicInfo;

                        TraineeSignupModel.ProfessionalInfo professionalInfo = new TraineeSignupModel.ProfessionalInfo();

                        if (App.SelectedView == "Trainer")
                        {


                            professionalInfo.accolades = App.LoginResponse.professionalInfo.Accolades;
                            professionalInfo.experience = App.LoginResponse.professionalInfo.Experience;
                            professionalInfo.speciality = App.LoginResponse.professionalInfo.Speciality;
                            professionalInfo.services = new List<TraineeSignupModel.Services>();
                            professionalInfo.certifications = new List<TraineeSignupModel.Certifications>();

                            foreach (var item in App.LoginResponse.professionalInfo.services)
                            {
                                TraineeSignupModel.Services services = new TraineeSignupModel.Services();

                                services.charges = Convert.ToDouble(item.Charges);
                                services.chargingPeriod = item.ChargingPeriod;
                                services.serviceName = item.ServiceName;
                                services.teamSize = item.TeamSize;
                                services.workLocaton = item.WorkLocaton;
                                services.schedule = new List<TraineeSignupModel.Schedule>();
                                foreach (var scheduleItem in item.schedules)
                                {
                                    TraineeSignupModel.Schedule schedule = new TraineeSignupModel.Schedule();

                                    schedule.day = scheduleItem.Day;
                                    schedule.endTime = scheduleItem.EndTime;
                                    schedule.month = scheduleItem.Month;
                                    schedule.scheduleType = scheduleItem.ScheduleType;
                                    schedule.startTime = scheduleItem.StartTime;
                                    schedule.weekDay = scheduleItem.WeekDay;
                                    schedule.year = scheduleItem.Year;

                                    services.schedule.Add(schedule);
                                }

                                professionalInfo.services.Add(services);
                            }


                            foreach (var certItem in App.LoginResponse.professionalInfo.certifications)
                            {
                                TraineeSignupModel.Certifications certifications = new TraineeSignupModel.Certifications();
                                certifications.certification = certItem.Certification;

                                professionalInfo.certifications.Add(certifications);
                            }


                            //signupModel.professionalInfo.certifications = App.LoginResponse.professionalInfo.certifications;
                            //signupModel.professionalInfo.services = App.LoginResponse.professionalInfo.services;
                        }

                        signupModel.professionalInfo = professionalInfo;

                        String signupSuccess = await SignUpAsync(signupModel);
                        await CredentialsService.SaveCredentials(userName: App.LoginResponse.Email, password: App.LoginResponse.Password, member: App.LoginResponse, userType: App.SelectedView);
                        return signupSuccess;
                    }
                    else
                    {
                        return "Internal Server Error. Please try again.";
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                    return "Internal Server Error. Please try again.";
                }
            }
            else
            {
                return "Internet Connectivity error. Please try again.";
            }

        }

        public async Task<string> UpdateInstagramMedia(string accessToken)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                if (accessToken != null)
                {
                    var url = "https://api.instagram.com/v1/users/self/media/recent/?access_token=" + accessToken;

                    var uri = new Uri(url);

                    try
                    {
                        HttpResponseMessage response = null;

                        response = await client.GetAsync(uri);


                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = response.Content.ReadAsStringAsync().Result;

                            var instagramMedia = JsonConvert.DeserializeObject<InstagramMedia>(responseJson);
                            App.InstagramMedia = instagramMedia;
                            return "Success";
                        }
                        else
                        {
                            return "Internal Server Error. Please try again.";
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(@"ERROR {0}", ex.Message);
                        return "Internal Server Error. Please try again.";
                    }
                }
                else
                {
                    return "Authentication Error. Please try again";
                }
            }
            else
            {
                return "Internet Connectivity error. Please try again.";
            }
        }

        public async Task<string> GetFacebookInfo()
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                String access_token = await GetAccessToken();

                if (access_token == "Internal Server Error. Please try again.")
                {
                    return "Internal Server Error. Please try again.";
                }


                String url = "https://graph.facebook.com/v3.2/me?fields=name,picture,age_range,birthday,devices,email,first_name,last_name,gender,languages&access_token=" + access_token;

                var uri = new Uri(url);

                try
                {

                    HttpResponseMessage response = null;

                    response = await client.GetAsync(uri);


                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        //FacebookProfile facebook = JsonConvert.DeserializeObject<FacebookProfile>(responseJson);

                        //App.FacebookProfile = facebook;

                        return "Success";
                    }
                    else
                    {
                        return "Internal Server Error. Please try again.";
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                    return "Internal Server Error. Please try again.";
                }
            }
            else
            {
                return "Internet Connectivity error. Please try again.";
            }

        }


        public async Task<string> GetAccessToken()
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                String url = "https://graph.facebook.com/v3.2/oauth/access_token?client_id=1699986470106189&redirect_uri=https://www.facebook.com/connect/login_success.html&client_secret=8eec9d2368947acef2839159f6410863&code=" + App.access_code;

                var uri = new Uri(url);

                try
                {
                    HttpResponseMessage response = null;
                    response = await client.GetAsync(uri);


                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        FacebookAccessTokenModel facebookAccessTokenModel = JsonConvert.DeserializeObject<FacebookAccessTokenModel>(responseJson);

                        return facebookAccessTokenModel.AccessToken;
                    }
                    else
                    {
                        return "Internal Server Error. Please try again.";
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                    return "Internal Server Error. Please try again.";
                }
            }
            else
            {
                return "Internet Connectivity error. Please try again.";
            }

        }


        public async Task<TrainerListModel> FetchTrainerList()
        {
            TrainerListModel trainerListModel = new TrainerListModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                var uri = new Uri("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/trainerlist");

                try
                {
                    var content = new StringContent("{}", Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    response = await client.PostAsync(uri, content);


                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        trainerListModel = JsonConvert.DeserializeObject<TrainerListModel>(responseJson);

                        return trainerListModel;
                    }
                    else
                    {
                        return trainerListModel;
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                    return trainerListModel;
                }
            }
            else
            {
                return trainerListModel;
            }

        }


        public async Task<string> SignUpAsync(TraineeSignupModel signupModel)
        {

            Debug.WriteLine(JsonConvert.SerializeObject(signupModel));

            if (CrossConnectivity.Current.IsConnected)
            {
                var uri = new Uri("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/signup");

                try
                {
                    var json = JsonConvert.SerializeObject(signupModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    response = await client.PostAsync(uri, content);


                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        SignUpResponseModel responseItem = JsonConvert.DeserializeObject<Models.SignUpResponseModel>(responseJson);

                        if (responseItem.status.status == "SUCCESS" || responseItem.status.status == "Success")
                        {
                            if (signupModel.profile == "Trainer" && responseItem.status.IsStripeAccountCreated == "false")
                            {
                                App.TrainerStripeUrl = responseItem.status.StripeRedirectUrl;
                            }

                            LoginRequestModel loginRequestModel = new LoginRequestModel(App.SelectedView, signupModel.email, App.SelectedView, signupModel.password);
                            var message = await App.TodoManager.Login(loginRequestModel);

                            if (message == "SUCCESS" || message == "Success")
                            {
                                return "Success";
                            }
                            else
                            {
                                return "Internal Server Error. Please try again.";
                            }
                        }
                        else
                        {
                            return "Internal Server Error. Please try again.";
                        }

                    }
                    else
                    {
                        return "Internal Server Error. Please try again.";
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                    return "Internal Server Error. Please try again.";
                }
            }
            else
            {
                return "Internet Connectivity error. Please try again.";
            }

        }

        public async Task<string> LoginAsync(LoginRequestModel loginRequestModel)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                var uri = new Uri("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/validateMember");
                //var uri = new Uri("https://trainmeapp.in:6443/FitnessApp/manageservices/v1/members/validateMember");
                try
                {
                    var json = JsonConvert.SerializeObject(loginRequestModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        LoginResponse responseItem = JsonConvert.DeserializeObject<Models.LoginResponse>(responseJson);

                        if (loginRequestModel.profile == "Trainer" && responseItem.stripeInfo.IsStripeAccountCreated == "false")
                        {
                            App.TrainerStripeUrl = responseItem.stripeInfo.StripeRedirectUrl;
                        }

                        foreach (var item in responseItem.member)
                        {
                            if (item.Profile == App.SelectedView && loginRequestModel.password != "qwertyqazxcvbnm")
                            {
                                App.LoginResponse = item;
                            }
                            else if (loginRequestModel.password == "qwertyqazxcvbnm" && item.Profile == "Trainer")
                            {
                                App.TrainerData = item;
                            }
                        }

                        return "Success";

                    }
                    else
                    {
                        return "Internal Server Error. Please try again.";
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                    return "Internal Server Error. Please try again.";
                }
            }
            else
            {
                return "Internet Connectivity error. Please try again.";
            }

        }
        /*
         * Not implementated methods
        */

        public LoginResponse.SyncLoginResponse LoginResponse(string SelectedInput)
        {
            throw new NotImplementedException();
        }

        public async Task<SetTimeSlotResponseModel> SetCalenderEvent(SetTimeSlotsRequestModel setTimeSlots)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var uri = new Uri("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/addTimeSlots");
                    var json = JsonConvert.SerializeObject(setTimeSlots);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<SetTimeSlotResponseModel>(responseJson);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
            return null;
        }

        public async Task<GetTimeSlotResponse> GetTimeSlots(GetTimeSlotRequest getTimeSlotRequest)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    Uri uri;

                    if (App.SelectedView == "Trainee")
                    {
                        uri = new Uri("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/trainee/getTimeSlots");
                    }
                    else
                    {
                        uri = new Uri("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/getTimeSlots");
                    }


                    var json = JsonConvert.SerializeObject(getTimeSlotRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<GetTimeSlotResponse>(responseJson);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
            return null;
        }


        public async Task<PaymentResponse> StartPayment(PaymentRequest paymentRequest)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var uri = new Uri("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/startPayment");
                    var json = JsonConvert.SerializeObject(paymentRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<PaymentResponse>(responseJson);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
            return null;
        }

        public async Task<string> SubmitRating(RatingRequestModel ratingRequestModel)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var uri = new Uri("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/addTReview");
                    var json = JsonConvert.SerializeObject(ratingRequestModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Failure";
                    }
                }
                catch (Exception ex)
                {
                    return "Failure";
                }
            }
            else
            {
                return "Failure";
            }
        }

        public async Task<string> CreateOtpService(CreateOtpModel createOtpModel)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var uri = new Uri("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/startPasswordReset");
                    var json = JsonConvert.SerializeObject(createOtpModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<ResetResponseModel>(responseJson);
                        return result.statusInfo.Status == "Sent" ? "Success" : "Failure";
                    }
                    else
                    {
                        return "Failure";
                    }
                }
                catch (Exception ex)
                {
                    return "Failure";
                }
            }
            else
            {
                return "Failure";
            }
        }

        public async Task<string> SubmitOtpService(SubmitOtpModel submitOtpModel)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var uri = new Uri("https://arkho-app.org:6443/FitnessApp/manageservices/v1/members/updatePassword");
                    var json = JsonConvert.SerializeObject(submitOtpModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<ResetResponseModel>(responseJson);
                        return result.statusInfo.Status;
                    }
                    else
                    {
                        return "Failure";
                    }
                }
                catch (Exception ex)
                {
                    return "Failure";
                }
            }
            else
            {
                return "Failure";
            }
        }

        async public Task<string> GetInstaToken(string token)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("client_id", "688505365337395"),
                    new KeyValuePair<string, string>("client_secret", "1309b6738dc52c0b82dd3fa63463ec09"),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("redirect_uri", "https://www.arkho-app.com/"),
                    new KeyValuePair<string, string>("code", token),
                    });

                var url = "https://api.instagram.com/oauth/access_token";

                var uri = new Uri(url);

                try
                {

                    HttpResponseMessage response = null;

                    response = await client.PostAsync(uri,requestContent);


                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;

                        var instagramUser = JsonConvert.DeserializeObject<InstagramTokenResponse>(responseJson);
                        App.InstaAccessToken = instagramUser.access_token;

                        return "Success";
                    }
                    else
                    {
                        return "Internal Server Error. Please try again.";
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                    return "Internal Server Error. Please try again.";
                }
            }
            else
            {
                return "Internet Connectivity error. Please try again.";
            }
        }
    }
}
