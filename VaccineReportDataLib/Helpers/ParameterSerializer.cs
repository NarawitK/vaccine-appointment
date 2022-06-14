using System.Text;
using VaccineReportDataLib.DataModels.UI;

namespace VaccineReportDataLib.Helpers
{
    public static class ParameterSerializer
    {
        public static string AppointParameterSerialize(string basedStatement, IFormModel form)
        {
            System.Diagnostics.Debug.WriteLine(form.StartDate);
            string fixedStatement = string.Format(" WHERE ov.vaccine_plan_no = {0} " +
                "&& ov.person_vaccine_id = {1} " +
                "&& o.vstdate = '{2}' " +
                "&& otp.plan_end_date = '{3}' ", 
                form.Dose, form.VaccineCode, form.GetStartDate, form.GetEndDate);
            StringBuilder sb = new StringBuilder(basedStatement);
            sb.Append(fixedStatement);
            if (form.SubVaccinePlanCode.HasValue)
                sb.Append(string.Format("&& otp.treatment_plan_type_id = {0} ", form.SubVaccinePlanCode));
            if(!string.IsNullOrEmpty(form.DoctorCode))
                sb.Append(string.Format("ov.doctor_code = '{0}' ", form.DoctorCode));
            sb.Append("GROUP BY o.hn");
            return sb.ToString().TrimEnd();
        }
    }
}
