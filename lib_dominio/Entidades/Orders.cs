using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Orders
    {
        public int Id { get; set; }
        public string? OrderRef { get; set; }
        public string? CustomerRemark { get; set; }
        public string? ServiceCenterRemark { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("VehicleId")] public Vehicles? _Vehicle { get; set; }
        [ForeignKey("EmployeeId")] public Employees? _Employees { get; set; }

        [JsonIgnore]
        public List<OrderServices>? OrderServicesList { get; set; }
    }
}
