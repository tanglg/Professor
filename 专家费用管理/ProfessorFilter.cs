using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 专家费用管理
{
    public partial class ProfessorFilter : UserControl
    {
        public Action<Professor> SelectProfessor;
        public ProfessorFilter()
        {
            InitializeComponent();
        }
        
        private void ProfessorFilter_Click(object sender, EventArgs e)
        {
            
        }

        private void ProfessorFilter_Load(object sender, EventArgs e)
        {
            listBoxControl1.DataSource = UI.Professors;
        }

        private void listBoxControl1_DoubleClick(object sender, EventArgs e)
        {
            SelectProfessor((Professor)listBoxControl1.SelectedItem);
            textEdit1.Text = string.Empty;
        }

        private void textEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            listBoxControl1.DataSource = UI.Professors.FindAll(o=>o.CardNumber.Contains(textEdit1.Text));
        }

        private void textEdit1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBoxControl1.Focus();
                listBoxControl1.SelectedIndex = 0;
            }
        }

        private void listBoxControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectProfessor((Professor)listBoxControl1.SelectedItem);
                textEdit1.Text = string.Empty;
            }
        }
    }
}
