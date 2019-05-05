using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Interfaces
{
    public interface IMobile: ILocatable
    {
        void GoTo(int x, int y);

        void MoveNorth();
        void MoveEast();
        void MoveSouth();
        void MoveWest();

        void Face(Direction dir);
        void MoveForward();
        void Turn(int degrees);

    }
}
