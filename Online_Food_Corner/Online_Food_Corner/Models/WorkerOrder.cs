using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Food_Corner.Models
{
    public class WorkerOrder
    {
        public int Id { get; set; }
        public Worker Worker { get; set; }
        public int WorkerId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }

    }
}