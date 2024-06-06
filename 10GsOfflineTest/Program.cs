using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10GsOfflineTest
{
    internal class Program
    {
        static Random random = new Random();

        static int Rand()
        {
            return random.Next(100, 201);
        }

        static int Pregunta(string pregunta) // 1. Fluorescent tubes broken in a year. 2. Total cost of fluorescent tubes per year per classroom. 3. Recalculate.
        {
            pregunta = pregunta.ToLower();

            if (pregunta.Contains("1") || pregunta.Contains("broken") || pregunta.Contains("rotos"))
            {
                return 1;
            }
            else if (pregunta.Contains("2") || pregunta.Contains("cost"))
            {
                return 2;
            }
            else if (pregunta.Contains("3") || pregunta.Contains("recalculate"))
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }

        static void CalculateValues(out int totalBrokenTubes, out double totalCost)
        {
            const int hoursPerDay = 15; // 15 hours in a day
            const int daysPerWeek = 5; // 5 days in a week
            const int weeksPerMonth = 4; // 4 weeks in a month
            const int monthsPerYear = 9; // 9 months in a year
            const int unitsInClassroom = 4; // 4 units in a classroom
            const int tubesPerUnit = 4; // 4 tubes per unit
            const double tubeCost = 7.0; // 7 USD per tube

            int totalHoursPerYear = hoursPerDay * daysPerWeek * weeksPerMonth * monthsPerYear; // 540 hours in a year

            totalBrokenTubes = 0; // Initialize total broken tubes
            totalCost = 0; // Initialize total cost

            for (int unit = 0; unit < unitsInClassroom; unit++)
            {
                int[] tubeLife = new int[tubesPerUnit]; // Initialize tube life
                int[] tubeHoursUsed = new int[tubesPerUnit]; // Initialize tube hours used
                int brokenTubesInUnit = 0;

                for (int i = 0; i < tubesPerUnit; i++)
                {
                    tubeLife[i] = Rand(); // Generate random tube life
                    tubeHoursUsed[i] = 0; // Initialize tube hours used
                }

                for (int hour = 0; hour < totalHoursPerYear; hour++) // Loop through hours
                {
                    for (int i = 0; i < tubesPerUnit; i++)
                    {
                        if (tubeHoursUsed[i] < tubeLife[i]) // Check if tube is still in use
                        {
                            tubeHoursUsed[i]++; // Increment tube hours used
                        }

                        if (tubeHoursUsed[i] == tubeLife[i]) // Check if tube is broken
                        {
                            brokenTubesInUnit++; // Increment broken tubes
                        }
                    }

                    // If two tubes are broken, replace all four tubes
                    if (brokenTubesInUnit >= 2)
                    {
                        totalBrokenTubes += tubesPerUnit; // Increment total broken tubes
                        totalCost += tubesPerUnit * tubeCost; // Increment total cost

                        // Replace all tubes in the unit
                        for (int i = 0; i < tubesPerUnit; i++) // Loop through tubes
                        {
                            tubeLife[i] = Rand();
                            tubeHoursUsed[i] = 0;
                        }

                        brokenTubesInUnit = 0;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            RunProgram(); 
        }

        static void RunProgram()
        {
            CalculateValues(out int totalBrokenTubes, out double totalCost); // Calculate values TotalBrokenTubes and TotalCost

            string input;
            do
            {
                Console.WriteLine("Enter an option:");
                Console.WriteLine("1. Show the number of fluorescent tubes broken in a year.");
                Console.WriteLine("2. Show the total cost of fluorescent tubes per year per classroom.");
                Console.WriteLine("3. Recalculate.");
                Console.WriteLine("Type 'exit' or 'salir' to exit.");

                input = Console.ReadLine().ToLower();

                switch (Pregunta(input)) // Check if input is valid
                {
                    case 1:
                        Console.WriteLine("1. Fluorescent tubes broken in a year: " + totalBrokenTubes);
                        break;
                    case 2:
                        Console.WriteLine("2. Total cost of fluorescent tubes per year per classroom: " + totalCost + " USD");
                        break;
                    case 3:
                        Console.WriteLine("Recalculating...");
                        RunProgram(); // Recalculate and run program
                        return;
                    default:
                        if (input != "exit" && input != "salir")
                        {
                            Console.WriteLine("Invalid option. Please try again.");
                        }
                        break;
                }

            } while (input != "exit" && input != "salir");
        }
    }
}
