using MarsRover.Implemantations;
using MarsRover.Interfaces;
using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover
{
    public class Calculator : ICalculator
    {
        IHeadingOrganizer headingOrganizer;
        public Calculator()
        {
            headingOrganizer = new HeadingOrganizer();
        }
        public void CalculatePosition(string heading, string instructions, ref Rover rover)
        {
            headingOrganizer.InitHeadingFirstPosition(heading);
            var letter = instructions.ToCharArray();
            foreach (var item in letter)
            {
                headingOrganizer.CalculateHeading(item, ref rover);
            }
        }

        public void AddInputToList(ReturnValue rv, InputType inputType, ref List<Rover> roverList, Rover rover, ref List<string> instructionList, Instruction instructions)
        {
            if (!rv.Value)
            {
                Console.WriteLine(rv.Message);
            }
            else
            {
                if (inputType == InputType.Rover)
                    roverList.Add(rover);
                else if(inputType == InputType.Instruction)
                    instructionList.Add(instructions.Value);
            }
        }
    }
}
