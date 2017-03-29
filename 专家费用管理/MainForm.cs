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
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                InitMonthFilter();
                InitYearFilter();
                tabPane1.SelectedPage = tabNavigationPage1;
            }
            catch (Exception ex)
            {
                new WinformCommon.ErrorForm(ex.Message, ex.StackTrace).ShowDialog();
            }
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
            try
            {
                if (tabPane1.SelectedPage.Name == "tabNavigationPage2")
                {
                }
                else if (tabPane1.SelectedPage.Name == "tabNavigationPage3")
                {
                }
                else if (tabPane1.SelectedPage.Name == "tabNavigationPage1")
                {
                    var newProfessor = register1.Save();
                    historyData1.RefreshData();
                    if (newProfessor.Count > 0)
                    {
                        professorDatabase1.InsertData(newProfessor);
                    }
                }
            }
            catch (Exception ex)
            {
                new WinformCommon.ErrorForm(ex.Message, ex.StackTrace).ShowDialog();
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//查询
            try
            {
                historyData1.SearchData(barEditItem1.EditValue.ToString().Substring(0, barEditItem1.EditValue.ToString().Length - 1), barEditItem2.EditValue.ToString().Substring(0, barEditItem2.EditValue.ToString().Length - 1));
            }
            catch (Exception ex)
            {
                new WinformCommon.ErrorForm(ex.Message, ex.StackTrace).ShowDialog();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//退出
            Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//导出到excel
            try
            {
                if (tabPane1.SelectedPage.Name == "tabNavigationPage2")
                {
                    historyData1.Export();
                }
            }
            catch (Exception ex)
            {
                new WinformCommon.ErrorForm(ex.Message, ex.StackTrace).ShowDialog();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//刷新
            try
            {
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
            catch (Exception ex)
            {
                new WinformCommon.ErrorForm(ex.Message, ex.StackTrace).ShowDialog();
            }
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//菜单退出
            barButtonItem1_ItemClick(null, null);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//菜单-文件-登记
            tabPane1.SelectedPage = tabNavigationPage1;
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//菜单-文件-历史记录
            tabPane1.SelectedPage = tabNavigationPage2;
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//菜单-文件-专家库维护
            tabPane1.SelectedPage = tabNavigationPage3;
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//菜单-编辑-保存
            barButtonItem3_ItemClick(null,null);
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//菜单-编辑-查询
            barButtonItem6_ItemClick(null, null);
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//菜单-编辑-导出
            barButtonItem4_ItemClick(null, null);
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//菜单-工具-计算器
            var filePath = System.IO.Path.Combine(Application.StartupPath, "calc.exe");
            if (System.IO.File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(filePath);
            }
        }
    }
}
