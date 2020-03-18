using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestApp.Flowchart.Controls
{
    /// <summary>
    /// DistributeNode.xaml 的交互逻辑
    /// </summary>
    public partial class DistributeNode : UserControl
    {
        public DistributeNode()
        {
            InitializeComponent();
        }

        public String Coefficient()
        {
            if (_coe1Tbx.Text == null)
                return "0";
            else
                return _coe1Tbx.Text;
        }

        public String Coefficient2()
        {
            if (_coe2Tbx.Text == null)
                return "0";
            else
                return _coe2Tbx.Text;
        }

        public String Coefficient3()
        {
            if (_coe3Tbx.Text == null)
                return "0";
            else
                return _coe3Tbx.Text;
        }
    }
}
