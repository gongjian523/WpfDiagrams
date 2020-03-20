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

namespace PayloadApp.Flowchart.Controls
{
    /// <summary>
    /// ConstantPayloadNode.xaml 的交互逻辑
    /// </summary>
    public partial class ConstantPayloadNode : UserControl
    {
        public ConstantPayloadNode(string para)
        {
            InitializeComponent();
            _paraTbx.Text = para;
        }

        public String Constant()
        {
            return _constTbl.Text;         
        }

        public String Parameter()
        {
            if (_paraTbx.Text == null || _paraTbx.Text == "")
                return "0";
            else
            {
                return _paraTbx.Text;
            }     
        }
    }
}
