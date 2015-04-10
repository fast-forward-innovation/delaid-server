using System;

namespace delaid_GTFS
{
	/// <summary>
	/// Times that a vehicle arrives at and departs from individual stops for each trip.
	/// </summary>
	public class GTFSStopTimes
	{
		public GTFSStopTimes ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="delaid_GTFS.GTFSStopTimes"/> class.
		/// </summary>
		public GTFSStopTimes (GTFSTrip trip, GTFSStop stop, int stopSequence, 
			float arrivalTime, float departureTime)
		{
			
		}

		/// <summary>
		/// Gets or sets the trip.
		/// </summary>
		/// <value>The trip.</value>
//		public GTFSTrip Trip {
//			get;
//			set;
//		}

		/// <summary>
		/// Gets or sets the stop.
		/// </summary>
		/// <value>The stop.</value>
		public GTFSStop Stop {
			get;
			set;
		}

		public string StopID {
			get;
			set;
		}

		public string TripID {
			get;
			set;
		}

		/// <summary>
		/// identifies the order of the stops for a particular trip. 
		/// </summary>
		/// <value>The stop sequence.</value>
		/// <description>The values for stop_sequence must be 
		/// non-negative integers, and they must increase along the trip.</description>
		public int StopSequence {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the arrival time *in hours*.
		/// </summary>
		/// <value>The arrival time.</value>
		/// <description>Defined as "noon minus 12h" (effectively midnight, except for 
		/// days on which daylight savings time changes occur) at the beginning of the 
		/// service date.</description>
		public float ArrivalTime {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the departure time *in hours*. 
		/// </summary>
		/// <value>The departure time.</value>
		/// <description>Defined as "noon minus 12h" (effectively midnight, except for 
		/// days on which daylight savings time changes occur) at the beginning of the 
		/// service date.</description>
		public float DepartureTime {
			get;
			set;
		}
	}
}

