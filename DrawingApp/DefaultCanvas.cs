using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp
{
    public class DefaultCanvas : Control, ICanvas
    {
        private ITool activeTool;
        private List<DrawingObject> drawingObjects;
        public UndoRedo undoredo;

        public DefaultCanvas()
        {
            Init();
        }

        public void Init()
        {
            this.drawingObjects = new List<DrawingObject>();
            this.DoubleBuffered = true;
            this.undoredo = new UndoRedo();

            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;

            this.Paint += DefaultCanvas_Paint;
            this.MouseDown += DefaultCanvas_MouseDown;
            this.MouseMove += DefaultCanvas_MouseMove;
            this.MouseUp += DefaultCanvas_MouseUp;
            this.KeyDown += DefaultCanvas_KeyDown;
        }

        private void DefaultCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(this.activeTool != null)
            {
                this.activeTool.ToolKeyDown(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.activeTool != null)
            {
                this.activeTool.ToolMouseDown(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.activeTool != null)
            {
                this.activeTool.ToolMouseUp(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.activeTool != null)
            {
                this.activeTool.ToolMouseMove(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (DrawingObject obj in drawingObjects)
            {
                obj.Graphics = e.Graphics;
                obj.draw();
            }
        }

        public void Repaint()
        {
            this.Invalidate();
            this.Update();
        }

        public void SetActiveTool(ITool tool)
        {
            this.activeTool = tool;
        }

        public void SetBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        public void AddDrawingObject(DrawingObject drawingObject)
        {
            this.drawingObjects.Add(drawingObject);
            AddToUndo(drawingObject);
            this.Repaint();
        }

        public void RemoveDrawingObject(DrawingObject drawingObject)
        {
            this.drawingObjects.Remove(drawingObject);
            this.Repaint();
        }

        public DrawingObject GetObjectAt(int x, int y, bool multiSelect)
        {
            if (!multiSelect)
                DeselectAll();
            foreach (DrawingObject drawingObject in drawingObjects)
            {
                if (drawingObject.intersect(x,y))
                {
                    drawingObject.Select();
                    return drawingObject;
                }
            }
            return null;
        }

        public void DeselectAll()
        {
            foreach (DrawingObject drawingObject in drawingObjects)
            {
                drawingObject.Deselect();
            }
        }

        public void cleaning(HashSet<DrawingObject> drawingObjects)
        {
            this.drawingObjects = new List<DrawingObject>(this.drawingObjects.Except(drawingObjects));
            this.Repaint();
        }

        public void UndoClicked()
        {
            Console.WriteLine("undo clicked");
            this.undoredo.Execute();
            this.Repaint();
        }

        public void AddToUndo(DrawingObject drawingObject)
        {
            this.undoredo.addUndo(drawingObject);
        }
    }
}
