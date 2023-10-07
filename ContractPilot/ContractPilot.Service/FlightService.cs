using ContractPilot.Service.Model;
using CPCommon.Data;
using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ContractPilot.Service;

public class FlightService
{
    private SimConnectService _simConnectService;

    private CPContext _db;

    private List<PlaneLocation> _planePath = new List<PlaneLocation>();

    private List<Point> _BreadCrumbs = new List<Point>();

    private Airport _CurrentAirport = new Airport();

    private Parking _CurrentParking = new Parking();

    private double _MaxAltitude;

    private double _MaxG;

    public List<Point> BreadCrumbs
    {
        get
        {
            return _BreadCrumbs;
        }
        set
        {
            if (value != _BreadCrumbs)
            {
                _BreadCrumbs = value;
                NotifyPropertyChanged("BreadCrumbs");
            }
        }
    }

    public Airport CurrentAirport
    {
        get
        {
            return _CurrentAirport;
        }
        set
        {
            if (value != _CurrentAirport)
            {
                _CurrentAirport = value;
                NotifyPropertyChanged("CurrentAirport");
            }
        }
    }

    public Parking CurrentParking
    {
        get
        {
            return _CurrentParking;
        }
        set
        {
            if (value != _CurrentParking)
            {
                _CurrentParking = value;
                NotifyPropertyChanged("CurrentParking");
            }
        }
    }

    public double MaxAltitude
    {
        get
        {
            return _MaxAltitude;
        }
        set
        {
            _MaxAltitude = value;
        }
    }

    public double MaxG
    {
        get
        {
            return _MaxG;
        }
        set
        {
            _MaxG = value;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public FlightService(SimConnectService simConnectService, CPContext db)
    {
        //IL_0017: Unknown result type (might be due to invalid IL or missing references)
        //IL_0021: Expected O, but got Unknown
        //IL_0022: Unknown result type (might be due to invalid IL or missing references)
        //IL_002c: Expected O, but got Unknown
        _db = db;
        _simConnectService = simConnectService;
        _simConnectService.PropertyChanged += PlaneLocationPropertyChanged;
        _simConnectService.PropertyChanged += FlightInfoPropertyChanged;
    }

    protected void NotifyPropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public async Task StartFlightAsync()
    {
        _simConnectService.StartStopReceivingData(receive: true);
        await Task.Delay(1000);
        await LogAirportAndParking();
    }

    public async Task StopFlight()
    {
        _simConnectService.StartStopReceivingData(receive: false);
        await LogAirportAndParking();
    }

    public async Task DropPayloadAsync()
    {
        Point planeLocation = new Point(new Coordinate(_simConnectService.PlaneLocation.PLANE_LONGITUDE, _simConnectService.PlaneLocation.PLANE_LATITUDE));
        _simConnectService.SendPlaneConfiguration(new Dictionary<int, double> { { 5, 0.0 } });
        string droppedAtString = $"Dropped at X:{planeLocation.X} Y:{planeLocation.Y}";
        if (((Geometry)planeLocation).IsWithinDistance((Geometry)new Point(new Coordinate(-106.98118, 38.86977)), 0.011))
        {
            Console.WriteLine("You WIN! " + droppedAtString);
        }
        else
        {
            Console.WriteLine("You Lose! " + droppedAtString);
        }
    }

    private async Task LogAirportAndParking()
    {
        Point planeLocation = new Point(new Coordinate(_simConnectService.PlaneLocation.PLANE_LONGITUDE, _simConnectService.PlaneLocation.PLANE_LATITUDE));
        Airport startAirport = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<Airport>(((IQueryable<Airport>)_db.Airports).Where((Expression<Func<Airport, bool>>)((Airport p) => ((Geometry)p.Location).IsWithinDistance((Geometry)(object)planeLocation, 0.005))), default(CancellationToken));
        CurrentAirport = startAirport;
        CurrentParking = null;
        if (startAirport != null)
        {
            CurrentParking = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<Parking>(((IQueryable<Parking>)_db.Parking).Where((Expression<Func<Parking, bool>>)((Parking p) => p.AirportId == ((BaseModel)startAirport).Id)).Where((Expression<Func<Parking, bool>>)((Parking p) => ((Geometry)p.Location).IsWithinDistance((Geometry)(object)planeLocation, 0.0001))), default(CancellationToken));
        }

        Console.WriteLine(BuildLog());
    }

    private string BuildLog()
    {
        var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 4);
        defaultInterpolatedStringHandler.AppendLiteral("Plane: ");
        defaultInterpolatedStringHandler.AppendFormatted(_simConnectService.PlaneInfo.TITLE);
        defaultInterpolatedStringHandler.AppendLiteral(" Airport: ");
        Airport currentAirport = CurrentAirport;
        defaultInterpolatedStringHandler.AppendFormatted((currentAirport != null) ? currentAirport.Ident : null);
        defaultInterpolatedStringHandler.AppendLiteral(" Parking: ");
        Parking currentParking = CurrentParking;
        defaultInterpolatedStringHandler.AppendFormatted((currentParking != null) ? currentParking.Name : null);
        defaultInterpolatedStringHandler.AppendLiteral("-");
        Parking currentParking2 = CurrentParking;
        defaultInterpolatedStringHandler.AppendFormatted((currentParking2 != null) ? new int?(currentParking2.Number) : null);
        return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    private void PlaneLocationPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        //IL_00a0: Unknown result type (might be due to invalid IL or missing references)
        //IL_00a5: Unknown result type (might be due to invalid IL or missing references)
        //IL_00b2: Unknown result type (might be due to invalid IL or missing references)
        //IL_00c4: Expected O, but got Unknown
        //IL_00bf: Unknown result type (might be due to invalid IL or missing references)
        //IL_00c9: Expected O, but got Unknown
        if (e.PropertyName == "PlaneLocation")
        {
            PlaneLocation newLocation = _simConnectService.PlaneLocation;
            Console.WriteLine($"Plane Location: Lat: {newLocation.PLANE_LATITUDE} Lon: {newLocation.PLANE_LONGITUDE} Alt: {newLocation.PLANE_ALTITUDE}");
            _planePath.Add(newLocation);
            BreadCrumbs.Add(new Point(new Coordinate
            {
                X = newLocation.PLANE_LONGITUDE,
                Y = newLocation.PLANE_LATITUDE
            }));
        }
    }

    private void FlightInfoPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "FlightInfo")
        {
            double currentAlt = _simConnectService.FlightInfo.PLANE_ALT_ABOVE_GROUND;
            if (currentAlt > MaxAltitude)
            {
                MaxAltitude = currentAlt;
            }
            if (MaxAltitude > 1100.0)
            {
                Console.WriteLine($"You lose.  Alt is: {MaxAltitude}");
            }
            double currentG = _simConnectService.FlightInfo.MAX_G_FORCE;
            if (currentG > MaxG)
            {
                MaxG = currentG;
            }
            if (MaxG > 2.0)
            {
                Console.WriteLine($"You lose.  MaxG is: {MaxG}");
            }
        }
    }
}
