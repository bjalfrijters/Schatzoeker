using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Schatzoeker.ViewModel
{
    class MapHandler
    {
        public Geolocator Geo;
        public Geopoint CurrentPoint;
        public Geoposition CurrentPosition;

        public MapHandler()
        {
            Geo = new Geolocator();
        }


        public Geopoint getCurrentPoint()
        {
            return CurrentPoint;
        }

        public async Task getCurrentPosition()
        {
            CurrentPosition = await Geo.GetGeopositionAsync();
            CurrentPoint = CurrentPosition.Coordinate.Point;
        }

    }
}
