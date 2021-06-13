using CPCommon.Model;
using CPCommon.Data;
using NetTopologySuite.Geometries;
using System;
using System.Linq;

namespace CPCommon
{
    class Program
    {
        static void Main(string[] args)
        {
            var lnmAirportDb = new LnmAirportContext();
            var cpAirportDb = new CPContext();

            var airports = lnmAirportDb.Airports.ToList();

            foreach (var airport in airports)
            {
                var point = new Point(new Coordinate(airport.lonx, airport.laty));
                var cpAirport = new Airport()
                {
                    Altitude = airport.altitude,
                    Id = Guid.NewGuid(),
                    LatY = airport.laty,
                    LonX = airport.lonx,
                    Location = point,
                    Ident = airport.ident,
                    Name = airport.name,
                    LongestRunwayLength = airport.longest_runway_length,
                    City = airport.city,
                };
                cpAirportDb.Add(cpAirport);
            }
            cpAirportDb.SaveChanges();
            cpAirportDb.Dispose();
            lnmAirportDb.Dispose();
        }
    }
}
