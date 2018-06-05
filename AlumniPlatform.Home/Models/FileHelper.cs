using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using AlumniPlatform.Logic.Model;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AlumniPlatform.Home.Models
{
    /// <summary>
    /// 文件上传服务
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 基本路径
        /// </summary>
        public static string BasePath = System.AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 保存Excel文件，返回相对路径
        /// </summary>
        /// <param name="excelFile">文件</param>
        /// <param name="filePath">返回路径</param>
        /// <returns></returns>
        public static bool SaveFile(HttpPostedFileBase excelFile, string type, out string filePath)
        {
            filePath = string.Empty;
            if (excelFile == null)
            {
                return false;
            }
            string fileName = excelFile.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();
            string name = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;

            string date = DateTime.Now.Date.ToString("yyyy-MM");
            string path = Path.Combine(BasePath, "File\\" + type + "\\" + date + "\\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string savePath = path + name;

            //filePath = "/File/" + type + "/" + date + "/" + name;

            filePath = savePath;

            excelFile.SaveAs(savePath);

            return true;
        }

        public static DataTable ReadFile(string filePath, string sheetName, bool isConergy)
        {
            IWorkbook hssfworkbook;

            #region//初始化信息

            try
            {
                using (FileStream file = File.OpenRead(filePath))
                {
                    hssfworkbook = new XSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            #endregion

            ISheet sheet = hssfworkbook.GetSheet(sheetName);//.GetSheetAt(0);
            if (sheet == null)
                return null;
            IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable();
            rows.MoveNext();
            XSSFRow row = (XSSFRow)rows.Current;

            if (isConergy)
            {
                for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
                {
                    if (string.IsNullOrEmpty(row.GetCell(j).ToString()))
                    {
                        break;
                    }
                    //将第一列作为列表头  
                    dt.Columns.Add(row.GetCell(j).ToString());

                }
            }
            else
            {
                int count = sheet.GetRow(0).LastCellNum;
                for (int j = 0; j < count; j++)
                {
                    //将第一列作为列表头  
                    if (j > 13 && j <= 26)
                    {
                        dt.Columns.Add(row.GetCell(j).ToString()).SetOrdinal(j);
                    }
                    else
                        dt.Columns.Add(row.GetCell(j).ToString());

                    if (j == 13)
                    {
                        j += 13;
                    }
                    if (j == 32)
                    {
                        j = 13;
                        count = 27;
                        rows.MoveNext();
                        row = (XSSFRow)rows.Current;
                    }
                }
            }
            while (rows.MoveNext())
            {

                row = (XSSFRow)rows.Current;
                int num = row.RowNum;

                DataRow dr = dt.NewRow();
                bool isEmpty = false;

                bool flag = false;
                foreach (ICell temp in row.Cells)
                {
                    if (!string.IsNullOrEmpty(temp.ToString()))
                    {
                        flag = true;
                    }
                }

                if (!flag)
                {
                    break;
                }
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;

        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="inspectionInfos"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static byte[] ExportToExcel(string[] headers, List<AlumniInfo> alumniInfos, string sheetName)
        {
            HSSFWorkbook wk = new HSSFWorkbook();
            ISheet sheet = wk.CreateSheet(sheetName);

            IRow headerRow = sheet.CreateRow(0);
            //设置表头字体
            IFont font = wk.CreateFont();
            font.FontName = "宋体";
            font.Boldweight = (short)FontBoldWeight.Bold;
            //设置表头样式
            ICellStyle style = wk.CreateCellStyle();
            style.FillForegroundColor = HSSFColor.Yellow.Index;
            style.FillPattern = FillPattern.SolidForeground;//填充模式
            style.WrapText = true;
            style.SetFont(font);
            style.Alignment = HorizontalAlignment.Center;

            // 建立表头
            for (int i = 0; i < headers.Length; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(headers[i]);//添加表头


                cell.CellStyle = style;

                sheet.SetColumnWidth(i, 10 * 256);
            }

            int rowIndex = 1;
            foreach (AlumniInfo info in alumniInfos)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                dataRow.CreateCell(0).SetCellValue(info.SerialNumber);
                dataRow.CreateCell(1).SetCellValue(info.RealName);
                dataRow.CreateCell(2).SetCellValue(info.Gender);
                dataRow.CreateCell(3).SetCellValue(info.Nation);
                dataRow.CreateCell(4).SetCellValue(info.NativePlace);
                dataRow.CreateCell(5).SetCellValue(info.BirthDate.ToString());
                dataRow.CreateCell(6).SetCellValue(info.MaritalStatus);
                dataRow.CreateCell(7).SetCellValue(info.Phone);
                dataRow.CreateCell(8).SetCellValue(info.Email);
                dataRow.CreateCell(9).SetCellValue(info.Address);
                dataRow.CreateCell(10).SetCellValue(info.MaxDegree);
                dataRow.CreateCell(11).SetCellValue(info.GraduatedFrom);
                dataRow.CreateCell(12).SetCellValue(info.Major);
                dataRow.CreateCell(13).SetCellValue(info.GraduationDate.ToString());
                dataRow.CreateCell(14).SetCellValue(info.Hobby);
                dataRow.CreateCell(15).SetCellValue(info.AlumniPosition);
                dataRow.CreateCell(16).SetCellValue(info.Company);
                dataRow.CreateCell(17).SetCellValue(info.Position);
                dataRow.CreateCell(18).SetCellValue(info.IsCertified);
                dataRow.CreateCell(19).SetCellValue(info.Remark);
                rowIndex++;
            }

            MemoryStream ms = new MemoryStream();
            wk.Write(ms);

            byte[] buffer = new byte[ms.Length];
            ms.Seek(0, SeekOrigin.Begin);
            ms.Read(buffer, 0, buffer.Length);


            ms.Close();
            ms.Dispose();

            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public static DataTable ReadFile(string strFileName, string sheetName, int rowIndex)
        {
            DataTable dt = new DataTable();

            IWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = WorkbookFactory.Create(file);
            }
            ISheet sheet = hssfworkbook.GetSheet(sheetName);
            IEnumerator rows = sheet.GetRowEnumerator();
            rows.MoveNext();
            XSSFRow row = (XSSFRow)rows.Current;
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                if (string.IsNullOrEmpty(row.GetCell(j).ToString()))
                {
                    break;
                }
                //将第一列作为列表头  
                dt.Columns.Add(row.GetCell(j).ToString());

            }
            while (rows.MoveNext())
            {
                row = (XSSFRow)rows.Current;
                int num = row.RowNum;

                DataRow dr = dt.NewRow();
                bool isEmpty = false;

                bool flag = false;
                foreach (ICell temp in row.Cells)
                {
                    if (!string.IsNullOrEmpty(temp.ToString()))
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    break;
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        flag = false;
                        break;
                    }
                    switch (cell.CellType)
                    {
                        case CellType.Numeric:
                            if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型
                            {
                                dr[i] = cell.DateCellValue.ToString("yyyy-MM-dd");
                            }
                            else//其他数字类型
                            {
                                dr[i] = cell.NumericCellValue;
                            }
                            break;
                        case CellType.Blank:
                            dr[i] = string.Empty;
                            break;
                        case CellType.Formula:
                            if (Path.GetExtension(strFileName).ToLower().Trim() == ".xlsx")
                            {
                                XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(hssfworkbook);
                                CellValue v=eva.Evaluate(cell);
                                if (eva.Evaluate(cell).CellType == CellType.Numeric)
                                {
                                    if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型
                                    {
                                        dr[i] = cell.DateCellValue.ToString("yyyy-MM-dd");
                                    }
                                    else//其他数字类型
                                    {
                                        dr[i] = cell.NumericCellValue;
                                    }
                                }
                                else
                                {
                                    dr[i] = eva.Evaluate(cell).StringValue;
                                }
                            }
                            else
                            {
                                HSSFFormulaEvaluator eva = new HSSFFormulaEvaluator(hssfworkbook);
                                if (eva.Evaluate(cell).CellType == CellType.Numeric)
                                {
                                    if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型
                                    {
                                        dr[i] = cell.DateCellValue.ToString("yyyy-MM-dd");
                                    }
                                    else//其他数字类型
                                    {
                                        dr[i] = cell.NumericCellValue;
                                    }
                                }
                                else
                                {
                                    dr[i] = eva.Evaluate(cell).StringValue;
                                }
                            }
                            break;
                        default:
                            dr[i] = cell.StringCellValue;
                            break;
                    }

                }
                if(flag)
                    dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}