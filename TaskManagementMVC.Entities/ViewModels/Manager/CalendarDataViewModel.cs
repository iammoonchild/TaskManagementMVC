using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Entities.ViewModels.Manager
{
    public class CalendarDataViewModel
    {
        public long Id { get; set; }
        public string title { get; set; }
    }

    public class CalendarTaskViewModel
    {
        public long Id { get; set; }
        public long resourceId { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string color { get; set; }
    }
}
