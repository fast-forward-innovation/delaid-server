using System;

namespace delaid_GTFS
{
	/// <summary>
	/// Valid route types as defined here: 
	/// https://developers.google.com/transit/gtfs/reference#routes_fields
	/// </summary>
	public enum RouteTypes {LightRail, Subway, Rail, Bus, Ferry, CableCar, Gondola, Funicular}

	/// <summary>
	/// Transit routes. A route is a group of trips that are displayed to riders as a single service.
	/// </summary>
	public class GTFSRoute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="delaid_GTFS.GTFSRoute"/> class.
		/// </summary>
		/// <param name="id">Route ID.</param>
		/// <param name="shortName">Short name.</param>
		/// <param name="longName">Long name.</param>
		/// <param name="routeType">Route type.</param>
		public GTFSRoute(string id, string shortName, string longName, RouteTypes routeType)
		{
			this.RouteID = id;
			this.ShortName = shortName;
			this.LongName = longName;
			this.RouteType = routeType;
		}

		/// <summary>
		/// The route_id field contains an ID that uniquely identifies a route.
		/// </summary>
		/// <value>The route ID string</value>
		public string RouteID {
			get; 
			set;
		}

		/// <summary>
		/// The route_short_name contains the short name of a route. 
		/// </summary>
		/// <value>The short name string.</value>
		/// <description>
		/// This will often be a short, abstract identifier like "32", "100X", or "Green" that 
		/// riders use to identify a route, but which doesn't give any indication of what 
		/// places the route serves. At least one of route_short_name or route_long_name must 
		/// be specified, or potentially both if appropriate. If the route does not have a short 
		/// name, please specify a route_long_name and use an empty string as the value for 
		/// this field.
		/// </description>
		public string ShortName {
			get;
			set;
		}

		/// <summary>
		/// The route_long_name contains the full name of a route. 
		/// </summary>
		/// <value>The long name.</value>
		/// <description>
		/// This name is generally more descriptive than the route_short_name and will often 
		/// include the route's destination or stop. At least one of route_short_name or 
		/// route_long_name must be specified, or potentially both if appropriate. If the route 
		/// does not have a long name, please specify a route_short_name and use an empty string 
		/// as the value for this field.
		/// </description>
		public string LongName {
			get;
			set;
		}

		/// <summary>
		/// Describes the type of transportation used on a route.
		/// </summary>
		/// <value>The type of the route.</value>
		public RouteTypes RouteType {
			get;
			set;
		}

		/// <summary>
		/// The route_desc field contains a description of a route. This field is optional.
		/// </summary>
		/// <value>The route desc.</value>
		/// <description>
		/// Please provide useful, quality information. Do not simply duplicate the 
		/// name of the route. For example, "A trains operate between Inwood-207 St, Manhattan 
		/// and Far Rockaway-Mott Avenue, Queens at all times. Also from about 6AM until about 
		/// midnight, additional A trains operate between Inwood-207 St and Lefferts Boulevard 
		/// (trains typically alternate between Lefferts Blvd and Far Rockaway)."
		/// </description>
		public string RouteDesc {
			get;
			set;
		}
	}
}

