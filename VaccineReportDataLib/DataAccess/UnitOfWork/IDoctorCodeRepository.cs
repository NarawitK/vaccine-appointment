using System.Collections.Generic;
using System.Threading.Tasks;
using VaccineReportDataLib.DataModels.UI;

namespace VaccineReportDataLib.DataAccess.UnitOfWork
{
    public interface IDoctorCodeRepository
    {
        Task<IEnumerable<DoctorCodeModel>> GetAllDoctorsAsync();
        Task<IEnumerable<DoctorCodeModel>> GetDoctorsByIdAsync(IEnumerable<string> doctorCode);
    }
}
