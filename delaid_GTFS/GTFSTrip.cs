using System;

namespace delaid_GTFS
{
	/// <summary>
	/// Trips for each route. A trip is a sequence of two or more stops that occurs at specific time.
	/// </summary>
	public class GTFSTrip
	{
		public GTFSTrip()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="delaid_GTFS.GTFSTrip"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="route">Route.</param>
		public GTFSTrip (string id, GTFSRoute route)
		{
			this.TripID = id;
			this.Route = route;
		}

		/// <summary>
		/// The trip_id field contains an ID that identifies a trip. The trip_id is dataset unique.
		/// </summary>
		/// <value>The trip ID.</value>
		public string TripID {
			get;
			set;
		}

		/// <summary>
		/// Points to the route associated with this trip.
		/// </summary>
		/// <value>The route.</value>
		public GTFSRoute Route {
			get;
			set;
		}

		/// <summary>
		/// The trip_short_name field contains the text that appears in schedules and 
		/// sign boards to identify the trip to passengers.
		/// </summary>
		/// <value>The short name.</value>
		/// <description>for example, to identify train numbers for commuter rail trips. 
		/// If riders do not commonly rely on trip names, please leave this field blank.
		/// A trip_short_name value, if provided, should uniquely identify a trip within a 
		/// service day; it should not be used for destination names or limited/express designations.
		/// </description>
		public string ShortName {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the service ID.
		/// </summary>
		/// <value>The service ID.</value>
		public string ServiceID {
			get;
			set;
		}
	}
}

