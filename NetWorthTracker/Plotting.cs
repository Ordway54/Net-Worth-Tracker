using ScottPlot.WPF;

namespace NetWorthTracker
{
    class Plotting
    {
        public static void DisplayNetWorthPlot(WpfPlot plotControl, DateRange? dateRange = null)
        {
            var logEntries = new Dictionary<DateTime, double>();

            if (dateRange != null) 
            {
                logEntries = Database.GetNetWorthValues(dateRange);
            }

            else
            {
                logEntries = Database.GetNetWorthValues();
            }

            DateTime[] xValues = logEntries.Keys.ToArray();
            double[] yValues = logEntries.Values.ToArray();

            plotControl.Plot.Axes.DateTimeTicksBottom();
            plotControl.Plot.Add.Scatter(xValues, yValues);
            plotControl.Plot.YLabel("Dollars (USD)");
            plotControl.Plot.Title("My Net Worth");
        }
    }
}
