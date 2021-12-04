using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover
{
    public class Helper
    {
        ReturnValue rv = new ReturnValue();
        List<string> compass = new List<string>() { "N", "E", "S", "W" };
        List<char> instructionList = new List<char>() { 'L', 'R', 'M' };
        int rotatePosition;
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
                        bool check = CheckHeading(array[2]);
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

        private bool CheckHeading(string heading)
        {
            return compass.Contains(heading);
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

        public void CalculatePosition(string heading, string instructions, ref Rover rover)
        {
            InitHeadingFirstPosition(heading);
            var letter = instructions.ToCharArray();
            foreach (var item in letter)
            {
                CalculateHeading(item, ref rover);
            }
        }

        private void InitHeadingFirstPosition(string txt)
        {
            switch (txt)
            {
                case "N":
                    rotatePosition = 0;
                    break;
                case "E":
                    rotatePosition = 1;
                    break;
                case "S":
                    rotatePosition = 2;
                    break;
                case "W":
                    rotatePosition = 3;
                    break;
            }
        }

        private void CalculateHeading(char txt, ref Rover rover)
        {
            if (txt == 'L')
            {
                rotatePosition = (rotatePosition - 1 < 0 ? (rotatePosition + 4 - 1) : rotatePosition - 1) % 4;
                rover.Heading = compass[rotatePosition];
            }
            else if (txt == 'R')
            {
                rotatePosition = (rotatePosition + 1) % 4;
                rover.Heading = compass[rotatePosition];
            }
            else if (txt == 'M')
            {
                switch (rotatePosition)
                {
                    case 0:
                        rover.PositionY = rover.PositionY + 1;
                        break;
                    case 1:
                        rover.PositionX = rover.PositionX + 1;
                        break;
                    case 2:
                        rover.PositionY = rover.PositionY - 1;
                        break;
                    case 3:
                        rover.PositionX = rover.PositionX - 1;
                        break;
                }
            }
        }
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

        public void DisplayErrorMessageOrAddInputToList(ReturnValue rv, string key, ref List<Rover> roverList, Rover rover, ref List<string> instructionList, string instructions)
        {
            if (!rv.Value)
            {
                Console.WriteLine(rv.Message);
            }
            else
            {
                if (key == "r")
                    roverList.Add(rover);
                else
                    instructionList.Add(instructions);
            }
        }
    }
}
