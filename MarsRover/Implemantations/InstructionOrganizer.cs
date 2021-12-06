using MarsRover.Interfaces;
using MarsRover.Model;
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
        public ReturnValue CheckInstructions(Instruction instructions)
        {
            rv.Value = true;
            var array = instructions.Value.ToCharArray();
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
