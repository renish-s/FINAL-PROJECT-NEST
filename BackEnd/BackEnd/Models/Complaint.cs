using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintId { get; set; }
        public string Productname { get; set; }
        public string Complaints { get; set; }
        public int LoginId { get; set; }
        public string Status { get; set; }

    }
}
