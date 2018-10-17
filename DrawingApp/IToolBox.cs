using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{

    public delegate void ToolSelectedEventHandler(ITool tool);

    public interface IToolBox
    {
        event ToolSelectedEventHandler ToolSelected;
        void addTool(ITool tool);
        void removeTool(ITool tool);
        ITool ActiveTool { get; }
    }
}
