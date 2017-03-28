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
    public partial class ProfessorDatabase : DevExpress.XtraEditors.XtraUserControl
    {
        public ProfessorDatabase()
        {
            InitializeComponent();

            InitProfessorData();

            ExtractProfessorData();
        }
        public void InsertData(List<Professor> foList)
        {
            foreach (var fo in foList)
            {
                spreadsheetControl1.ActiveWorksheet.Rows.Insert(2);
                spreadsheetControl1.ActiveWorksheet.Cells[2,0].SetValueFromText(fo.CardNumber );
                spreadsheetControl1.ActiveWorksheet.Cells[2, 1].SetValueFromText(fo.Name);
                spreadsheetControl1.ActiveWorksheet.Cells[2, 2].SetValueFromText(fo.PhoneNumber);
            }
            spreadsheetControl1.SaveDocument(UI.ProfessorFilePath, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
        }
        public void RefreshData()
        {
            InitProfessorData();
        }
        private void InitProfessorData()
        {
            if (File.Exists(UI.ProfessorFilePath))
            {
                spreadsheetControl1.LoadDocument(UI.ProfessorFilePath);
            }
            else
            {
                throw new Exception(string.Format("专家库文件丢失：{0}", UI.ProfessorFilePath));
            }
        }

        private void ProfessorDatabase_Load(object sender, EventArgs e)
        {
            
        }
        private void ExtractProfessorData()
        {
            int count = ExcelCommonFunction.GetLastUsedRowIndex(spreadsheetControl1.ActiveWorksheet);
            for (int rowIndex = 2; rowIndex < count; rowIndex++)
            {
                var professor = new Professor();
                professor.Name = spreadsheetControl1.ActiveWorksheet.GetCellValue(1, rowIndex).ToString( );
                professor.PhoneNumber= spreadsheetControl1.ActiveWorksheet.GetCellValue(2, rowIndex).ToString( );
                professor.CardNumber = spreadsheetControl1.ActiveWorksheet.GetCellValue(0, rowIndex).ToString();
                UI.Professors.Add(professor);
            }
        }
    }
}
