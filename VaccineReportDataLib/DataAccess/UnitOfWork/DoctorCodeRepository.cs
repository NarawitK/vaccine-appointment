using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VaccineReportDataLib.DataModels.UI;

namespace VaccineReportDataLib.DataAccess.UnitOfWork
{
    public class DoctorCodeRepository : IDoctorCodeRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;

        public DoctorCodeRepository(IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            _dbConnection = dbConnection;
            _dbTransaction = dbTransaction;
        }

        public async Task<IEnumerable<DoctorCodeModel>> GetAllDoctorsAsync()
        {
            string tableName = "person_vaccine";
            string statement = $"SELECT person_vaccine_id AS VaccineCode, vaccine_name AS Name from {tableName} WHERE update_moph_registry = 'Y'";
            return await _dbConnection.QueryAsync<DoctorCodeModel>(statement, null, _dbTransaction, 10, CommandType.Text);
        }

        public async Task<IEnumerable<DoctorCodeModel>> GetDoctorsByIdAsync(IEnumerable<string> doctorCode)
        {
            string tableName = "person_vaccine";
            string statement = $"SELECT person_vaccine_id AS VaccineCode, vaccine_name AS Name from {tableName} WHERE update_moph_registry = 'Y'";
            return await _dbConnection.QueryAsync<DoctorCodeModel>(statement, null, _dbTransaction, 10, CommandType.Text);
        }
    }
}
