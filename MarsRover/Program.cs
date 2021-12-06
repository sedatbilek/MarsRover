using MarsRover.Implemantations;
using MarsRover.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            Plateau plateau = new Plateau();
            Helper helper = new Helper();
            List<Rover> roverList = new List<Rover>();
            List<string> instructionList = new List<string>();
            IRoverOrganizer roverOrganizer = new RoverOrganizer();
            IPlateauOrganizer plateauOrganizer = new PlateauOrganizer();
            IInstructionOrganizer instructionOrganizer = new InstructionOrganizer();
            while (true)
            {
                ConfigurationManager.RefreshSection("appSettings");
                Console.WriteLine("Please enter rectangle up an right coordinates (please send input value '*' and Enter for calculating when you enter the all information)");
                roverList.Clear();
                instructionList.Clear();
                var input = Console.ReadLine();
                ReturnValue rv = plateauOrganizer.GetPlateauCoordinateAndValidate(input, ref plateau);

                if (!rv.Value)
                    Console.WriteLine(rv.Message);

                while (rv.Value)
                {
                    input = Console.ReadLine();
                    Rover rover = new Rover();
                    string instructions;

                    if (input == "*")
                    {
                        for (int i = 0; i < roverList.Count; i++)
                        {
                            rover = roverList[i];
                            helper.CalculatePosition(rover.Heading, instructionList[i], ref rover);
                            string check = ConfigurationManager.AppSettings["EndlessLoop"];

                            if (check == "true")
                                plateauOrganizer.CalculateEndlessPlateau(ref rover, ref plateau);
                            if ((rover.PositionX > plateau.Right || rover.PositionY > plateau.Up || rover.PositionX < 0 || rover.PositionY < 0) && check == "false")
                            {
                                Console.WriteLine("UZAY BOŞLUĞU, Girilen talimatlar '(" + instructionList[i] + "') sonrası rover mars dışına çıktı. Hesaplanan koordinatlar (" + rover.PositionX + ", " + rover.PositionY + ")");
                            }
                            else
                            {
                                Console.WriteLine(rover.PositionX + " " + rover.PositionY + " " + rover.Heading);
                            }
                        }
                        break;
                    }
                    else
                    {
                        rv = roverOrganizer.ValidateRoverInformation(input, ref rover);
                        helper.DisplayErrorMessageOrAddInputToList(rv, InputType.Rover, ref roverList, rover, ref instructionList, null);
                        if (!rv.Value)
                            break;
                        rv = roverOrganizer.CheckRoverInformationOnPlateau(rover, plateau);

                        if (!rv.Value)
                        {
                            Console.WriteLine(rv.Message);
                        }

                        instructions = Console.ReadLine().Trim();
                        rv = instructionOrganizer.CheckInstructions(instructions);
                        helper.DisplayErrorMessageOrAddInputToList(rv, InputType.Instruction, ref roverList, null, ref instructionList, instructions);
                        if (!rv.Value)
                            break;
                    }
                }
            }
        }
    }

    public enum InputType
    {
        Rover,
        Instruction
    }
}
