using DrawingApp.Shapes;
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
        //private DrawingObject drawingObject;
        private HashSet<DrawingObject> drawingObjects;
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
            this.drawingObjects = new HashSet<DrawingObject>();
        }

        public void ToolKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.G)
            {
                DrawingObject compositeObject = new Composite(this.drawingObjects);
                this.canvas.AddDrawingObject(compositeObject);
                this.canvas.cleaning(this.drawingObjects);
                this.drawingObjects = new HashSet<DrawingObject>();
            }
            else if(e.Control && e.KeyCode == Keys.Z)
            {
                this.canvas.UndoClicked();
            }
            else if(e.Control && e.KeyCode == Keys.Y)
            {
                this.canvas.RedoClicked();
            }
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            initPoint = new System.Drawing.Point(e.X, e.Y);
            DrawingObject drawingObject = canvas.GetObjectAt(e.X, e.Y, CtrlKeyisPressed());
            this.drawingObjects.Add(drawingObject);
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (drawingObjects != null)
            {
                int xTrans = e.X - this.initPoint.X;
                int yTrans = e.Y - this.initPoint.Y;
                this.initPoint = new System.Drawing.Point(e.X, e.Y);
                foreach (DrawingObject drawingObject in drawingObjects)
                {
                    if (drawingObject != null && !CtrlKeyisPressed())
                    {
                        drawingObject.translate(xTrans, yTrans);
                        drawingObject.addMemento();
                        this.canvas.AddToUndo(drawingObject);
                    }
                        
                }
            }
            
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            
            /*foreach (DrawingObject drawingObject in drawingObjects)
            {
                if(drawingObject != null)
                {
                    drawingObject.addMemento();
                    this.canvas.AddToUndo(drawingObject);
                }
                
            }*/
            if (!CtrlKeyisPressed())
                this.drawingObjects.Clear();
        }

        public bool CtrlKeyisPressed()
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                return true;
            }
            return false;
        }
    }
}
