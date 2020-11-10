using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace DFS.ViewModels
{
    public class ImagePopupViewModel : INotifyPropertyChanged
    {
        private string imageSource;
        public string ImageSource {
            get { return imageSource; }
            set {
                imageSource = value;
                RaisePropertyChanged(nameof(ImageSource));
            }
        }

        public ICommand CloseCommand { get { return new Command(CloseHandler); } }
        private async void CloseHandler(object obj)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public ImagePopupViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
