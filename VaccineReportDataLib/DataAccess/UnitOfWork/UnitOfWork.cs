using DatabaseCore.Interfaces;
using System.Data;

namespace VaccineReportDataLib.DataAccess.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction _dbTransaction;
        private IDbConnection _dbConnection;
        private IDoctorCodeRepository _doctorCodeRepository;
        private IVaccineRepository _vaccineRepository;
        public UnitOfWork(IDatabase dbConnection)
        {
            //Currently Use MyISAM db engine which not support transaction. So transaction is disabled.
            _dbConnection = dbConnection.Connection;
            _dbTransaction = null;
            /*
             * Uncomment here for TX Usage
			 * _dbConnection = dbConnection.Open();
             * _dbTransaction = _dbConnection.BeginTransaction();
            */
        }

        public IDoctorCodeRepository DoctorCodeRepository => _doctorCodeRepository ??= new DoctorCodeRepository(_dbConnection, _dbTransaction);
        public IVaccineRepository VaccineRepository=> _vaccineRepository ??= new VaccineRepository(_dbConnection, _dbTransaction);
     

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch
            {
                _dbTransaction.Rollback();
                throw;
            }
            finally
            {
                _dbTransaction.Dispose();
                _dbTransaction = _dbConnection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _doctorCodeRepository = null;
            _vaccineRepository = null;
        }

        public void Dispose()
        {
            if (_dbTransaction != null)
            {
                _dbTransaction.Dispose();
                _dbTransaction = null;
            }
            if (_dbConnection != null)
            {
                _dbConnection.Dispose();
                _dbConnection = null;
            }
        }
    }
}