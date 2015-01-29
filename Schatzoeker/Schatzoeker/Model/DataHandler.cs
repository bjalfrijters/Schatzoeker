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
            Waypoints.Add(new Waypoint("Kasteel van Breda", new Geopoint(new BasicGeoposition() { Latitude = 51.59061, Longitude = 4.77624 })));
            Waypoints.Add(new Waypoint("Stadhouderspoort", new Geopoint(new BasicGeoposition() { Latitude = 51.58992, Longitude = 4.77634 })));
            Waypoints.Add(new Waypoint("Kruising Kasteelplein/Cingelstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.59033, Longitude = 4.77623 })));
            Waypoints.Add(new Waypoint("Huis van Brecht", new Geopoint(new BasicGeoposition() { Latitude = 51.59043, Longitude = 4.77518 })));
            Waypoints.Add(new Waypoint("2e bocht Cingelstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.59000, Longitude = 4.77429 })));
            Waypoints.Add(new Waypoint("Spanjaardsgat", new Geopoint(new BasicGeoposition() { Latitude = 51.59010, Longitude = 4.77336 })));
            Waypoints.Add(new Waypoint("Vismarkt", new Geopoint(new BasicGeoposition() { Latitude = 51.58982, Longitude = 4.77321 })));
            Waypoints.Add(new Waypoint("Havermarkt", new Geopoint(new BasicGeoposition() { Latitude = 51.58932, Longitude = 4.77444 })));
            Waypoints.Add(new Waypoint("Driehoek Kerkplein 1", new Geopoint(new BasicGeoposition() { Latitude = 51.58872, Longitude = 4.77501 })));
            Waypoints.Add(new Waypoint("Grote of Onze Lieve Vrouwekerk", new Geopoint(new BasicGeoposition() { Latitude = 51.58878, Longitude = 4.77549 })));
            Waypoints.Add(new Waypoint("Driehoek Kerkplein 3", new Geopoint(new BasicGeoposition() { Latitude = 51.58864, Longitude = 4.77501 })));
            Waypoints.Add(new Waypoint("Het poortje", new Geopoint(new BasicGeoposition() { Latitude = 51.58822, Longitude = 4.77525 })));
            Waypoints.Add(new Waypoint("Ridderstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58716, Longitude = 4.77582 })));
            Waypoints.Add(new Waypoint("Grote Markt", new Geopoint(new BasicGeoposition() { Latitude = 51.58747, Longitude = 4.77662 })));
            Waypoints.Add(new Waypoint("Het Wit Lam", new Geopoint(new BasicGeoposition() { Latitude = 51.58771, Longitude = 4.77652 })));
            Waypoints.Add(new Waypoint("Bevrijdingsmonument", new Geopoint(new BasicGeoposition() { Latitude = 51.58797, Longitude = 4.77638 })));
            Waypoints.Add(new Waypoint("Stadshuis", new Geopoint(new BasicGeoposition() { Latitude = 51.58885, Longitude = 4.77616 })));
            Waypoints.Add(new Waypoint("Kruising Grote Markt / Stadserf", new Geopoint(new BasicGeoposition() { Latitude = 51.58883, Longitude = 4.77617 })));
            Waypoints.Add(new Waypoint("Achterkant stadshuis", new Geopoint(new BasicGeoposition() { Latitude = 51.58889, Longitude = 4.77659 })));
            Waypoints.Add(new Waypoint("Kruising Grote Markt / Stadserf (terug gaan)", new Geopoint(new BasicGeoposition() { Latitude = 51.58883, Longitude = 4.77617 })));
            Waypoints.Add(new Waypoint("Terug naar begin Grote Markt", new Geopoint(new BasicGeoposition() { Latitude = 51.58747, Longitude = 4.77662 })));
            Waypoints.Add(new Waypoint("Antonius van Paduakerk", new Geopoint(new BasicGeoposition() { Latitude = 51.58761, Longitude = 4.77712 })));
            Waypoints.Add(new Waypoint("Kruising St. Jansstraat / Molenstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58828, Longitude = 4.77858 })));
            Waypoints.Add(new Waypoint("Bibliotheek", new Geopoint(new BasicGeoposition() { Latitude = 51.58773, Longitude = 4.77948 })));
            Waypoints.Add(new Waypoint("Kruising Molenstraat / Kloosterplein", new Geopoint(new BasicGeoposition() { Latitude = 51.58752, Longitude = 4.77994 })));
            Waypoints.Add(new Waypoint("Kloosterkazerne", new Geopoint(new BasicGeoposition() { Latitude = 51.58794, Longitude = 4.78105 })));
            Waypoints.Add(new Waypoint("Chasse Theater", new Geopoint(new BasicGeoposition() { Latitude = 51.58794, Longitude = 4.78218 })));
            Waypoints.Add(new Waypoint("1e bocht Kloosterplein / Begin Vlaszak", new Geopoint(new BasicGeoposition() { Latitude = 51.58794, Longitude = 4.78105 })));
            Waypoints.Add(new Waypoint("Binding van Isaäc", new Geopoint(new BasicGeoposition() { Latitude = 51.58862, Longitude = 4.78079 })));
            Waypoints.Add(new Waypoint("Einde Vlaszak / Begin Boschstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58955, Longitude = 4.78038 })));
            Waypoints.Add(new Waypoint("Beyerd", new Geopoint(new BasicGeoposition() { Latitude = 51.58966, Longitude = 4.78076 })));
            Waypoints.Add(new Waypoint("Gasthuispoort", new Geopoint(new BasicGeoposition() { Latitude = 51.58939, Longitude = 4.77982 })));
            Waypoints.Add(new Waypoint("2e bocht Veemarktstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58905, Longitude = 4.77981 })));
            Waypoints.Add(new Waypoint("Kruising St. Annastraat / Veemarktstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58846, Longitude = 4.77830 })));
            Waypoints.Add(new Waypoint("Willem Merkxtuin", new Geopoint(new BasicGeoposition() { Latitude = 51.58905, Longitude = 4.77801 })));
            Waypoints.Add(new Waypoint("Binnen Willem Merkxtuin", new Geopoint(new BasicGeoposition() { Latitude = 51.58918, Longitude = 4.77841 })));
            Waypoints.Add(new Waypoint("Uitgang Willem Merkxtuin", new Geopoint(new BasicGeoposition() { Latitude = 51.58905, Longitude = 4.77801 })));
            Waypoints.Add(new Waypoint("Kruising Catharinastraat / St. Annastraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58960, Longitude = 4.77770 })));
            Waypoints.Add(new Waypoint("Begijnenhof", new Geopoint(new BasicGeoposition() { Latitude = 51.58965, Longitude = 4.77830 })));
            Waypoints.Add(new Waypoint("Binnen Begijnenhof", new Geopoint(new BasicGeoposition() { Latitude = 51.58997, Longitude = 4.77810 })));
            Waypoints.Add(new Waypoint("Uitgang Begijnenhof", new Geopoint(new BasicGeoposition() { Latitude = 51.58965, Longitude = 4.77830 })));
            Waypoints.Add(new Waypoint("Eindpunt stadswandeling", new Geopoint(new BasicGeoposition() { Latitude = 51.58950, Longitude = 4.77649 })));


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