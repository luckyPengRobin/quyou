using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//using System.Web;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
namespace ToolKits
{
    public class ExcelHelper
    {
        /// <summary>
        /// Read Excel to datatable
        /// </summary>
        /// <param name="FileFullName"></param>
        /// <returns></returns>
        public static DataTable Read(string FileFullName)
        {
            if (File.Exists(FileFullName))
            {
                DataTable tb = new DataTable();

                if (Path.GetExtension(FileFullName).ToLower().Equals(".xls"))
                {
                    tb = ReadExcel_NPOI(FileFullName);
                }
                else
                {
                    tb = ReadExcel_EPPlus(FileFullName);   //.xlsx
                }

                File.Delete(FileFullName);   //delete file
                return tb;            
            }           
            return null;       
        }

        /// <summary>
        /// 读取excel 第headrow行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        private static DataTable ReadExcel_NPOI(string strFileName, int headRow)
        {
            headRow--;
            DataTable dt = new DataTable();
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(headRow);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (headRow + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }



        /// <summary>
        /// read excel by EPPlus to data table 
        /// 它的索引号是从1开始的，主要使用的类型位于OfficeOpenXml空间
        /// </summary>
        /// <param name="xlsStream"></param>
        /// start from 1
        /// <returns></returns>
        private static DataTable ReadExcel_EPPlus(string FileFullName, int headrow)
        {
            DataTable table = new DataTable();
            using (FileStream filestream = new FileStream(FileFullName, FileMode.Open, FileAccess.Read))
            {
                using (ExcelPackage package = new ExcelPackage(filestream))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[1];

                    int colCount = sheet.Dimension.End.Column;
                    int rowCount = sheet.Dimension.End.Row;

                    for (ushort j = 1; j <= colCount; j++)
                    {
                        table.Columns.Add(new DataColumn(sheet.Cells[headrow, j].Value == null ? "" : sheet.Cells[headrow, j].Value.ToString()));
                    }

                    for (ushort i = (ushort)(headrow + 1); i <= rowCount; i++)
                    {
                        DataRow row = table.NewRow();
                        for (ushort j = 1; j <= colCount; j++)
                        {

                            row[j - 1] = sheet.Cells[i, j].Value;

                            if (j == 3 && sheet.Cells[i,j].Value==null) {

                                row[j - 1] = " ";
                            }

                        }
                        table.Rows.Add(row);
                    }
                }
            }
            return table;
        }



        public static DataTable Read(string FileFullName, int headrow)
        {
            if (File.Exists(FileFullName))
            {
                DataTable tb = new DataTable();

                if (Path.GetExtension(FileFullName).ToLower().Equals(".xls"))
                {
                    tb = ReadExcel_NPOI(FileFullName, headrow);
                }
                else
                {
                    tb = ReadExcel_EPPlus(FileFullName, headrow);   //.xlsx
                }

                File.Delete(FileFullName);   //delete file
                return tb;
            }
            return null;
        }
        /// <summary>
        /// BOM  only accept .xlsx
        /// </summary>
        /// <param name="FileFullName"></param>
        /// <returns></returns>
        public static DataTable Read_PO(string FileFullName)
        {
            if (File.Exists(FileFullName))
            {
                DataTable table = new DataTable();
                using (FileStream filestream = new FileStream(FileFullName, FileMode.Open, FileAccess.Read))
                {
                    using (ExcelPackage package = new ExcelPackage(filestream))
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets[1];

                        int colCount = sheet.Dimension.End.Column;
                        int rowCount = sheet.Dimension.End.Row;
 
                        for (ushort j = 1; j <= colCount; j++)
                        {
                            table.Columns.Add(new DataColumn(sheet.Cells[2, j].Value == null ? "" : sheet.Cells[2, j].Value.ToString()));                        

                        }

                        table.Columns["Qty"].DataType = typeof(decimal);
                        table.Columns["UnitPrice"].DataType = typeof(decimal);
                        table.Columns["OuterBoxDim"].DataType = typeof(decimal);
                        table.Columns["OuterBoxQty"].DataType = typeof(decimal);
                        table.Columns["Weight"].DataType = typeof(decimal);


                        for (ushort i = 3; i <= rowCount; i++)
                        {
                            DataRow row = table.NewRow();
                            for (ushort j = 1; j <= colCount; j++)
                            {
                               

                                row[j - 1] = sheet.Cells[i, j].Value == null ? "" : sheet.Cells[i, j].Value;                             
                            }
                            table.Rows.Add(row);
                        }



                    }
                }
                

                File.Delete(FileFullName);   //delete file
              
                return table;
            }
            return null;
        }

        /// <summary>
        /// read excel by EPPlus to data table 
        /// 它的索引号是从1开始的，主要使用的类型位于OfficeOpenXml空间
        /// </summary>
        /// <param name="xlsStream"></param>
        /// <returns></returns>
        private static DataTable ReadExcel_EPPlus(string FileFullName)
        {
            DataTable table = new DataTable();
            using (FileStream filestream = new FileStream(FileFullName, FileMode.Open, FileAccess.Read))
            {
                using (ExcelPackage package = new ExcelPackage(filestream))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[1];

                    int colCount = sheet.Dimension.End.Column;
                    int rowCount = sheet.Dimension.End.Row;

                    for (ushort j = 1; j <= colCount; j++)
                    {
                        table.Columns.Add(new DataColumn(sheet.Cells[1, j].Value == null ? "" : sheet.Cells[1, j].Value.ToString()));
                    }

                    for (ushort i = 2; i <= rowCount; i++)
                    {
                        DataRow row = table.NewRow();
                        for (ushort j = 1; j <= colCount; j++)
                        {

                            row[j - 1] = sheet.Cells[i, j].Value;
                           
                        }
                        table.Rows.Add(row);
                    }
                }
            }
            return table;
        }


        /// <summary>
        /// 读取excel 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        private static DataTable ReadExcel_NPOI(string strFileName)
        {
            DataTable dt = new DataTable();
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }



        /// <summary>
        /// pre check, only one sheet acceptable
        /// </summary>
        /// <param name="FileFullName"></param>
        /// <returns></returns>
        public static string preCheck(string FileFullName)
        {
            string rtnMsg = string.Empty;
            using (FileStream filestream = new FileStream(FileFullName, FileMode.Open, FileAccess.Read))
            {
                using (ExcelPackage package = new ExcelPackage(filestream))
                {
                    int coutSheets = package.Workbook.Worksheets.Count;
                    if(coutSheets >1)
                    {
                        rtnMsg = "Error,only one sheet acceptable";
                    }
                }
            }
            return rtnMsg;


        }




        

    }
}
