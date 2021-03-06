﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public interface IMemento
    {
        void saveUndoMemento(Dictionary<string, Point> currentState);
        Dictionary<string, Point> retriveUndoMemento();
        void saveRedoMemento(Dictionary<string, Point> currentState);
        Dictionary<string, Point> retriveRedoMemento();
    }
}
