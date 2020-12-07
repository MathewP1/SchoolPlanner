using SchoolPlanner.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPlanner.Models
{
    public class PlannerData
    {
        public IEnumerable<Room> Rooms;
        public string currentRoom;
    }
}
