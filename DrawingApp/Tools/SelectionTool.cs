using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp.Tools
{
    class SelectionTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private DrawingObject drawingObject;
        private System.Drawing.Point initPoint;

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

        public SelectionTool()
        {
            this.Name = "Selection tool";
            this.ToolTipText = "Selection tool";
            this.Text = "Select";
            this.CheckOnClick = true;
        }
        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            initPoint = new System.Drawing.Point(e.X, e.Y);
            drawingObject = canvas.GetObjectAt(e.X, e.Y);
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (drawingObject != null)
            {
                int xTrans = e.X - this.initPoint.X;
                int yTrans = e.Y - this.initPoint.Y;
                this.initPoint = new System.Drawing.Point(e.X, e.Y);
                drawingObject.translate(xTrans, yTrans);
            }
            
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            drawingObject = null;
        }
    }
}
