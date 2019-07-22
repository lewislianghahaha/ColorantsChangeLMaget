using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorantsChangeLMaget
{
    public class DtList
    {
        /// <summary>
        /// 结果集输出临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_MeasureMentColordt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 6; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "内部色号";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 1:
                        dc.ColumnName = "色母编码";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2:
                        dc.ColumnName = "色母密度";
                        dc.DataType = Type.GetType("System.Decimal"); 
                        break;
                    case 3:
                        dc.ColumnName = "色母量(G)";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    case 4:
                        dc.ColumnName = "色母量(KG)";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    case 5:
                        dc.ColumnName = "色母量(L)";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    case 6:
                        dc.ColumnName = "体积占比";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }
    }
}
