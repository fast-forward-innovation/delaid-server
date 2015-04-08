using System;
using delaid_GTFS;

namespace GTFSFeedTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string feedPath = "/Users/mike/Documents/mbta_playground/MBTA_GTFS/";
			var feed = new GTFSFeed ();
			feed.ParseFeed (feedPath);

			foreach (var route in feed.Routes) {
				System.Console.WriteLine (route.RouteID + " " + route.ShortName );
			}
		}
	}
}
