using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ClockIn
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            DataAccess = new DataAccess();
            GetClockStatus();
            UpdateFormStatus();
        }

        private DataAccess DataAccess {get;set;}
        private Log Log { get; set; }
        private string ClockStatus { get; set; }
        private DateTime LastEventTime { get; set; }

        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log = new Log(DataAccess);
            Log.Show();
        }

        private void GetClockStatus()
        {
            //indexoutofrange
            try
            {
                DataAccess.FillDataSet();
                //get the last event type, ordered by dt_created
                string status = DataAccess.DataSet.Tables["ClockEvents"].Rows[0].ItemArray[1].ToString();
                if (status == "Out" || status == "In" || status == "BreakStart" || status == "BreakEnd")
                {
                    ClockStatus = status;
                    LastEventTime = DateTime.Parse(DataAccess.DataSet.Tables["ClockEvents"].Rows[0].ItemArray[2].ToString());
                }
                else
                {
                    MessageBox.Show("Invalid Clock status: " + status);
                }
            }
            catch (IndexOutOfRangeException)
            {
                ClockStatus = "Out";
            }
        }//GetClockStatus()

        private void UpdateFormStatus()
        {
            GetClockStatus();
            //Changes the text and form controls based on the ClockStatus
            if (ClockStatus == "Out")
            {
                btnClock.Enabled = true;
                btnBreak.Enabled = false;
                btnClock.Text = "Clock In";
                lblClockStatus.Text = "You are clocked out.";
            }
            else if (ClockStatus == "In")
            {
                btnClock.Enabled = true;
                btnBreak.Enabled = true;
                btnClock.Text = "Clock Out";
                lblClockStatus.Text = "You clocked in at " + LastEventTime;
            }
            else if (ClockStatus == "BreakStart")
            {
                btnClock.Enabled = false;
                btnBreak.Enabled = true;
                lblClockStatus.Text = "You took a break at " + LastEventTime;
                btnBreak.Text = "End Break";
            }
            else if (ClockStatus == "BreakEnd")
            {
                btnClock.Enabled = true;
                btnBreak.Enabled = true;
                lblClockStatus.Text = "You are clocked in";
                btnBreak.Text = "Start Break";
            }
        }//UpdateFormStatus()

        private void btnClock_Click(object sender, EventArgs e)
        {
            string eventType;
            if (ClockStatus == "Out") { eventType = "In"; }
            else { eventType = "Out"; }
            DataAccess.InsertEvent(new ClockEvent(eventType, DateTime.Now));
            UpdateFormStatus();
        }//Click btnClock()

        private void btnBreak_Click(object sender, EventArgs e)
        {
            string eventType;
            //Button won't be enabled unless the user can start or end a break
            //(i.e. not clocked out)
            eventType = (ClockStatus == "BreakStart") ? (eventType = "BreakEnd") : (eventType = "BreakStart");
            DataAccess.InsertEvent(new ClockEvent(eventType, DateTime.Now));
            UpdateFormStatus();
        }

        private void clearEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataAccess.ClearTable();
            GetClockStatus();
            UpdateFormStatus();
        }

    }
}
