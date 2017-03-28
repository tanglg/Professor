using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 专家费用管理
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void tabPane1_SelectedPageChanging(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangingEventArgs e)
        {
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            barEditItem1.EditValue = "2017年";
            barEditItem2.EditValue = "三月";

            tabPane1.SelectedPage = tabNavigationPage1;
        }
        private void FakeProfessor()
        {
 
        }
    }
}
