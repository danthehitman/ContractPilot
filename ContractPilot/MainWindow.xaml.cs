using CPCommon.Data;
using MapControl;
using MapControl.Caching;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using ViewModel;

namespace ContractPilot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum DUMMYENUM
        {
            Dummy = 0
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct Struct1
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String title;
        };

        private SimConnect simconnect;

        const int WM_USER_SIMCONNECT = 0x0402;

        /// <summary>
        /// Contains the list of all the SimConnect properties we will read, the unit is separated by coma by our own code.
        /// </summary>
        private readonly Dictionary<int, string> simConnectProperties = new()
        {
            {1,"PLANE LONGITUDE,degree" },
            {2,"PLANE LATITUDE,degree" },
            {3,"PLANE HEADING DEGREES MAGNETIC,degree" },
            {4,"PLANE ALTITUDE,feet" },
            {5,"AIRSPEED INDICATED,knots" },
            {6,"TITLE,string"},
        };

        static MainWindow()
        {
            try
            {
                ImageLoader.HttpClient.DefaultRequestHeaders.Add("User-Agent", "XAML Map Control Test Application");

                TileImageLoader.Cache = new ImageFileCache(TileImageLoader.DefaultCacheFolder);
                //TileImageLoader.Cache = new FileDbCache(TileImageLoader.DefaultCacheFolder);
                //TileImageLoader.Cache = new SQLiteCache(TileImageLoader.DefaultCacheFolder);
                //TileImageLoader.Cache = null;

                BingMapsTileLayer.ApiKey = File.ReadAllText(@"..\..\..\BingMapsApiKey.txt")?.Trim();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            if (TileImageLoader.Cache is ImageFileCache cache)
            {
                Loaded += async (s, e) =>
                {
                    await Task.Delay(2000);
                    await cache.Clean();
                };
            }

            // Starts our connection and poller
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick; ;
            timer.Start();

            using (var db = new CPContext())
            {
                var airports = db.Airports.ToList();
                foreach(var airport in airports)
                {
                    ((MapViewModel)DataContext).AddPoint(airport.Ident, airport.Location.Y, airport.Location.X);
                }
            }

            //using (var db = new AirportContext())
            //{
            //    // Note: This sample requires the database to be created before running.

            //    // Read
            //    Console.WriteLine("Querying for a blog");
            //    var airports = db.Airports.ToList();

            //    foreach (var airport in airports)
            //    {
            //        ((MapViewModel)DataContext).AddPoint(airport.ident, airport.laty, airport.lonx);
            //    }
            //}
        }

        /// <summary>
        /// We received a disconnection from SimConnect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void Sim_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            lblStatus.Content = "Disconnected";
        }

        /// <summary>
        /// We received a connection from SimConnect.
        /// Let's register all the properties we need.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void Sim_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            lblStatus.Content = "Connected";

            simconnect.AddToDataDefinition((DUMMYENUM)6, "Title", null, SIMCONNECT_DATATYPE.STRING256, 0, SimConnect.SIMCONNECT_UNUSED);

            /// IMPORTANT: Register it with the simconnect managed wrapper marshaller
            /// If you skip this step, you will only receive a uint in the .dwData field.
            simconnect.RegisterDataDefineStruct<Struct1>((DUMMYENUM)6);

            //foreach (var toConnect in simConnectProperties)
            //{
            //    string[] values = toConnect.Value.Split(new char[] { ',' });
            //    /// Define a data structure
            //    simconnect.AddToDataDefinition((DUMMYENUM)toConnect.Key, values[0], values[1], SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            //    //if (toConnect.Key == 6)
            //    //    lblPlaneType.Content = values[1];

            //    //GetLabelForUid(100 + toConnect.Key).Content = values[1];
            //}
        }

        /// <summary>
        /// Returns a label based on a uid number
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        private Label GetLabelForUid(int uid)
        {
            return (Label)mainGrid.Children
                 .Cast<UIElement>()
                 .First(row => row.Uid == uid.ToString());
        }

        private void Connect()
        {
            try
            {
                simconnect = new SimConnect(this.Title, GetHWinSource().Handle, WM_USER_SIMCONNECT, null, 0);
                simconnect.OnRecvOpen += Sim_OnRecvOpen;
                simconnect.OnRecvQuit += Sim_OnRecvQuit;
                simconnect.OnRecvSimobjectDataBytype += Sim_OnRecvSimobjectDataBytype;
                simconnect.OnRecvEvent += Sim_OnRecvEvent;
                simconnect.SubscribeToSystemEvent((DUMMYENUM)100, "SimStart");
            }
            catch (COMException ex)
            {
                simconnect = null;
            }
        }

        /// <summary>
        /// Let's disconnect from SimConnect
        /// </summary>
        public void Disconnect()
        {
            if (simconnect != null)
            {
                simconnect.Dispose();
                simconnect = null;
                lblStatus.Content = "Disconnected";
            }
        }

        /// <summary>
        /// Received data from SimConnect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void Sim_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            int iRequest = (int)data.dwRequestID;
            Struct1 dValue = (Struct1)data.dwData[0];

            if (iRequest == 6)
                lblPlaneType.Content = dValue.title;
            //GetLabelForUid(iRequest).Content = dValue.ToString();
        }

        private void Sim_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT data)
        {
            Timer_Tick(new object(), new EventArgs());
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (simconnect == null) // We are not connected, let's try to connect
                Connect();
            else // We are connected, let's try to grab the data from the Sim
            {
                try
                {
                    simconnect.RequestDataOnSimObjectType((DUMMYENUM)6, (DUMMYENUM)6, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                }
                catch (COMException ex)
                {
                    Disconnect();
                }
            }
        }

        /// <summary>
        ///  Direct reference to the window pointer
        /// </summary>
        /// <returns></returns>
        protected HwndSource GetHWinSource()
        {
            return PresentationSource.FromVisual(this) as HwndSource;
        }

        /// <summary>
        /// Handles Windows events directly, for example to grab the SimConnect connection
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="iMsg"></param>
        /// <param name="hWParam"></param>
        /// <param name="hLParam"></param>
        /// <param name="bHandled"></param>
        /// <returns></returns>
        private IntPtr WndProc(IntPtr hWnd, int iMsg, IntPtr hWParam, IntPtr hLParam, ref bool bHandled)
        {
            try
            {
                if (iMsg == WM_USER_SIMCONNECT)
                    ReceiveSimConnectMessage();
            }
            catch (COMException ex)
            {
                Disconnect();
            }

            return IntPtr.Zero;
        }

        public void ReceiveSimConnectMessage()
        {
            simconnect?.ReceiveMessage();
        }

        /// <summary>
        /// Once the window is loaded, let's hook to the WinProc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource windowsSource = GetHWinSource();
            windowsSource.AddHook(WndProc);

            Connect();
        }

        #region MapControlHandlers


        private void MapItemTouchDown(object sender, TouchEventArgs e)
        {
            var mapItem = (MapItem)sender;
            mapItem.IsSelected = !mapItem.IsSelected;
            e.Handled = true;

        }

        private void MapMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //map.ZoomMap(e.GetPosition(map), Math.Floor(map.ZoomLevel + 1.5));
                //map.ZoomToBounds(new BoundingBox(53, 7, 54, 9));
                map.TargetCenter = map.ViewToLocation(e.GetPosition(map));
            }
        }

        private void MapMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //map.ZoomMap(e.GetPosition(map), Math.Ceiling(map.ZoomLevel - 1.5));
            }
        }

        private void MapMouseMove(object sender, MouseEventArgs e)
        {
            var location = map.ViewToLocation(e.GetPosition(map));
            var latitude = (int)Math.Round(location.Latitude * 60000d);
            var longitude = (int)Math.Round(MapControl.Location.NormalizeLongitude(location.Longitude) * 60000d);
            var latHemisphere = 'N';
            var lonHemisphere = 'E';

            if (latitude < 0)
            {
                latitude = -latitude;
                latHemisphere = 'S';
            }

            if (longitude < 0)
            {
                longitude = -longitude;
                lonHemisphere = 'W';
            }

            mouseLocation.Text = string.Format(CultureInfo.InvariantCulture,
                "{0}  {1:00} {2:00.000}\n{3} {4:000} {5:00.000}",
                latHemisphere, latitude / 60000, (latitude % 60000) / 1000d,
                lonHemisphere, longitude / 60000, (longitude % 60000) / 1000d);
        }

        private void MapMouseLeave(object sender, MouseEventArgs e)
        {
            mouseLocation.Text = string.Empty;
        }

        private void MapManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior.DesiredDeceleration = 0.001;
        }

        private void SeamarksChecked(object sender, RoutedEventArgs e)
        {
            map.Children.Insert(map.Children.IndexOf(graticule), ((MapViewModel)DataContext).MapLayers.SeamarksLayer);
        }

        private void SeamarksUnchecked(object sender, RoutedEventArgs e)
        {
            map.Children.Remove(((MapViewModel)DataContext).MapLayers.SeamarksLayer);
        }
        #endregion
    }
}
