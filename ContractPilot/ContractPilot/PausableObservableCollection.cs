using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ContractPilot;

public class PausableObservableCollection<T> : ObservableCollection<T>
{
	private bool _notificationSupressed;

	private bool _supressNotification;

	public bool SupressNotification
	{
		get
		{
			return _supressNotification;
		}
		set
		{
			_supressNotification = value;
			if (!_supressNotification && _notificationSupressed)
			{
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
				_notificationSupressed = false;
			}
		}
	}

	protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
	{
		if (SupressNotification)
		{
			_notificationSupressed = true;
		}
		else
		{
			base.OnCollectionChanged(e);
		}
	}
}
