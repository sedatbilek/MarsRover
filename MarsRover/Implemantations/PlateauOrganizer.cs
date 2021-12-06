using MarsRover.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Implemantations
{
    public class PlateauOrganizer : IPlateauOrganizer
    {
        public ReturnValue GetPlateauCoordinateAndValidate(string upRight, ref Plateau plateau)
        {
            ReturnValue rv = new ReturnValue();

            var array = upRight.Split(" ");

            if (array.Length != 2)
            {
                rv.Value = false;
                rv.Message = "\nEntered coordinate information must be equal two (X and Y coordinates)";
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (!uint.TryParse(array[i], out uint value))
                {
                    rv.Value = false;
                    rv.Message = "\nPlease enter coordinates natural number format, invalid value '" + upRight + "'";
                    break;
                }
                else
                {
                    if (i == 0)
                        plateau.Right = (int)value;
                    else
                        plateau.Up = (int)value;
                }
            }

            return rv;
        }
        //this code block add for endless loop for detail read appconfig
        public void CalculateEndlessPlateau(ref Rover rover, ref Plateau plateau)
        {
            while (true)
            {
                if (rover.PositionX > plateau.Right)
                    rover.PositionX = rover.PositionX - plateau.Right;
                else if (rover.PositionX < 0)
                    rover.PositionX = rover.PositionX + plateau.Right;
                else if (rover.PositionY > plateau.Up)
                    rover.PositionY = rover.PositionY - plateau.Up;
                else if (rover.PositionY < 0)
                    rover.PositionY = rover.PositionY + plateau.Up;
                else
                    break;
            }
        }
    }
}
