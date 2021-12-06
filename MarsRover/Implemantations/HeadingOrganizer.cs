using MarsRover.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Implemantations
{
    public class HeadingOrganizer : IHeadingOrganizer
    {
        List<string> compass = new List<string>() { "N", "E", "S", "W" };
        int rotatePosition;
        public bool CheckHeading(string heading)
        {
            return compass.Contains(heading);
        }

        public void InitHeadingFirstPosition(string txt)
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

        public void CalculateHeading(char txt, ref Rover rover)
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
    }
}
