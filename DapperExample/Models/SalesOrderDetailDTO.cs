using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExample.Models
{
    public class SalesOrderDetailDTO
    {
        public string ItemCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal InvQty { get; set; }
        public int UomEntry { get; set; }
        public string WhsCode { get; set; }
        public string ReturnReason { get; set; }
    }
}
