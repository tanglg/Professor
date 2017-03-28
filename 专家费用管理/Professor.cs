using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 专家费用管理
{
    public class Professor
    {
        public string Name { get; set; }
        public string CardNumber { get; set;     }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})",Name,CardNumber);
        }
    }
}
