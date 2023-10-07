using MapControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using static MapControl.BingMapsTileLayer;

namespace ContractPilot.ViewModel;

public class MapLayers : INotifyPropertyChanged
{
    private readonly Dictionary<string, UIElement> mapLayers = new Dictionary<string, UIElement>
    {
        {
            "OpenStreetMap",
            (UIElement)new MapTileLayer
            {
                TileSource = new TileSource
                {
                    UriTemplate = "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                },
                SourceName = "OpenStreetMap",
                Description = "© [OpenStreetMap contributors](http://www.openstreetmap.org/copyright)"
            }
        },
        {
            "OpenStreetMap German",
            (UIElement)new MapTileLayer
            {
                TileSource = new TileSource
                {
                    UriTemplate = "https://{s}.tile.openstreetmap.de/{z}/{x}/{y}.png"
                },
                SourceName = "OpenStreetMap German",
                Description = "© [OpenStreetMap contributors](http://www.openstreetmap.org/copyright)"
            }
        },
        {
            "OpenStreetMap French",
            (UIElement)new MapTileLayer
            {
                TileSource = new TileSource
                {
                    UriTemplate = "https://{s}.tile.openstreetmap.fr/osmfr/{z}/{x}/{y}.png"
                },
                SourceName = "OpenStreetMap French",
                Description = "© [OpenStreetMap France](https://www.openstreetmap.fr/mentions-legales/) © [OpenStreetMap contributors](http://www.openstreetmap.org/copyright)"
            }
        },
        {
            "OpenTopoMap",
            (UIElement)new MapTileLayer
            {
                TileSource = new TileSource
                {
                    UriTemplate = "https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png"
                },
                SourceName = "OpenTopoMap",
                Description = "© [OpenTopoMap](https://opentopomap.org/) © [OpenStreetMap contributors](http://www.openstreetmap.org/copyright)",
                MaxZoomLevel = 17
            }
        },
        {
            "Seamarks",
            (UIElement)new MapTileLayer
            {
                TileSource = new TileSource
                {
                    UriTemplate = "http://tiles.openseamap.org/seamark/{z}/{x}/{y}.png"
                },
                SourceName = "OpenSeaMap",
                MinZoomLevel = 9,
                MaxZoomLevel = 18
            }
        },
        {
            "Bing Maps Road",
            (UIElement)new BingMapsTileLayer
            {
                Mode = (MapMode)0,
                SourceName = "Bing Maps Road",
                Description = "© [Microsoft](http://www.bing.com/maps/)"
            }
        },
        {
            "Bing Maps Aerial",
            (UIElement)new BingMapsTileLayer
            {
                Mode = (MapMode)1,
                SourceName = "Bing Maps Aerial",
                Description = "© [Microsoft](http://www.bing.com/maps/)",
                MapForeground = new SolidColorBrush(Colors.White),
                MapBackground = new SolidColorBrush(Colors.Black)
            }
        },
        {
            "Bing Maps Aerial with Labels",
            (UIElement)new BingMapsTileLayer
            {
                Mode = (MapMode)2,
                SourceName = "Bing Maps Hybrid",
                Description = "© [Microsoft](http://www.bing.com/maps/)",
                MapForeground = new SolidColorBrush(Colors.White),
                MapBackground = new SolidColorBrush(Colors.Black)
            }
        },
        {
            "TopPlusOpen WMTS",
            (UIElement)new WmtsTileLayer
            {
                SourceName = "TopPlusOpen",
                Description = "© [BKG](https://gdz.bkg.bund.de/index.php/default/webdienste/topplus-produkte/wmts-topplusopen-wmts-topplus-open.html)",
                CapabilitiesUri = new Uri("https://sgx.geodatenzentrum.de/wmts_topplus_open/1.0.0/WMTSCapabilities.xml")
            }
        },
        {
            "TopPlusOpen WMS",
            (UIElement)new WmsImageLayer
            {
                Description = "© [BKG](https://gdz.bkg.bund.de/index.php/default/webdienste/topplus-produkte/wms-topplusopen-mit-layer-fur-normalausgabe-und-druck-wms-topplus-open.html)",
                ServiceUri = new Uri("https://sgx.geodatenzentrum.de/wms_topplus_open")
            }
        },
        {
            "OpenStreetMap WMS",
            (UIElement)new WmsImageLayer
            {
                Description = "© [terrestris GmbH & Co. KG](http://ows.terrestris.de/) © [OpenStreetMap contributors](http://www.openstreetmap.org/copyright)",
                ServiceUri = new Uri("http://ows.terrestris.de/osm/service")
            }
        }
    };

    private string currentMapLayerName = "OpenStreetMap";

    public string CurrentMapLayerName
    {
        get
        {
            return currentMapLayerName;
        }
        set
        {
            currentMapLayerName = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMapLayerName"));
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMapLayer"));
        }
    }

    public UIElement CurrentMapLayer => mapLayers[currentMapLayerName];

    public UIElement SeamarksLayer => mapLayers["Seamarks"];

    public List<string> MapLayerNames { get; } = new List<string> { "OpenStreetMap", "OpenStreetMap German", "OpenStreetMap French", "OpenTopoMap", "TopPlusOpen WMTS", "TopPlusOpen WMS", "OpenStreetMap WMS", "SevenCs ChartServer WMS" };


    public event PropertyChangedEventHandler PropertyChanged;

    public MapLayers()
    {
        //IL_000c: Unknown result type (might be due to invalid IL or missing references)
        //IL_0011: Unknown result type (might be due to invalid IL or missing references)
        //IL_0012: Unknown result type (might be due to invalid IL or missing references)
        //IL_0017: Unknown result type (might be due to invalid IL or missing references)
        //IL_0028: Expected O, but got Unknown
        //IL_0029: Unknown result type (might be due to invalid IL or missing references)
        //IL_0035: Unknown result type (might be due to invalid IL or missing references)
        //IL_0046: Expected O, but got Unknown
        //IL_004d: Unknown result type (might be due to invalid IL or missing references)
        //IL_0052: Unknown result type (might be due to invalid IL or missing references)
        //IL_0053: Unknown result type (might be due to invalid IL or missing references)
        //IL_0058: Unknown result type (might be due to invalid IL or missing references)
        //IL_0069: Expected O, but got Unknown
        //IL_006a: Unknown result type (might be due to invalid IL or missing references)
        //IL_0076: Unknown result type (might be due to invalid IL or missing references)
        //IL_0087: Expected O, but got Unknown
        //IL_008e: Unknown result type (might be due to invalid IL or missing references)
        //IL_0093: Unknown result type (might be due to invalid IL or missing references)
        //IL_0094: Unknown result type (might be due to invalid IL or missing references)
        //IL_0099: Unknown result type (might be due to invalid IL or missing references)
        //IL_00aa: Expected O, but got Unknown
        //IL_00ab: Unknown result type (might be due to invalid IL or missing references)
        //IL_00b7: Unknown result type (might be due to invalid IL or missing references)
        //IL_00c8: Expected O, but got Unknown
        //IL_00cf: Unknown result type (might be due to invalid IL or missing references)
        //IL_00d4: Unknown result type (might be due to invalid IL or missing references)
        //IL_00d5: Unknown result type (might be due to invalid IL or missing references)
        //IL_00da: Unknown result type (might be due to invalid IL or missing references)
        //IL_00eb: Expected O, but got Unknown
        //IL_00ec: Unknown result type (might be due to invalid IL or missing references)
        //IL_00f8: Unknown result type (might be due to invalid IL or missing references)
        //IL_0104: Unknown result type (might be due to invalid IL or missing references)
        //IL_0112: Expected O, but got Unknown
        //IL_0119: Unknown result type (might be due to invalid IL or missing references)
        //IL_011e: Unknown result type (might be due to invalid IL or missing references)
        //IL_011f: Unknown result type (might be due to invalid IL or missing references)
        //IL_0124: Unknown result type (might be due to invalid IL or missing references)
        //IL_0135: Expected O, but got Unknown
        //IL_0136: Unknown result type (might be due to invalid IL or missing references)
        //IL_0142: Unknown result type (might be due to invalid IL or missing references)
        //IL_014b: Unknown result type (might be due to invalid IL or missing references)
        //IL_0159: Expected O, but got Unknown
        //IL_0160: Unknown result type (might be due to invalid IL or missing references)
        //IL_0165: Unknown result type (might be due to invalid IL or missing references)
        //IL_016d: Unknown result type (might be due to invalid IL or missing references)
        //IL_0179: Unknown result type (might be due to invalid IL or missing references)
        //IL_018a: Expected O, but got Unknown
        //IL_0191: Unknown result type (might be due to invalid IL or missing references)
        //IL_0196: Unknown result type (might be due to invalid IL or missing references)
        //IL_019e: Unknown result type (might be due to invalid IL or missing references)
        //IL_01aa: Unknown result type (might be due to invalid IL or missing references)
        //IL_01b6: Unknown result type (might be due to invalid IL or missing references)
        //IL_01c7: Unknown result type (might be due to invalid IL or missing references)
        //IL_01dd: Expected O, but got Unknown
        //IL_01e4: Unknown result type (might be due to invalid IL or missing references)
        //IL_01e9: Unknown result type (might be due to invalid IL or missing references)
        //IL_01f1: Unknown result type (might be due to invalid IL or missing references)
        //IL_01fd: Unknown result type (might be due to invalid IL or missing references)
        //IL_0209: Unknown result type (might be due to invalid IL or missing references)
        //IL_021a: Unknown result type (might be due to invalid IL or missing references)
        //IL_0230: Expected O, but got Unknown
        //IL_0237: Unknown result type (might be due to invalid IL or missing references)
        //IL_023c: Unknown result type (might be due to invalid IL or missing references)
        //IL_0248: Unknown result type (might be due to invalid IL or missing references)
        //IL_0254: Unknown result type (might be due to invalid IL or missing references)
        //IL_026a: Expected O, but got Unknown
        //IL_0271: Unknown result type (might be due to invalid IL or missing references)
        //IL_0276: Unknown result type (might be due to invalid IL or missing references)
        //IL_0282: Unknown result type (might be due to invalid IL or missing references)
        //IL_0298: Expected O, but got Unknown
        //IL_029f: Unknown result type (might be due to invalid IL or missing references)
        //IL_02a4: Unknown result type (might be due to invalid IL or missing references)
        //IL_02b0: Unknown result type (might be due to invalid IL or missing references)
        //IL_02c6: Expected O, but got Unknown
        if (!string.IsNullOrEmpty(BingMapsTileLayer.ApiKey))
        {
            MapLayerNames.Add("Bing Maps Road");
            MapLayerNames.Add("Bing Maps Aerial");
            MapLayerNames.Add("Bing Maps Aerial with Labels");
        }
    }
}
