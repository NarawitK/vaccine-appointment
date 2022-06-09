using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VaccineReportDataLib.DataModels.Query;
using VaccineReportDataLib.DataModels.UI;

namespace VaccineReportDataLib.DataAccess.UnitOfWork
{
    public class VaccineRepository : IVaccineRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;

        public VaccineRepository(IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            _dbConnection = dbConnection;
            _dbTransaction = dbTransaction;
        }

        public async Task<IEnumerable<IAppointmentResult>> GetAppointResultAsync(IFormModel formModel)
        {
            string tableName = "person_vaccine";
            string statement = $"SELECT otp.plan_end_date 'next_sched_date', DATE(ov.immunization_datetime) 'injection_date',pv.vaccine_name 'vaccine_name', MAX(ov.vaccine_plan_no) 'inject_no', o.hn, pt.cid,pt.pname, pt.fname, pt.lname, pt.birthday, YEAR(CURDATE())-YEAR(pt.birthday) 'age',CONCAT(IF(pt.addrpart = '-', '-', pt.addrpart), ' หมู่ ', IF(pt.moopart <>'', pt.moopart, '-'), ' ', t.full_name) 'address',IF(pt.hometel IS NOT NULL OR pt.hometel <> '', pt.hometel, pt.informtel) 'tel'FROM {tableName} ov LEFT JOIN person_vaccine pv USING(person_vaccine_id) INNER JOIN ovst o USING(vn) LEFT JOIN patient pt USING(hn) LEFT JOIN thaiaddress t ON pt.amppart = t.amppart && pt.tmbpart = t.tmbpart && pt.chwpart = t.chwpart LEFT JOIN ovst_treatment_plan otp USING(ovst_treatment_plan_id) WHERE ov.person_vaccine_id = 82 && ov.vaccine_plan_no = 1 && otp.plan_end_date = '2022-08-04' && o.vstdate = '2022-05-12' #&& otp.treatment_plan_type_id = 14 #&& ov.doctor_code = '058' GROUP BY o.hn";
            return await _dbConnection.QueryAsync<AppointResult>(statement, null, _dbTransaction, 10, CommandType.Text);
        }

        public async Task<IEnumerable<VaccineComboBoxModel>> GetAllVaccineAsync()
        {
            string tableName = "person_vaccine";
            string statement = $"SELECT person_vaccine_id AS VaccineCode, vaccine_name AS Name from {tableName} WHERE update_moph_registry = 'Y'";
            return await _dbConnection.QueryAsync<VaccineComboBoxModel>(statement, null, _dbTransaction, 10, CommandType.Text);
        }

        public async Task<IEnumerable<SubPlanComboBoxModel>> GetSubPlanByVaccineIdAsync(int vaccineId)
        {
            string tableName = "treatment_plan_type_schedule";
            string statement = $"SELECT tpts.treatment_plan_type_schedule_id AS SubPlanKey, tpt.treatment_plan_ref_id AS PlanKey, tpts.treatment_description AS SubPlanName FROM {tableName} tpts LEFT JOIN treatment_plan_type tpt USING(treatment_plan_type_id) WHERE tpt.treatment_plan_ref_id = 84";
            return await _dbConnection.QueryAsync<SubPlanComboBoxModel>(statement, null, _dbTransaction, 10, CommandType.Text);
        }
    }
}