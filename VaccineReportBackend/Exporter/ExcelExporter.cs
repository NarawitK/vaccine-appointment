using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace VaccineReportBackend.Exporter
{
    public class ExcelExporter
    {
        public static void GenerateExcel(DataTable dt, string? path = null)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            Excel.Application excelApp = new Excel.Application();  
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Add();  
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)excelWorkBook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            foreach (DataTable tbl in ds.Tables)
            {
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = tbl.TableName;

                // Add Cols.
                for(int i = 1; i < tbl.Columns.Count+1; i++)
                {
                    excelWorkSheet.Cells[1, i] = tbl.Columns[i - 1].ColumnName;
                }

                //Add Rows.
                for (int j = 0; j < tbl.Rows.Count; j++)
                {
                    for(int k = 0; k < tbl.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = tbl.Rows[j].ItemArray[k].ToString();
                    }
                }
            }

            

        }
        public static DataTable ConvertToDataTable<T>(IEnumerable<T> models)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(PropertyInfo prop in Props)
            {
                dt.Columns.Add(prop.Name);
            }
            foreach(T item in models)
            {
                var values = new object[Props.Length];
                for(int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
    }
}
