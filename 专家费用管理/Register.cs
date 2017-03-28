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
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Win;
using DevExpress.XtraSpreadsheet;

namespace 专家费用管理
{
    public partial class Register : DevExpress.XtraEditors.XtraUserControl
    {
        RepositoryItemPopupContainerEdit _popContainer = new RepositoryItemPopupContainerEdit();
        ProfessorFilter _filter = new ProfessorFilter();
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

            return rowIndex>2 && spreadsheetControl1.Document.Worksheets[0].GetCellValue(colIndex, 2).ToString() == "身份证号";
        }
        private void spreadsheetControl1_CustomCellEdit(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCustomCellEditEventArgs e)
        {
            if (!IsCurrentColumnPoP(e.ColumnIndex,e.RowIndex)) return;//仅处理身份证号列
            
            e.RepositoryItem = _popContainer;
        }

        private void spreadsheetControl1_SelectionChanged(object sender, EventArgs e)
        {
            if (!IsCurrentColumnPoP(spreadsheetControl1.ActiveCell.ColumnIndex, spreadsheetControl1.ActiveCell.RowIndex)) return;
            spreadsheetControl1.OpenCellEditor(DevExpress.XtraSpreadsheet.CellEditorMode.Edit);
        }
    }
}
