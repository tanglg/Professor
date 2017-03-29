using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraSpreadsheet;

namespace 专家费用管理
{
    public partial class HistoryData : UserControl
    {
        public List<int> YearList = null;
        public HistoryData()
        {
            InitializeComponent();

            spreadsheetControl1.Options.Behavior.Worksheet.Insert = DocumentCapability.Hidden;
            InitHistoryData();
            YearList = GetExistYearList();
        }
        public void Export()
        {
            var save = new SaveFileDialog();
            save.Filter = "Excel(2007或更高)|*.xlsx";
            save.FileName = DateTime.Today.ToString("yyyyMMdd")+"导出文件";
            if (save.ShowDialog() != DialogResult.OK) return;
            spreadsheetControl1.Document.SaveDocument(save.FileName, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
        }
        public void SearchData(string year, string month)
        {
            var count = ExcelCommonFunction.GetLastUsedRowIndex(spreadsheetControl1.ActiveWorksheet);
            for (int i = count; i >1; i--)
            {
                if (spreadsheetControl1.ActiveWorksheet.GetCellValue(5, i).ToString() != year ||
                    spreadsheetControl1.ActiveWorksheet.GetCellValue(6, i).ToString() != month)
                {
                    spreadsheetControl1.ActiveWorksheet.Rows.Remove(i);
                }
            }
        }
        public void RefreshData()
        {
            InitHistoryData();

            YearList = GetExistYearList();
        }
        private void InitHistoryData()
        {
            
            if (File.Exists(UI.HistoryFilePath))
            {
                spreadsheetControl1.LoadDocument(UI.HistoryFilePath);
            }
            else
            {
                throw new Exception("历史数据文件丢失");
            }
        }

        private void HistoryData_Load(object sender, EventArgs e)
        {
            
        }
        public List<int> GetExistYearList()
        {
            List<int> list = new List<int>();
            var count = ExcelCommonFunction.GetLastUsedRowIndex(spreadsheetControl1.ActiveWorksheet);
            for (int i = count; i > 1; i--)
            {
                int value = Convert.ToInt32(spreadsheetControl1.ActiveWorksheet.GetCellValue(5, i).ToString());
                if (!list.Any(o => o == value))
                {
                    list.Add(value);
                }
            }
            
            list.OrderByDescending(o => o);
            return list;
        }

        private void spreadsheetControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            e.Menu.Items.Clear();
        }
    }
}
