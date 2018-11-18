using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public class UndoRedo : ICommand
    {
        public Stack<DrawingObject> undoStack;
        public Stack<DrawingObject> redoStack;
        public ICanvas canvas;

        public ICanvas TargetCanvas
        {
            get
            {
                return this.canvas;
            }
            set
            {
                this.canvas = value;
            }
        }

        public UndoRedo()
        {
            this.undoStack = new Stack<DrawingObject>();
            
        }

        public void Execute()
        {
            if (undoStack.Count != 0)
            {
                DrawingObject lastChanged = undoStack.Pop();
                bool stillExist = lastChanged.removeMemento();
            }
            Console.WriteLine("empty stack");
        }

        public void Unexecute()
        {
            
        }

        public void addUndo(DrawingObject drawingObject)
        {
            this.undoStack.Push(drawingObject);
        }
    }
}
