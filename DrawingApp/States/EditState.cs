using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp.States
{
    public class EditState : DrawingState
    {

        public static DrawingState instance;

        public static DrawingState GetInstance()
        {
            if (instance == null)
            {
                instance = new EditState();
            }
            return instance;
        }

        public override void Draw(DrawingObject obj)
        {
            obj.EditView();
        }

        public override void Deselect(DrawingObject obj)
        {
            obj.ChangeState(StaticState.GetInstance());
        }
    }
}
