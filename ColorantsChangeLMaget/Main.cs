using System;
using System.Threading;
using System.Windows.Forms;
using Mergedt;

namespace ColorantsChangeLMaget
{
    public partial class Main : Form
    {
        Load load = new Load();
        TaskLogic task=new TaskLogic();

        public Main()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            btnGenerate.Click += BtnGenerate_Click;
            btnclose.Click += Btnclose_Click;
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtbandname.Text=="" || txtprodid.Text=="") throw new Exception("一定要填写两项才可以继续");
                task.TaskId = 0;
                task.BrandName = txtbandname.Text;
                task.Productid = Convert.ToInt32(txtprodid.Text);

                //使用子线程工作(作用:通过调用子线程进行控制Load窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                var dt = task.Dt;
                if(dt.Rows.Count==0)throw new Exception("运算异常");
                else
                {
                    var clickMessage = $"运算成功,是否进行导出至Excel?";

                    if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        ExportDttoExcel();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导出DT至EXCEL
        /// </summary>
        void ExportDttoExcel()
        {
            try
            {
                var saveFileDialog = new SaveFileDialog { Filter = "Xlsx文件|*.xlsx" };
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                var fileAdd = saveFileDialog.FileName;

                //将所需的值赋到Task类内
                task.TaskId = 1;
                task.FileAddress = fileAdd;

                //使用子线程工作(作用:通过调用子线程进行控制Load窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                if (!task.Exportreslut) throw new Exception("导出异常");
                else
                {
                    MessageBox.Show($"导出成功!可从EXCEL中查阅导出效果", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        ///子线程使用(重:用于监视功能调用情况,当完成时进行关闭LoadForm)
        /// </summary>
        private void Start()
        {
            task.StartTask();

            //当完成后将Form2子窗体关闭
            this.Invoke((ThreadStart)(() => {
                load.Close();
            }));
        }
    }
}
