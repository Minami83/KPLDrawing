using DrawingApp.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp
{
    public partial class Form1 : Form
    {
        private IToolBox toolbox;
        private ITool tool;
        private ICanvas canvas;

        public Form1()
        {
            InitializeComponent();
            initForm();
        }

        private void initForm()
        {

            #region ToolBox
            this.toolbox = new DefaultToolBox();
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add((Control)this.toolbox);
            #endregion

            #region Tool
            this.toolbox.addTool(new LineTool());
            this.toolbox.addTool(new RectangleTool());
            this.toolbox.addTool(new SelectionTool());
            this.toolbox.ToolSelected += ToolBox_ToolSelected;
            #endregion

            #region Canvas
            this.canvas = new DefaultCanvas();
            this.toolStripContainer1.ContentPanel.Controls.Add((Control)this.canvas);
            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ToolBox_ToolSelected(ITool tool)
        {
            if (this.canvas != null)
            {
                this.canvas.SetActiveTool(tool);
                tool.TargetCanvas = this.canvas;
            }
        }
    }
}
