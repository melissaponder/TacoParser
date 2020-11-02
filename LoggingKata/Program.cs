using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    public class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------
           
            logger.LogInfo("Log initialized");

            // DONE - use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line

            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            // DONE - Create a new instance of your TacoParser class

            var parser = new TacoParser();

            // DONE - Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);

            var locations = lines.Select(line => parser.Parse(line)).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS

            if (lines.Length == 0)
            {
                logger.LogError("There are 0 lines.", null);
            }
            if (lines.Length == 1)
            {
                logger.LogWarning("There is only 1 line.");
            }

            // Now that your Parse method is completed, START BELOW ----------

            // DONE - TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // DONE - Create a `double` variable to store the distance

            ITrackable tb1 = new TacoBell();
            ITrackable tb2 = new TacoBell();

            double distance1 = 0;

            // DONE - Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`


            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            // Create a new corA Coordinate with your locA's lat and long

            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            // Create a new Coordinate with your locB's lat and long

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.

            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                var corA = locA.Location;
                GeoCoordinate loc1 = new GeoCoordinate(corA.Latitude, corA.Longitude);
                for (int j = 0; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    var corB = locB.Location;
                    GeoCoordinate loc2 = new GeoCoordinate(corB.Latitude, corB.Longitude);
                    double distance = loc2.GetDistanceTo(loc1); 
                    
                    if (distance1 < distance)
                    {
                        distance1 = distance;
                        tb1 = locA;
                        tb2 = locB;
                    }
                }
            }

            logger.LogInfo($"{tb1.Name} and {tb2.Name}");

            Console.WriteLine($"The distance between {tb1.Name} and {tb2.Name} is {distance1}");
        }
    }
}
