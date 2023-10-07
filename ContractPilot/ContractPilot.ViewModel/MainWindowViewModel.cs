using System.ComponentModel;
using System.Windows.Input;
using ContractPilot.Command;
using ContractPilot.Service;
using CPCommon.Data;

namespace ContractPilot.ViewModel;

public class MainWindowViewModel : BaseViewModel
{
	private SimConnectService _simConnectService;

	private FlightService _flightService;

	private CPContext _db;

	private bool _SimConnected;

	private BaseViewModel _activeViewModel;

	public bool SimConnected
	{
		get
		{
			return _SimConnected;
		}
		set
		{
			if (value != _SimConnected)
			{
				_SimConnected = value;
				NotifyPropertyChanged("SimConnected");
			}
		}
	}

	public BaseViewModel ActiveViewModel
	{
		get
		{
			return _activeViewModel;
		}
		set
		{
			_activeViewModel = value;
			NotifyPropertyChanged("ActiveViewModel");
		}
	}

	public MapViewModel MapViewModel { get; set; }

	public StatusViewModel StatusViewModel { get; set; }

	public AirplaneMakeModelInfoEditorViewModel AirplaneMakeModelInfoEditorViewModel { get; set; }

	public ICommand UpdateViewCommand { get; set; }

	public MainWindowViewModel(SimConnectService simConnectService, FlightService flightService, CPContext db)
	{
		_simConnectService = simConnectService;
		_flightService = flightService;
		_db = db;
		simConnectService.PropertyChanged += SimConnectService_PropertyChanged;
		UpdateViewCommand = new UpdateViewCommand(this);
		MapViewModel = new MapViewModel(_simConnectService);
		StatusViewModel = new StatusViewModel(_flightService);
		AirplaneMakeModelInfoEditorViewModel = new AirplaneMakeModelInfoEditorViewModel();
		ActiveViewModel = MapViewModel;
	}

	private void SimConnectService_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "SimConnect")
		{
			SimConnected = _simConnectService.SimConnect != null;
		}
	}
}
