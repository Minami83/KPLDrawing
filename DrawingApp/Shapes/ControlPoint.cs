using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp.Shapes
{
    class ControlPoint : DrawingObject
    {
        public Point startPoint { get; set; }
        public Point endPoint { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public List<List<DrawingObject>> observersList;

        private Pen pen;

        public ControlPoint()
        {
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;
            this.memento = new DrawingObjectMemento();
            List<DrawingObject> empty_list = new List<DrawingObject>();
            List<DrawingObject> empty_list2 = new List<DrawingObject>();
            this.observersList = new List<List<DrawingObject>>();
            this.observersList.Add(empty_list);
            this.observersList.Add(empty_list2);
        }

        public ControlPoint(Point startPoint) : this()
        {
            this.startPoint = startPoint;
        }

        public ControlPoint(Point startPoint, Point endPoint) : this(startPoint)
        {
            this.endPoint = endPoint;
        }

        private void adjuster()
        {
            if (this.endPoint.X >= this.startPoint.X)
            {
                this.Width = this.endPoint.X - this.startPoint.X;
            }
            else
            {
                this.Width = this.startPoint.X - this.endPoint.X;
                int temp = this.startPoint.X;
                this.startPoint = new Point(this.endPoint.X, this.startPoint.Y);
                this.endPoint = new Point(temp, this.endPoint.Y);
            }

            if (this.endPoint.Y >= this.startPoint.Y)
            {
                this.Height = this.endPoint.Y - this.startPoint.Y;
            }
            else
            {
                this.Height = this.startPoint.Y - this.endPoint.Y;
                int temp = this.startPoint.Y;
                this.startPoint = new Point(this.startPoint.X, this.endPoint.Y);
                this.endPoint = new Point(this.endPoint.X, temp);
            }
        }

        /*public override void draw()
        {
            adjuster();
            this.Graphics.DrawRectangle(this.pen, this.startPoint.X, this.startPoint.Y, this.Width, this.Height);
        }*/

        public override void translate(int xTrans, int yTrans)
        {
            this.startPoint = new Point(this.startPoint.X + xTrans, this.startPoint.Y + yTrans);
            this.endPoint = new Point(this.endPoint.X + xTrans, this.endPoint.Y + yTrans);
            this.onChange(xTrans, yTrans);
        }

        public override bool intersect(int x, int y)
        {
            Point clickedPoint = new Point(x, y);
            if (clickedPoint.X >= startPoint.X && clickedPoint.Y >= startPoint.Y
                && clickedPoint.X <= endPoint.X && clickedPoint.Y <= endPoint.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void StaticView()
        {
            adjuster();
            this.pen.Color = Color.Black;
            String a = "X: " + this.startPoint.X + " Y: " + this.startPoint.Y + " width: " + this.Width + " height: " + this.Height + " pen: " + this.pen;
            //Console.WriteLine(a);
            this.Graphics.DrawRectangle(this.pen, this.startPoint.X, this.startPoint.Y, this.Width, this.Height);
        }

        public override void EditView()
        {
            adjuster();
            this.pen.Color = Color.Red;
            String a = "X: " + this.startPoint.X + " Y: " + this.startPoint.Y + " width: " + this.Width + " height: " + this.Height + " pen: " + this.pen;
            //Console.WriteLine(a);
            this.Graphics.DrawRectangle(this.pen, this.startPoint.X, this.startPoint.Y, this.Width, this.Height);
        }

        public override void onChange(int dx, int dy)
        {
            int i = 0;
            foreach (List<DrawingObject> observers in observersList)
            {
                foreach (DrawingObject observer in observers)
                {
                    observer.Update(i, dx, dy);
                }
                i++;
            }
        }

        public override void addObserver(int type, DrawingObject observer)
        {
            this.observersList[type].Add(observer);
        }

        public override void removeObserver(int type, DrawingObject observer)
        {
            this.observersList[type].Remove(observer);
        }

        public override void addMemento()
        {
            Dictionary<string, Point> currentState = new Dictionary<string, Point>();
            currentState.Add("start", this.startPoint);
            currentState.Add("end", this.endPoint);
            this.memento.saveUndoMemento(currentState);
        }

        public override bool removeMemento()
        {
            Dictionary<string, Point> lastState = this.memento.retriveUndoMemento();
            if (lastState == null)
            {
                return false;
            }
            this.memento.saveRedoMemento(lastState);
            int dx = lastState["start"].X - this.startPoint.X;
            int dy = lastState["start"].Y - this.startPoint.Y;
            this.startPoint = lastState["start"];
            this.endPoint = lastState["end"];
            onChange(dx, dy);
            return true;
        }

        public override bool restoreMemento()
        {
            Dictionary<string, Point> lastState = this.memento.retriveRedoMemento();
            if (lastState == null)
            {
                return false;
            }
            int dx = lastState["start"].X - this.startPoint.X;
            int dy = lastState["start"].Y - this.startPoint.Y;
            this.startPoint = lastState["start"];
            this.endPoint = lastState["end"];
            onChange(dx, dy);
            return true;
        }
    }
}
