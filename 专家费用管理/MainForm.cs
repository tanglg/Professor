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
            InitMonthFilter();
            InitYearFilter();
            tabPane1.SelectedPage = tabNavigationPage1;
        }
        private void InitYearFilter()
        {
            if (historyData1.YearList == null || historyData1.YearList.Count == 0) return;
            for (int i = 0; i < historyData1.YearList.Count; i++)
            {
                repositoryItemComboBox_year.Items.Add(historyData1.YearList[i] + "年");
            }
            barEditItem1.EditValue = historyData1.YearList[0] + "年";
        }
        private void InitMonthFilter()
        {
            for (int i = 1; i < 13; i++)
            {
                repositoryItemComboBox_month.Items.Add(i.ToString()+"月");
            }
            barEditItem2.EditValue = DateTime.Today.Month.ToString() + "月";
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//保存
            if (tabPane1.SelectedPage.Name == "tabNavigationPage2")
            {
            }
            else if (tabPane1.SelectedPage.Name == "tabNavigationPage3")
            {
            }
            else if (tabPane1.SelectedPage.Name == "tabNavigationPage1")
            {
                register1.Save();
                historyData1.RefreshData();
                professorDatabase1.RefreshData();
            }
            
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//查询
            historyData1.SearchData(barEditItem1.EditValue.ToString().Substring(0, barEditItem1.EditValue.ToString().Length - 1), barEditItem2.EditValue.ToString().Substring(0, barEditItem2.EditValue.ToString().Length - 1));
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//退出
            Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//导出到excel

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//刷新
            if (tabPane1.SelectedPage.Name == "tabNavigationPage2")
            {
                historyData1.RefreshData();
            }
            else if (tabPane1.SelectedPage.Name == "tabNavigationPage3")
            {
                professorDatabase1.RefreshData();
            }
            else if (tabPane1.SelectedPage.Name == "tabNavigationPage1")
            {
                register1.RefreshData();
            }
        }
    }
}
