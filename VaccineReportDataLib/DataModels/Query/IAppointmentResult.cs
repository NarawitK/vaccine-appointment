using System;

namespace VaccineReportDataLib.DataModels.Query
{
    public interface IAppointmentResult
    {
        public DateTime NextScheduleDate { get; set; }
        public DateTime InjectionDate { get; set; }
        public string VaccineName { get; set; }
        public int InjectionNumber { get; set; }
        public string HN { get; set; }
        public string CID { get; set; }
        public string Initials { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string Fullname { get; }

    }
}
