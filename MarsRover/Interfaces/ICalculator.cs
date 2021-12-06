using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Interfaces
{
    interface ICalculator
    {
        void CalculatePosition(string heading, string instructions, ref Rover rover);
        void AddInputToList(ReturnValue rv, InputType inputType, ref List<Rover> roverList, Rover rover, ref List<string> instructionList, Instruction instructions);
    }
}
