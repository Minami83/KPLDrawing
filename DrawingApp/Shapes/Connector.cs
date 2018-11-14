﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp.Shapes
{
    class Connector : DrawingObject
    {
        public Point startPoint { get; set; }
        public Point endPoint { get; set; }

        private Pen pen;

        public Connector()
        {
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;
        }

        public Connector(Point startPoint) : this()
        {
            this.startPoint = startPoint;
        }

        public Connector(Point startPoint, Point endPoint) : this(startPoint)
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
            this.pen.Color = Color.Black;
            this.Graphics.DrawLine(this.pen, startPoint, endPoint);
        }

        public override void Update(int type, int dx, int dy)
        {
            if (type == 0) startPoint = new Point(startPoint.X + dx, startPoint.Y + dy);
            else if (type == 1) endPoint = new Point(endPoint.X + dx, endPoint.Y + dy);
        }
    }
}
