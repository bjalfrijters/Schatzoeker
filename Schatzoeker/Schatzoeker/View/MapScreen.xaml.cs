using Schatzoeker.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Schatzoeker.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapScreen : Page
    {
        private MapHandler _mapHandler;

        public MapScreen()
        {
            this.InitializeComponent();
            _mapHandler = new MapHandler();

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UpdatePositionMapScreen();
         
            MapControl1.Style = MapStyle.AerialWithRoads;
            MapControl1.ZoomLevel = 14;
            MapControl1.LandmarksVisible = true;
            
        }

        private async void UpdatePositionMapScreen()
        {
           try
           {
               _mapHandler.CurrentPosition = await _mapHandler.Geo.GetGeopositionAsync();
               if (_mapHandler.CurrentPosition != null)
                   _mapHandler.setCurrentPoint(_mapHandler.CurrentPosition.Coordinate.Point);
               MapControl1.Center = _mapHandler.getCurrentPoint();
           }
           catch (Exception e)
           {
               Debug.WriteLine(e.ToString());
           }
        }

        private void setTreasurePosition(Geopoint treasurePoint)
        {
            treasurePoint = new Geopoint(new BasicGeoposition(){Longitude = 51.5884, Latitude = 4.7636});
            MapIcon treasureIcon = new MapIcon();
            treasureIcon.Location = treasurePoint;
            treasureIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            treasureIcon.Title = "Schat";
            MapControl1.MapElements.Add(treasureIcon);
        }
        
        
    }
}
