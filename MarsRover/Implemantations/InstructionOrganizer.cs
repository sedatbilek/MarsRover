using MarsRover.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Implemantations
{
    public class InstructionOrganizer : IInstructionOrganizer
    {
        List<char> instructionList = new List<char>() { 'L', 'R', 'M' };

        ReturnValue rv;
        public InstructionOrganizer()
        {
            rv = new ReturnValue();
        }
        public ReturnValue CheckInstructions(string instructions)
        {
            rv.Value = true;
            var array = instructions.ToCharArray();
            foreach (var item in array)
            {
                if (!instructionList.Contains(item))
                {
                    rv.Value = false;
                    rv.Message = "\nPlease enter one of these letters (L, R, W) for instructions, input value invalid '" + instructions + "'";
                }
            }

            return rv;
        }
    }
}
