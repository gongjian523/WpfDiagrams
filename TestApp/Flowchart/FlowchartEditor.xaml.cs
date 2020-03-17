using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Aga.Diagrams;
using Aga.Diagrams.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace TestApp.Flowchart
{
	public partial class FlowchartEditor : UserControl
	{
		private ItemsControlDragHelper _dragHelper;

		public FlowchartEditor()
		{
			InitializeComponent();

			var model = CreateModel();
			_editor.Controller = new Controller(_editor, model);
			_editor.DragDropTool = new DragDropTool(_editor, model);
			_editor.DragTool = new CustomMoveResizeTool(_editor, model) 
			{ 
				MoveGridCell = _editor.GridCellSize
			};
			_editor.LinkTool = new CustomLinkTool(_editor);
			_editor.Selection.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Selection_PropertyChanged);
			_dragHelper = new ItemsControlDragHelper(_toolbox, this);

			FillToolbox();
		}

		private void FillToolbox()
		{
			foreach (NodeKinds nk in Enum.GetValues(typeof(NodeKinds)))
			{
                if (nk == NodeKinds.Action || nk == NodeKinds.Condition || nk == NodeKinds.End || nk == NodeKinds.Start)
                {
                    continue;
                }

                var node = new FlowNode(nk);
				node.Text = nk.ToString();
				var ui = Controller.CreateContent(node);
				ui.Width = 100;
				ui.Height = 30;
				ui.Margin = new Thickness(5);
				ui.Tag = nk;
				_toolbox.Items.Add(ui);
			}
		}

		private FlowchartModel CreateModel()
		{
			var model = new FlowchartModel();
			
			var start = new FlowNode(NodeKinds.Start);
			start.Row = 0;
			start.Column = 1;
			start.Text = "Start";

			var ac0 = new FlowNode(NodeKinds.Action);
			ac0.Row = 1;
			ac0.Column = 1;
			ac0.Text = "i = 0";

			var cond = new FlowNode(NodeKinds.Condition);
			cond.Row = 2;
			cond.Column = 1;
			cond.Text = "i < n";

			var ac1 = new FlowNode(NodeKinds.Action);
			ac1.Row = 3;
			ac1.Column = 1;
			ac1.Text = "do something";

			var ac2 = new FlowNode(NodeKinds.Action);
			ac2.Row = 4;
			ac2.Column = 1;
			ac2.Text = "i++";

			var end = new FlowNode(NodeKinds.End);
			end.Row = 3;
			end.Column = 2;
			end.Text = "End";

            //model.Nodes.Add(start);
            //model.Nodes.Add(cond);
            //model.Nodes.Add(ac0);
            //model.Nodes.Add(ac1);
            //model.Nodes.Add(ac2);
            //model.Nodes.Add(end);

            //model.Links.Add(new Link(start, PortKinds.Bottom, ac0, PortKinds.Top));
            //model.Links.Add(new Link(ac0, PortKinds.Bottom, cond, PortKinds.Top));

            //model.Links.Add(new Link(cond, PortKinds.Bottom, ac1, PortKinds.Top) { Text = "True" });
            //model.Links.Add(new Link(cond, PortKinds.Right, end, PortKinds.Top) { Text = "False" });

            //model.Links.Add(new Link(ac1, PortKinds.Bottom, ac2, PortKinds.Top));
            //model.Links.Add(new Link(ac2, PortKinds.Bottom, cond, PortKinds.Top));

            var cpn = new FlowNode(NodeKinds.ConstantPayload);
            cpn.Row = 0;
            cpn.Column = 0;

            var fpn = new FlowNode(NodeKinds.FlexiblePaylaod);
            fpn.Row = 2;
            fpn.Column = 0;

            var dn = new FlowNode(NodeKinds.Distribute);
            dn.Row = 1;
            dn.Column = 2;

            var otn = new FlowNode(NodeKinds.Output);
            otn.Row = 1;
            otn.Column = 4;

            model.Nodes.Add(otn);
            model.Nodes.Add(cpn);
            model.Nodes.Add(fpn);
            model.Nodes.Add(dn);

            model.Links.Add(new Link(cpn, PortKinds.Right, dn, PortKinds.Top));
            model.Links.Add(new Link(fpn, PortKinds.Right, dn, PortKinds.Top));
            model.Links.Add(new Link(dn, PortKinds.Right, otn, PortKinds.Top));

            return model;
		}

		void Selection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var p = _editor.Selection.Primary;
			_propertiesView.SelectedObject = p != null ? p.ModelElement : null;
		}
	}
}
