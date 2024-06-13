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

        static void CalculateValues(out int totalBrokenTubes, out double totalCost, out int totalChangedTubes)
        {
            const int hoursPerDay = 15; // 15 hours in a day
            const int daysPerWeek = 5; // 5 days in a week
            const int weeksPerMonth = 4; // 4 weeks in a month
            const int monthsPerYear = 9; // 9 months in a year
            const int unitsInClassroom = 4; // 4 units in a classroom
            const int tubesPerUnit = 4; // 4 tubes per unit
            const double tubeCost = 7.0; // 7 USD per tube

            int totalHoursPerYear = hoursPerDay * daysPerWeek * weeksPerMonth * monthsPerYear; 

            totalBrokenTubes = 0; // Initialize total broken tubes
            totalCost = (unitsInClassroom * tubesPerUnit) * tubeCost; // Initialize total cost
            totalChangedTubes = 0; // Initialize total of Changed Tubes
            for (int unit = 0; unit < unitsInClassroom; unit++)
            {
                int[] tubeLife = new int[tubesPerUnit]; // Initialize tube life
                int[] tubeHoursUsed = new int[tubesPerUnit]; // Initialize tube hours used
                bool[] tubeBroken = new bool[tubesPerUnit];
                int brokenTubesInUnit = 0;

                for (int i = 0; i < tubesPerUnit; i++)
                {
                    tubeLife[i] = Rand(); // Generate random tube life
                    tubeHoursUsed[i] = 0; // Initialize tube hours used
                    tubeBroken[i] = false;
                }

                for (int hour = 0; hour < totalHoursPerYear; hour++) // Loop through hours
                {
                    for (int i = 0; i < tubesPerUnit; i++)
                    {
                        if (tubeHoursUsed[i] < tubeLife[i]) // Check if tube is still in use
                        {
                            tubeHoursUsed[i]++; // Increment tube hours used
                        }

                        if (tubeHoursUsed[i] >= tubeLife[i] && tubeBroken[i] ==false) // Check if tube is broken
                        {
                            brokenTubesInUnit++; // Increment broken tubes
                            tubeBroken[i] = true;
                            // If two tubes are broken, replace all four tubes
                            if (brokenTubesInUnit >= 2)
                            {
                                totalBrokenTubes += brokenTubesInUnit; // Increment total broken tubes
                                totalChangedTubes += tubesPerUnit;
                                totalCost += tubesPerUnit * tubeCost; // Increment total cost
                                // Replace all tubes in the unit
                                for (int c = 0; c < tubesPerUnit; c++) // Loop through tubes
                                {
                                    tubeLife[c] = Rand();
                                    tubeHoursUsed[c] = 0;
                                    tubeBroken[c] = false;
                                }
                                brokenTubesInUnit = 0;
                                break;
                            }
                        }
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
            CalculateValues(out int totalBrokenTubes, out double totalCost, out int totalChangedTubes); // Calculate values TotalBrokenTubes and TotalCost

            string input;
            do
            {
                Console.WriteLine("AMOUNT OF BROKEN TUBES: {0}", totalBrokenTubes);
                Console.WriteLine("REPLACED TUBES: {0}", totalChangedTubes);
                Console.WriteLine("AMOUNT OF CASH: {0}", totalCost);
                input = Console.ReadLine().ToLower();
                RunProgram(); // Recalculate and run program

            } while (input != "exit");
        }
    }
}
