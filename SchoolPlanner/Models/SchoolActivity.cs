using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPlanner.Models
{
    public class SchoolActivity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Room { get; set; }
        [Required]
        public string Group { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public int Slot { get; set; }
        [Required]
        public string Teacher { get; set; }

    }
}
