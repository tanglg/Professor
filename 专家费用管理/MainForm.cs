﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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
                listBoxControl1.Visible = false;
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
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    var newProfessor = register1.Save();
                    historyData1.RefreshData();
                    if (newProfessor.Count > 0)
                    {
                        professorDatabase1.InsertData(newProfessor);
                    }
                    register1.InitRegisterTemplate();
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                new WinformCommon.ErrorForm(ex.Message, ex.StackTrace).ShowDialog();
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//查询
            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                tabPane1.SelectedPage = tabNavigationPage2;
                historyData1.SearchData(barEditItem1.EditValue.ToString().Substring(0, barEditItem1.EditValue.ToString().Length - 1), barEditItem2.EditValue.ToString().Substring(0, barEditItem2.EditValue.ToString().Length - 1));
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
            catch (Exception ex)
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
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
                tabPane1.SelectedPage = tabNavigationPage2;
                historyData1.Export();
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
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
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
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
            catch (Exception ex)
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
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

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//菜单-编辑-资源目录
            try
            {
                System.Diagnostics.Process.Start(UI.ResourceFolder);
            }
            catch (Exception ex)
            {
                new WinformCommon.ErrorForm(ex.Message, ex.StackTrace).ShowDialog();
            }
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {//菜单-帮助-关于
            new About().ShowDialog();
        }

        private void textEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {//过滤专家
            FilterProfessor(textEdit1.Text);
        }
        private void FilterProfessor(string filter)
        {
            try
            {
                SuspendLayout();
                listBoxControl1.Visible = true;
                listBoxControl1.DataSource = UI.Professors.FindAll(o => o.CardNumber.Contains(textEdit1.Text) || o.Name.Contains(filter));
                ResumeLayout();
            }
            catch (Exception ex)
            {
                new WinformCommon.ErrorForm(ex.Message, ex.StackTrace).ShowDialog();
            }
        }

        private void textEdit1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBoxControl1.Focus();
                listBoxControl1.SelectedIndex = 0;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                listBoxControl1.Visible = false;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                //在文本框中敲回车时
                if (((List<Professor>)listBoxControl1.DataSource ).Count > 0)
                {
                    SetProfessorToExcel();
                }
                else
                {
                    if (!string.IsNullOrEmpty(textEdit1.Text))
                    {
                        var pro = new Professor();
                        if (Regex.IsMatch(textEdit1.Text, @"\A\d+[X|x]?\z", RegexOptions.Multiline))
                        { //数字则认为是身份证号
                            pro.CardNumber = textEdit1.Text;
                        }
                        else
                        {//其他认为是姓名
                            pro.Name = textEdit1.Text;
                        }
                        SetProfessorToExcel(pro);
                    }
                }
            }
        }

        private void listBoxControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetProfessorToExcel();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                listBoxControl1.Visible = false;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (listBoxControl1.SelectedIndex == 0)
                {
                    textEdit1.Focus();
                }
            }
        }

        private void listBoxControl1_DoubleClick(object sender, EventArgs e)
        {
            SetProfessorToExcel();
        }
        private void SetProfessorToExcel()
        {
            SetProfessorToExcel((Professor)listBoxControl1.SelectedItem);
            
        }
        private void SetProfessorToExcel(Professor professor)
        {
            try
            {
                register1.SetProfessor(professor);
                textEdit1.Text = string.Empty;
                listBoxControl1.Visible = false;
            }
            catch (Exception ex)
            {
                new WinformCommon.ErrorForm(ex.Message, ex.StackTrace).ShowDialog();
            }
        }
        private void textEdit1_Leave(object sender, EventArgs e)
        {
            //listBoxControl1.Visible = false;
        }

        private void groupControl1_Click(object sender, EventArgs e)
        {
            listBoxControl1.Visible = false;
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            listBoxControl1.Visible = false;
        }

        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (e.Page == tabNavigationPage1)
            {
                textEdit1.Focus();
            }
        }

        private void barDockControlTop_Click(object sender, EventArgs e)
        {
            listBoxControl1.Visible = false;
        }
    }
}
