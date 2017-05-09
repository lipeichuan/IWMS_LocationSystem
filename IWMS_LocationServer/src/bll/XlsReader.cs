using NLog;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWMS_LocationServer.src.bll
{
    public class XlsReader
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static List<string> readSn(string file)
        {
            List<string> snlist = new List<string>();
            try
            {
                FileInfo newFile = new FileInfo(file);
                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    for (int i = 0; i < package.Workbook.Worksheets.Count; i++)
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets.ElementAt(i);
                        if (workSheet.Dimension != null)
                        {
                            for (int j = workSheet.Dimension.Start.Row; j <= workSheet.Dimension.End.Row; j++)
                            {
                                string strSn = workSheet.Cells[j, 1].Value.ToString();
                                snlist.Add(strSn);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return snlist;
        }
    }
}
