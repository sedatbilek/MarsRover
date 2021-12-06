using MarsRover.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Implemantations
{
    public class RoverOrganizer : IRoverOrganizer
    {
        ReturnValue rv;
        IHeadingOrganizer headingOrganizer;
        public RoverOrganizer()
        {
            rv = new ReturnValue();
            headingOrganizer = new HeadingOrganizer();
        }

        public ReturnValue ValidateRoverInformation(string roverInformation, ref Rover rover)
        {
            rv.Value = true;
            var array = roverInformation.Split(" ");

            if (array.Length != 3)
            {
                rv.Value = false;
                rv.Message = "\nRover information must be equal 3 X, Y coordinates and Heading info ('N', 'E', 'S', 'W')";
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (!uint.TryParse(array[i], out uint value) && i != 2)
                {
                    rv.Value = false;
                    rv.Message = "\nPlease enter coordinates natural number format, invalid value '" + roverInformation + "'";
                    break;
                }
                else
                {
                    if (i == 0)
                        rover.PositionX = (int)value;
                    else if (i == 1)
                        rover.PositionY = (int)value;
                    else
                    {
                        bool check = headingOrganizer.CheckHeading(array[2]);
                        if (!check)
                        {
                            rv.Value = false;
                            rv.Message = "\nPlease enter one of these letters (N, W, E, S) for heading, invalid value '" + array[2] + "' for heading information";
                        }
                        else
                            rover.Heading = array[2];
                    }
                }
            }

            return rv;
        }

        public ReturnValue CheckRoverInformationOnPlateau(Rover rover, Plateau plateau)
        {
            rv.Value = true;
            if (rover.PositionX > plateau.Right || rover.PositionY > plateau.Up)
            {
                rv.Value = false;
                rv.Message = "\nPlease enter rover coordinates on the plateau, (" + rover.PositionX + "," + rover.PositionY + ") not in (" + plateau.Right + "," + plateau.Up + ")";
            }
            return rv;
        }
    }
}
