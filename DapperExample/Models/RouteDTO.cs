using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExample.Models
{
    public class RouteDTO
    {
        public string RouteId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public IEnumerable<SalesOrderDTO> SalesOrders { get; set; }
    }
}
