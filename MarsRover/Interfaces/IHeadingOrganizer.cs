using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Interfaces
{
    interface IHeadingOrganizer
    {
        bool CheckHeading(string heading);
        void InitHeadingFirstPosition(string txt);
        void CalculateHeading(char txt, ref Rover rover);
    }
}
