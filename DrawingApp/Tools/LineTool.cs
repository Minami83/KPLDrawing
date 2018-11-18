using DrawingApp.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp.Tools
{
    public class LineTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private Line line;

        public Cursor Cursor
        {
            get
            {
                return Cursors.Arrow;
            }
        }

        public ICanvas TargetCanvas
        {
            get
            {
                return this.canvas;
            }
            set
            {
                this.canvas = value;
            }
        }

        public LineTool()
        {
            this.Name = "Line Tool";
            this.ToolTipText = "Line tool";
            Init();
        }

        private void Init()
        {
            this.Text = "line";
            this.CheckOnClick = true;
        }

        public void ToolKeyDown(object sender, KeyEventArgs e)
        {
            // do nothing
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            line = new Line(new System.Drawing.Point(e.X, e.Y));
            line.endPoint = new System.Drawing.Point(e.X, e.Y);
            canvas.AddDrawingObject(line);
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                line.endPoint = new System.Drawing.Point(e.X, e.Y);
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                line.endPoint = new System.Drawing.Point(e.X, e.Y);
            }
        }
    }
}
