using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevexpressCN;

namespace 专家费用管理
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");
            TextEditCN.Active = new TextEditCN();
            XtraSpreadsheetLocalizerCN.Active = new XtraSpreadsheetLocalizerCN();
            XtraSpreadsheetFunctionNameLocalizerCN.Active = new XtraSpreadsheetFunctionNameLocalizerCN();
            OfficeLocalizerCN.Active = new OfficeLocalizerCN();
            XtraSpreadsheetCellErrorNameLocalizerCN.Active = new XtraSpreadsheetCellErrorNameLocalizerCN();
            Application.Run(new MainForm());
        }
    }
}
