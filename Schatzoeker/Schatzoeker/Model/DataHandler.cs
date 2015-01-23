using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Windows.Storage;
using SQLite;
using Windows.Devices.Geolocation;

namespace Schatzoeker.Model
{
    class DataHandler
    {
        private String dbConnection;

        /// <summary>
        /// constructor naar een bestaande database.
        /// </summary>
        /// <param name="inputFile">
        /// de naam van de database file met een extensie.
        /// </param>      
        public DataHandler(string inputFile)
        {
            dbConnection = String.Format("Data Source={0}", inputFile);
        }

        /// <summary>
        /// constructor naar een bestaande database.
        /// </summary>
        /// <param name="inputFile">
        /// de naam van de database file met een extensie.
        /// </param>   
        /// <param name="construct">
        /// als deze true staat word de database leeg gegooid en opnieuw gevult met het aanmaken van de database.
        /// </param>      
        /// <returns>
        /// 
        /// </returns>
        public DataHandler(string inputFile,bool construct)
        {
            dbConnection = String.Format("Data Source={0}", inputFile);
            if (construct)
            setupDataBase();
        }

        
        private void setupDataBase()
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                cnn.DropTable<Waypoint>();
                cnn.Query<Waypoint>(@"CREATE TABLE IF NOT EXISTS
                                waypoint (Id      INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            description    VARCHAR( 140 ),
                                            latitude    REAL,
                                            longitude    REAL
                            );");

                cnn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            fillDataBase();
        }

        private void fillDataBase()
        {
            List<Waypoint> Waypoints = new List<Waypoint>();

            Waypoints.Add(new Waypoint("VVV Breda", new Geopoint(new BasicGeoposition() { Latitude = 51.59380, Longitude = 4.77963 })));
            Waypoints.Add(new Waypoint("Liefdeszuster", new Geopoint(new BasicGeoposition() { Latitude = 51.59307, Longitude = 4.77969 })));
            Waypoints.Add(new Waypoint("Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 })));
            Waypoints.Add(new Waypoint("Nassau Baronie Monument", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 })));
            Waypoints.Add(new Waypoint("The Light House", new Geopoint(new BasicGeoposition() { Latitude = 51.59256, Longitude = 4.77889 })));
            Waypoints.Add(new Waypoint("1e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59265, Longitude = 4.77844 })));
            Waypoints.Add(new Waypoint("2e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59258, Longitude = 4.77806 })));
            Waypoints.Add(new Waypoint("Einde park", new Geopoint(new BasicGeoposition() { Latitude = 51.59059, Longitude = 4.77707 })));

            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                foreach (Waypoint waypoint in Waypoints)
                    cnn.Insert(waypoint);
            
                cnn.Commit();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// haalt alle waypoints op.
        /// </summary>     
        /// <returns>
        /// een List met alle waypoints in de database.
        /// </returns>
        public List<Waypoint> getWaypoints()
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);


                List<Waypoint> waypoint = cnn.Query<Waypoint>(
                    @"SELECT * FROM waypoint"
                    ).ToList();

                cnn.Close();
                return waypoint;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// haalt een random waypoint op uit de database.
        /// </summary>     
        /// <returns>
        /// 1 waypoint random gekozen uit de database
        /// </returns>
        public Waypoint getRandomWaypoint(){
            IQueryable<Waypoint> waypoint;
            int sum = count("waypoint");
            try
            {

                SQLiteConnection cnn = new SQLiteConnection(dbConnection);

                Random rnd = new Random();
                int randomnumber = rnd.Next(sum) + 1;

                //waypoint = cnn.Query<Waypoint>(
                //    @"SELECT * FROM waypoint WHERE id = " + randomnumber );
                var waypoints = cnn.Query<Waypoint>(
                    @"SELECT * FROM waypoint")
                    .AsQueryable();

                // Omdat er linq in moet en de database het niet ondersteund hebben we het buiten de database gedaan
                // dus eerst een iqueryable(list) ophalen en daar met een linq overheen gaan om de random er uit te pakken
                waypoint = from x in waypoints
                           where x.Id == randomnumber
                           select x;

                cnn.Close();
                return waypoint.First();
               ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// haal alle puzzles uit de database
        /// </summary>    
        /// <returns>
        /// een lijst met alle puzzles uit de database
        /// </returns>
       
        private int count(string tableName)
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);

                string query = "SELECT id FROM " + tableName;
                List<Waypoint> Puzzles = cnn.Query<Waypoint>(query);
                cnn.Close();
                return Puzzles.Count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}