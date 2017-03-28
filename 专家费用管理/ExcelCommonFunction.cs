using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 专家费用管理
{
    class ExcelCommonFunction
    {
        public static int GetLastUsedRowIndex(DevExpress.Spreadsheet.Worksheet sheet)
        {
            int emptyRowCount = 0;
            bool isCurrentRowEmpty = true;
            for (int row = 0; row < 65530; row++)
            {
                isCurrentRowEmpty = true;
                for (int col = 0; col < 27; col++)
                {//判断当前行的每一列是否为空
                    if (!sheet.Cells[row, col].Value.IsEmpty
                        || !string.IsNullOrEmpty(sheet.Cells[row, col].Value.TextValue))
                    {//只要有一列不为空，则当前行整体上不为空,跳到下一行开始重新判断
                        isCurrentRowEmpty = false;
                        emptyRowCount = 0;
                        break;
                    }
                }

                if (isCurrentRowEmpty)
                {
                    emptyRowCount++;
                }

                if (emptyRowCount == 3)
                {
                    return row - 3;
                }
            }
            throw new Exception(string.Format("没有计算出{0}中的有效数据行号", sheet.Name));
        }
    }
}
