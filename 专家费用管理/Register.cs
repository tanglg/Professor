using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Win;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;

namespace 专家费用管理
{
    public partial class Register : DevExpress.XtraEditors.XtraUserControl
    {
        RepositoryItemPopupContainerEdit _popContainer = new RepositoryItemPopupContainerEdit();
        ProfessorFilter _filter = new ProfessorFilter();
        int _startRowIndex = 3;
        public Register()
        {
            InitializeComponent();

            spreadsheetControl1.Options.Behavior.Worksheet.Insert = DocumentCapability.Hidden;
        }
        private void InitRegisterTemplate()
        {
            if (File.Exists(UI.RegisterTemplateFilePath))
            {
                spreadsheetControl1.LoadDocument(UI.RegisterTemplateFilePath);
                spreadsheetControl1.Document.Worksheets[0].Cells[1, 0].SetValueFromText(DateTime.Today.ToString("yyyy年MM月"));
            }
            else
            {
                throw new Exception("登记模板文件丢失");
            }
        }
        private string GetDateString()
        {
            return spreadsheetControl1.Document.Worksheets[0].Cells[1, 0].Value.ToString();
        }
        private void GetYearAndMonth(out int year,out int month)
        {
            var dateString = GetDateString();
            var math = Regex.Match(dateString, @"(\d+)", RegexOptions.Multiline);
            year = Convert.ToInt32(math.Groups[1].Value);
            math.NextMatch();
            month = Convert.ToInt32(math.NextMatch().Groups [1].Value);
        }
        public List<Professor> Save()
        {
            spreadsheetControl1.CloseCellEditor(CellEditorEnterValueMode.ActiveCell);

            var count = ExcelCommonFunction.GetLastUsedRowIndex(spreadsheetControl1.ActiveWorksheet);

            int year, month;
            GetYearAndMonth(out year, out month);

            Workbook workbook = new Workbook();

            using (FileStream stream = new FileStream(UI.HistoryFilePath, FileMode.Open))
            {
                workbook.LoadDocument(stream, DocumentFormat.OpenXml);
            }

            List<Professor> newProfessorList = new List<Professor>();
            for (int rowIndex =count ; rowIndex >=_startRowIndex; rowIndex--)
            {
                workbook.Worksheets[0].Rows.Insert(2);
                //身份证号
                workbook.Worksheets[0].Cells[2, 0].SetValueFromText(spreadsheetControl1.ActiveWorksheet .GetCellValue(0,rowIndex).ToString());
                //姓名
                workbook.Worksheets[0].Cells[2, 1].SetValueFromText(spreadsheetControl1.ActiveWorksheet.GetCellValue(1, rowIndex).ToString());
                //联系电话
                workbook.Worksheets[0].Cells[2, 2].SetValueFromText(spreadsheetControl1.ActiveWorksheet.GetCellValue(2, rowIndex).ToString());
                //金额
                workbook.Worksheets[0].Cells[2, 3].SetValueFromText(spreadsheetControl1.ActiveWorksheet.GetCellValue(3, rowIndex).ToString());
                //项目名称
                workbook.Worksheets[0].Cells[2, 4].SetValueFromText(spreadsheetControl1.ActiveWorksheet.GetCellValue(4, rowIndex).ToString());
                //年
                workbook.Worksheets[0].Cells[2, 5].SetValueFromText(year.ToString());
                //月
                workbook.Worksheets[0].Cells[2, 6].SetValueFromText(month.ToString());

                if (UI.Professors.Any(o => o.CardNumber.Trim().ToUpper() != spreadsheetControl1.ActiveWorksheet.GetCellValue(0, rowIndex).ToString().Trim().ToUpper()))
                {
                    var fo = new Professor();
                    fo.CardNumber = spreadsheetControl1.ActiveWorksheet.GetCellValue(0, rowIndex).ToString();
                    fo.Name = spreadsheetControl1.ActiveWorksheet.GetCellValue(1, rowIndex).ToString();
                    fo.PhoneNumber = spreadsheetControl1.ActiveWorksheet.GetCellValue(2, rowIndex).ToString();

                    newProfessorList.Add(fo);
                }
            }

            workbook.SaveDocument(UI.HistoryFilePath,DocumentFormat.Xlsx);

            RefreshData();

            return newProfessorList;
        }
        
        public void RefreshData()
        {
            for (int rowIndex = ExcelCommonFunction.GetLastUsedRowIndex(spreadsheetControl1.ActiveWorksheet); rowIndex >= _startRowIndex; rowIndex--)
            {
                spreadsheetControl1.ActiveWorksheet.Rows.Remove(rowIndex);
            }
        }
        private void Register_Load(object sender, EventArgs e)
        {
            InitRegisterTemplate();

            _popContainer.PopupFormSize = new System.Drawing.Size(180, 300);
            PopupContainerControl popupControl = new PopupContainerControl();
            _filter.SelectProfessor = GetProfessor;
            _filter.Dock = DockStyle.Fill;
            popupControl.Controls.Add(_filter);

            _popContainer.PopupControl = popupControl;
            _popContainer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }
        private void GetProfessor(Professor pf)
        {
            spreadsheetControl1.CloseCellEditor(CellEditorEnterValueMode.ActiveCell);
            spreadsheetControl1.Document.Worksheets[0].Cells[spreadsheetControl1.ActiveCell.RowIndex, spreadsheetControl1.ActiveCell.ColumnIndex ].SetValueFromText(pf.CardNumber);
            spreadsheetControl1.Document.Worksheets[0].Cells[spreadsheetControl1.ActiveCell.RowIndex, spreadsheetControl1.ActiveCell.ColumnIndex + 1].SetValueFromText(pf.Name);
            spreadsheetControl1.Document.Worksheets[0].Cells[spreadsheetControl1.ActiveCell.RowIndex, spreadsheetControl1.ActiveCell.ColumnIndex + 2].SetValueFromText(pf.PhoneNumber);
            spreadsheetControl1.Document.Worksheets[0].SelectedCell = spreadsheetControl1.Document.Worksheets[0].Cells[spreadsheetControl1.ActiveCell.RowIndex+1, spreadsheetControl1.ActiveCell.ColumnIndex];
        }
        private bool IsCurrentColumnPoP(int colIndex,int rowIndex)
        {
            return rowIndex >= _startRowIndex && spreadsheetControl1.Document.Worksheets[0].GetCellValue(colIndex, _startRowIndex-1).ToString() == "身份证号";
        }
        private void spreadsheetControl1_CustomCellEdit(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCustomCellEditEventArgs e)
        {
            if (!IsCurrentColumnPoP(e.ColumnIndex,e.RowIndex)) return;//仅处理身份证号列
            
            e.RepositoryItem = _popContainer;
        }

        private void spreadsheetControl1_SelectionChanged(object sender, EventArgs e)
        {
            if (!IsCurrentColumnPoP(spreadsheetControl1.ActiveCell.ColumnIndex, spreadsheetControl1.ActiveCell.RowIndex)) return;
            spreadsheetControl1.OpenCellEditor(DevExpress.XtraSpreadsheet.CellEditorMode.Enter);
        }

        private void spreadsheetControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            e.Menu.Items.Clear();
        }
    }
}
