# OnlineMarket

##Technologies Used
###Back-End
---
    Framework: .NET 8
    Database: PostgreSQL
    ORM: Entity Framework Core (for CRUD operations)
    Dapper: For optimized SQL queries
    Validation: FluentValidation
    API Documentation: Swagger/OpenAPI

###Front-End
---
    Framework: React (with TypeScript)
    HTTP Client: Axios (for API communication)

##Database Schema
---
    Orders Table
        Id (Primary Key) – int
        CreatedOn – datetime
        CustomerFullName – nvarchar(100)
        CustomerPhone – nvarchar(15)
---
    Products Table
        Id (Primary Key) – int
        Code – nvarchar(50) (Unique)
        Name – nvarchar(100)
        Price – money
---
    OrderProducts Table
        Id (Primary Key) – int
        OrderId (Foreign Key -> Orders) – int
        ProductId (Foreign Key -> Products) – int
        Amount – int
        TotalPrice – money

##Features
---
    ###Order Management
        Create an order with multiple products.
        Retrieve all orders with product details.
        Get order details by Id.
---
    ###Product Management
        Create new products.
        Retrieve all products.
        Get product by Id or Code.
        Update product by Id.