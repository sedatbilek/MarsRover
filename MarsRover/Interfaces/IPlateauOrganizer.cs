using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Interfaces
{
    interface IPlateauOrganizer
    {
        ReturnValue GetPlateauCoordinate(string upRight, ref Plateau plateau);
        void CalculateEndlessPlateau(ref Rover rover, ref Plateau plateau);
    }
}
