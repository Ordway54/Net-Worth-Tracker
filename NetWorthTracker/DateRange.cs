namespace NetWorthTracker
{
    public readonly struct DateRange
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string StartDateFormatted
        {
            get { return StartDate.ToString(Database.DateFormatString); }
        }
        public string EndDateFormatted
        {
            get { return EndDate.ToString(Database.DateFormatString); }
        }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("End date must be greater than or equal to Start date.");
            }

            StartDate = startDate;
            EndDate = endDate;
        }

        public bool Contains(DateTime date)
        {
            return date >= StartDate && date <= EndDate;
        }
    }
}
