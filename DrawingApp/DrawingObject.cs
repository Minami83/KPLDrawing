using DrawingApp.States;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public abstract class DrawingObject : IObserver, IObservable
    {
        public Guid ID { get; set; }
        public Graphics Graphics { get; set; }
        public DrawingObjectMemento memento;
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

        public virtual void Update(int type, int dx, int dy)
        {
            
        }

        public virtual void onChange(int dx, int dy)
        {
            
        }

        public virtual void addObserver(int type, DrawingObject observer)
        {
            
        }

        public virtual void removeObserver(int type, DrawingObject observer)
        {
            
        }

        public virtual void addMemento()
        {

        }

        public virtual bool removeMemento()
        {
            return true;
        }

        public virtual bool restoreMemento()
        {
            return true;
        }
    }
}
