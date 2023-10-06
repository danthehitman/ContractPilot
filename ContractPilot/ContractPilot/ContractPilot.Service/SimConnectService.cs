using ContractPilot.Service.Enumeration;
using ContractPilot.Service.Model;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Threading;
using static Microsoft.FlightSimulator.SimConnect.SimConnect;

namespace ContractPilot.Service;

public class SimConnectService : INotifyPropertyChanged
{
    [CompilerGenerated]
    private static class _003C_003EO
    {
        public static RecvExceptionEventHandler _003C0_003E__Sim_OnRecvException;
    }

    private const string appName = "ContractPilot";

    private readonly SimRequestBuilder _requestBuilder;

    private const int WM_USER_SIMCONNECT = 1026;

    private readonly DispatcherTimer _pullTimer = new DispatcherTimer();

    private bool isReceivingDataFromSim;

    private PlaneState _PlaneState;

    private PlaneInfo _PlaneInfo;

    private PlaneLocation _PlaneLocation;

    private GpsInfo _GpsInfo;

    private FlightInfo _FlightInfo;

    private SimulationInfo _SimulationInfo;

    private Dictionary<int, double> _PayloadStationWeights = new Dictionary<int, double>();

    private SimConnect _SimConnect;

    public PlaneState PlaneState
    {
        get
        {
            return _PlaneState;
        }
        set
        {
            _PlaneState = value;
            NotifyPropertyChanged("PlaneState");
        }
    }

    public PlaneInfo PlaneInfo
    {
        get
        {
            return _PlaneInfo;
        }
        set
        {
            _PlaneInfo = value;
            NotifyPropertyChanged("PlaneInfo");
        }
    }

    public PlaneLocation PlaneLocation
    {
        get
        {
            return _PlaneLocation;
        }
        set
        {
            _PlaneLocation = value;
            NotifyPropertyChanged("PlaneLocation");
        }
    }

    public GpsInfo GpsInfo
    {
        get
        {
            return _GpsInfo;
        }
        set
        {
            _GpsInfo = value;
            NotifyPropertyChanged("GpsInfo");
        }
    }

    public FlightInfo FlightInfo
    {
        get
        {
            return _FlightInfo;
        }
        set
        {
            _FlightInfo = value;
            NotifyPropertyChanged("FlightInfo");
        }
    }

    public SimulationInfo SimulationInfo
    {
        get
        {
            return _SimulationInfo;
        }
        set
        {
            _SimulationInfo = value;
            NotifyPropertyChanged("SimulationInfo");
        }
    }

    public SimConnect SimConnect
    {
        get
        {
            return _SimConnect;
        }
        set
        {
            if (value != _SimConnect)
            {
                _SimConnect = value;
                NotifyPropertyChanged("SimConnect");
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void SetStationWeight(int station, double weight)
    {
        _PayloadStationWeights[station] = weight;
        NotifyPropertyChanged("_PayloadStationWeights");
    }

    public double? GetStationWeight(int station)
    {
        double value;
        return _PayloadStationWeights.TryGetValue(station, out value) ? new double?(value) : null;
    }

    public SimConnectService()
    {
        _requestBuilder = new SimRequestBuilder();
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Connect(nint winHandle)
    {
        //IL_0021: Unknown result type (might be due to invalid IL or missing references)
        //IL_002b: Expected O, but got Unknown
        //IL_0039: Unknown result type (might be due to invalid IL or missing references)
        //IL_0043: Expected O, but got Unknown
        //IL_0051: Unknown result type (might be due to invalid IL or missing references)
        //IL_005b: Expected O, but got Unknown
        //IL_0090: Unknown result type (might be due to invalid IL or missing references)
        //IL_009a: Expected O, but got Unknown
        //IL_00a8: Unknown result type (might be due to invalid IL or missing references)
        //IL_00b2: Expected O, but got Unknown
        //IL_00c0: Unknown result type (might be due to invalid IL or missing references)
        //IL_00ca: Expected O, but got Unknown
        //IL_0072: Unknown result type (might be due to invalid IL or missing references)
        //IL_0077: Unknown result type (might be due to invalid IL or missing references)
        //IL_007d: Expected O, but got Unknown
        if (SimConnect != null)
        {
            return;
        }
        try
        {
            SimConnect = new SimConnect("ContractPilot", (IntPtr)winHandle, 1026u, (WaitHandle)null, 0u);
            SimConnect.OnRecvOpen += new RecvOpenEventHandler(Sim_OnRecvOpen);
            SimConnect.OnRecvQuit += new RecvQuitEventHandler(Sim_OnRecvQuit);
            SimConnect simConnect = SimConnect;
            object obj = _003C_003EO._003C0_003E__Sim_OnRecvException;
            if (obj == null)
            {
                RecvExceptionEventHandler val = Sim_OnRecvException;
                _003C_003EO._003C0_003E__Sim_OnRecvException = val;
                obj = (object)val;
            }
            simConnect.OnRecvException += (RecvExceptionEventHandler)obj;
            SimConnect.OnRecvSimobjectDataBytype += new RecvSimobjectDataBytypeEventHandler(Sim_OnRecvSimobjectDataBytype);
            SimConnect.OnRecvSimobjectData += new RecvSimobjectDataEventHandler(Sim_OnRecvSimobjectData);
            SimConnect.OnRecvEvent += new RecvEventEventHandler(Sim_OnRecvEvent);
            SimConnect.SubscribeToSystemEvent((Enum)SimConnectEvent.SimStart, "SimStart");
            SimConnect.SubscribeToSystemEvent((Enum)SimConnectEvent.SimStop, "SimStop");
            SimConnect.SubscribeToSystemEvent((Enum)SimConnectEvent.Sim, "Sim");
            SimConnect.SubscribeToSystemEvent((Enum)SimConnectEvent.Crashed, "Crashed");
            SimConnect.SubscribeToSystemEvent((Enum)SimConnectEvent.PositionChanged, "PositionChanged");
            SimConnect.SubscribeToSystemEvent((Enum)SimConnectEvent.FlightLoaded, "FlightLoaded");
            SimConnect.SubscribeToSystemEvent((Enum)SimConnectEvent.FlightPlanActivated, "FlightPlanActivated");
            SimConnect.SubscribeToSystemEvent((Enum)SimConnectEvent.FlightPlanDeactivated, "FlightPlanDeactivated");
            RegisterDataDefinitions();
        }
        catch (COMException ex)
        {
            Console.WriteLine("SimConnect failed to connect: " + ex.Message);
            SimConnect = null;
        }
    }

    public void Disconnect()
    {
        if (SimConnect != null)
        {
            SimConnect.Dispose();
            SimConnect = null;
        }
    }

    private void RegisterDataDefinitions()
    {
        AddToDataDefinition<PlaneLocation>(DataDefinition.PlaneLocation);
        AddToDataDefinition<PlaneInfo>(DataDefinition.PlaneInfo);
        AddToDataDefinition<GpsInfo>(DataDefinition.GpsInfo);
        AddToDataDefinition<SimulationInfo>(DataDefinition.SimulationInfo);
        AddToDataDefinition<PlaneState>(DataDefinition.PlaneState);
        AddToDataDefinition<FlightInfo>(DataDefinition.FlightInfo);
    }

    private void AddToDataDefinition<T>(DataDefinition definition) where T : struct
    {
        //IL_0041: Unknown result type (might be due to invalid IL or missing references)
        foreach (SimRequest value in _requestBuilder.GetRequestsForStruct<T>())
        {
            SimConnect.AddToDataDefinition((Enum)definition, value.NameUnitTuple.Name, value.NameUnitTuple.Unit, value.DataType, 0f, SimConnect.SIMCONNECT_UNUSED);
        }
        SimConnect.RegisterDataDefineStruct<T>((Enum)definition);
    }

    private void AddToDataDefinition(string name, DataDefinition definition)
    {
        //IL_003b: Unknown result type (might be due to invalid IL or missing references)
        SimRequest request = _requestBuilder.GetRequest(name, definition);
        if (request != null)
        {
            SimConnect.AddToDataDefinition((Enum)definition, request.NameUnitTuple.Name, request.NameUnitTuple.Unit, request.DataType, 0f, SimConnect.SIMCONNECT_UNUSED);
        }
        SimConnect.RegisterDataDefineStruct<double>((Enum)definition);
    }

    public void StartStopReceivingData(bool receive)
    {
        //IL_0023: Unknown result type (might be due to invalid IL or missing references)
        //IL_003b: Unknown result type (might be due to invalid IL or missing references)
        //IL_005d: Unknown result type (might be due to invalid IL or missing references)
        if (receive != isReceivingDataFromSim)
        {
            isReceivingDataFromSim = receive;
            SIMCONNECT_PERIOD period = (SIMCONNECT_PERIOD)(receive ? 4 : 0);
            SimConnect.RequestDataOnSimObject((Enum)DataDefinition.PlaneLocation, (Enum)DataDefinition.PlaneLocation, SimConnect.SIMCONNECT_OBJECT_ID_USER, period, (SIMCONNECT_DATA_REQUEST_FLAG)1, 0u, 0u, 0u);
            SimConnect.RequestDataOnSimObject((Enum)DataDefinition.FlightInfo, (Enum)DataDefinition.FlightInfo, SimConnect.SIMCONNECT_OBJECT_ID_USER, period, (SIMCONNECT_DATA_REQUEST_FLAG)0, 0u, 0u, 0u);
            RequestPlaneInfo();
            Dictionary<int, double> planeWeights = new Dictionary<int, double>
            {
                { 1, 194.8 },
                { 5, 500.0 }
            };
            SendPlaneConfiguration(planeWeights);
        }
    }

    public void RequestPlaneState()
    {
        SimConnect.RequestDataOnSimObjectType((Enum)DataDefinition.PlaneState, (Enum)DataDefinition.PlaneState, 0u, (SIMCONNECT_SIMOBJECT_TYPE)0);
    }

    public void RequestPlaneInfo()
    {
        SimConnect.RequestDataOnSimObjectType((Enum)DataDefinition.PlaneInfo, (Enum)DataDefinition.PlaneInfo, 0u, (SIMCONNECT_SIMOBJECT_TYPE)0);
    }

    public void RequestSimulationInfo()
    {
        SimConnect.RequestDataOnSimObjectType((Enum)DataDefinition.SimulationInfo, (Enum)DataDefinition.SimulationInfo, 0u, (SIMCONNECT_SIMOBJECT_TYPE)0);
    }

    public void RequestGpsInfo()
    {
        SimConnect.RequestDataOnSimObjectType((Enum)DataDefinition.GpsInfo, (Enum)DataDefinition.GpsInfo, 0u, (SIMCONNECT_SIMOBJECT_TYPE)0);
    }

    public void SendPlaneConfiguration(Dictionary<int, double> stationWeightConfiguration = null)
    {
        PlaneState planeState = default(PlaneState);
        planeState.FUEL_TANK_LEFT_MAIN_QUANTITY = 12.5;
        PlaneState setStruct = planeState;
        SimConnect.SetDataOnSimObject((Enum)DataDefinition.PlaneState, SimConnect.SIMCONNECT_OBJECT_ID_USER, (SIMCONNECT_DATA_SET_FLAG)0, (object)setStruct);
        foreach (KeyValuePair<int, double> weight in stationWeightConfiguration)
        {
            DataDefinition def = (DataDefinition)(6 + weight.Key);
            AddToDataDefinition($"PAYLOAD_STATION_WEIGHT__{weight.Key}", def);
            SimConnect.SetDataOnSimObject((Enum)def, SimConnect.SIMCONNECT_OBJECT_ID_USER, (SIMCONNECT_DATA_SET_FLAG)0, (object)weight.Value);
            SimConnect.RequestDataOnSimObject((Enum)def, (Enum)def, SimConnect.SIMCONNECT_OBJECT_ID_USER, (SIMCONNECT_PERIOD)4, (SIMCONNECT_DATA_REQUEST_FLAG)0, 0u, 0u, 0u);
        }
    }

    private void UpdateSimDataStructs(SIMCONNECT_RECV_SIMOBJECT_DATA data)
    {
        object val = data.dwData.FirstOrDefault();
        if (val != null)
        {
            Console.WriteLine($"received data for id: {data.dwDefineID}");
            switch ((double)data.dwDefineID)
            {
                case 0L:
                    FlightInfo = (FlightInfo)val;
                    Console.WriteLine($"Altitude AG: {FlightInfo.PLANE_ALT_ABOVE_GROUND} Max G: {FlightInfo.MAX_G_FORCE}");
                    break;
                case 1L:
                    PlaneLocation = (PlaneLocation)val;
                    break;
                case 2L:
                    PlaneInfo = (PlaneInfo)val;
                    break;
                case 3L:
                    SimulationInfo = (SimulationInfo)val;
                    break;
                case 4L:
                    GpsInfo = (GpsInfo)val;
                    break;
                case 5L:
                    PlaneState = (PlaneState)val;
                    break;
                default:
                    SetStationWeight((int)data.dwDefineID, (double)val);
                    break;
            }
        }
    }

    private void Sim_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
    {
        Disconnect();
    }

    private static void Sim_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
    {
        //IL_0007: Unknown result type (might be due to invalid IL or missing references)
        SIMCONNECT_EXCEPTION eException = (SIMCONNECT_EXCEPTION)data.dwException;
        Console.WriteLine("SimConnect_OnRecvException: " + eException.ToString());
    }

    private void Sim_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
    {
    }

    private void Sim_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
    {
        UpdateSimDataStructs((SIMCONNECT_RECV_SIMOBJECT_DATA)(object)data);
    }

    private void Sim_OnRecvSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data)
    {
        UpdateSimDataStructs(data);
    }

    private void Sim_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT data)
    {
        switch ((SimConnectEvent)data.uEventID)
        {
            case SimConnectEvent.Sim:
                {
                    string text = ((data.dwData == 0) ? "stopped" : "started");
                    Console.WriteLine("Sim has " + text);
                    break;
                }
            case SimConnectEvent.SimStart:
                Console.WriteLine("SimStart fired");
                break;
            case SimConnectEvent.SimStop:
                Console.WriteLine("SimStop fired");
                break;
            case SimConnectEvent.PositionChanged:
                Console.WriteLine("Position changed!");
                break;
            case SimConnectEvent.FlightLoaded:
                Console.WriteLine("Flight loaded");
                break;
            case SimConnectEvent.FlightPlanActivated:
                Console.WriteLine("Flight plan activated");
                break;
            case SimConnectEvent.FlightPlanDeactivated:
                Console.WriteLine("Flight plan deactivated");
                break;
            case SimConnectEvent.Crashed:
                Console.WriteLine("Plane crashed");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ReceiveSimConnectMessage()
    {
        SimConnect simConnect = SimConnect;
        if (simConnect != null)
        {
            simConnect.ReceiveMessage();
        }
    }
}
