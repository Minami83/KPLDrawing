using DrawingApp.States;
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

        public DrawingState state;

        public DrawingObject()
        {
            ID = Guid.NewGuid();
            this.ChangeState(StaticState.GetInstance());
        }

        public void ChangeState(DrawingState state)
        {
            this.state = state;
        }

        public virtual void draw()
        {
            this.state.Draw(this);
        }

        public void Select()
        {
            this.state.Select(this);
        }

        public void Deselect()
        {
            this.state.Deselect(this);
        }

        public abstract void StaticView();
        public abstract void EditView();
        public abstract void translate(int xTrans, int yTrans);
        public abstract bool intersect(int x, int y);

    }
}
