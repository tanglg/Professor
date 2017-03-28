using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace 专家费用管理
{
    public partial class Register : DevExpress.XtraEditors.XtraUserControl
    {
        public Register()
        {
            InitializeComponent();
        }
        private void InitRegisterTemplate()
        {
            var filePath = Path.Combine(Application.StartupPath, "登记模板.xlsx");
            if (File.Exists(filePath))
            {
                spreadsheetControl1.LoadDocument(filePath);
            }
            else
            {
                throw new Exception("登记模板文件丢失");
            }
        }
        private void Register_Load(object sender, EventArgs e)
        {
            //InitRegisterTemplate();
        }
    }
}
