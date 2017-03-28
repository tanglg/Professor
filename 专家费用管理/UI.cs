using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 专家费用管理
{
    class UI
    {
        public static List<Professor> Professors { get; set; }
        static UI()
        {
            Professors = new List<Professor>();
        }
        public static string HistoryFilePath { get { return Path.Combine(Application.StartupPath, "历史数据.xlsx"); } }
        public static string ProfessorFilePath { get { return Path.Combine(Application.StartupPath, "专家库.xlsx"); } }
    }
}
