using CPCommon.Data;
using CPCommon.Model;
using Lnm2CpLoader.Data;
using Lnm2CpLoader.Model;
using NetTopologySuite.Geometries;

namespace Lnm2CpLoader;
internal class Program
{
    private static void Main(string[] args)
    {
        LnmAirportContext lnmAirportDb = new();
        CPContext cpAirportDb = new();
        List<LnmAirportModel> airports = lnmAirportDb.Airports.ToList();
        foreach (LnmAirportModel airport in airports)
        {
            Point aPoint = new(new Coordinate(airport.lonx, airport.laty));
            Airport cpAirport = new()
            {
                Altitude = airport.altitude,
                Id = Guid.NewGuid(),
                LatY = airport.laty,
                LonX = airport.lonx,
                Location = aPoint,
                Ident = airport.ident,
                Name = airport.name,
                LongestRunwayLength = airport.longest_runway_length,
                City = airport.city
            };
            cpAirportDb.Add(cpAirport);
            List<LnmParkingModel> parkings = lnmAirportDb.Parking.Where((LnmParkingModel p) => p.airport_id == airport.airport_id).ToList();
            foreach (LnmParkingModel parking in parkings)
            {
                Point pPoint = new(new Coordinate(parking.lonx, parking.laty));
                Parking cpParking = new()
                {
                    Id = Guid.NewGuid(),
                    AirportId = cpAirport.Id,
                    LatY = parking.laty,
                    LonX = parking.lonx,
                    Heading = parking.heading,
                    Location = pPoint,
                    Name = parking.name,
                    Number = parking.number,
                    Radius = parking.radius,
                    Type = parking.type
                };
                cpAirportDb.Add(cpParking);
            }
        }
        cpAirportDb.SaveChanges();
        cpAirportDb.Dispose();
        lnmAirportDb.Dispose();
    }
}
