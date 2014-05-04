using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockIn
{
    public class ClockEvent
    {
        public ClockEvent(String eventType, DateTime dt_created)
        {
            this.EventType = eventType;
            this.DT_Created = dt_created;
        }

        public String EventType { get; set; }
        public DateTime DT_Created { get; set; }

        public override string ToString()
        {
            String result = DT_Created.ToString() + ": ";
            String appendType;
            switch (EventType)
            {
                case "In":
                    appendType = "Clocked in";
                    break;
                case "Out":
                    appendType = "Clocked out";
                    break;
                case "BreakStart":
                    appendType = "Took a break";
                    break;
                case "BreakEnd":
                    appendType = "Came back from break";
                    break;
                default:
                    appendType = "Error";
                    break;
            }

            result += appendType;
            return result;
        }//ToString()
    }
}
