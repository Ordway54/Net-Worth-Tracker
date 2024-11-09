using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetWorthTracker
{
    public class NetWorthLogEntry
    {
        private DateTime Date;
        public string FormattedDate
        {
            get { return Date.ToString(Database.DateFormatString); }
        }
        public double NetWorth {  get; private set; }
        private Dictionary<string, double> AccountBalances = new Dictionary<string, double>();

        public NetWorthLogEntry(DateTime date)
        {
            Date = date;
        }

        private void CalculateNetWorth()
        {
            double netWorth = 0;

            foreach (KeyValuePair<string, double> entry in AccountBalances)
            {
                netWorth += entry.Value;
            }

            NetWorth = netWorth;
        }

        public void AddAccountBalance(string accountName, double balance)
        {
            AccountBalances.Add(accountName, balance);
            CalculateNetWorth();
        }

        public Dictionary<string, double> GetAccountBalances()
        {
            return AccountBalances;
        }
    }
}
