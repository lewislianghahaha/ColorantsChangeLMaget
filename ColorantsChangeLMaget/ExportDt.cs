using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using NPOI.XSSF.UserModel;

namespace ColorantsChangeLMaget
{
    public class ExportDt
    {
        SqlList sqlList=new SqlList();

        /// <summary>
        /// 根据指定条件查询对应DT
        /// </summary>
        /// <returns></returns>
        public DataTable SearchDt(string brandName, int productId)
        {
            var dt = new DataTable();
            try
            {
                var sqlscript = sqlList.Get_Record(brandName, productId);
                var sqlDataAdapter = new SqlDataAdapter(sqlscript, GetConn());
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception)
            {
                dt.Columns.Clear();
                dt.Rows.Clear();
            }
            return dt;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public bool ExportdtToExcel(string fileAddress,DataTable tempdt)
        {
            var reslut = true;
            var sheetcount = 0;  //记录所需的sheet页总数
            var rownum = 1;

            try
            {
                //声明一个WorkBook
                var xssfWorkbook = new XSSFWorkbook();
                //先执行MEASUREMENT_COLOR sheet页(注:1)先列表temp行数判断需拆分多少个sheet表进行填充; 以一个sheet表有9W行记录填充为基准)
                sheetcount = tempdt.Rows.Count % 90000 == 0 ? tempdt.Rows.Count / 90000 : tempdt.Rows.Count / 90000 + 1;
                //i为EXCEL的Sheet页数ID
                for (var i = 1; i <= sheetcount; i++)
                {
                    //创建sheet页
                    var sheet = xssfWorkbook.CreateSheet("sheet" + i);
                    //创建"标题行"
                    var row = sheet.CreateRow(0);
                    //创建sheet页各列标题
                    for (var j = 0; j < tempdt.Columns.Count; j++)
                    {
                        //设置列宽度
                        sheet.SetColumnWidth(j, (int)((20 + 0.72) * 256));
                        //创建标题
                        switch (j)
                        {
                            #region SetCellValue
                            case 0:
                                row.CreateCell(j).SetCellValue("内部色号");
                                break;
                            case 1:
                                row.CreateCell(j).SetCellValue("色母编码");
                                break;
                            case 2:
                                row.CreateCell(j).SetCellValue("色母密度");
                                break;
                            case 3:
                                row.CreateCell(j).SetCellValue("色母量(G)");
                                break;
                            case 4:
                                row.CreateCell(j).SetCellValue("色母量(KG)");
                                break;
                            case 5:
                                row.CreateCell(j).SetCellValue("色母量(L)");
                                break;
                            case 6:
                                row.CreateCell(j).SetCellValue("体积占比");
                                break;
                                #endregion
                        }
                    }

                    //计算进行循环的起始行
                    var startrow = (i - 1) * 90000;
                    //计算进行循环的结束行
                    var endrow = i == sheetcount ? tempdt.Rows.Count : i * 90000;

                    //每一个sheet表显示90000行  
                    for (var j = startrow; j < endrow; j++)
                    {
                        //创建行
                        row = sheet.CreateRow(rownum);
                        //循环获取DT内的列值记录
                        for (var k = 0; k < tempdt.Columns.Count; k++)
                        {
                            if (k == 0 || k == 1)
                            {
                                row.CreateCell(k).SetCellValue(Convert.ToString(tempdt.Rows[j][k]));
                            }
                            else
                            {
                               row.CreateCell(k).SetCellValue(Convert.ToDouble(tempdt.Rows[j][k])); 
                            }
                            
                        }
                        rownum++;
                    }
                    //当一个SHEET页填充完毕后,需将变量初始化
                    rownum = 1;
                }

                ////写入数据
                var file = new FileStream(fileAddress, FileMode.Create);
                xssfWorkbook.Write(file);
                file.Close();
            }
            catch (Exception)
            {
                reslut = false;
            }
            return reslut;
        }

        public SqlConnection GetConn()
        {
            var conn = new Conn();
            var sqlcon = new SqlConnection(conn.GetConnectionString());
            return sqlcon;
        }
    }
}
