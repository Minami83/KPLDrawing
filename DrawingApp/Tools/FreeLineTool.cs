using DrawingApp.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp.Tools
{
    public class FreeLineTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private Composite comp;
        private Shapes.Rectangle control_point;
        private Point clickPoint;
        private bool isComposite;
        DrawingObject new_line1, new_line2;

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

        public FreeLineTool()
        {
            this.Name = "Free Line Tool";
            this.ToolTipText = "Free Line tool";
            Init();
        }

        private void Init()
        {
            this.Text = "free";
            this.CheckOnClick = true;
            this.isComposite = false;
        }

        public void ToolKeyDown(object sender, KeyEventArgs e)
        {
            // do nothing
        }

        public void addControlPoint(DrawingObject selected_object)
        {
            Line li = (Line)selected_object;
            if (li == null) return;
            this.new_line1 = new Line(li.startPoint, clickPoint);
            this.new_line2 = new Line(clickPoint, li.endPoint);
            this.control_point = new Shapes.Rectangle(clickPoint, new Point(clickPoint.X + 1, clickPoint.Y + 1));

            this.control_point.addObserver(1, this.new_line1);
            this.control_point.addObserver(0, this.new_line2);

            this.canvas.RemoveDrawingObject(selected_object);
            this.canvas.AddDrawingObject(this.new_line1);
            this.canvas.AddDrawingObject(this.new_line2);
            this.canvas.AddDrawingObject(this.control_point);
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            DrawingObject selected_object = this.canvas.GetObjectAt(e.X, e.Y, false);
            this.clickPoint = new Point(e.X, e.Y);
            if (selected_object != null && selected_object.object_type == "Line")
            {
                Console.WriteLine("Line Clicked");
                addControlPoint(selected_object);
            }
            else if (selected_object != null && selected_object.object_type == "Composite")
            {
                Console.WriteLine("Composite Clicked");
                this.isComposite = true;
                this.comp = (Composite)selected_object;
                DrawingObject intersected_line = comp.getLinetoTransform(clickPoint);
                Line li = (Line)intersected_line;
                addControlPoint(li);
                comp.removeObject(intersected_line);
            }
        }

        public void MouseUpLine()
        {
            HashSet<DrawingObject> drawingObjects = new HashSet<DrawingObject>();
            drawingObjects.Add(this.new_line1);
            drawingObjects.Add(this.new_line2);

            DrawingObject composite = new Composite(drawingObjects);
            this.canvas.AddDrawingObject(composite);
            this.canvas.cleaning(drawingObjects);
            this.canvas.RemoveDrawingObject(this.control_point);
            this.control_point = null;
        }

        public void MouseUpComposite()
        {
            HashSet<DrawingObject> drawingObjects = new HashSet<DrawingObject>();
            drawingObjects.Add(this.new_line1);
            drawingObjects.Add(this.new_line2);

            this.comp.addObject(this.new_line1);
            this.comp.addObject(this.new_line2);
            this.canvas.cleaning(drawingObjects);
            this.canvas.RemoveDrawingObject(this.control_point);
            this.control_point = null;
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.control_point != null)
            {
                if (this.isComposite) MouseUpComposite();
                else MouseUpLine();
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.control_point != null)
            {
                int xTrans = e.X - this.clickPoint.X;
                int yTrans = e.Y - this.clickPoint.Y;
                this.clickPoint = new Point(e.X, e.Y);
                this.control_point.translate(xTrans, yTrans);
            }
        }

  
    }
}
