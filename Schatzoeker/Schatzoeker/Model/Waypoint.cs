using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Schatzoeker.Model
{

    class Waypoint
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column("description")]
        public string _description { get; set; }
        [Column("latitude")]
        public double latitude { get; set; }
        [Column("longitude")]
        public double longitude { get; set; }

        public Waypoint(String discriptie, Geopoint location)
        {
            _description = discriptie;
            latitude = location.Position.Latitude;
            longitude = location.Position.Longitude;

        }

        public Waypoint()
        {

        }

        public Geopoint getLocation()
        {
            return new Geopoint(new BasicGeoposition() { Latitude = latitude, Longitude = longitude });
        }


    }
}
