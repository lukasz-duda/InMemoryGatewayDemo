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

        public void ScheduleWork(DateTime date)
        {
            workingDays.Add(date);
        }

        public bool ScheduledToWork(DateTime date)
        {
            return workingDays.Any(workDate => workDate == date);
        }

        public virtual Sector Sector { get; set; }
    }
}
