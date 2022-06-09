using System.Collections.Generic;
using System.Threading.Tasks;
using VaccineReportDataLib.DataModels.UI;
using VaccineReportDataLib.DataModels.Query;

namespace VaccineReportDataLib.DataAccess.UnitOfWork
{
    public interface IVaccineRepository
    {
        Task<IEnumerable<VaccineComboBoxModel>> GetAllVaccineAsync();
        Task<IEnumerable<SubPlanComboBoxModel>> GetSubPlanByVaccineIdAsync(int vaccineId);
        Task<IEnumerable<IAppointmentResult>> GetAppointResultAsync(IFormModel formModel);
    }
}
