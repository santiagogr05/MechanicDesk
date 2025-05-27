using lib_dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ut_presentacion.Nucleo
{
    public class EntidadesNucleo
    {
        public static Brands? Brands()
        {
            var brand = new Brands();
            brand.BrandName = "Mazda";
            brand.OriginCountry = "Japon";

            return brand;
        }

        public static Customers? Customers()
        {
            var customer = new Customers();
            customer.CustomerName = "Juan Perez";
            customer.Identification = "1234567890";
            customer.PhoneNumber = "3004125879";
            customer.Email = "juanchoperez@yahoo.com";

            return customer;
        }

        public static Vehicles? Vehicles(Brands brand, Customers customer)
        {
            var vehicle = new Vehicles();
            vehicle.Plate = "LKJ781";
            vehicle.Chassis = "CH7-416541";
            vehicle.Color = "Rojo";
            vehicle.Engine = "V6 Gasolina";
            vehicle.BrandId = brand.Id;
            vehicle.CustomerId = customer.Id;
            
            return vehicle;
        }

        public static Employees? Employees()
        {
            var employee = new Employees();
            employee.EmployeeName = "Alfronio Gustambo";
            employee.Identification = "7452136880";
            employee.Active = true;

            return employee;
        }

        public static Orders? Orders(Vehicles vehicle, Employees employee)
        {
            var order = new Orders();
            order.OrderRef = "D004";
            order.CustomerRemark = "Cuidado con el volante";
            order.ServiceCenterRemark = "Pintura rallada";
            order.VehicleId = vehicle.Id;
            order.EmployeeId = employee.Id;

            return order;
        }

        public static PaymentMethods? PaymentMethods()
        {
            var paymentMethod = new PaymentMethods();
            paymentMethod.PaymentMethod = "Bonos";
            paymentMethod.Active = true;

            return paymentMethod;
        }

        public static Sales? Sales(Orders order, PaymentMethods paymentMethod)
        {
            var sales = new Sales();
            sales.SaleRef = "S004";
            sales.Total = 50;
            sales.PaymentMethodId = paymentMethod.Id;
            sales.OrderId = order.Id;
          

            return sales;
        }

        public static Services? Services()
        {
            var service = new Services();
            service.ServiceName = "Cambio neumaticos";
            service.Price = 150.22m;
            service.Reference = "S42";
            service.StimatedTime = "16 horas";

            return service;
        }

        public static OrderServices? OrderServices(Orders order, Services service)
        {
            var orderService = new OrderServices();
            orderService.OrderId = order.Id;
            orderService.ServiceId = service.Id;
        
            return orderService;
        }

        public static Products? Products(Categories category)
        {
            var product = new Products();
            product.ProductName = "Faros frontales";
            product.Reference = "P69";
            product.PurchasePrice = 115.00m;
            product.SalePrice = product.PurchasePrice * 1.2m;
            product.CategoryId = category.Id;

            return product;
        }

        public static Categories? Categories()
        {
            var category = new Categories();
            category.CategoryName = "Neumaticos";
            category.Active = true;
            return category;
        }

        public static ServicesProducts? ServicesProducts(Products product, Services service) { 
        
            var serviceProduct = new ServicesProducts();
            serviceProduct.ServiceId = service.Id;
            serviceProduct.ProductId = product.Id;

            return serviceProduct;
        }
    }
}
