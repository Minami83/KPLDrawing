using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public abstract class DrawingState
    {

        

        public abstract void Draw(DrawingObject obj);

        public virtual void Select(DrawingObject obj)
        {

        }

        public virtual void Deselect(DrawingObject obj)
        {

        }
    }
}
