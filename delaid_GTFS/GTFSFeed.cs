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
			String routesFile = System.IO.Path.Combine (feedPath, "routes.txt");

			using (System.IO.StreamReader textReader = System.IO.File.OpenText (routesFile)) 
			{
				var csv = new CsvReader (textReader);
				csv.Configuration.RegisterClassMap<GTFSRouteMap> ();
				this.Routes = csv.GetRecords<GTFSRoute>().ToList();
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
}

