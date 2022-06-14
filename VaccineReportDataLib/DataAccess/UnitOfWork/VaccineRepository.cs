using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VaccineReportDataLib.DataModels.Query;
using VaccineReportDataLib.DataModels.UI;
using VaccineReportDataLib.Helpers;

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
            string tableName = "ovst_vaccine";
            string basedStatement = $"SELECT otp.plan_end_date 'NextScheduleDate', DATE(ov.immunization_datetime) 'InjectionDate', pv.vaccine_name 'VaccineName', MAX(ov.vaccine_plan_no) 'InjectionNumber', o.hn 'HN', pt.cid 'CID', pt.pname 'Initials', pt.fname 'Firstname', pt.lname 'Surname', pt.birthday 'Birthdate', YEAR(CURDATE())-YEAR(pt.birthday) 'Age', CONCAT(IF(pt.addrpart = '-', '-', pt.addrpart), ' หมู่ ', IF(pt.moopart <>'', pt.moopart, '-'), ' ', t.full_name) 'Address',IF(pt.hometel IS NOT NULL OR pt.hometel <> '', pt.hometel, pt.informtel) 'Telephone' " +
                $"FROM {tableName} ov " +
                $"LEFT JOIN person_vaccine pv USING(person_vaccine_id) " +
                $"INNER JOIN ovst o USING(vn) LEFT JOIN patient pt USING(hn) " +
                $"LEFT JOIN thaiaddress t ON pt.amppart = t.amppart && pt.tmbpart = t.tmbpart && pt.chwpart = t.chwpart " +
                $"LEFT JOIN ovst_treatment_plan otp USING(ovst_treatment_plan_id)";
            basedStatement = ParameterSerializer.AppointParameterSerialize(basedStatement, formModel);
            return await _dbConnection.QueryAsync<AppointResult>(basedStatement, null, _dbTransaction, 10, CommandType.Text);
        }

        public async Task<IEnumerable<VaccineComboBoxModel>> GetAllVaccineAsync()
        {
            string tableName = "person_vaccine";
            string statement = $"SELECT person_vaccine_id AS VaccineCode, vaccine_name AS Name from {tableName} WHERE update_moph_registry = 'Y'";
            return await _dbConnection.QueryAsync<VaccineComboBoxModel>(statement, null, _dbTransaction, 10, CommandType.Text);
        }

        public async Task<IEnumerable<SubPlanComboBoxModel>> GetAllSubPlanAsync()
        {
            string tableName = "treatment_plan_type_schedule";
            string statement = $"SELECT tpts.treatment_plan_type_id AS SubPlanKey,  tpt.treatment_plan_ref_id AS VaccinePlanKey, tpts.treatment_description AS SubPlanName FROM {tableName} tpts" +
                $" LEFT JOIN treatment_plan_type tpt USING(treatment_plan_type_id)" +
                $" WHERE tpts.treatment_number > 1";
            return await _dbConnection.QueryAsync<SubPlanComboBoxModel>(statement, null, _dbTransaction, 10, CommandType.Text);
        }

        public async Task<IEnumerable<SubPlanComboBoxModel>> GetSubPlanByVaccineIdAsync(int vaccineId)
        {
            string tableName = "treatment_plan_type_schedule";
            string statement = $"SELECT tpts.treatment_plan_type_id AS SubPlanKey, tpt.treatment_plan_ref_id AS VaccinePlanKey, tpts.treatment_description AS SubPlanName FROM {tableName} tpts " +
                $"LEFT JOIN treatment_plan_type tpt USING(treatment_plan_type_id) WHERE tpt.treatment_plan_ref_id = {vaccineId} && tpts.treatment_number > 1";
            return await _dbConnection.QueryAsync<SubPlanComboBoxModel>(statement, null, _dbTransaction, 10, CommandType.Text);
        }
    }
}
