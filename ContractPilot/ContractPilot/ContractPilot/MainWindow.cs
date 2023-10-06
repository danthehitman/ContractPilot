using ContractPilot.Service;
using ContractPilot.ViewModel;
using CPCommon.Data;
using MapControl;
using MapControl.Caching;
using System;
using System.IO;
using System.Runtime.Caching;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Threading;

namespace ContractPilot;

public partial class MainWindow : Window, IComponentConnector
{
    private const int WM_USER_SIMCONNECT = 1026;

    private SimConnectService _simConnectService;

    private FlightService _flightService;

    private CPContext _db;

    static MainWindow()
    {
        //IL_0032: Unknown result type (might be due to invalid IL or missing references)
        //IL_003c: Expected O, but got Unknown
        ConsoleAllocator.ShowConsoleWindow();
        Console.WriteLine("TEST++++++++++++++++++++++++++++");
        try
        {
            ImageLoader.HttpClient.DefaultRequestHeaders.Add("User-Agent", "XAML Map Control Test Application");
            TileImageLoader.Cache = (ObjectCache)new ImageFileCache(TileImageLoader.DefaultCacheFolder);
            BingMapsTileLayer.ApiKey = File.ReadAllText("..\\..\\..\\BingMapsApiKey.txt")?.Trim();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public MainWindow()
    {
        //IL_001a: Unknown result type (might be due to invalid IL or missing references)
        //IL_0024: Expected O, but got Unknown
        _simConnectService = new SimConnectService();
        _db = new CPContext();
        _flightService = new FlightService(_simConnectService, _db);
        InitializeComponent();
        base.DataContext = new MainWindowViewModel(_simConnectService, _flightService, _db);
        ObjectCache cache2 = TileImageLoader.Cache;
        ImageFileCache cache = (ImageFileCache)(object)((cache2 is ImageFileCache) ? cache2 : null);
        if (cache != null)
        {
            base.Loaded += async delegate
            {
                await Task.Delay(2000);
                await cache.Clean();
            };
        }
        DispatcherTimer timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1.0)
        };
        timer.Start();
    }

    protected HwndSource GetHWinSource()
    {
        return PresentationSource.FromVisual(this) as HwndSource;
    }

    private nint WndProc(nint hWnd, int iMsg, nint hWParam, nint hLParam, ref bool bHandled)
    {
        try
        {
            if (iMsg == 1026)
            {
                _simConnectService.ReceiveSimConnectMessage();
            }
        }
        catch (COMException ex)
        {
            Console.WriteLine("Exception in WndProc: " + ex.Message);
            _simConnectService.Disconnect();
        }
        return IntPtr.Zero;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        HwndSource windowsSource = GetHWinSource();
        windowsSource.AddHook(WndProc);
        _simConnectService.Connect(windowsSource.Handle);
    }
}
