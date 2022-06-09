using System;
using System.ComponentModel;

namespace VaccineReportDataLib.DataModels.UI
{
    public interface IFormModel : INotifyPropertyChanged
    {
        public int VaccineCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? SubVaccinePlanCode { get; set; }
        public int? DoctorCode { get; set; }
    }
}
