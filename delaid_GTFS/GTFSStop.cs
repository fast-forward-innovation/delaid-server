using System;

namespace delaid_GTFS
{
	/// <summary>
	/// Individual locations where vehicles pick up or drop off passengers.
	/// </summary>
	public class GTFSStop
	{
		public GTFSStop()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="delaid_GTFS.GTFSStop"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="name">The name of the stop.</param> 
		public GTFSStop (string id, string name)
		{
			this.StopID = id;
			this.Name = name;
		}

		/// <summary>
		/// Gets or sets the stop I.
		/// </summary>
		/// <value>The stop I.</value>
		public string StopID {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The desc.</value>
		public string Desc {
			get;
			set;
		}

		/// <summary>
		/// The latitude of a stop or station. The field value must be a valid WGS 84 latitude.
		/// </summary>
		/// <value>The lat.</value>
		public float Lat {
			get;
			set;
		}

		/// <summary>
		/// The longitude of a stop or station. The field value must be a valid WGS 84 
		/// longitude value from -180 to 180.
		/// </summary>
		/// <value>The lon.</value>
		public float Lon {
			get;
			set;
		}

	}
}

