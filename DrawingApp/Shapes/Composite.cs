using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp.Shapes
{
    public class Composite : DrawingObject
    {
        private List<DrawingObject> drawingObjects;

        public Composite(HashSet<DrawingObject> drawingObjects)
        {
            foreach (DrawingObject a in drawingObjects)
            {
                Console.WriteLine(a);
            }
            this.drawingObjects = new List<DrawingObject>(drawingObjects);
            this.object_type = "Composite";
        }

        public override void EditView()
        {
            foreach (DrawingObject drawingObject in drawingObjects)
            {
                drawingObject.Graphics = this.Graphics;
                drawingObject.EditView();
            }
        }

        public override bool intersect(int x, int y)
        {
            bool intersect_exist = false;
            foreach (DrawingObject drawingObject in drawingObjects)
            {
                intersect_exist = drawingObject.intersect(x, y);
                if (intersect_exist) break;
            }
            return intersect_exist;
        }

        public override void StaticView()
        {
            foreach (DrawingObject drawingObject in drawingObjects)
            {
                drawingObject.Graphics = this.Graphics;
                drawingObject.StaticView();
            }
        }

        public override void translate(int xTrans, int yTrans)
        {
            foreach (DrawingObject drawingObject in drawingObjects)
            {
                drawingObject.translate(xTrans, yTrans);
            }
        }

        public DrawingObject getLinetoTransform(Point clickPoint)
        {
            foreach(DrawingObject d in drawingObjects)
            {
                bool intersect = d.intersect(clickPoint.X, clickPoint.Y);
                if (intersect && d.object_type == "Line") return d;
            }
            return null;
        }

        public void removeObject(DrawingObject drawingObject)
        {
            this.drawingObjects.Remove(drawingObject);
        }

        public void addObject(DrawingObject drawingObject)
        {
            this.drawingObjects.Add(drawingObject);
        }

        public override void addMemento()
        {
            foreach (DrawingObject d in drawingObjects)
            {
                d.addMemento();
            }
        }

        public override bool removeMemento()
        {
            foreach (DrawingObject d in drawingObjects)
            {
                d.removeMemento();
            }
            return true;
        }

        public override bool restoreMemento()
        {
            foreach (DrawingObject d in drawingObjects)
            {
                d.restoreMemento();
            }
            return true;
        }

    }
}
