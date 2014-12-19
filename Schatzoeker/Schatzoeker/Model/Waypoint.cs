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
        private Geopoint location;
        private String discriptie;

        public Waypoint(String discriptie, Geopoint location)
        {
            this.discriptie = discriptie;
            this.location = location;
        }
        public Geopoint getLocation()
        {
            return location;
        }

        public String getDiscriptie()
        {
            return discriptie;
        }


    }
}
