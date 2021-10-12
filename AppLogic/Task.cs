using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class Task
    {
        public string Description { get; set; }

        public eTaskPriority Priority { get; set; }

        public eTaskType Type { get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Description);
        }
    }
}
