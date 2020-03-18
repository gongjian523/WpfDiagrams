using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Controls;
using TestApp.Flowchart.Controls;

namespace TestApp.Flowchart
{
	class FlowNode: INotifyPropertyChanged
	{
		[Browsable(false)]
		public NodeKinds Kind { get; private set; }

		private int _column;
        [Browsable(false)]
        public int Column
		{
			get { return _column; }
			set 
			{ 
				_column = value;
				OnPropertyChanged("Column");
			}
		}

		private int _row;
        [Browsable(false)]
        public int Row
		{
			get { return _row; }
			set 
			{ 
				_row = value;
				OnPropertyChanged("Row");
			}
		}

		private string _text;
        [Browsable(false)]
        public string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				OnPropertyChanged("Text");
			}
		}


        private UserControl _subControl;
        [Browsable(false)]
        public UserControl SubControl
        {
            get { return _subControl; }
            set
            {
                _subControl = value;
                //OnPropertyChanged("SubControl");
            }
        }


        private List<FlowNode> _listFlowNode = new List<FlowNode>();
        [Browsable(false)]
        public List<FlowNode> ListFlowNode
        {
            get { return _listFlowNode; }
            //set
            //{
            //    _listFlowNode = value;
            //    //OnPropertyChanged("SubControl");
            //}
        }

        private string  _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        private string _a;
        public string A
        {
            get { return _a; }
            set
            {
                _a = value;
                OnPropertyChanged("A");
            }
        }

        private string _b;
        public string B
        {
            get { return _b; }
            set
            {
                _b = value;
                OnPropertyChanged("B");
            }
        }

        private string _c;
        public string C
        {
            get { return _c; }
            set
            {
                _c = value;
                OnPropertyChanged("C");
            }
        }


        public FlowNode(NodeKinds kind)
		{
			Kind = kind;
		}

		public IEnumerable<PortKinds> GetPorts()
		{
			switch(Kind)
			{
				case NodeKinds.Start:
					yield return PortKinds.Bottom;
					break;
				case NodeKinds.End:
					yield return PortKinds.Top;
					break;
				case NodeKinds.Action:
					yield return PortKinds.Top;
					yield return PortKinds.Bottom;
					break;
				case NodeKinds.Condition:
					yield return PortKinds.Top;
					yield return PortKinds.Bottom;
					yield return PortKinds.Left;
					yield return PortKinds.Right;
					break;
                case NodeKinds.ConstantPayload:
                    yield return PortKinds.Right;
                    break;
                case NodeKinds.FlexiblePaylaod:
                    yield return PortKinds.Right;
                    break;
                case NodeKinds.Distribute:
                    //yield return PortKinds.Left;
                    yield return PortKinds.Top;
                    yield return PortKinds.Right;
                    break;
                case NodeKinds.Output:
                    //yield return PortKinds.Left;
                    yield return PortKinds.Top;
                    break;
            }
		}

        public void AddNode(FlowNode node)
        {
            _listFlowNode.Add(node); 
        }

        public void RemoveNode(FlowNode node)
        {
            _listFlowNode.Remove(node);
        }

        public void ClearNodes()
        {
            _listFlowNode.Clear();
        }

        public List<string> ResultList()
        {     
            List<string> list = new List<string>();

            foreach (var dNode in _listFlowNode)
            {
                if(dNode.Kind != NodeKinds.Distribute)
                {
                    continue;
                }

                double result = 0;
                string rltStr = "";

                foreach (var node in dNode.ListFlowNode)
                {
                    string subStr = "";
                    double subRst = 0;

                    if (node.Kind == NodeKinds.ConstantPayload)
                    {
                        ConstantPayloadNode cpn = (ConstantPayloadNode)node.SubControl;

                        double constant = Convert.ToDouble(cpn.Constant());
                        double para = Convert.ToDouble(cpn.Parameter());
                        subRst = constant * para;
                        subStr = cpn.Constant() + "X" + cpn.Parameter();
                    }
                    else if (node.Kind == NodeKinds.FlexiblePaylaod)
                    {
                        FlexiblePayloadNode fpn = (FlexiblePayloadNode)node.SubControl;

                        double constant = Convert.ToDouble(fpn.Constant());
                        double para = Convert.ToDouble(fpn.Parameter());
                        double para2 = Convert.ToDouble(fpn.Parameter2());

                        subRst = constant * para * para2;
                        subStr = fpn.Constant() + "X" + fpn.Parameter() + "X" + fpn.Parameter2();
                    }

                    result += subRst;
                    if (rltStr == "")
                    {
                        rltStr = subStr;
                    }
                    else
                    {
                        rltStr = rltStr + "+" + subStr;
                    }
                }

                rltStr = rltStr + "=" + result.ToString();

                DistributeNode dn = (DistributeNode)dNode.SubControl;

                string coeAStr = dn.Coefficient();
                double rstA = result * Convert.ToDouble(coeAStr);
                string strA = "A " +  result.ToString() + "X" + coeAStr + "= " + "" + rstA.ToString();

                string coeBStr = dn.Coefficient2();
                double rstB = result * Convert.ToDouble(coeBStr);
                string strB = "B " +  result.ToString() + "X" + coeBStr + "= " + "" + rstB.ToString();

                string coeCStr = dn.Coefficient3();
                double rstC = result * Convert.ToDouble(coeCStr);
                string strC = "C " + result.ToString() + "X" + coeCStr + "= " + "" + rstC.ToString();

                list.Add(rltStr);
                list.Add(strA);
                list.Add(strB);
                list.Add(strC);
            }

            _result = list[0];
            _a = list[1];
            _b = list[2];
            _c = list[3];

            return list;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		#endregion
	}

	enum NodeKinds { Start, End, Action, Condition, ConstantPayload, FlexiblePaylaod, Distribute, Output }
}


