using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public interface IObservable
    {
        void onChange(int dx, int dy);
        void addObserver(int type, DrawingObject observer);
        void removeObserver(int type, DrawingObject obsever);
    }
}
