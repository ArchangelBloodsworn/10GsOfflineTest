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

            int totalHoursPerYear = hoursPerDay * daysPerWeek * weeksPerMonth * monthsPerYear;  // Total hours per year 

            totalBrokenTubes = 0; // Initialize total broken tubes
            totalCost = (unitsInClassroom * tubesPerUnit) * tubeCost; // Initialize total cost, include the firsts tubes
            totalChangedTubes = unitsInClassroom * tubesPerUnit; // Initialize total of Changed Tubes, include the firsts tubes
            for (int unit = 0; unit < unitsInClassroom; unit++)
            {
                int[] tubeLife = new int[tubesPerUnit]; // Initialize tube life
                int[] tubeHoursUsed = new int[tubesPerUnit]; // Initialize tube hours used
                bool[] tubeBroken = new bool[tubesPerUnit]; // Initialize the variable to know which tube is broken, false = new, true = broken
                int brokenTubesInUnit = 0;

                for (int i = 0; i < tubesPerUnit; i++)
                {
                    tubeLife[i] = Rand(); // Generate random tube life
                    tubeHoursUsed[i] = 0; // Initialize tube hours used
                    tubeBroken[i] = false; // Initialize the tube as new
                }

                for (int hour = 0; hour < totalHoursPerYear; hour++) // Loop through hours
                {
                    for (int i = 0; i < tubesPerUnit; i++) // Loop through tubes in each unit
                    {
                        if (tubeHoursUsed[i] < tubeLife[i]) // Check if tube is still in use
                        {
                            tubeHoursUsed[i]++; // Increment tube hours used
                        }

                        if (tubeHoursUsed[i] == tubeLife[i] && tubeBroken[i] ==false) // Check if tube is broken for the first time
                        {
                            brokenTubesInUnit++; // Increment broken tubes
                            tubeBroken[i] = true; // Set tube as broken
                        }
                    }

                    ///
                    /// The next IF is outside of the above FOR loop because it waits for the hour to end 
                    /// since it might be that one tube breaks at hour 120 and two at hour 150, thus totaling 3 broken tubes
                    /// because it might be that 2, 3, or even 4 tubes break at the same time.
                    ///
                    if (brokenTubesInUnit >= 2) // If two tubes are broken, replace all four tubes
                    {
                        totalBrokenTubes += brokenTubesInUnit; // Increment total broken tubes
                        totalChangedTubes += tubesPerUnit; // Increment total changed tubes
                        totalCost += tubesPerUnit * tubeCost; // Increment total cost
                        // Replace all tubes in the unit
                        for (int c = 0; c < tubesPerUnit; c++) 
                        {
                            tubeLife[c] = Rand(); // Generate random tube life
                            tubeHoursUsed[c] = 0; // Initialize tube hours used
                            tubeBroken[c] = false; // Initialize the tube as new
                        }
                        brokenTubesInUnit = 0; // Initialize broken tubes in unit
                    }
                }
            }
        }

        static void Main(string[] args) // Here start the program
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
