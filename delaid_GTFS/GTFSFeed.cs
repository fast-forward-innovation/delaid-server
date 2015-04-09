using System;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;

namespace delaid_GTFS
{
	/// <summary>
	/// A container for the delaid-relevant data from a GTFS feed. 
	/// </summary>
	/// <remarks>
	/// Feed documentation here: 
	/// https://developers.google.com/transit/gtfs/reference#calendar_dates_fields
	/// </remarks>
	public class GTFSFeed
	{
		public GTFSFeed ()
		{
		}

		public List<GTFSRoute> Routes {
			get;
			set;
		}

		public List<GTFSTrip> Trips {
			get;
			set;
		}

		public List<GTFSStop> Stops {
			get;
			set;
		}

		/// <summary>
		/// Parse the files in a GTFS feed into this object.
		/// </summary>
		/// <param name="feedPath">The path to the feed files. Ideally this would take the path
		/// (or URL) to the .zip file directly. I'll work that out later.</param>
		public void ParseFeed(string feedPath)
		{
			String routesFile = System.IO.Path.Combine(feedPath, "routes.txt");
			String stopsFile = System.IO.Path.Combine(feedPath, "stops.txt");
			String tripsFile = System.IO.Path.Combine(feedPath, "trips.txt");

			using (System.IO.StreamReader textReader = System.IO.File.OpenText (routesFile)) 
			{
				var csv = new CsvReader(textReader);
				csv.Configuration.RegisterClassMap<GTFSRouteMap>();
				this.Routes = csv.GetRecords<GTFSRoute>().ToList();
			}

			using (System.IO.StreamReader textReader = System.IO.File.OpenText(stopsFile)) 
			{
				var csv = new CsvReader (textReader);
				csv.Configuration.RegisterClassMap<GTFSStopMap> ();
				this.Stops = csv.GetRecords<GTFSStop>().ToList();
			}

			using (System.IO.StreamReader textReader = System.IO.File.OpenText(tripsFile)) 
			{
				var csv = new CsvReader (textReader);

				this.Trips = new List<GTFSTrip> ();

				string currentRouteID = "";
				GTFSRoute currentRoute = null;

				while (csv.Read ()) 
				{
					string routeId = csv.GetField(0).Replace("\"", "");
					if (routeId != currentRouteID) {
						currentRouteID = routeId;
						currentRoute = this.Routes.First (route => route.RouteID == routeId);
					}
					var trip = new GTFSTrip (csv.GetField(2).Replace("\"", ""), currentRoute);
					trip.ServiceID = csv.GetField (1).Replace ("\"", "");
					trip.ShortName = csv.GetField (4).Replace ("\"", "");
					this.Trips.Add (trip);
				}
			}
		}
	}

	public sealed class GTFSRouteMap : CsvClassMap<GTFSRoute>
	{
		public GTFSRouteMap()
		{
			Map (m => m.RouteID).ConvertUsing(row => row.GetField(0).Replace("\"", ""));
			Map (m => m.ShortName).ConvertUsing(row => row.GetField(2).Replace("\"", "")).Default("");
			Map (m => m.LongName).ConvertUsing(row => row.GetField(3).Replace("\"", "")).Default("");
			Map (m => m.RouteDesc).ConvertUsing(row => row.GetField(4).Replace("\"", "")).Default("");
			Map (m => m.RouteType).ConvertUsing (row => (RouteTypes)row.GetField<int> (5));
		}
	}

	public sealed class GTFSStopMap : CsvClassMap<GTFSStop>
	{
		public GTFSStopMap()
		{
			Map (m => m.StopID).ConvertUsing(row => row.GetField(0).Replace("\"", ""));
			Map (m => m.Name).ConvertUsing(row => row.GetField(2).Replace("\"", "")).Default("");
			Map (m => m.Desc).ConvertUsing(row => row.GetField(3).Replace("\"", "")).Default("");
			Map (m => m.Lat).ConvertUsing(row => Convert.ToSingle(row.GetField(4).Replace("\"", "")));
			Map (m => m.Lon).ConvertUsing(row => Convert.ToSingle(row.GetField(5).Replace("\"", "")));
		}
	}
}

