namespace VaccineReportDataLib.DataModels.UI
{
    public class DoctorCodeModel
    {
        public string DoctorCode { get; set; }
        public string Initials { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string FullName { get => string.Format("{0} {1} {2}", Initials, Firstname, Surname);
    }
}
