using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public interface IMemento
    {
        void saveMemento();
        void retriveMemento();
    }
}
