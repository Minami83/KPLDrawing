using DrawingApp.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp.Tools
{
    class ConnectorTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private Connector connector;
        private DrawingObject start_obj, end_obj;

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

        public ConnectorTool()
        {
            this.Name = "Connector tool";
            this.ToolTipText = "Conn tool";
            this.Text = "Conn";
            this.CheckOnClick = true;
        }

        public void ToolKeyDown(object sender, KeyEventArgs e)
        {
            // do nothing
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                start_obj = this.canvas.GetObjectAt(e.X, e.Y, false);
                this.connector = new Connector(new System.Drawing.Point(e.X, e.Y));
                //start_obj.addObserver(0, this.connector);
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {

        }
        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                end_obj = this.canvas.GetObjectAt(e.X, e.Y, false);
                connector.endPoint = new System.Drawing.Point(e.X, e.Y);
                start_obj.addObserver(0, this.connector);
                end_obj.addObserver(1, this.connector);
                this.canvas.AddDrawingObject(this.connector);
            }
        }
    }
}
