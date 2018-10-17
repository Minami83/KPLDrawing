using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public abstract class DrawingObject
    {
        public Guid ID { get; set; }
        public Graphics Graphics { get; set; }

        public DrawingObject()
        {
            ID = Guid.NewGuid();
        }

        public abstract void draw();
        public abstract void translate(int xTrans, int yTrans);
        public abstract bool intersect(int x, int y);

    }
}
