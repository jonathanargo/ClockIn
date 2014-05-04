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
    public partial class Log : Form
    {
        public Log(DataAccess dataAccess)
        {
            InitializeComponent();
            this.DataAccess = dataAccess;
            this.DataAccess.FillDataSet();
            FillLog();
        }

        private DataAccess DataAccess { get; set; }

        private void FillLog()
        {
            if (rbtSummary.Checked == true)
            {
                FillLogSummary();
            }
            else
            {
                DataTable dt = DataAccess.DataSet.Tables["ClockEvents"];

                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (i < 17)
                    {
                        String type = dr["EventType"].ToString();
                        Debug.WriteLine("Attempting to parse date: " + dr["DT_Created"].ToString() + " for event " + dr["EventID"].ToString());
                        DateTime created = DateTime.Parse(dr["DT_Created"].ToString());
                        ClockEvent thisEvent = new ClockEvent(type, created);
                        lbxLog.Items.Add(thisEvent.ToString());
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }//FillLog()

        private void FillLogSummary()
        {
            int startIndex = -1, endIndex = -1;
            DateTime startTime, endTime;
            int i = 0;
            foreach (DataRow dr in DataAccess.DataSet.Tables["ClockEvents"].Rows)
            {
                if (dr["EventType"].ToString() == "In")
                {
                    startIndex = i;
                    startTime = DateTime.Parse(dr["DT_Created"].ToString());
                }
                else if (dr["EventType"].ToString() == "Out")
                {
                    endIndex = i;
                    endTime = DateTime.Parse(dr["DT_Created"].ToString());
                }

                if (startIndex > 0 && endIndex > 0)
                {

                }
                
            }

        }

        public void RefreshLog()
        {
            DataAccess.FillDataSet();
            FillLog();
        }
    }
}

//look through clockevents table. Every time there's a clock out and a clock in, create a string representing the time between them.

/*
 * 1. Go through ClockEvents, 1 event at a time
 * 2. record what type of event was encountered by assigning a value to the index var
 * 3. check to see if you have both a start and an end. If so, make an entry




*/