using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Plugin.Media;
using System.Collections.Generic;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using DFS.Utils;
using XamForms.Controls;
using Newtonsoft.Json;
using System.Linq;

namespace DFS.ViewModels
{
    public class SignupViewModel : INotifyPropertyChanged
    {
        private Plugin.Geolocator.Abstractions.Position position = new Plugin.Geolocator.Abstractions.Position();

        private Boolean _isTrainerView { get; set; }

        public Boolean IsTrainerView
        {
            get
            {
                return _isTrainerView;
            }
            set
            {
                _isTrainerView = value;

                RaisePropertyChanged(nameof(IsTrainerView));
            }
        }

        private Boolean _isModifyViewVisible { get; set; }

        public Boolean IsModifyViewVisible
        {
            get
            {
                return _isModifyViewVisible;
            }
            set
            {
                _isModifyViewVisible = value;

                RaisePropertyChanged(nameof(IsModifyViewVisible));
            }
        }

        private string _startTime;
        public string StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;

                RaisePropertyChanged(nameof(StartTime));
            }
        }
        private string _displayDate;
        public string DisplayDate
        {
            get
            {
                return _displayDate;
            }
            set
            {
                _displayDate = value;

                RaisePropertyChanged(nameof(DisplayDate));
            }
        }


        private string _endTime;
        public string EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;

                RaisePropertyChanged(nameof(EndTime));
            }
        }

        private string _instaImages;
        public string InstaImages
        {
            get
            {
                return _instaImages;
            }
            set
            {
                _instaImages = value;

                RaisePropertyChanged(nameof(InstaImages));
            }
        }

        private ObservableCollection<String> _titleList { get; set; }

        public ObservableCollection<String> TitleList
        {
            get
            {
                return _titleList;
            }
            set {
                _titleList = value;

                RaisePropertyChanged(nameof(TitleList));
            }
        }

        private int _titleIndex { get; set; }

        public int TitleIndex
        {
            get
            {
                return _titleIndex;
            }
            set
            {
                _titleIndex = value;

                RaisePropertyChanged(nameof(TitleIndex));
            }
        }

        private ObservableCollection<String> _genderList { get; set; }

        public ObservableCollection<String> GenderList
        {
            get
            {
                return _genderList;
            }
            set
            {
                _genderList = value;

                RaisePropertyChanged(nameof(GenderList));
            }
        }

        private int _genderIndex { get; set; }

        public int GenderIndex
        {
            get
            {
                return _genderIndex;
            }
            set
            {
                _genderIndex = value;

                RaisePropertyChanged(nameof(GenderIndex));
            }
        }

        private String _selectedView { get; set; }

        public String SelectedView
        {
            get
            {
                return _selectedView;
            }
            set
            {
                _selectedView = value;

                RaisePropertyChanged(nameof(SelectedView));
            }
        }

        private String _emailAddress { get; set; }

        public String EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;

                RaisePropertyChanged(nameof(EmailAddress));
            }
        }

        private String _password { get; set; }

        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;

                RaisePropertyChanged(nameof(Password));
            }
        }

        private String _confirmPassword { get; set; }

        public String ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;

                RaisePropertyChanged(nameof(ConfirmPassword));
            }
        }


        private DateTime _dateOfBirth { get; set; }

        public DateTime DateOfBirth 
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
                RaisePropertyChanged(nameof(DateOfBirth));
            }
        }

        private String _userIcon { get; set; }

        public String UserIcon
        {
            get
            {
                return _userIcon;
            }
            set
            {
                _userIcon = value;

                RaisePropertyChanged(nameof(UserIcon));
            }
        }

        private String _user64String { get; set; }

        public String User64String
        {
            get
            {
                return _user64String;
            }
            set
            {
                _user64String = value;

                RaisePropertyChanged(nameof(User64String));
            }
        }

        private Boolean _isServiceInProgress;
        public Boolean IsServiceInProgress
        {
            get { return _isServiceInProgress; }
            set
            {
                _isServiceInProgress = value;
                RaisePropertyChanged(nameof(IsServiceInProgress));
            }
        }

        private Boolean _timeSelectionVisible;
        public Boolean TimeSelectionVisible
        {
            get { return _timeSelectionVisible; }
            set
            {
                _timeSelectionVisible = value;
                RaisePropertyChanged(nameof(TimeSelectionVisible));
            }
        }


        private ObservableCollection<Models.CustomSignupModel> _staticListData;

        public ObservableCollection<Models.CustomSignupModel> StaticListData
        {
            get { return _staticListData; }
            set
            {
                _staticListData = value;
                RaisePropertyChanged(nameof(StaticListData));
            }
        }

        private int _selectedCalenderIndex { get; set; }

        public int SelectedCalenderIndex
        {
            get
            {
                return _selectedCalenderIndex;
            }
            set
            {
                _selectedCalenderIndex = value;

                RaisePropertyChanged(nameof(SelectedCalenderIndex));
            }
        }

        private ObservableCollection<String> _listViewData;
        public ObservableCollection<String> ListViewData
        {
            get
            {
                return _listViewData;
            }
            set
            {
                _listViewData = value;

                RaisePropertyChanged(nameof(ListViewData));
            }
        }

        private String _recentlySelectedItem { get; set; }

        public String RecentlySelectedItem
        {
            get
            {
                return _recentlySelectedItem;
            }
            set
            {
                if (value == null)
                    return;

                _recentlySelectedItem = value;
                RaisePropertyChanged(nameof(RecentlySelectedItem));
                if (TimeHeader == "Please Select Starting Time")
                {
                    StaticListData[1][SelectedCalenderIndex].selectedTime[(StaticListData[1][SelectedCalenderIndex].selectedTime.Count) - 1].StartTime = value;
                    //SelectedTime[SelectedTime.Count - 1].StartTime = value;
                    TimeHeader = "Please Select End Time";
                }
                else
                {

                    if(ListViewData.IndexOf(StaticListData[1][SelectedCalenderIndex].selectedTime[(StaticListData[1][SelectedCalenderIndex].selectedTime.Count) - 1].StartTime) >= ListViewData.IndexOf(value))
                    {
                        MessagingCenter.Send<SignupViewModel, String>(this, "Alert", "End time can't be earlier than Starting time.");
                    }
                    else
                    {
                        TimeSelectionVisible = false;
                        TimeHeader = "Please Select Starting Time";
                        StaticListData[1][SelectedCalenderIndex].selectedTime[(StaticListData[1][SelectedCalenderIndex].selectedTime.Count) - 1].EndTime = value;

                    }


                }


            }
        }

        private String _timeHeader { get; set; }

        public String TimeHeader
        {
            get
            {
                return _timeHeader;
            }
            set
            {
                _timeHeader = value;

                RaisePropertyChanged(nameof(TimeHeader));
            }
        }

        private ObservableCollection<XamForms.Controls.SpecialDate> attendances;
        public ObservableCollection<XamForms.Controls.SpecialDate> Attendances
        {
            get { return attendances; }
            set { attendances = value; RaisePropertyChanged(nameof(Attendances)); }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand PictureCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand MinusCommand { get; set; }
        public ICommand CalendarCommand { get; set; }
        public ICommand HideCalenderCommand { get; private set; }
        public ICommand ResetCommand { get; set; }
        public ICommand ModifyCommand { get; set; }
        public ICommand DeleteEventCommand { get; set; }
        public ICommand CancelEventCommand { get; set; }
        void SelectImage()
        {

        }

        public ICommand DateChosen
        {
            get
            {
                return new Command( (obj) => {
                    System.Diagnostics.Debug.WriteLine(obj as DateTime?);

                    DateTime dateTime = (DateTime)obj;

                    foreach(var item in Attendances)
                    {
                        if (item.Date == dateTime)
                        {
                            var index = Attendances.IndexOf(item);
                            Attendances.Move(index, Attendances.Count - 1);

                            //StaticListData[1][SelectedCalenderIndex].selectedTime.Contains()
                            //var data = StaticListData[1][SelectedCalenderIndex].selectedTime.Where(x => x.Day == dateTime.Day + "");
                            for(int TimeIndex = 0; TimeIndex < StaticListData[1][SelectedCalenderIndex].selectedTime.Count; TimeIndex++)
                            {
                                if(StaticListData[1][SelectedCalenderIndex].selectedTime[TimeIndex].Day == dateTime.Day + "")
                                {
                                    StartTime = StaticListData[1][SelectedCalenderIndex].selectedTime[TimeIndex].StartTime;
                                    EndTime = StaticListData[1][SelectedCalenderIndex].selectedTime[TimeIndex].EndTime;
                                    DisplayDate = StaticListData[1][SelectedCalenderIndex].selectedTime[TimeIndex].Month + "/" + StaticListData[1][SelectedCalenderIndex].selectedTime[TimeIndex].Day + "/" + StaticListData[1][SelectedCalenderIndex].selectedTime[TimeIndex].Year;
                                    StaticListData[1][SelectedCalenderIndex].selectedTime.Move(TimeIndex, StaticListData[1][SelectedCalenderIndex].selectedTime.Count - 1);
                                    break;
                                }
                            }
                            IsModifyViewVisible = true;
                            return;
                        }
                    }

                    Attendances.Add(new SpecialDate(dateTime) { BackgroundColor = Color.Red, Selectable = true });

                    Models.SelectedTime selectedTime = new Models.SelectedTime();
                    selectedTime.Day = dateTime.Day + "";
                    selectedTime.Month = dateTime.Month + "";
                    selectedTime.Year = dateTime.Year + "";
                    selectedTime.WeekDay = dateTime.DayOfWeek.ToString();
                    selectedTime.SelectedIndex = SelectedCalenderIndex;

                    StaticListData[1][SelectedCalenderIndex].selectedTime.Add(selectedTime);

                    InitializeCalender();
                    TimeSelectionVisible = true;

                });
            }
        }



        public Task Initialization { get; private set; }

        public SignupViewModel()
        {
            InitializeCalender();
            IsModifyViewVisible = false;
            GenderList = new ObservableCollection<String>();
            GenderList.Add("Male");
            GenderList.Add("Female");

            DateOfBirth = new DateTime(2000, 1, 1);

            TimeSelectionVisible = false;

            _timeHeader = "Please Select Starting Time";

            UserIcon = "defaultIcon.png";

            User64String = "NA";

            InstaImages = "";

            // Intialize commands
            SaveCommand = new Command(() => SaveClicked());
            PictureCommand = new Command(() => SelectImage());
            AddCommand = new Command(AddRow);
            MinusCommand = new Command(DeleteRow);
            CalendarCommand = new Command(CalenderSelction);
            HideCalenderCommand = new Command(() => OnDateSelection());
            ResetCommand = new Command(ResetCalender);
            CancelEventCommand = new Command(() =>
            {
                IsModifyViewVisible = false;
            });

            ModifyCommand = new Command(() =>
            {
                InitializeCalender();
                IsModifyViewVisible = false;
                TimeSelectionVisible = true;
            });

            DeleteEventCommand = new Command(() =>
            {
                Attendances.RemoveAt(Attendances.Count - 1);
                var listData = Attendances.ToList();
                Attendances = new ObservableCollection<SpecialDate>(listData);
                StaticListData[1][SelectedCalenderIndex].selectedTime.RemoveAt(StaticListData[1][SelectedCalenderIndex].selectedTime.Count - 1);
                IsModifyViewVisible = false;
            });

            //SelectedTime = new ObservableCollection<Models.SelectedTime>();
            Attendances = new ObservableCollection<SpecialDate>();
            IsServiceInProgress = false;


            Initialization = InitializeAsync();

        }

        private void DeleteRow(object obj)
        {
            var item = obj as Models.SignupData;

            int index = StaticListData[1].IndexOf(item);

            if(StaticListData[1][index].InputType == "OnlyMinus")
            {
                StaticListData[1].RemoveAt(index + 1);
                StaticListData[1].RemoveAt(index);
                if (StaticListData[1][index - 1].InputType == "Minus")
                {
                    StaticListData[1].RemoveAt(index);
                }

            }
            else if (StaticListData[1][index + 1].InputType == "Service")
            {
                StaticListData[1].RemoveAt(index + 1);
                if (StaticListData[1][index + 1].InputType == "OnlyMinus")
                {
                    StaticListData[1].RemoveAt(index + 1);
                }
            }
        }

        private void ResetCalender()
        {
            Attendances = new ObservableCollection<SpecialDate>();
            StaticListData[1][SelectedCalenderIndex].selectedTime = new ObservableCollection<Models.SelectedTime>();


        }

        private void OnDateSelection()
        {
            MessagingCenter.Send<SignupViewModel>(this, "MoveBack");
        }

        private void CalenderSelction(object obj)
        {
            var item = obj as Models.SignupData;

            SelectedCalenderIndex = StaticListData[1].IndexOf(item);
            Attendances = new ObservableCollection<SpecialDate>();
            InitializeCalender();

            if (StaticListData[1][SelectedCalenderIndex].selectedTime != null && StaticListData[1][SelectedCalenderIndex].selectedTime.Count > 0)
            {
                foreach(var timeItem in StaticListData[1][SelectedCalenderIndex].selectedTime)
                {
                    DateTime dateTime = new DateTime(Convert.ToInt16(timeItem.Year), Convert.ToInt16(timeItem.Month), Convert.ToInt16(timeItem.Day));

                    Attendances.Add(new SpecialDate(dateTime) { BackgroundColor = Color.Red, Selectable = true });
                }
            }
            else
            {
                StaticListData[1][SelectedCalenderIndex].selectedTime = new ObservableCollection<Models.SelectedTime>();
            }

            MessagingCenter.Send<SignupViewModel>(this, "SignUpCalenderPage");
        }

        private void AddRow(object obj)
        {
            var item = obj as Models.SignupData;

            int index = StaticListData[1].IndexOf(item);



            if(item.PlaceholderText == "Awards")
            {
                StaticListData[1].Insert(index + 1, new Models.SignupData { InputType = "Entry", IsAdditionAvailable = false, PlaceholderText = "Enter Awards"});
            }
            else if (item.PlaceholderText == "Certification")
            {
                StaticListData[1].Insert(index + 1, new Models.SignupData { InputType = "Entry", PlaceholderText = "Enter Certification", IsAdditionAvailable = false });
            }
            else if (item.PlaceholderText == "Service")
            {
                int intialIndex = index;
                for (var i = index + 1; i < StaticListData[1].Count; i++)
                {
                    if(StaticListData[1][i].InputType == "Service")
                    {
                        continue;
                    }
                    else
                    {
                        index = i;
                        break;
                    }

                }

                if (intialIndex + 1 == index)
                {
                    StaticListData[1].Insert(index, new Models.SignupData { InputType = "Service" });
                }
                else
                {
                    StaticListData[1].Insert(index, new Models.SignupData { InputType = "OnlyMinus" });
                    StaticListData[1].Insert(index + 1, new Models.SignupData { InputType = "Service" });
                }
            }


        }

        private async void SaveClicked()
        {
            IsServiceInProgress = true;

            Models.TraineeSignupModel signupModel = new Models.TraineeSignupModel();

            signupModel.email = EmailAddress;
            signupModel.password = Password;
            signupModel.profile = SelectedView;
            signupModel.signUpMetod = (Password == "fb@trainme") ? "FB" : "App";
            signupModel.imagePayload = (User64String != null) ? User64String : "NA";


            Models.TraineeSignupModel.BasicInfo basicInfo = new Models.TraineeSignupModel.BasicInfo();
            basicInfo.address = position.Accuracy + "";
            basicInfo.country = position.Altitude + "";
            basicInfo.id = 1;
            basicInfo.imageUrl = ((User64String == null || User64String == "NA" ) && UserIcon != "defaultIcon.png" && UserIcon != null && UserIcon != "NA") ? UserIcon : "NA" ;
            basicInfo.instaGramId = "";
            basicInfo.latitude = position.Latitude + "";
            basicInfo.longitude = position.Longitude + "";
            basicInfo.valueAdded = "NA";
            basicInfo.instaGramImages = InstaImages;

            if (StaticListData[0][1].MainSelectedData == null || StaticListData[0][7].MainSelectedData == null || StaticListData[0][1].MainSelectedData == "" || StaticListData[0][7].MainSelectedData == "")
            {
                MessagingCenter.Send<SignupViewModel, String>(this, "SignUpFailure", "Please enter all manadatory fields.");
                IsServiceInProgress = false;
                return;
            }


            basicInfo.name = StaticListData[0][1].MainSelectedData;
            basicInfo.gender = StaticListData[0][3].SelectionData[StaticListData[0][3].SelectedIndex];
            basicInfo.dateOfBirth = StaticListData[0][5].SelectedDate.ToString();
            basicInfo.mobileNumber = StaticListData[0][7].MainSelectedData;
            basicInfo.phoneNumber = StaticListData[0][7].MainSelectedData;
            basicInfo.height = StaticListData[0][9].MainSelectedData;
            basicInfo.weight = StaticListData[0][11].MainSelectedData;
            basicInfo.sportsInterest = StaticListData[0][13].MainSelectedData;
            basicInfo.anyMedicalCondition = StaticListData[0][15].MainSelectedData;
            basicInfo.state = StaticListData[0][17].MainSelectedData;


            Models.TraineeSignupModel.ProfessionalInfo professionalInfo = new Models.TraineeSignupModel.ProfessionalInfo();


            if (App.SelectedView == "Trainer")
            {
                // Add certificates
                List<Models.TraineeSignupModel.Certifications> certifications = new List<Models.TraineeSignupModel.Certifications>();

                // Add Services
                List<Models.TraineeSignupModel.Services> services = new List<Models.TraineeSignupModel.Services>();


                var enteredProfessionalInfo = StaticListData[1];
                foreach (var item in enteredProfessionalInfo)
                {
                    if (item.PlaceholderText == "Enter Specialty" && item.InputType == "Entry")
                    {
                        professionalInfo.speciality = item.MainSelectedData;
                    }
                    else if (item.PlaceholderText == "Enter Experience" && item.InputType == "Entry")
                    {
                        professionalInfo.experience = item.MainSelectedData;
                    }
                    else if (item.PlaceholderText == "Enter Awards")
                    {
                        professionalInfo.accolades += item.MainSelectedData + " | ";
                    }
                    else if (item.PlaceholderText == "Enter PayPal ID")
                    {
                        signupModel.paypalId = item.MainSelectedData;
                    }
                    else if (item.PlaceholderText == "Enter Certification")
                    {
                        Models.TraineeSignupModel.Certifications certification = new Models.TraineeSignupModel.Certifications();
                        certification.certification = item.MainSelectedData;

                        certifications.Add(certification);
                    }
                    else if (item.InputType == "Service")
                    {
                        Models.TraineeSignupModel.Services service = new Models.TraineeSignupModel.Services();

                        if (item.SessionDesc != null && item.MainSelectedData != null && item.SessionLocation != null && item.SessionTeam != null)
                        {

                            service.chargingPeriod = item.SessionDesc;
                            service.serviceName = item.MainSelectedData;
                            service.charges = (item.SessionAmount == "" || item.SessionAmount == null) ? 0 : Convert.ToDouble(item.SessionAmount);
                            service.workLocaton = item.SessionLocation;
                            service.teamSize = item.SessionTeam;

                            List<Models.TraineeSignupModel.Schedule> schedules = new List<Models.TraineeSignupModel.Schedule>();

                            if (item.selectedTime != null)
                            {

                                foreach (var timeItem in item.selectedTime)
                                {
                                    Models.TraineeSignupModel.Schedule schedule = new Models.TraineeSignupModel.Schedule();

                                    schedule.day = timeItem.Day;
                                    schedule.month = timeItem.Month;
                                    schedule.year = timeItem.Year;
                                    schedule.scheduleType = "Week";
                                    schedule.startTime = timeItem.StartTime;
                                    schedule.endTime = timeItem.EndTime;
                                    schedule.weekDay = timeItem.WeekDay;

                                    schedules.Add(schedule);
                                }

                                service.schedule = schedules;


                                services.Add(service);
                            }

                        }
                    }


                }// end of foreach



                if (professionalInfo.accolades.Contains(" | "))
                {
                    professionalInfo.accolades = professionalInfo.accolades.Substring(0, professionalInfo.accolades.Length - 2);
                }

                professionalInfo.certifications = certifications;

                professionalInfo.services = services;

                //if (professionalInfo.accolades == null || professionalInfo.experience == null || professionalInfo.speciality == null || professionalInfo.certifications.Count < 1 || professionalInfo.services.Count < 1)
                //{
                //    MessagingCenter.Send<SignupViewModel, String>(this, "SignUpFailure", "Please enter all manadatory fields.");
                //    IsServiceInProgress = false;
                //    return;
                //}
            }

            // Creating the final object
            signupModel.basicInfo = basicInfo;
            signupModel.professionalInfo = professionalInfo;

            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(signupModel));

            var message = await App.TodoManager.SignUp(signupModel);

            if (message == "Success")
            {
                //Application.Current.MainPage = new RootPage(App.SelectedView);
                MessagingCenter.Send<SignupViewModel>(this, "SignUpSuccess");
            }
            else
            {
                MessagingCenter.Send<SignupViewModel, String>(this, "SignUpFailure", "Internal Issue. Please Try Again.");
            }

            IsServiceInProgress = false;

        }

        private async Task InitializeAsync()
        {

            try
            {
                IsServiceInProgress = true;
                Models.LoginRequestModel loginRequestModel = new Models.LoginRequestModel("App", App.LoginResponse.Email, App.SelectedView, App.LoginResponse.Password);
                var message = await App.TodoManager.Login(loginRequestModel);
                IsServiceInProgress = false;
                CheckUpdateProfile();

                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    GetCurrentPosition();

                }
                else if (status != PermissionStatus.Unknown)
                {

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to get location: " + ex);

            }


        }

        private void CheckUpdateProfile()
        {
            if (App.LoginResponse == null || App.LoginResponse.Email == "" || App.LoginResponse.Email == null)
            {
                StaticListData = new ObservableCollection<Models.CustomSignupModel>();

                Models.CustomSignupModel basicSignUpModel = new Models.CustomSignupModel { HeaderName = "Basic Information" };

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Name *", IsAdditionAvailable = false });
                // Index Number 1 (Name)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Entry", PlaceholderText = "Enter Name", IsAdditionAvailable = false });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Gender", IsAdditionAvailable = false });
                // Index Number 3 (Gender)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Gender", IsAdditionAvailable = false, SelectionData = GenderList });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Date of Birth", IsAdditionAvailable = false });
                // Index Number 5 (DOB)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Date of Birth", SelectedDate = DateOfBirth, IsAdditionAvailable = true });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Phone Number *", IsAdditionAvailable = false });
                // Index Number 7 (Phone)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Phone Number" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Height (in inches)", IsAdditionAvailable = false });
                // Index Number 9 (Height)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Height" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Weight (in lbs)", IsAdditionAvailable = false });
                // Index Number 11 (Weight)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Weight" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Goal", IsAdditionAvailable = false });
                // Index Number 13 (Sports Interest)
                //basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Sport", IsAdditionAvailable = false, SelectionData = SportsList });
                basicSignUpModel.Add(new Models.SignupData { InputType = "Entry", IsAdditionAvailable = false, PlaceholderText = "Enter Goal" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Medical Information", IsAdditionAvailable = false });
                // Index Number 15 (Medical Info)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Editor", PlaceholderText = "Enter Medical Information" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Location", IsAdditionAvailable = false });
                // Index Number 17 (Location)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Entry", IsAdditionAvailable = false, PlaceholderText = "Enter Location" });


                StaticListData.Add(basicSignUpModel);

                if (App.SelectedView == "Trainer")
                {

                    Models.CustomSignupModel serviceSignUpModel = new Models.CustomSignupModel { HeaderName = "Services" };

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Specialty", IsAdditionAvailable = false });
                    // Index Number 1 (Specialty)
                    //serviceSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Specialty", IsAdditionAvailable = false, SelectionData = SpecialityList });
                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Entry", IsAdditionAvailable = false, PlaceholderText = "Enter Specialty" });

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Experience (in years)", IsAdditionAvailable = false });
                    // Index Number 1 (Experiance)
                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Entry", PlaceholderText = "Enter Experience", IsAdditionAvailable = true });

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Awards", IsAdditionAvailable = true });
                    // Index Number 1 (Awards)
                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Entry", PlaceholderText = "Enter Awards", IsAdditionAvailable = false });

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Certification", IsAdditionAvailable = true });
                    // Index Number 1 (Certification)
                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Entry", PlaceholderText = "Enter Certification", IsAdditionAvailable = false });

                    //serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Service", IsAdditionAvailable = true });
                    // Index Number 1 (Service)
                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Minus", PlaceholderText = "Service" });
                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Service" });

                    if (App.TrainerStripeUrl != "")
                    {
                        serviceSignUpModel.Add(new Models.SignupData { InputType = "Button", MainSelectedData = "Connect Stripe Account", IsAdditionAvailable = false });
                    }
                    else
                    {
                        serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "You stripe account is connected.", IsAdditionAvailable = false });
                    }

                    StaticListData.Add(serviceSignUpModel);
                }
            }
            else
            {
                StaticListData = new ObservableCollection<Models.CustomSignupModel>();

                Models.CustomSignupModel basicSignUpModel = new Models.CustomSignupModel { HeaderName = "Basic Information" };

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Name *", IsAdditionAvailable = false });
                // Index Number 1 (Name)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData = App.LoginResponse.basicInfo.Name, InputType = "Entry", PlaceholderText = "Enter Name", IsAdditionAvailable = false });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Gender", IsAdditionAvailable = false });
                // Index Number 3 (Gender)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Gender", IsAdditionAvailable = false, SelectionData = GenderList, SelectedIndex = GenderList.IndexOf(App.LoginResponse.basicInfo.Gender) });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Date of Birth", IsAdditionAvailable = false });
                // Index Number 5 (DOB)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Date of Birth", SelectedDate = DateTime.Parse(App.LoginResponse.basicInfo.DateOfBirth), IsAdditionAvailable = true });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Phone Number *", IsAdditionAvailable = false });
                // Index Number 7 (Phone)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData = App.LoginResponse.basicInfo.PhoneNumber, InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Phone Number" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Height (in cm)", IsAdditionAvailable = false });
                // Index Number 9 (Height)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData = App.LoginResponse.basicInfo.Height, InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Height" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Weight (in kg)", IsAdditionAvailable = false });
                // Index Number 11 (Weight)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData = App.LoginResponse.basicInfo.Weight, InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Weight" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Goal", IsAdditionAvailable = false });
                // Index Number 13 (Sports Interest)
                //basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Ente Sport", IsAdditionAvailable = false, SelectionData = SportsList , SelectedIndex = SportsList.IndexOf(App.LoginResponse.basicInfo.SportsInterest) });
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData = App.LoginResponse.basicInfo.SportsInterest, InputType = "Entry", IsAdditionAvailable = false, PlaceholderText = "Enter Goal" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Medical Information", IsAdditionAvailable = false });
                // Index Number 15 (Medical Info)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData = App.LoginResponse.basicInfo.AnyMedicalCondition, InputType = "Editor", PlaceholderText = "Enter Medical Information" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Location", IsAdditionAvailable = false });
                // Index Number 17 (Location)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData = App.LoginResponse.basicInfo.State, InputType = "Entry", IsAdditionAvailable = false, PlaceholderText = "Enter Location" });


                StaticListData.Add(basicSignUpModel);

                if (App.SelectedView == "Trainer")
                {
                    Models.CustomSignupModel serviceSignUpModel = new Models.CustomSignupModel { HeaderName = "Services" };

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Specialty", IsAdditionAvailable = false });
                    // Index Number 1 (Specialty)
                    //serviceSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Specialty", IsAdditionAvailable = false, SelectionData = SpecialityList, SelectedIndex = SpecialityList.IndexOf(App.LoginResponse.professionalInfo.Speciality) });
                    serviceSignUpModel.Add(new Models.SignupData { MainSelectedData = App.LoginResponse.professionalInfo.Speciality, InputType = "Entry", IsAdditionAvailable = false, PlaceholderText = "Enter Specialty" });

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Experience", IsAdditionAvailable = false });
                    // Index Number 1 (Experiance)
                    serviceSignUpModel.Add(new Models.SignupData { MainSelectedData = App.LoginResponse.professionalInfo.Experience, InputType = "Entry", PlaceholderText = "Enter Experience", IsAdditionAvailable = true });

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Awards", IsAdditionAvailable = true });
                    // Index Number 1 (Awards)
                    serviceSignUpModel.Add(new Models.SignupData { MainSelectedData = App.LoginResponse.professionalInfo.Accolades, InputType = "Entry", PlaceholderText = "Enter Awards", IsAdditionAvailable = false });

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Certification", IsAdditionAvailable = true });
                    // Index Number 1 (Certification)

                    foreach (var certItem in App.LoginResponse.professionalInfo.certifications)
                    {
                        serviceSignUpModel.Add(new Models.SignupData { MainSelectedData = certItem.Certification, InputType = "Entry", PlaceholderText = "Enter Certification", IsAdditionAvailable = false });
                    }


                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Minus", PlaceholderText = "Service" });
                    //serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Service", IsAdditionAvailable = true });
                    // Index Number 1 (Service)
                    var isMoreThanOne = false;
                    foreach (var serviceItem in App.LoginResponse.professionalInfo.services)
                    {
                        ObservableCollection<Models.SelectedTime> selectedTimes = new ObservableCollection<Models.SelectedTime>();
                        foreach (var scheduleItem in serviceItem.schedules)
                        {
                            Models.SelectedTime selectedTime = new Models.SelectedTime();

                            selectedTime.Day = scheduleItem.Day;
                            selectedTime.Month = scheduleItem.Month;
                            selectedTime.Year = scheduleItem.Year;
                            selectedTime.EndTime = scheduleItem.EndTime;
                            selectedTime.ScheduleType = scheduleItem.ScheduleType;
                            selectedTime.StartTime = scheduleItem.StartTime;
                            selectedTime.WeekDay = scheduleItem.WeekDay;
                            //selectedTime.SelectedIndex = ;
                            selectedTimes.Add(selectedTime);

                        }
                        if(isMoreThanOne)
                        {
                            serviceSignUpModel.Add(new Models.SignupData { InputType = "OnlyMinus" });
                        }
                        isMoreThanOne = true;
                        serviceSignUpModel.Add(new Models.SignupData { SessionLocation = serviceItem.WorkLocaton, SessionTeam = serviceItem.TeamSize, selectedTime = selectedTimes, MainSelectedData = serviceItem.ServiceName, SessionDesc = serviceItem.ChargingPeriod, SessionAmount = serviceItem.Charges, InputType = "Service" });
                    }

                    if (App.TrainerStripeUrl != "")
                    {
                        serviceSignUpModel.Add(new Models.SignupData { InputType = "Button", MainSelectedData = "Connect Stripe Account", IsAdditionAvailable = false });
                    }
                    else
                    {
                        serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "You stripe account is connected.", IsAdditionAvailable = false });
                    }

                    StaticListData.Add(serviceSignUpModel);
                }




                EmailAddress = App.LoginResponse.Email;
                Password = App.LoginResponse.Password;
                User64String = App.LoginResponse.ImagePayload;
                SelectedView = App.SelectedView;
                UserIcon = App.LoginResponse.basicInfo.ImageUrl;
                InstaImages = App.LoginResponse.basicInfo.InstaGramImages;

            }

        }


        private async void GetCurrentPosition()
        {

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;


                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                if (position == null)
                {
                    position = await locator.GetLastKnownLocationAsync();

                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to get location: " + ex);
            }

        }

        private void InitializeCalender()
        {
            ListViewData = new ObservableCollection<String>();

            ListViewData.Add("12:00 AM");
            ListViewData.Add("12:15 AM");
            ListViewData.Add("12:30 AM");
            ListViewData.Add("12:45 AM");
            ListViewData.Add("1:00 AM");
            ListViewData.Add("1:15 AM");
            ListViewData.Add("1:30 AM");
            ListViewData.Add("1:45 AM");
            ListViewData.Add("2:00 AM");
            ListViewData.Add("2:15 AM");
            ListViewData.Add("2:30 AM");
            ListViewData.Add("2:45 AM");
            ListViewData.Add("3:00 AM");
            ListViewData.Add("3:15 AM");
            ListViewData.Add("3:30 AM");
            ListViewData.Add("3:45 AM");
            ListViewData.Add("4:00 AM");
            ListViewData.Add("4:15 AM");
            ListViewData.Add("4:30 AM");
            ListViewData.Add("4:45 AM");
            ListViewData.Add("5:00 AM");
            ListViewData.Add("5:15 AM");
            ListViewData.Add("5:30 AM");
            ListViewData.Add("5:45 AM");
            ListViewData.Add("6:00 AM");
            ListViewData.Add("6:15 AM");
            ListViewData.Add("6:30 AM");
            ListViewData.Add("6:45 AM");
            ListViewData.Add("7:00 AM");
            ListViewData.Add("7:15 AM");
            ListViewData.Add("7:30 AM");
            ListViewData.Add("7:45 AM");
            ListViewData.Add("8:00 AM");
            ListViewData.Add("8:15 AM");
            ListViewData.Add("8:30 AM");
            ListViewData.Add("8:45 AM");
            ListViewData.Add("9:00 AM");
            ListViewData.Add("9:00 AM");
            ListViewData.Add("9:15 AM");
            ListViewData.Add("9:30 AM");
            ListViewData.Add("9:45 AM");
            ListViewData.Add("10:00 AM");
            ListViewData.Add("10:15 AM");
            ListViewData.Add("10:30 AM");
            ListViewData.Add("10:45 AM");
            ListViewData.Add("11:00 AM");
            ListViewData.Add("11:15 AM");
            ListViewData.Add("11:30 AM");
            ListViewData.Add("11:45 AM");
            ListViewData.Add("12:00 PM");
            ListViewData.Add("12:15 PM");
            ListViewData.Add("12:30 PM");
            ListViewData.Add("12:45 PM");
            ListViewData.Add("1:00 PM");
            ListViewData.Add("1:15 PM");
            ListViewData.Add("1:30 PM");
            ListViewData.Add("1:45 PM");
            ListViewData.Add("2:00 PM");
            ListViewData.Add("2:15 PM");
            ListViewData.Add("2:30 PM");
            ListViewData.Add("2:45 PM");
            ListViewData.Add("3:00 PM");
            ListViewData.Add("3:15 PM");
            ListViewData.Add("3:30 PM");
            ListViewData.Add("3:45 PM");
            ListViewData.Add("4:00 PM");
            ListViewData.Add("4:15 PM");
            ListViewData.Add("4:30 PM");
            ListViewData.Add("4:45 PM");
            ListViewData.Add("5:00 PM");
            ListViewData.Add("5:15 PM");
            ListViewData.Add("5:30 PM");
            ListViewData.Add("5:45 PM");
            ListViewData.Add("6:00 PM");
            ListViewData.Add("6:15 PM");
            ListViewData.Add("6:30 PM");
            ListViewData.Add("6:45 PM");
            ListViewData.Add("7:00 PM");
            ListViewData.Add("7:15 PM");
            ListViewData.Add("7:30 PM");
            ListViewData.Add("7:45 PM");
            ListViewData.Add("8:00 PM");
            ListViewData.Add("8:15 PM");
            ListViewData.Add("8:30 PM");
            ListViewData.Add("8:45 PM");
            ListViewData.Add("9:00 PM");
            ListViewData.Add("9:15 PM");
            ListViewData.Add("9:30 PM");
            ListViewData.Add("9:45 PM");
            ListViewData.Add("10:00 PM");
            ListViewData.Add("10:15 PM");
            ListViewData.Add("10:30 PM");
            ListViewData.Add("10:45 PM");
            ListViewData.Add("11:00 PM");
            ListViewData.Add("11:15 PM");
            ListViewData.Add("11:30 PM");
            ListViewData.Add("11:45 PM");

        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
