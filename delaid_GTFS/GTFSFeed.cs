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

		private float ConvertTimeStampToHours(string timeStamp)
		{
			return 0;
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
			String stopTimesFile = System.IO.Path.Combine(feedPath, "stop_times.txt");

			// Parse the routes
			using (System.IO.StreamReader textReader = System.IO.File.OpenText (routesFile)) 
			{
				var csv = new CsvReader(textReader);
				csv.Configuration.RegisterClassMap<GTFSRouteMap>();
				this.Routes = csv.GetRecords<GTFSRoute>().ToList();
			}

			// Parse the stops
			using (System.IO.StreamReader textReader = System.IO.File.OpenText(stopsFile)) 
			{
				var csv = new CsvReader (textReader);
				csv.Configuration.RegisterClassMap<GTFSStopMap> ();
				this.Stops = csv.GetRecords<GTFSStop>().ToList();
			}

			// Parse the trips
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
				
			this.Trips = this.Trips.OrderBy (trip => trip.TripID).ToList ();
			this.Stops = this.Stops.OrderBy (stop => stop.StopID).ToList ();

			// Parse the stop times and associate them with trips
			using (System.IO.StreamReader textReader = System.IO.File.OpenText(stopTimesFile)) 
			{
				var csv = new CsvReader (textReader);

				string currentTripID = "";
				GTFSTrip currentTrip = null;

				while (csv.Read ()) 
				{
					string tripId = csv.GetField(0).Replace("\"", "");
					if (tripId != currentTripID) {
						currentTripID = tripId;
						currentTrip = this.FindTrip (tripId);
						if (currentTrip.StopTimes == null) {
							currentTrip.StopTimes = new List<GTFSStopTimes> ();
						}
					}
					var stopTime = new GTFSStopTimes ();
					stopTime.TripID = tripId;
					//stopTime.Stop = this.FindStop (csv.GetField (3).Replace ("\"", ""));
					stopTime.StopID = csv.GetField (3).Replace ("\"", "");
					stopTime.StopSequence = csv.GetField<int> (4);
					stopTime.ArrivalTime = this.ConvertTimeStampToHours (csv.GetField (1));
					stopTime.DepartureTime = this.ConvertTimeStampToHours (csv.GetField (2));

					currentTrip.StopTimes.Add (stopTime);
				}
			}

			foreach (var trip in this.Trips) {
				if (trip.StopTimes == null) {
					Console.WriteLine ("Stop times empty for " + trip.TripID);
				} else {
					trip.StopTimes = trip.StopTimes.OrderBy (st => st.StopSequence).ToList();
				}
			}
		}

		public GTFSTrip FindTrip (string tripId)
		{
			int iMax = this.Trips.Count;
			int iMin = 0;
			int i = (int)(iMax / 2);

			while (true) {
				// For string compare: -1 means the 1 before 2, +1 means 2 before 1, 0 means equal
				int strcmp = String.Compare (this.Trips [i].TripID, tripId);
				if (strcmp > 0) {
					iMax = i;
					i = (int)((i + iMin) / 2);
				} else if (strcmp < 0) {
					iMin = i;
					i = (int)((iMax + i) / 2);
				} else 
				{
					// We found it!
					break;
				}

			}
			return this.Trips [i];
		}

		public GTFSStop FindStop (string stopId)
		{
			int iMax = this.Stops.Count;
			int iMin = 0;
			int i = (int)(iMax / 2);

			while (true) {
				// For string compare: -1 means the 1 before 2, +1 means 2 before 1, 0 means equal
				int strcmp = String.Compare (this.Stops [i].StopID, stopId);
				if (strcmp > 0) {
					iMax = i;
					i = (int)((i + iMin) / 2);
				} else if (strcmp < 0) {
					iMin = i;
					i = (int)((iMax + i) / 2);
				} else 
				{
					// We found it!
					break;
				}

			}
			return this.Stops [i];
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

