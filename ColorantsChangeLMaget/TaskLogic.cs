using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace ColorantsChangeLMaget
{
    public class TaskLogic
    {
        ExportDt exportDt=new ExportDt();

        private int _taskid;
        private string _brandname;          //品牌名称
        private int _productid;             //产品系列ID
        private string _fileAddress;       //文件地址
        private DataTable _dt;             //返回运算后的记录DT
        private bool _exportreslut;        //返回导功结果

        #region Set
        /// <summary>
        /// 中转ID
        /// </summary>
        public int TaskId { set { _taskid = value; } }

        /// <summary>
        /// //接收文件地址信息
        /// </summary>
        public string FileAddress { set { _fileAddress = value; } }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { set { _brandname = value; } }

        /// <summary>
        /// 产品系列ID
        /// </summary>
        public int Productid { set { _productid = value; } }

        #endregion

        #region Get
        /// <summary>
        /// 返回运算成功的DT(导出时使用)
        /// </summary>
        public DataTable Dt => _dt;

        /// <summary>
        /// 返回运算成功的DT(导出时使用)
        /// </summary>
        public bool Exportreslut => _exportreslut;
        #endregion

        public void StartTask()
        {
            Thread.Sleep(1000);

            switch (_taskid)
            {
                //运算
                case 0:
                    SearchDt(_brandname, _productid);
                    break;
                //导出
                case 1:
                    ExportdtToExcel(_fileAddress,_dt);
                    break;
            }
        }

        private void SearchDt(string brandName, int productId)
        {
            _dt = exportDt.SearchDt(brandName, productId);
        }

        public void ExportdtToExcel(string fileAddress, DataTable tempdt)
        {
            _exportreslut = exportDt.ExportdtToExcel(_fileAddress, _dt);
        }

    }
}
