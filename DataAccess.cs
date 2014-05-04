using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace ClockIn
{
    public class DataAccess
    {
        public DataAccess()
        {
            this.ConnectionString = "Data Source=C:\\Dev\\ClockIn\\db\\ClockIn.db; Version=3;";
            FillDataSet();
        }

        public DataAccess(String connectionString )
        {
            this.ConnectionString = connectionString;
            FillDataSet();
        }

        public DataSet DataSet { get; set; }

        private string ConnectionString { get; set; }
        private SQLiteConnection Connection { get; set; }
        private SQLiteCommand Command { get; set; }
        private SQLiteDataAdapter Adapter { get; set; }

        public void FillDataSet()
        {
            try
            {
                using (Connection = new SQLiteConnection(ConnectionString))
                {
                    Connection.Open();

                    //Start with a fresh DataSet
                    DataSet = new DataSet();

                    //Fill all Events into ClockEvents
                    string cmdString =
                        "SELECT EventID, EventType, DateTime(DT_Created) as DT_Created " +
                        "FROM ClockEvents " +
                        "ORDER BY DT_Created DESC";
                    Command = new SQLiteCommand(cmdString, Connection);
                    Adapter = new SQLiteDataAdapter(Command);
                    Adapter.Fill(DataSet = new DataSet(), "ClockEvents");

                    //Fill 10 most recent events into Log
                    cmdString =
                        "SELECT EventID, EventType, DateTime(DT_Created) as DT_Created " +
                        "FROM ClockEvents " +
                        "ORDER BY DT_Created DESC " +
                        "LIMIT 10";
                    Command = new SQLiteCommand(cmdString, Connection);
                    Adapter.SelectCommand = Command;
                    Adapter.Fill(DataSet, "Log");

                    //Fill EventIDs, ordered desc
                    cmdString = "SELECT EventID FROM ClockEvents ORDER BY EventID DESC";
                    Command = new SQLiteCommand(cmdString, Connection);
                    Adapter.SelectCommand = Command;
                    Adapter.Fill(DataSet, "EventIDs");

                    Connection.Close();
                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show("Error in FillDataSet(): " + ex.Message);
            }
        }//FillDataSet

        public void InsertEvent(ClockEvent clockEvent)
        {
                try
                {
                    using (Connection = new SQLiteConnection(ConnectionString))
                    {
                        String eventID = GetNextClockEventID();
                        String eventType = clockEvent.EventType;
                        String eventDateTime = clockEvent.DT_Created.ToString("yyyy-MM-dd HH:mm:ss");

                        String query =
                            "INSERT INTO ClockEvents " +
                            "VALUES (@eventID, @eventType, @eventDateTime)";

                        Connection.Open();

                        Command = new SQLiteCommand(query, Connection);
                        Command.Parameters.AddWithValue("@eventID", eventID);
                        Command.Parameters.AddWithValue("@eventType", eventType);
                        Command.Parameters.AddWithValue("@eventDateTime", eventDateTime);

                        Command.ExecuteNonQuery();

                        Connection.Close();
                    }
                    FillDataSet();
                }
                catch (Exception ex)
                {
                    //TODO: Specify exception
                    System.Windows.Forms.MessageBox.Show("Error in InsertEvent(): " + ex.Message);
                }            
        }//InsertEvent()

        public string GetNextClockEventID()
        {
            try
            {
                int result = int.Parse(DataSet.Tables["EventIDs"].Rows[0].ItemArray[0].ToString()) + 1;
                return result.ToString();
            }
            catch (IndexOutOfRangeException ex)
            {
                return "0";
            }
        }//GetNextClockEventID()

        public void ClearTable()
        {
            try
            {
                using (Connection = new SQLiteConnection(ConnectionString))
                {
                    Connection.Open();
                    string command = "DELETE FROM ClockEvents";
                    Command = new SQLiteCommand(command, Connection);
                    Command.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("All events have been truncated");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        
    }
}
