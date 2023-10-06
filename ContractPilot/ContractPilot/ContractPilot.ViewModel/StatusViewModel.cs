using System.Threading.Tasks;
using ContractPilot.Command;
using ContractPilot.Service;

namespace ContractPilot.ViewModel;

public class StatusViewModel : BaseViewModel
{
	private FlightService _flightService;

	private bool _flightRunning = false;

	public string StartStopLabel => _flightRunning ? "Stop Flight" : "Start Flight";

	public IAsyncCommand<object> StartStopFlightCommand { get; set; }

	public IAsyncCommand<object> DropPayloadCommand { get; set; }

	public string PlaneName { get; set; } = "";


	public StatusViewModel(FlightService flightService)
	{
		_flightService = flightService;
		StartStopFlightCommand = new AsyncCommand<object>(StartStopFlightAsync);
		DropPayloadCommand = new AsyncCommand<object>(DropPayloadAsync);
	}

	private async Task StartStopFlightAsync(object param)
	{
		_flightRunning = !_flightRunning;
		NotifyPropertyChanged("StartStopLabel");
		if (_flightRunning)
		{
			await _flightService.StartFlightAsync();
		}
		else
		{
			await _flightService.StopFlight();
		}
	}

	private async Task DropPayloadAsync(object param)
	{
		await _flightService.DropPayloadAsync();
	}
}
