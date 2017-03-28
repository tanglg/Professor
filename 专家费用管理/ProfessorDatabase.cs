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
        private void InitProfessorData()
        {
            var filePath = Path.Combine(Application.StartupPath,"专家库.xlsx");
            if (File.Exists(filePath))
            {
                spreadsheetControl1.LoadDocument(filePath);
            }
            else
            {
                throw new Exception(string.Format("专家库文件丢失：{0}",filePath));
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
                professor.Name = spreadsheetControl1.ActiveWorksheet.GetCellValue(0, rowIndex).ToString( );
                professor.PhoneNumber= spreadsheetControl1.ActiveWorksheet.GetCellValue(1, rowIndex).ToString( );
                professor.CardNumber = spreadsheetControl1.ActiveWorksheet.GetCellValue(2, rowIndex).ToString();
                UI.Professors.Add(professor);
            }
        }
    }
}
