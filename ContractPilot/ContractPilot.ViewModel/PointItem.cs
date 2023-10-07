using MapControl;

namespace ContractPilot.ViewModel;

public class PointItem : BaseViewModel
{
	private string name;

	private Location location;

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
			NotifyPropertyChanged("Name");
		}
	}

	public Location Location
	{
		get
		{
			return location;
		}
		set
		{
			location = value;
			NotifyPropertyChanged("Location");
		}
	}
}
