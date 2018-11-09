using System;
using System.Collections.Generic;
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
    }
}
