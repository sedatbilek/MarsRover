using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Interfaces
{
    interface IRoverOrganizer
    {
        ReturnValue ValidateRoverInformation(string roverInformation, ref Rover rover);
        ReturnValue CheckRoverInformationOnPlateau(Rover rover, Plateau plateau);
    }
}
