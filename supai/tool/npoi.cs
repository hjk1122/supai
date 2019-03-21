using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using supai.model;

namespace supai.tool
{
    public class npoi
    {
        /// <summary>
        /// 输出excel文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string WriteFile(string filePath, List<FYModel> lstFYModel)
        {
            string result = "";
            try
            {
                int rowCount = lstFYModel.Count;
                HSSFWorkbook workbook2003 = new HSSFWorkbook(); //新建工作簿  
                workbook2003.CreateSheet("Sheet1");  //新建1个Sheet工作表              
                HSSFSheet SheetOne = (HSSFSheet)workbook2003.GetSheet("Sheet1"); //获取名称为Sheet1的工作表  
                                                                                 
                for (int i = 0; i < rowCount+1; i++)
                {
                    SheetOne.CreateRow(i);   //创建行  
                }
                //对每一行创建2个单元格  
                HSSFRow SheetRow = (HSSFRow)SheetOne.GetRow(0);  //获取Sheet1工作表的首行  
                HSSFCell[] SheetCell = new HSSFCell[2];
                for (int i = 0; i < 2; i++)
                {
                    SheetCell[i] = (HSSFCell)SheetRow.CreateCell(i);  //为第一行创建2个单元格  
                }
                SheetCell[0].SetCellValue("关联单号");            
                SheetCell[1].SetCellValue("分运单号"); 
                for(int i=1;i<rowCount+1;i++)
                {
                    HSSFRow SheetRow1 = (HSSFRow)SheetOne.GetRow(i);    
                    //HSSFCell[] SheetCell1 = new HSSFCell[2];
                    for (int j = 0; j < 2; j++)
                    {
                        SheetCell[j] = (HSSFCell)SheetRow1.CreateCell(j);  //为第一行创建2个单元格
                    }
                    SheetCell[0].SetCellValue(lstFYModel[i - 1].GLid);
                    SheetCell[1].SetCellValue(lstFYModel[i - 1].FYid);

                }
                string filepath = filePath;
                FileStream file2003 = new FileStream(filepath, FileMode.Create);
                workbook2003.Write(file2003);
                file2003.Close();
                workbook2003.Close();
                result = "1";
            }
            catch (Exception e)
            {
                result = "-1";
            }

            return result;
        }

        public List<string> GetDH(string strPath)
        {
            List<string> lstDH=new List<string>();
            IWorkbook workbook = null;  //新建IWorkbook对象  
            string fileName = strPath;
            FileStream fileStream = new FileStream(@fileName, FileMode.Open, FileAccess.Read);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
            {
                workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
            }
            else if (fileName.IndexOf(".xls") > 0) // 2003版本  
            {
                workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
            }
            ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  
            IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  
            for (int i = 1; i < sheet.LastRowNum; i++)  //对工作表每一行  
            {
                row = sheet.GetRow(i);   //row读入第i行数据  
                if (row != null)
                {
                    //for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
                    //{
                        string cellValue = row.GetCell(0).ToString(); //获取i行j列数据  
                        //Console.WriteLine(cellValue);
                    lstDH.Add(cellValue);
                    //}
                }
            }
            //Console.ReadLine();
            fileStream.Close();
            workbook.Close();
            return lstDH;
        }
    }
    
}
