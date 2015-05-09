using System;
using System.Collections.Generic;
using System.Linq;

namespace InMemoryGatewayDemo
{
    public class Employee : Entity
    {
        private List<DateTime> workingDays;

        public Employee()
        {
            workingDays = new List<DateTime>();
        }

        public void ScheduleWork(DateTime dateTime)
        {
            workingDays.Add(dateTime.Date);
        }

        public bool ScheduledToWork(DateTime dateTime)
        {
            return workingDays.Any(workDate => workDate == dateTime.Date);
        }

        public virtual Sector Sector { get; set; }
    }
}
