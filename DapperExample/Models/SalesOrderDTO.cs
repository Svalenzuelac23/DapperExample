using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExample.Models
{
    public class SalesOrderDTO
    {
        public DateTime DocDate { get; set; }
        public int DocNum { get; set; }
        public int DocEntry { get; set; }
        public string CardCode { get; set; }
        public string Comments { get; set; }
        public long IdWms { get; set; }
        public IEnumerable<SalesOrderDetailDTO> Details { get; set; }
    }
}
