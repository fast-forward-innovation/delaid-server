using System;

namespace delaid_GTFS
{
	public enum CalendarExceptionTypes {Added=1, Removed}

	/// <summary>
	/// Exceptions for the service IDs in the GTFSCalendar
	/// </summary>
	public class GTFSCalendarDates
	{
		public GTFSCalendarDates (GTFSCalendar service, DateTime date, 
			CalendarExceptionTypes exceptionType)
		{
			this.Service = service;
			this.ExceptionDate = date;
			this.ExceptionType = exceptionType;
		}

		public GTFSCalendar Service {
			get;
			set;
		}

		public DateTime ExceptionDate {
			get;
			set;
		}

		public CalendarExceptionTypes ExceptionType {
			get;
			set;
		}
	}
}

