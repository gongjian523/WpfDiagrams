using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Controls;

namespace TestApp.Flowchart
{
	class FlowNode: INotifyPropertyChanged
	{
		[Browsable(false)]
		public NodeKinds Kind { get; private set; }

		private int _column;
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
        public List<FlowNode> ListFlowNode
        {
            get { return _listFlowNode; }
            //set
            //{
            //    _listFlowNode = value;
            //    //OnPropertyChanged("SubControl");
            //}
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


