using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public interface ICommand
    {
        ICanvas TargetCanvas { get; set; }

        void Execute();
        void Unexecute();
    }
}
