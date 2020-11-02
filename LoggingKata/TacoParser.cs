using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();

        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            // If your array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                // Log that and return null
                logger.LogWarning("The length is less than three.");

                // Do not fail if one record parsing fails, return null
                return null; // TODO Implement
            }

            // grab the latitude from your array at index 0
            // grab the longitude from your array at index 1
            // grab the name from your array at index 2

            // You're going to need to parse your string as a `double`
            // which is similar to parsing a string as an `int`

            double latitude = 0;
            double longitude = 0;
            var testLat = double.TryParse(cells[0], out latitude);
            var testLong = double.TryParse(cells[1], out longitude);
            var name = cells[2];

            // DONE - You'll need to create a TacoBell class
            //        that conforms to ITrackable

            // Then, you'll need an instance of the TacoBell class
            // With the name and point set correctly

            TacoBell tacobell = new TacoBell();

            tacobell.Name = name;
            Point num1 = new Point();
            num1.Longitude = longitude;
            num1.Latitude = latitude;
           

            // Then, return the instance of your TacoBell class
            // Since it conforms to ITrackable

            if ((testLat == false) || (testLong == false))
            {
                logger.LogError("Could Not Parse.", null);
            }

            tacobell.Location = num1;

            return tacobell;
        }
    }
}