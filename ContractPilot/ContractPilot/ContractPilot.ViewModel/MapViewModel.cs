using System.Collections.ObjectModel;
using System.ComponentModel;
using ContractPilot.Command;
using ContractPilot.Service;
using ContractPilot.Service.Model;
using MapControl;

namespace ContractPilot.ViewModel;

public class MapViewModel : BaseViewModel
{
	private string _MouseCoordinatesText;

	private Location mapCenter = new Location(53.5, 8.2);

	private PausableObservableCollection<PointItem> _Points = new PausableObservableCollection<PointItem>();

	private ObservableCollection<PointItem> _BreadCrumbs = new ObservableCollection<PointItem>();

	private SimConnectService _simConnectService;

	public string MouseCoordinatesText
	{
		get
		{
			return _MouseCoordinatesText;
		}
		set
		{
			if (value != _MouseCoordinatesText)
			{
				_MouseCoordinatesText = value;
				NotifyPropertyChanged("MouseCoordinatesText");
			}
		}
	}

	public Location MapCenter
	{
		get
		{
			return mapCenter;
		}
		set
		{
			mapCenter = value;
			NotifyPropertyChanged("MapCenter");
		}
	}

	public PausableObservableCollection<PointItem> Points
	{
		get
		{
			return _Points;
		}
		set
		{
			if (value != _Points)
			{
				_Points = value;
				NotifyPropertyChanged("Points");
			}
		}
	}

	public ObservableCollection<PointItem> BreadCrumbs
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

	public IAsyncCommand<object> MapItemTouchDownCommand { get; set; }

	public ObservableCollection<PointItem> Pushpins { get; } = new ObservableCollection<PointItem>();


	public ObservableCollection<Polyline> Polylines { get; } = new ObservableCollection<Polyline>();


	public MapLayers MapLayers { get; } = new MapLayers();


	public void AddPoint(string name, double lat, double lon)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		Points.Add(new PointItem
		{
			Name = name,
			Location = new Location(lat, lon)
		});
	}

	public MapViewModel(SimConnectService simConnectService)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Expected O, but got Unknown
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Expected O, but got Unknown
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Expected O, but got Unknown
		_simConnectService = simConnectService;
		_simConnectService.PropertyChanged += PlaneLocationPropertyChanged;
		Points.Add(new PointItem
		{
			Name = "Steinbake Leitdamm",
			Location = new Location(53.51217, 8.16603)
		});
		Points.Add(new PointItem
		{
			Name = "Buhne 2",
			Location = new Location(53.50926, 8.15815)
		});
		Points.Add(new PointItem
		{
			Name = "Buhne 4",
			Location = new Location(53.50468, 8.15343)
		});
		Points.Add(new PointItem
		{
			Name = "Buhne 6",
			Location = new Location(53.50092, 8.15267)
		});
		Points.Add(new PointItem
		{
			Name = "Buhne 8",
			Location = new Location(53.49871, 8.15321)
		});
		Points.Add(new PointItem
		{
			Name = "Buhne 10",
			Location = new Location(53.4935, 8.15563)
		});
		Pushpins.Add(new PointItem
		{
			Name = "WHV - Eckwarderhörne",
			Location = new Location(53.5495, 8.1877)
		});
		Pushpins.Add(new PointItem
		{
			Name = "JadeWeserPort",
			Location = new Location(53.5914, 8.14)
		});
		Pushpins.Add(new PointItem
		{
			Name = "Kurhaus Dangast",
			Location = new Location(53.447, 8.1114)
		});
		Pushpins.Add(new PointItem
		{
			Name = "Eckwarderhörne",
			Location = new Location(53.5207, 8.2323)
		});
		Polylines.Add(new Polyline
		{
			Locations = LocationCollection.Parse("53.5140,8.1451 53.5123,8.1506 53.5156,8.1623 53.5276,8.1757 53.5491,8.1852 53.5495,8.1877 53.5426,8.1993 53.5184,8.2219 53.5182,8.2386 53.5195,8.2387")
		});
		Polylines.Add(new Polyline
		{
			Locations = LocationCollection.Parse("53.5978,8.1212 53.6018,8.1494 53.5859,8.1554 53.5852,8.1531 53.5841,8.1539 53.5802,8.1392 53.5826,8.1309 53.5867,8.1317 53.5978,8.1212")
		});
	}

	private void PlaneLocationPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		if (e.PropertyName == "PlaneLocation")
		{
			PlaneLocation newLocation = _simConnectService.PlaneLocation;
			BreadCrumbs.Add(new PointItem
			{
				Location = new Location(newLocation.PLANE_LATITUDE, newLocation.PLANE_LONGITUDE)
			});
		}
	}
}
