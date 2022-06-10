using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VaccineReportDataLib.DataModels.UI;
using VaccineReportBackend.Commands;
using VaccineReportDataLib.DataAccess;

namespace VaccineReportBackend.ViewModels
{
    public class MainViewModel : FormModel
    {
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

        #region Command Properties
        public RelayCommand SendCommand { get; }
        public RelayCommand ToExcelCommand { get; }
        #endregion

        public MainViewModel()
        {
            SendCommand = new RelayCommand(GetSomethingCommand, GetSomething_CanExec);
            ToExcelCommand = new RelayCommand(ExportExcelCommand, GetSomething_CanExec);
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
