using System;
using delaid_GTFS;

namespace GTFSFeedTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string feedPath = "/Users/mike/Documents/mbta_playground/MBTA_GTFS/";

			DateTime start = DateTime.Now;

			var feed = new GTFSFeed ();
			feed.ParseFeed (feedPath);

			TimeSpan runTime = DateTime.Now - start;
			Console.WriteLine ("Time to run: " + runTime.TotalSeconds + " s");

			Console.WriteLine ("### Routes ###");
			for (int i=20; i<30; i++) {
				var route = feed.Routes[i];
				Console.WriteLine (route.RouteID + ", " + route.ShortName );
			}

			Console.WriteLine ("");
			Console.WriteLine ("### Stops ###");
			for (int i=20; i<30; i++) {
				var stop = feed.Stops[i];
				Console.WriteLine (stop.StopID + ", " + stop.Name + 
					", " + stop.Lat.ToString() + ", " + stop.Lon.ToString());
			}

			Console.WriteLine ("");
			Console.WriteLine ("### Trips ###");
			for (int i = 1000; i < 1020; i++) {
				var trip = feed.Trips [i];

				System.Console.WriteLine (trip.Route.RouteID + ", " + trip.TripID + 
					", " + trip.ShortName);
			}
		}
	}
}
