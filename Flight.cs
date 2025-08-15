using System;

namespace TravelAgencyApp
{
    public class Flight
    {
        public string Code { get; }
        public string Airline { get; }
        public string Origin { get; }
        public string Destination { get; }
        public string Day { get; }
        public string Time { get; }
        public int SeatsAvailable { get; private set; }
        public double Cost { get; }

        public Flight(string code, string airline, string origin, string destination, string day, string time, int seats, double cost)
        {
            Code = code;
            Airline = airline;
            Origin = origin;
            Destination = destination;
            Day = day;
            Time = time;
            SeatsAvailable = seats;
            Cost = cost;
        }

        public void BookSeat()
        {
            if (SeatsAvailable <= 0)
                throw new InvalidOperationException("No seats available.");
            SeatsAvailable--;
        }

        public override string ToString()
        {
            return $"{Code} | {Airline} | {Origin} -> {Destination} | {Day} {Time} | ${Cost} | Seats: {SeatsAvailable}";
        }
    }
}
