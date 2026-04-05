using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    [Table("Routes")]
    public class Routes
    {
        [Key]
        public int RouteID { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string OriginStation { get; set; }
        public string DestinationStation { get; set; }
        public string EstimatedDuration { get; set; }
        public decimal TicketPrice { get; set; }
        public string RouteType { get; set; }
    }
}
