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
        private readonly string tableName = "doctor";

        public DoctorCodeRepository(IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            _dbConnection = dbConnection;
            _dbTransaction = dbTransaction;
        }

        public async Task<IEnumerable<DoctorCodeModel>> GetAllDoctorsAsync()
        {
            string statement = $"SELECT code AS DoctorCode, pname AS Initials, fname AS Firstname, lname AS Surname FROM {tableName}";
            return await _dbConnection.QueryAsync<DoctorCodeModel>(statement, null, _dbTransaction, 10, CommandType.Text);
        }

        public async Task<IEnumerable<DoctorCodeModel>> GetDoctorsByIdAsync(IEnumerable<string> doctorCode)
        {
            string statement = $"SELECT code AS DoctorCode, pname AS Initials, fname AS Firstname, lname AS Surname FROM {tableName} WHERE code = '{doctorCode}'";
            return await _dbConnection.QueryAsync<DoctorCodeModel>(statement, null, _dbTransaction, 10, CommandType.Text);
        }
    }
}
