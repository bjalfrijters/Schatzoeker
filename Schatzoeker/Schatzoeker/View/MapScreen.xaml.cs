﻿using Schatzoeker.Model;
using Schatzoeker.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
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
        private MapIcon _meIcon = new MapIcon();
        private DataHandler _dataHandler = null;
        private Waypoint treasurePoint;
        private MapHandler _mapHandler;
        private int score;
        private string playerName;
        private string message;

        public MapScreen()
        {
            this.InitializeComponent();

            GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
            _mapHandler = new MapHandler();
            if (_dataHandler == null)
            {
                _dataHandler = new DataHandler("Database", true);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _mapHandler.Geo = new Geolocator();
            _mapHandler.Geo.DesiredAccuracyInMeters = 50;
            _mapHandler.Geo.MovementThreshold = 10;
            _mapHandler.Geo.ReportInterval = 100;
            _mapHandler.Geo.PositionChanged += geo_PositionChanged;
            _meIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/me.png"));
            _meIcon.ZIndex = 2;
     
            GeofenceMonitor.Current.Geofences.Clear();

            Geoposition curPosition = await _mapHandler.Geo.GetGeopositionAsync();

            if(Frame.BackStack.Last().SourcePageType == typeof(MainPage)) {
                if (e.Parameter != null)
                {
                    var playerVar = e.Parameter;
                    playerName = (String)playerVar;
                    Debug.WriteLine(playerName + "name in mapscreen");
                    
                }
            }
            else if (this.Frame.BackStack.Last().SourcePageType == typeof(PuzzleScreen))
            {
            if (e.Parameter != null)
            {
                var messageVar = e.Parameter;
                message = (String)messageVar;
                char[] splitToken = { ':' };
                string[] messageSplit = message.Split(splitToken);
                playerName = messageSplit[0];
                string scoreString = messageSplit[1];
                score = Int32.Parse(scoreString);
                Debug.WriteLine(message + "message in mapscreen");

            }
            }

            message = playerName + ":" + score;


            AddTreasureToMapWithGeofence();

            MapControl1.Style = MapStyle.AerialWithRoads;
            MapControl1.MapElements.Add(_meIcon);

            await ShowRouteOnMap(curPosition.Coordinate.Point, treasurePoint.getLocation());
            await MapControl1.TrySetViewAsync(curPosition.Coordinate.Point, 18, 0, 0, MapAnimationKind.Default);         
            
        }

        private async void OnGeofenceStateChanged(GeofenceMonitor sender, object e)
        {
            Debug.WriteLine("I will go to another page");
            var reports = sender.ReadReports();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,  () =>
            {
                foreach (var report in reports)
                {
                    var state = report.NewState;

                    // this field can be used in case you need the id of geofence
                    var geofence = report.Geofence;
                    Debug.WriteLine("I'm about to go to the other page");
                    if (state == GeofenceState.Entered)
                    {
                        this.Frame.Navigate(typeof(PuzzleScreen), message);
                        Debug.WriteLine("I should go to the other page");
                    }
                    else if (state == GeofenceState.Exited)
                    {
                        //do nothing
                        Debug.WriteLine(GeofenceState.Exited.ToString());
                    }
                }
            });
        }

        private async void geo_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            var location = new Geopoint(new BasicGeoposition() { Latitude = args.Position.Coordinate.Point.Position.Latitude, Longitude = args.Position.Coordinate.Point.Position.Longitude });
            // Showing in the Map  
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                _meIcon.Location = location;
            });
            await MapControl1.TrySetViewAsync(location, 18, 0, 0, MapAnimationKind.None);
        }

        public void AddTreasureToMapWithGeofence()
        {
            MapIcon icon = new MapIcon();
            treasurePoint = _dataHandler.getRandomWaypoint();
            icon.Location = treasurePoint.getLocation();
            icon.NormalizedAnchorPoint = new Point(1.0, 2.0);
            icon.Title = "Schat";
            MapControl1.MapElements.Add(icon);

            string fenceKey = new string(treasurePoint.Id.ToString().ToCharArray());

            Geofence treasureFence = null;
            BasicGeoposition treasure = icon.Location.Position;
            Geocircle treasureCircle = new Geocircle(treasure, 30.0);

            bool singleUse = true;

            // want to listen for enter geofence, exit geofence and remove geofence events
            // you can select a subset of these event states
            MonitoredGeofenceStates mask = 0;

            mask |= MonitoredGeofenceStates.Entered;
            mask |= MonitoredGeofenceStates.Exited;
            mask |= MonitoredGeofenceStates.Removed;

            // setting up how long you need to be in geofence for enter event to fire
            TimeSpan dwellTime;

            dwellTime = new TimeSpan(10);

            treasureFence = new Geofence(fenceKey, treasureCircle, mask, singleUse, dwellTime);
            GeofenceMonitor.Current.Geofences.Add(treasureFence);
            
        }


        private async Task ShowRouteOnMap(Geopoint start, Geopoint end)
        {

            // Get the route between the points.
            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetWalkingRouteAsync(
                start,
                end);


            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.White;
                viewOfRoute.OutlineColor = Colors.White;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                MapControl1.Routes.Add(viewOfRoute);
                System.Diagnostics.Debug.WriteLine("MapScreen-ShowRouteOnMap: Added route to the map.");
                // Fit the MapControl to the route.
                await MapControl1.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, null, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
        }     
    }
}
