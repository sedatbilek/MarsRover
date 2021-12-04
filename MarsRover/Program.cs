using System;
using System.Collections.Generic;
using System.Configuration;

namespace MarsRover
{
    class Program
    {
        static Plateau plateau = new Plateau();
        static void Main(string[] args)
        {
            Helper helper = new Helper();
            List<Rover> roverList = new List<Rover>();
            List<string> instructionList = new List<string>();

            while (true)
            {
                ConfigurationManager.RefreshSection("appSettings");
                Console.WriteLine("Please enter rectangle up an right coordinates (please send input value '*' and Enter for calculating when you enter the all information)");
                roverList.Clear();
                instructionList.Clear();
                var input = Console.ReadLine();
                ReturnValue rv = helper.GetPlateauCoordinateAndValidate(input, ref plateau);

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
                            
                            if(check == "true")
                                helper.CalculateEndlessPlateau(ref rover, ref plateau);
                            if ((rover.PositionX > plateau.Right || rover.PositionY > plateau.Up || rover.PositionX < 0 || rover.PositionY < 0) && check =="false")
                            {
                                Console.WriteLine("UZAY BOŞLUĞU, Girilen talimatlar '("+ instructionList[i] + "') sonrası rover mars dışına çıktı.");
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
                        rv = helper.ValidateRoverInformation(input, ref rover);
                        helper.DisplayErrorMessageOrAddInputToList(rv, "r", ref roverList, rover, ref instructionList, null);
                        if (!rv.Value)
                            break;
                        rv = helper.CheckRoverInformationOnPlateau(rover, plateau);

                        if (!rv.Value)
                        {
                            Console.WriteLine(rv.Message);
                        }

                        instructions = Console.ReadLine().Trim();
                        rv = helper.CheckInstructions(instructions);
                        helper.DisplayErrorMessageOrAddInputToList(rv, "i", ref roverList, null, ref instructionList, instructions);
                        if (!rv.Value)
                            break;
                    }
                }
            }
        }
    }
}
