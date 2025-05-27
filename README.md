# **Mechanical Workshop Software - Data Model Documentation**

This document provides a detailed explanation of each class and its attributes in the mechanical workshop management system.

---

## **1. Users**
Represents the owner or administrator of a mechanical workshop, responsible for managing customers, vehicles, services, and transactions.

### **Attributes:**
- `UserId (int)`: Unique identifier for the user in the system.
- `NIT (int)`: Tax identification number used for legal and financial purposes.
- `UserName (string?)`: Full name of the user or workshop owner.
- `Phone (string?)`: Contact phone number for communication.
- `Email (string?)`: Email address for notifications and correspondence.
- `Adress (string?)`: Physical location of the workshop.
- `VehiclesList (List<Vehicles>?)`: Collection of vehicles registered under this user.
- `CustomersList (List<Customers>?)`: List of customers who interact with the workshop.
- `BrandsList (List<Brands>?)`: Vehicle brands managed or serviced by the workshop.
- `OrdersList (List<Orders>?)`: List of work orders created under this user.
- `EmployeesList (List<Employees>?)`: Employees working in the workshop.
- `ServicesList (List<Services>?)`: Services offered by the workshop.
- `SalesList (List<Sales>?)`: Sales transactions recorded by the user.
- `PaymentMethodsList (List<PaymentMethods>?)`: Payment methods accepted in the workshop.
- `ProductsList (List<Products>?)`: Inventory of products available for sale or use.
- `CategoriesList (List<Categories>?)`: Categories under which products are classified.

---

## **2. Vehicles**
Represents customer vehicles serviced at the workshop.

### **Attributes:**
- `VehicleId (int)`: Unique identifier for each vehicle.
- `Plate (string?)`: Vehicle's license plate number.
- `Chassis (string?)`: Chassis number, used for identification.
- `Color (string?)`: The color of the vehicle.
- `Engine (string?)`: Engine specifications or identification number.
- `BrandId (int?)`: Reference to the vehicle's brand.
- `CustomerId (int?)`: The owner of the vehicle, linked to a customer.
- `UserId (int?)`: The workshop managing this vehicle.
- `_Brand (Brands?)`: Relationship to the `Brands` entity for brand details.
- `_Customer (Customers?)`: Relationship to the `Customers` entity for owner details.
- `_User (Users?)`: Relationship to the `Users` entity for workshop details.

---

## **3. Customers**
Represents individuals or companies that own vehicles serviced by the workshop.

### **Attributes:**
- `CustomerId (int)`: Unique identifier for each customer.
- `CustomerName (string?)`: Full name of the customer.
- `Identification (string?)`: National ID, tax number, or business registration.
- `PhoneNumber (string?)`: Contact number for communication.
- `Email (string?)`: Email address for correspondence.
- `UserId (int)`: The workshop owner managing this customer.
- `_User (Users?)`: Relationship to the `Users` entity.
- `VehiclesList (List<Vehicles>?)`: List of vehicles owned by the customer.

---

## **4. Brands**
Represents different vehicle brands serviced by the workshop.

### **Attributes:**
- `BrandId (int)`: Unique identifier for the brand.
- `BrandName (string?)`: Name of the vehicle brand (e.g., Toyota, Ford).
- `OriginCountry (string?)`: Country where the brand originates.
- `UserId (int)`: The workshop managing this brand.
- `_User (Users?)`: Relationship to the `Users` entity.

---

## **5. Orders**
Represents a work order created when a customer requests a service.

### **Attributes:**
- `OrderId (int)`: Unique identifier for each order.
- `OrderRef (string?)`: Reference code for tracking the order.
- `CustomerRemark (string?)`: Additional comments from the customer.
- `VehicleId (int)`: The vehicle associated with the order.
- `EmployeeId (int)`: The employee assigned to the order.
- `UserId (int)`: The workshop managing this order.
- `_User (Users?)`: Relationship to the `Users` entity.
- `_Vehicle (Vehicles?)`: Relationship to the `Vehicles` entity.
- `_Employees (Employees?)`: Relationship to the `Employees` entity.
- `OrderServicesList (List<OrderServices>?)`: Services linked to this order.

---

## **6. OrderServices**
Represents the services included in a specific order.

### **Attributes:**
- `OrderId (int)`: The order associated with the service.
- `ServiceId (int)`: The service performed.
- `_Order (Orders?)`: Relationship to the `Orders` entity.
- `_Service (Services?)`: Relationship to the `Services` entity.

---

## **7. Employees**
Represents workers in the workshop.

### **Attributes:**
- `EmployeeId (int)`: Unique identifier for the employee.
- `Identification (string?)`: Employee's personal ID or work ID.
- `EmployeeName (string?)`: Full name of the employee.
- `Active (bool)`: Indicates if the employee is currently working.
- `UserId (int)`: The workshop managing this employee.
- `_User (Users?)`: Relationship to the `Users` entity.
- `_OrdersList (List<Orders>?)`: Work orders assigned to this employee.

---

## **8. Services**
Represents the various services provided by the workshop.

### **Attributes:**
- `ServiceId (int)`: Unique identifier for the service.
- `ServiceName (string?)`: Name of the service (e.g., oil change, tire replacement).
- `Reference (string?)`: Internal reference code.
- `Price (double)`: Cost of the service.
- `StimatedTime (string?)`: Estimated time to complete the service.
- `UserId (int)`: The workshop managing this service.
- `_User (Users?)`: Relationship to the `Users` entity.
- `OrderServicesList (List<OrderServices>?)`: Orders that include this service.
- `ServicesProductsList (List<ServicesProducts>?)`: Products used for this service.

---

## **9. ServicesProducts**
Represents the relationship between services and the products used for them.

### **Attributes:**
- `ServiceId (int)`: The service requiring the product.
- `ProductId (int)`: The product used in the service.
- `_Service (Services?)`: Relationship to the `Services` entity.
- `_Product (Products?)`: Relationship to the `Products` entity.

---

## **10. Sales**
Represents sales transactions in the workshop.

### **Attributes:**
- `SaleId (int)`: Unique identifier for the sale.
- `SaleRef (string?)`: Reference code for tracking.
- `PaymentMethodId (int)`: Payment method used.
- `OrderId (int)`: The order related to the sale.
- `UserId (int)`: The workshop managing the sale.
- `_User (Users?)`: Relationship to the `Users` entity.
- `_PaymentMethod (PaymentMethods?)`: Relationship to the `PaymentMethods` entity.
- `_Order (Orders?)`: Relationship to the `Orders` entity.

---

## **11. PaymentMethods**
Represents the accepted payment methods in the workshop.

### **Attributes:**
- `PaymentMethodId (int)`: Unique identifier.
- `PaymentMethod (string?)`: Name of the payment method (e.g., cash, credit card).
- `Active (bool)`: Indicates if the payment method is currently accepted.
- `UserId (int)`: The workshop managing this payment method.
- `_User (Users?)`: Relationship to the `Users` entity.
- `SalesList (List<Sales>?)`: Sales transactions using this payment method.

---

## **12. Products**
Represents products available for sale or use in the workshop.

### **Attributes:**
- `ProductId (int)`: Unique identifier.
- `ProductName (string?)`: Name of the product.
- `Reference (string?)`: Internal reference code.
- `PurchasePrice (double)`: Cost price of the product.
- `SalePrice (double)`: Selling price.
- `CategoryId (int)`: Category classification.
- `UserId (int)`: The workshop managing this product.

---

## **13. Categories**
Represents different categories of products.

### **Attributes:**
- `CategoryId (int)`: Unique identifier.
- `CategoryName (string?)`: Name of the category.
- `Active (bool)`: Indicates if the category is in use.
- `UserId (int)`: The workshop managing this category.
