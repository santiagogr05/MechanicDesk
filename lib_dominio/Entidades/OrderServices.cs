using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class OrderServices
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("OrderId")] public Orders? _Order { get; set; }
        [ForeignKey("ServiceId")] public Services? _Service { get; set; }
    }
}
