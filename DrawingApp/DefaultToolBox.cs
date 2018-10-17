using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp
{
    class DefaultToolBox : ToolStrip, IToolBox
    {
        private ITool activeTool;

        public ITool ActiveTool
        {
            get
            {
                return this.activeTool;
            }
        }

        public event ToolSelectedEventHandler ToolSelected;

        public void addTool(ITool tool)
        {
            if (tool is ToolStripButton)
            {
                ToolStripButton toogleButton = (ToolStripButton)tool;

                if (toogleButton.CheckOnClick)
                {
                    toogleButton.CheckedChanged += toogleButton_CheckedChanged;
                }

                this.Items.Add(toogleButton);
            }
        }

        public void removeTool(ITool tool)
        {
            foreach (ToolStripItem i in this.Items)
            {
                if (i is ITool)
                {
                    if (i.Equals(tool))
                    {
                        this.Items.Remove(i);
                    }
                }
            }
        }

        private void toogleButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is ToolStripButton)
            {
                ToolStripButton button = (ToolStripButton)sender;

                if (button.Checked)
                {
                    if (button is ITool)
                    {
                        this.activeTool = (ITool)button;

                        if (ToolSelected != null)
                        {
                            ToolSelected(this.activeTool);
                        }

                        UncheckInactiveToogleButton();
                    }
                    else
                    {
                        throw new InvalidCastException("Cannot convert");
                    }
                }
            }
        }

        private void UncheckInactiveToogleButton()
        {
            foreach (ToolStripItem item in this.Items)
            {
                if (item != this.activeTool)
                {
                    if (item is ToolStripButton)
                    {
                        ((ToolStripButton)item).Checked = false;
                    }
                }
            }
        }
    }
}
