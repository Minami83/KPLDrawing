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
        public Stack<Dictionary<string, Point>> stateList;
        private int changed;

        public DrawingObjectMemento()
        {
            this.stateList = new Stack<Dictionary<string, Point>>();
        }

        public Dictionary<string, Point> retriveMemento()
        {
            //stateSlicer();
            if (stateList.Count != 0)
            {
                Dictionary<string, Point> lastState = this.stateList.Pop();
                return lastState;
            }
            else
            {
                return null;
            }
        }

        public void saveMemento(Dictionary<string, Point> currentState)
        {
            this.stateList.Push(currentState);
        }

        public void stateSlicer()
        {
            if (this.stateList.Count >= 3)
            {
                Stack<Dictionary<string, Point>> slicedState = new Stack<Dictionary<string, Point>>();
                int i = 0;
                foreach (var state in stateList)
                {
                    if ((i % 2) == 0)
                        slicedState.Push(state);
                }
                this.stateList = slicedState;
            }
        }
    }
}
