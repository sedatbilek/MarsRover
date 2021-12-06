using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Interfaces
{
    interface IInstructionOrganizer
    {
        ReturnValue CheckInstructions(string instructions);
    }
}
