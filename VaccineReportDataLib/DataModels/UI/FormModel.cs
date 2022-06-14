using System;
using System.Globalization;

namespace VaccineReportDataLib.DataModels.UI
{
    public class FormModel : ViewModelBase, IFormModel
    {
        private int _vaccineCode;
        private int _dose;
        private DateTime _startDate;
        private DateTime _endDate;
        private int? _subVaccinePlanCode;
        private string _doctorCode;
        public virtual int VaccineCode {
            get => _vaccineCode;
            set
            {
                _vaccineCode = value;
                OnPropertyChanged();
            }
        }
        public int Dose
        {
            get => _dose;
            set
            {
                _dose = value;
                OnPropertyChanged();
            }
        }

        public virtual DateTime StartDate {
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
        public string DoctorCode {
            get => _doctorCode;
            set
            {
                _doctorCode = value;
                OnPropertyChanged();
            }
        }

        public string GetStartDate => _startDate.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
        public string GetEndDate => _endDate.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
    }
}