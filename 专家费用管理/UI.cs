using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 专家费用管理
{
    class UI
    {
        public static List<Professor> Professors { get; set; }
        static UI()
        {
            Professors = new List<Professor>();
        }
    }
}
