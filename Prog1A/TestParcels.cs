// Program 1B
// CIS 200-01
// By: M9888
// Due: 10/2/2019

// File: TestParcels.cs
// This is a simple, console application designed to exercise the Parcel hierarchy.
// It creates several different Parcels and prints them based on LINQ queries.
// Criteria is listed in comments with each query.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace Prog1
{
    class TestParcels
    {
        // Precondition:  None
        // Postcondition: Parcels have been created and displayed
        static void Main(string[] args)
        {
            // Test Data - Magic Numbers OK
            Address ad1 = new Address("  John Smith  ", "   123 Any St.   ", "  Apt. 45", "Louisville   ", "  KY   ", 40202); // Test Address 1
            Address ad2 = new Address("Jane Doe", "987 Main St.", "Beverly Hills", "CA", 90210); // Test Address 2
            Address ad3 = new Address("James Kirk", "654 Roddenberry Way", "Suite 321", "El Paso", "TX", 79901); // Test Address 3
            Address ad4 = new Address("John Crichton", "678 Pau Place", "Apt. 7", "Portland", "ME", 04101); // Test Address 4

            Address ad5 = new Address("Test name", "Test address_1", "Test address_2", "Test city", "Test state", 99999); //Test address 5
            Address ad6 = new Address("Joe", "123 The Street", "wut", "NYC", "NY", 12345); //Test address 6
            Address ad7 = new Address("Dr. Wright", "I dont know where you live", "Louisville", "KY", 40217); //Test address 7
            Address ad8 = new Address("'Ol McDonald", "had a farm", "               eee eye eee eye", "Farmville", "T E X A S", 5); //Test address 8


            var parcelList = new List<Parcel>() // Test list of parcels
            {
                new Letter(ad1, ad3, 0.50M), // Test Letter 1
                new Letter(ad2, ad4, 1.20M), // Test Letter 2
                new Letter(ad4, ad1, 1.70M), // Test Letter 3

                new GroundPackage(ad4, ad5, 5, 10, 1, 100), //Test Groundpackage 1
                new GroundPackage(ad1, ad6, 10, 10, 5, 25), //Test Groundpackage 2

                new NextDayAirPackage(ad6, ad7, 5, 10, 1, 100, 150), //Test Next Day Air Package Heavy
                new NextDayAirPackage(ad6, ad7, 5, 10, 100, 49.9, 150), //Test Next Day Air Package Large
                new NextDayAirPackage(ad6, ad7, 5, 10, 100, 50, 150), //Test Next Day Air Package Large & Heavy

                new TwoDayAirPackage(ad8, ad3, 5, 10, 1, 150, TwoDayAirPackage.Delivery.Saver), //Test Two Day Air Package that is a Saver
                new TwoDayAirPackage(ad7, ad2, 5, 10, 1, 75, TwoDayAirPackage.Delivery.Early) //Test Two Day Air Package that is a Early
            };

            string NL = Environment.NewLine; // NewLine shortcut

            // Display data
            WriteLine($"Program 1B - LINQ{NL}");

            // #1
            //LINQ query Select all Parcels and order by destination zip (descending)
            var SortedZip =
                    from one in parcelList 
                    let zip = one.DestinationAddress.Zip
                    orderby zip descending
                    select one;

            // display  SortedZip results
            Write("\n--------------------------------------LINQ 1 \n\n");
            foreach (var p in SortedZip)
            {
                Console.WriteLine($"{p.ToString()}{NL}");
                WriteLine($"____________________________________________{p.DestinationAddress.Zip}{NL}");
            }
            Pause();



            // #2
            //LINQ query Select all Parcels and order by cost (ascending)
            var SortedCost =
                    from two in parcelList
                    let cost = two.CalcCost()
                    orderby cost
                    select two;

            // display  SortedCost results
            Write("\n--------------------------------------LINQ 2 \n\n");
            foreach (var p in SortedCost)
            {
                Console.WriteLine($"{p.ToString()}{NL}");
                WriteLine($"_____________________________________________{p.CalcCost():C2}{NL}");
            }
            Pause();



            // #3
            //LINQ query Select all Parcels and order by Parcel type (ascending) and then cost (descending)
            var SortedType =
                    from three in parcelList
                    let type = three.GetType().ToString()
                    let cost = three.CalcCost()
                    orderby type, cost descending
                    select three;

            // display  SortedType results
            Write("\n--------------------------------------LINQ 3 \n\n");
            foreach (var p in SortedType)
            {
                Console.WriteLine($"{p.ToString()}{NL}");
                WriteLine($"_____________________________________________{p.GetType().ToString()}: {p.CalcCost():C2}{NL}");
            }
            Pause();



            // #4
            //LINQ query Select all AirPackage objects that are heavy and order by weight (descending)
            //var AirPackagesByWeight =
            //        from four in parcelList
            //        //let air = four as AirPackage
            //        where four is AirPackage && (four as AirPackage).IsHeavy()
            //        let Weight = four.Weight
            //        orderby Weight
            //        select four;

            var HeavyAPByWeight2 =
                    from four in parcelList
                    let airP = four as AirPackage // casts parcels as Airpackage
                    where (airP != null) && airP.IsHeavy() // because the conversion would return null if it can't becasted as AiPackage, we'll filter for nulls. Then use the IsHeavy() method
                    orderby airP.Weight descending // airP are already an airpackge so we can use Weight 
                    select four;

            // display  AirPackagesByWeight2 results
            Write("\n--------------------------------------LINQ 4 \n\n");
            foreach (var p in HeavyAPByWeight2)
            {
                Console.WriteLine($"{p.ToString()}{NL}");
                WriteLine($"_____________________________________________{p.GetType().ToString()}: {(p as AirPackage).Weight} lbs{NL}");
            }
            Pause();

        }

        // Precondition:  None
        // Postcondition: Pauses program execution until user presses Enter and
        //                then clears the screen
        public static void Pause()
        {
            WriteLine("Press Enter to Continue...");
            ReadLine();

            Console.Clear(); // Clear screen
        }
    }
}
