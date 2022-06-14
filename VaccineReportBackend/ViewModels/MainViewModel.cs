using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VaccineReportDataLib.DataModels.UI;
using VaccineReportBackend.Commands;
using VaccineReportDataLib.DataAccess.UnitOfWork;
using System.Collections.ObjectModel;
using VaccineReportDataLib.DataModels.Query;
using VaccineReportBackend.Exporter;

namespace VaccineReportBackend.ViewModels
{
    public class MainViewModel : FormModel
    {
        public override int VaccineCode { 
            get => base.VaccineCode;
            set {
                base.VaccineCode = value;
                PopulateSubPlanList(value);
                OnPropertyChanged();
            }  
        }
        public override DateTime StartDate { 
            get => base.StartDate; 
            set 
            { 
                base.StartDate = value;
                EndDate = ChangeEndDateOnStartDateChanged(StartDate, EndDate, 21);
                OnPropertyChanged();
            }
        }
        #region ComboBoxList
        private IEnumerable<SubPlanComboBoxModel> _defaultSubPlans;
        private ObservableCollection<VaccineComboBoxModel> _vaccines;
        public ObservableCollection<VaccineComboBoxModel> Vaccines
        {
            get => _vaccines;
            set
            {
                _vaccines = value;
                OnPropertyChanged();

            }
        }
        private ObservableCollection<SubPlanComboBoxModel> _subplans;
        public ObservableCollection<SubPlanComboBoxModel> SubPlans
        {
            get => _subplans;
            set
            {
                _subplans = value;
                OnPropertyChanged();
            }
        }        
        private ObservableCollection<DoctorCodeModel> _doctors;
        public ObservableCollection<DoctorCodeModel> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<IAppointmentResult> _appointmentResults;
        public ObservableCollection<IAppointmentResult> AppointmentResults
        {
            get => _appointmentResults;
            set
            {
                _appointmentResults = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Visibility
        private string _isDataGridVisible;
        public string IsDataGridVisible
        {
            get => _isDataGridVisible;
            set
            {
                _isDataGridVisible = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command Properties
        public RelayCommand SendCommand { get; }
        public RelayCommand ToExcelCommand { get; }
        #endregion

        public MainViewModel()
        {
            InitProperties();
            SendCommand = new RelayCommand(GetSomethingCommand, GetSomething_CanExec);
            ToExcelCommand = new RelayCommand(ExportExcelCommand, GetSomething_CanExec);
            Doctors = new ObservableCollection<DoctorCodeModel>();
            Vaccines = new ObservableCollection<VaccineComboBoxModel>();
            InitializeComboBoxDataSource();
        }

        private async void InitializeComboBoxDataSource()
        {
            try
            {
                using IUnitOfWork unitOfWork = Caller.GetDefaultUnitOfWork();
                Doctors = new ObservableCollection<DoctorCodeModel>(await unitOfWork.DoctorCodeRepository.GetAllDoctorsAsync());                
                Vaccines = new ObservableCollection<VaccineComboBoxModel>(await unitOfWork.VaccineRepository.GetAllVaccineAsync());
                Doctors.Insert(0, new DoctorCodeModel()
                {
                    DoctorCode = null,
                    Initials = null,
                    Firstname = "None", 
                    Surname = "Selected", 
                });
                _defaultSubPlans = await unitOfWork.VaccineRepository.GetAllSubPlanAsync();
            }
            catch(Exception e)
            {
                // TO-DOs:::
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        private void PopulateSubPlanList(int vaccineCode)
        {
            SubPlans = new ObservableCollection<SubPlanComboBoxModel>(_defaultSubPlans.Where(x => x.VaccinePlanKey == vaccineCode));
            SubPlans.Insert(0, new SubPlanComboBoxModel()
            {
                SubPlanKey = null,
                VaccinePlanKey = vaccineCode,
                SubPlanName = "None Selected",
            });
        }

        private DateTime ChangeEndDateOnStartDateChanged(DateTime startDate, DateTime endDate, double daysToAdd)
        {
            if(startDate >= endDate)
            {
                return endDate.AddDays(daysToAdd);
            }
            else
            {
                return endDate;
            }
        }

        #region CommandDeclaration
        private async void GetSomethingCommand()
        {
            try
            {
                using IUnitOfWork unitOfWork = Caller.GetDefaultUnitOfWork();
                AppointmentResults = new ObservableCollection<IAppointmentResult>(await unitOfWork.VaccineRepository.GetAppointResultAsync(this));
                IsDataGridVisible = "Visible";
            }
            catch(Exception e)
            {
                IsDataGridVisible = "Collapsed";
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        private void ExportExcelCommand()
        {
            ExcelExporter.GenerateExcel(ExcelExporter.ConvertToDataTable(AppointmentResults));
        }
        private bool GetSomething_CanExec()
        {
            // TO-DO::
            return true;
        }
        #endregion

        private void InitProperties()
        {
            Dose = 1;
            StartDate = DateTime.Today;
            EndDate = StartDate.AddDays(28);
            IsDataGridVisible = "Collapsed";
        }
    }
}
