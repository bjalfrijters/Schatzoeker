using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Schatzoeker.Model
{
    class WaypointDataSource
    {
        private static ObservableCollection<Waypoint> _waypoints = new ObservableCollection<Waypoint>();

        public static ObservableCollection<Waypoint> GetWaypoints()
        {
            if (_waypoints.Count == 0)
            {
                _waypoints.Add(new Waypoint("Treasure", new Geopoint(new BasicGeoposition() { Latitude = 51.59380, Longitude = 4.77963 }));
            }
            return _waypoints;
        }
    }
}
