using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace 专家费用管理
{
    public partial class SplashScreen1 : SplashScreen
    {
        public SplashScreen1()
        {
            InitializeComponent();

            labelControl1.Text = string.Format("Copyright © 2016-{0}",DateTime.Today.Year);
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }
    }
}