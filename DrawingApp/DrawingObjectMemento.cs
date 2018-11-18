using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public class DrawingObjectMemento : IMemento
    {
        public Stack<Dictionary<string, Point>> undoStateList;
        public Stack<Dictionary<string, Point>> redoStateList;

        public DrawingObjectMemento()
        {
            this.undoStateList = new Stack<Dictionary<string, Point>>();
            this.redoStateList = new Stack<Dictionary<string, Point>>();
        }

        public Dictionary<string, Point> retriveUndoMemento()
        {
            if (undoStateList.Count != 0)
            {
                Dictionary<string, Point> lastState = this.undoStateList.Pop();
                return lastState;
            }
            return null;
        }

        public void saveUndoMemento(Dictionary<string, Point> currentState)
        {
            this.undoStateList.Push(currentState);
        }

        public Dictionary<string, Point> retriveRedoMemento()
        {
            if (redoStateList.Count != 0)
            {
                Dictionary<string, Point> lastState = this.redoStateList.Pop();
                return lastState;
            }
            return null;
        }

        public void saveRedoMemento(Dictionary<string, Point> currentState)
        {
            this.redoStateList.Push(currentState);
        }

    }
}
