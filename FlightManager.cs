using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TravelAgencyApp
{
    public class FlightManager
    {
        private List<Flight> flights = new List<Flight>();
        private Dictionary<string, string> airports = new Dictionary<string, string>();

        public FlightManager(string flightsFile, string airportsFile)
        {
            LoadAirports(airportsFile);
            LoadFlights(flightsFile);
        }

        private void LoadAirports(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Airports file not found: {filePath}");

            foreach (var line in File.ReadAllLines(filePath).Skip(1)) // Skip header
            {
                var parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    string code = parts[0].Trim();
                    string name = parts[1].Trim();
                    if (!airports.ContainsKey(code))
                        airports[code] = name;
                }
            }
        }

        private void LoadFlights(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Flights file not found: {filePath}");

            foreach (var line in File.ReadAllLines(filePath).Skip(1)) // Skip header
            {
                var parts = line.Split(',');
                if (parts.Length >= 7)
                {
                    try
                    {
                        string code = parts[0].Trim();
                        string airline = parts[1].Trim();
                        string originCode = parts[2].Trim();
                        string destCode = parts[3].Trim();
                        string day = parts[4].Trim();
                        string time = parts[5].Trim();
                        int seats = int.Parse(parts[6]);
                        double cost = double.Parse(parts[7], CultureInfo.InvariantCulture);

                        string originName = airports.ContainsKey(originCode) ? airports[originCode] : originCode;
                        string destName = airports.ContainsKey(destCode) ? airports[destCode] : destCode;

                        flights.Add(new Flight(code, airline, originName, destName, day, time, seats, cost));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing flight line: {line} | {ex.Message}");
                    }
                }
            }
        }

        public List<Flight> FindFlights(string origin, string destination, string day)
        {
            return flights
                .Where(f => f.Origin.Equals(origin, StringComparison.OrdinalIgnoreCase) &&
                            f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase) &&
                            f.Day.Equals(day, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Flight> GetAllFlights() => flights;
    }
}
