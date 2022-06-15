using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace VaccineReportBackend.Exporter
{
    public class ExcelExporter
    {
        private static readonly IList<string> columnName = new List<string>()
        {
            "วันที่นัดถัดไป",
            "วันที่ฉีด",
            "ชื่อวัคซีน",
            "เข็มที่ฉีดในวันที่ฉีด",
            "HN",
            "เลขประจำตัวประชาชน",
            "คำนำหน้า",
            "ชื่อ",
            "นามสกุล",
            "ชื่อเต็ม",
            "วันเกิด",
            "อายุ",
            "ที่อยู่",
            "เบอร์โทรศัพท์"
        };
        public static void GenerateExcel(DataTable dt, string? path = null)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            Excel.Application excelApp = new Excel.Application();  
            if(excelApp == null)
            {
                throw new Exception("This function requires Office 2016 or later");
            }
            Excel.Workbook xlWorkBook = excelApp.Workbooks.Add();  
            Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkBook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            foreach (DataTable tbl in ds.Tables)
            {
                //Excel.Worksheet excelWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets.Add();
                xlWorksheet.Name = tbl.TableName;

                // Add Cols.
                for(int i=1; i < tbl.Columns.Count+1; i++)
                {
                    switch (tbl.Columns[i - 1].ColumnName)
                    {
                        case "NextScheduleDate":
                        case "InjectionDate":
                            xlWorksheet.Columns[i].NumberFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                            break;
                        case "HN":
                        case "Birthdate":
                        case "CID":
                        case "Telephone":
                            xlWorksheet.Columns[i].NumberFormat = "@";
                            break;
                        default:
                            break;
                    }
                    xlWorksheet.Cells[1, i] = columnName[i-1];
                    // xlWorksheet.Cells[1, i] = tbl.Columns[i-1].ColumnName;
                }

                //Add Rows.
                for (int j = 0; j < tbl.Rows.Count; j++)
                {
                    for(int k = 0; k < tbl.Columns.Count; k++)
                    {
                        xlWorksheet.Cells[j+2, k+1] = tbl.Rows[j].ItemArray[k].ToString();
                    }
                }

            };
            xlRange.EntireColumn.AutoFit();
            excelApp.Visible = true;
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(excelApp);
        }
        public static DataTable ConvertToDataTable<T>(IEnumerable<T> models)
        {
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            DataTable dt = new DataTable("VaccineAppointment");
            foreach(PropertyInfo prop in Props)
            {
                dt.Columns.Add(prop.Name);
            }
            foreach(T item in models)
            {
                object[] values = new object[Props.Length];
                for(int i = 0; i < Props.Length; i++)
                {
                    if(Props[i].GetValue(item, null).GetType().Name == "DateTime")
                    {
                        DateTime datetimeValue = (DateTime)Props[i].GetValue(item, null);
                        values[i] = datetimeValue.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
        
    }
}
