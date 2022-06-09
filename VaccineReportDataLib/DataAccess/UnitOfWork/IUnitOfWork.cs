using System;

namespace VaccineReportDataLib.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDoctorCodeRepository DoctorCodeRepository { get; }
        IVaccineRepository VaccineRepository { get; }
        void Commit();
    }
}
