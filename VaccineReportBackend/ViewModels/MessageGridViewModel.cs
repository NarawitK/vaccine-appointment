using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VaccineReportDataLib.DataModels.UI;

namespace VaccineReportBackend.ViewModels
{
    public class MessageGridViewModel : ViewModelBase
    {
        private readonly string StatusImgBasePath = "pack://application:,,,/Assets/";
        private readonly string[] imageNameTuple = new string[2] { "valid.png", "warning.png" };
        private ImageSource _statusImgPath;
        private string? _message;
        public ImageSource StatusImgPath
        {
            get => _statusImgPath;
            set
            {
                _statusImgPath = value;
                OnPropertyChanged();
            }
        }
        public string? Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }
        private bool _isMessageGridVisible;
        public bool IsMessageGridVisible
        {
            get => _isMessageGridVisible;
            set
            {
                _isMessageGridVisible = value;
                OnPropertyChanged();
            }
        }

        public MessageGridViewModel()
        {
            this.InitProperties();

        }
        private void InitProperties()
        {
            SetMessageGridMessage(false, IconStateEnum.Valid, "Application is Ready");

        }
        public void SetMessageGridMessage(bool visible, IconStateEnum iconState, string? message = null)
        {
            StatusImgPath = DetermineImageFileName(iconState);
            Message = message;
            SetMessageGridVisibilty(visible);
        }
        public void SetMessageGridVisibilty(bool visibility)
        {
            IsMessageGridVisible = visibility;
        }
        private ImageSource DetermineImageFileName(IconStateEnum iconState)
        {
            Uri uri = new(string.Format(@"{0}{1}", StatusImgBasePath, imageNameTuple[(int)iconState]), UriKind.RelativeOrAbsolute);
            ImageSource imgSource = new BitmapImage(uri);
            return imgSource;
        }
    }
}