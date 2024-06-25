using ContractPilot.ViewModel;
using MapControl;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace ContractPilot.View;

public partial class MapView : UserControl, IComponentConnector, IStyleConnector
{
    internal Map map;

    internal MapGraticule graticule;

    public MapView()
    {
        InitializeComponent();
    }

    private void MapItemTouchDown(object sender, MouseButtonEventArgs e)
    {
        //IL_0002: Unknown result type (might be due to invalid IL or missing references)
        //IL_0008: Expected O, but got Unknown
        MapItem mapItem = (MapItem)sender;
        ((ListBoxItem)(object)mapItem).IsSelected = !((ListBoxItem)(object)mapItem).IsSelected;
        e.Handled = true;
    }

    private void MapMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            ((MapBase)map).TargetCenter = ((MapBase)map).ViewToLocation(e.GetPosition((IInputElement)map));
        }
    }

    private void MapMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount != 2)
        {
        }
    }

    private void MapMouseMove(object sender, MouseEventArgs e)
    {
        var map = (MapBase)sender;
        Location location = map.ViewToLocation(e.GetPosition(map));
        int latitude = (int)Math.Round(location.Latitude * 60000.0);
        int longitude = (int)Math.Round(Location.NormalizeLongitude(location.Longitude) * 60000.0);
        char latHemisphere = 'N';
        char lonHemisphere = 'E';
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
        MapViewModel vm = base.DataContext as MapViewModel;
        vm.MouseCoordinatesText = string.Format(CultureInfo.InvariantCulture, "{0}  {1:00} {2:00.000}\n{3} {4:000} {5:00.000}", latHemisphere, latitude / 60000, (double)(latitude % 60000) / 1000.0, lonHemisphere, longitude / 60000, (double)(longitude % 60000) / 1000.0);
    }

    private void MapMouseLeave(object sender, MouseEventArgs e)
    {
        MapViewModel vm = base.DataContext as MapViewModel;
        vm.MouseCoordinatesText = string.Empty;
    }

    private void MapManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
    {
        e.TranslationBehavior.DesiredDeceleration = 0.001;
    }

    private void SeamarksChecked(object sender, RoutedEventArgs e)
    {
        ((Panel)(object)map).Children.Insert(((Panel)(object)map).Children.IndexOf((UIElement)(object)graticule), ((MapViewModel)base.DataContext).MapLayers.SeamarksLayer);
    }

    private void SeamarksUnchecked(object sender, RoutedEventArgs e)
    {
        ((Panel)(object)map).Children.Remove(((MapViewModel)base.DataContext).MapLayers.SeamarksLayer);
    }
}
