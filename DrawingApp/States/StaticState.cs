using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp.States
{
    public class StaticState : DrawingState
    {
        public static DrawingState instance;

        public static DrawingState GetInstance()
        {
            if (instance == null)
            {
                instance = new StaticState();
            }
            return instance;
        }

        public override void Draw(DrawingObject obj)
        {
            obj.StaticView();
        }

        public override void Select(DrawingObject obj)
        {
            obj.ChangeState(EditState.GetInstance());
        }
    }
}
