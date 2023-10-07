using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContractPilot.Command;
using CPCommon.Model;

namespace ContractPilot.ViewModel;

public class AirplaneMakeModelInfoEditorViewModel : BaseViewModel
{
	private AirplaneMakeModelInfo _makeModelInfoModel = new AirplaneMakeModelInfo();

	public IAsyncCommand<object> SaveCommand { get; set; }

	public Guid Id { get; set; }

	public string Name
	{
		get
		{
			return _makeModelInfoModel.Name;
		}
		set
		{
			if (value != _makeModelInfoModel.Name)
			{
				_makeModelInfoModel.Name = value;
				NotifyPropertyChanged("Name");
			}
		}
	}

	public int Range
	{
		get
		{
			return _makeModelInfoModel.Range;
		}
		set
		{
			if (value != _makeModelInfoModel.Range)
			{
				_makeModelInfoModel.Range = value;
				NotifyPropertyChanged("Range");
			}
		}
	}

	public int NumberOfEngines
	{
		get
		{
			return _makeModelInfoModel.NumberOfEngines;
		}
		set
		{
			if (value != _makeModelInfoModel.NumberOfEngines)
			{
				_makeModelInfoModel.NumberOfEngines = value;
				NotifyPropertyChanged("NumberOfEngines");
			}
		}
	}

	public double CapacityPounds
	{
		get
		{
			return _makeModelInfoModel.CapacityPounds;
		}
		set
		{
			if (value != _makeModelInfoModel.CapacityPounds)
			{
				_makeModelInfoModel.CapacityPounds = value;
				NotifyPropertyChanged("CapacityPounds");
			}
		}
	}

	public EngineType EngineType
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return _makeModelInfoModel.EngineType;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			if (value != _makeModelInfoModel.EngineType)
			{
				_makeModelInfoModel.EngineType = value;
				NotifyPropertyChanged("EngineType");
			}
		}
	}

	public ICollection<PayloadStation> PayloadStations
	{
		get
		{
			return _makeModelInfoModel.PayloadStations;
		}
		set
		{
			if (value != _makeModelInfoModel.PayloadStations)
			{
				_makeModelInfoModel.PayloadStations = value;
				NotifyPropertyChanged("PayloadStations");
			}
		}
	}

	public ICollection<FuelTank> FuelTanks
	{
		get
		{
			return _makeModelInfoModel.FuelTanks;
		}
		set
		{
			if (value != _makeModelInfoModel.FuelTanks)
			{
				_makeModelInfoModel.FuelTanks = value;
				NotifyPropertyChanged("FuelTanks");
			}
		}
	}

	public AirplaneMakeModelInfoEditorViewModel()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		_makeModelInfoModel.Name = "test1";
		_makeModelInfoModel.EngineType = (EngineType)0;
		SaveCommand = new AsyncCommand<object>(SaveAsync);
	}

	private async Task SaveAsync(object param)
	{
		_makeModelInfoModel = new AirplaneMakeModelInfo
		{
			Name = "NEWNAME"
		};
		NotifyPropertyChanged("Name");
	}
}
