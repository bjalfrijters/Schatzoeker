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
        public DataHandler(string inputFile)
        {
            dbConnection = String.Format("Data Source={0}", inputFile);
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

            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                //cnn.DropTable<Puzzle>();
                cnn.Query<Puzzle>(@"CREATE TABLE IF NOT EXISTS
                                puzzle (Id      INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            answer    VARCHAR( 140 )
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



            List<Puzzle> puzzles = new List<Puzzle>();
            puzzles.Add(new Puzzle("testing"));
            puzzles.Add(new Puzzle("stoel"));
            puzzles.Add(new Puzzle("deur"));
            puzzles.Add(new Puzzle("kledingkast"));
            puzzles.Add(new Puzzle("pennenbakje"));
            puzzles.Add(new Puzzle("schoen"));
            puzzles.Add(new Puzzle("muts"));

            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                foreach (Waypoint waypoint in Waypoints)
                    cnn.Insert(waypoint);

                foreach (Puzzle puzzle in puzzles)
                    cnn.Insert(puzzle);

                cnn.Commit();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

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

        public List<Puzzle> getAllPuzzles()
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);


                List<Puzzle> puzzle = cnn.Query<Puzzle>(
                    @"SELECT * FROM puzzle"
                    ).ToList();

                cnn.Close();
                return puzzle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string getRandomPuzzle(){
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);


                List<Puzzle> puzzle = cnn.Query<Puzzle>(
                    @"SELECT * FROM puzzle"
                    ).ToList();

                cnn.Close();
                Random rnd = new Random();
                string temp = puzzle[rnd.Next(puzzle.Count)]._answer;
                return temp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string getSpesificPuzzle(int id)
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);

                string query = "SELECT answer FROM puzzle WHERE Id = '" + id + "'";
                List<Puzzle> Puzzles = cnn.Query<Puzzle>(query);
                cnn.Close();
                return Puzzles[0]._answer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}