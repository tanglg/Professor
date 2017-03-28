using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 专家费用管理
{
    public partial class HistoryData : UserControl
    {
        public HistoryData()
        {
            InitializeComponent();
        }
        private void InitHistoryData()
        {
            var filePath = Path.Combine(Application.StartupPath, "历史数据.xlsx");
            if (File.Exists(filePath))
            {
                spreadsheetControl1.LoadDocument(filePath);
            }
            else
            {
                throw new Exception("历史数据文件丢失");
            }
        }

        private void HistoryData_Load(object sender, EventArgs e)
        {
            //InitHistoryData();
        }
    }
}
