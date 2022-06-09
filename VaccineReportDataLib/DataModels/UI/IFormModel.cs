using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineReportDataLib.DataModels.UI
{
    public interface IFormModel
    {
        public int VaccineCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? SubVaccinePlanCode { get; set; }
        public int? DoctorCode { get; set; }
    }
}
