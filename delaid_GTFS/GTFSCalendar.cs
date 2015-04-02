using System;

namespace delaid_GTFS
{
	public class GTFSCalendar
	{
		public GTFSCalendar (string serviceId, bool[] daysOfTheWeek, 
			DateTime startDate, DateTime endDate)
		{
			this.ServiceID = serviceId;
			this.DaysOfTheWeek = daysOfTheWeek;
			this.StartDate = startDate;
			this.EndDate = endDate;
		}

		public string ServiceID {
			get;
			set;
		}

		public bool[] DaysOfTheWeek {
			get;
			set;
		}

		public DateTime StartDate {
			get;
			set;
		}

		public DateTime EndDate {
			get;
			set;
		}

	}
}

