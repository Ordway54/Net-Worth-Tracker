using Microsoft.Data.Sqlite;

namespace NetWorthTracker
{
    public class Database
    {
        private const string connectionString = "Data Source=nwdatabase.db";
        public const string DateFormatString = "yyyy-MM-dd";

        public static void InsertRecord(NetWorthLogEntry logEntry)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var createTable = connection.CreateCommand();
                createTable.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Entries (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT);";

                createTable.ExecuteNonQuery();

                var accountBalances = logEntry.GetAccountBalances();

                foreach (var account in accountBalances)
                {
                    if (!ColumnExists(account.Key))
                    {
                        AddColumn(account.Key, "REAL");
                    }
                }

                if (!ColumnExists("NetWorth")) { AddColumn("NetWorth", "REAL"); }
                if (!ColumnExists("Date")) { AddColumn("Date", "TEXT"); }


                string columns = string.Join(", ", accountBalances.Keys);
                columns = columns + ", NetWorth, Date";

                string values = string.Join(", ", accountBalances.Keys.Select(key => '@' + key));
                values = values + ", @NetWorth, @Date";

                string query = $"INSERT INTO Entries ({columns}) VALUES ({values});";

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = query;

                foreach (var account in accountBalances)
                {
                    insertCommand.Parameters.AddWithValue('@' +  account.Key, account.Value);
                }

                insertCommand.Parameters.AddWithValue("@NetWorth", logEntry.NetWorth);
                insertCommand.Parameters.AddWithValue("@Date", logEntry.FormattedDate);

                insertCommand.ExecuteNonQuery();

                Console.WriteLine("Record inserted successfully.");
            }

        }

        private static bool ColumnExists(string columnName, string tableName = "Entries")
        {
            using (var connection = new SqliteConnection(connectionString)) {
                connection.Open();

                string query = $"PRAGMA table_info({tableName});";

                using (var command = connection.CreateCommand()) {
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader()) {

                        while (reader.Read()) {
                            string column = reader["name"].ToString();

                            if (column.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private static void AddColumn(string columnName, string columnType, string tableName = "Entries")
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string query = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType};";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
