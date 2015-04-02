using System;
using System.Collections.Generic;

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
	}
}

