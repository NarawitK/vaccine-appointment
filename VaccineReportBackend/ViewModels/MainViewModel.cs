using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VaccineReportDataLib.DataModels.UI;
using VaccineReportBackend.Commands;
using VaccineReportDataLib.DataAccess.UnitOfWork;
using System.Collections.ObjectModel;

namespace VaccineReportBackend.ViewModels
{
    public class MainViewModel : FormModel
    {
        #region ComboBoxList
        private ObservableCollection<SubPlanComboBoxModel> _defaultSubPlans;
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
        #endregion

        #region Visibility
        private bool _isDataGridVisible;
        public bool IsDataGridVisible
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
            Dose = 1;
            StartDate = DateTime.Today;
            EndDate = StartDate.AddDays(28);
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
                IEnumerable<DoctorCodeModel> doctorSet =  await unitOfWork.DoctorCodeRepository.GetAllDoctorsAsync();
                Doctors = new ObservableCollection<DoctorCodeModel>(doctorSet);                
                IEnumerable<VaccineComboBoxModel> vacSet =  await unitOfWork.VaccineRepository.GetAllVaccineAsync();
                Vaccines = new ObservableCollection<VaccineComboBoxModel>(vacSet);
                IEnumerable<SubPlanComboBoxModel> subplanSet = await unitOfWork.VaccineRepository.GetAllSubPlanAsync();
                _defaultSubPlans = new ObservableCollection<SubPlanComboBoxModel>(subplanSet);
            }
            catch(Exception e)
            {
                // TO-DOs:::
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        private async void SubPlanFilterTrigger()
        {

        }


        #region CommandDeclaration
        private void GetSomethingCommand()
        {
            // TO-DO::
        }

        private void ExportExcelCommand()
        {
            // TO-DO::
        }
        private bool GetSomething_CanExec()
        {
            // TO-DO::
            return true;
        }

        private void ResetWindow()
        {
            // TO-DO::
        }
        #endregion
    }
}
