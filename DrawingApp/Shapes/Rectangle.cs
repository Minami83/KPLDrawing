using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp.Shapes
{
    class Rectangle : DrawingObject
    {
        public Point startPoint { get; set; }
        public Point endPoint { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private Pen pen;

        public Rectangle()
        {
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;
        }

        public Rectangle(Point startPoint) : this()
        {
            this.startPoint = startPoint;
        }

        public Rectangle(Point startPoint, Point endPoint) : this(startPoint)
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

        public override void draw()
        {
            adjuster();
            this.Graphics.DrawRectangle(this.pen, this.startPoint.X, this.startPoint.Y, this.Width, this.Height);
        }

        public override void translate(int xTrans, int yTrans)
        {
            this.startPoint = new Point(this.startPoint.X + xTrans, this.startPoint.Y + yTrans);
            this.endPoint = new Point(this.endPoint.X + xTrans, this.endPoint.Y + yTrans);
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
    }
}
