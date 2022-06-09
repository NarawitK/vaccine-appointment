using System;

namespace VaccineReportDataLib.DataModels.UI
{
    public class FormModel : ViewModelBase, IFormModel
    {
        private int _vaccineCode;
        private DateTime _startDate;
        private DateTime _endDate;
        private int? _subVaccinePlanCode;
        private int? _doctorCode;
        public int VaccineCode {
            get => _vaccineCode;
            set
            {
                _vaccineCode = value;
                OnPropertyChanged();
            }
        }
        public DateTime StartDate {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndDate {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
        public int? SubVaccinePlanCode {
            get => _subVaccinePlanCode;
            set
            {
                _subVaccinePlanCode = value;
                OnPropertyChanged();
            }
        }
        public int? DoctorCode {
            get => _doctorCode;
            set
            {
                _doctorCode = value;
                OnPropertyChanged();
            }
        }

    }
}
