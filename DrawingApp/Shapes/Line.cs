﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp.Shapes
{
    public class Line : DrawingObject
    {
        public Point startPoint { get; set; }
        public Point endPoint { get; set; }
        public List<List<DrawingObject>> observersList;

        private Pen pen;

        public Line()
        {
            this.object_type = "Line";
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;
            this.memento = new DrawingObjectMemento();
            List<DrawingObject> empty_list = new List<DrawingObject>();
            List<DrawingObject> empty_list2 = new List<DrawingObject>();
            this.observersList = new List<List<DrawingObject>>();
            this.observersList.Add(empty_list);
            this.observersList.Add(empty_list2);
        }

        public Line(Point startPoint) : this()
        {
            this.startPoint = startPoint;
        }

        public Line(Point startPoint, Point endPoint) : this(startPoint)
        {
            this.endPoint = endPoint;
        }

        /*public override void draw()
        {
            this.Graphics.DrawLine(this.pen, startPoint, endPoint);
        }*/

        public override void translate(int xTrans, int yTrans)
        {
            this.startPoint = new Point(this.startPoint.X + xTrans, this.startPoint.Y + yTrans);
            this.endPoint = new Point(this.endPoint.X + xTrans, this.endPoint.Y + yTrans);
            this.onChange(xTrans, yTrans);
            this.addMemento();
        }

        private float Distance(Point a, Point b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public override bool intersect(int x, int y)
        {
            Point clickedPoint = new Point(x, y);
            float distToStart = Distance(startPoint, clickedPoint);
            float distToEnd = Distance(clickedPoint, endPoint);
            float distStartToEnd = Distance(startPoint, endPoint);

            return (Math.Abs(distToStart + distToEnd - distStartToEnd) < 3.0);
        }

        public override void StaticView()
        {
            this.pen.Color = Color.Black;
            this.Graphics.DrawLine(this.pen, startPoint, endPoint);
        }

        public override void EditView()
        {
            this.pen.Color = Color.Red;
            this.Graphics.DrawLine(this.pen, startPoint, endPoint);
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

        public int getYinLine(int x)
        {
            double m = getSlope();
            double closestYinLine = m * (x - startPoint.X) + startPoint.Y;
            return (int)closestYinLine;
        }

        public double getSlope()
        {
            double m = (endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X);
            return m;
        }

        public override void Update(int type, int dx, int dy)
        {
            if (type == 0) startPoint = new Point(startPoint.X + dx, startPoint.Y + dy);
            else if (type == 1) endPoint = new Point(endPoint.X + dx, endPoint.Y + dy);
        }
    }
}
