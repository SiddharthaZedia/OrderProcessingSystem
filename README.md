This is a simple Order Processing System for an e-commerce application built with ASP.NET Core. It allows for managing customers, products, and orders, and calculates the total price for each order based on the products it contains.

Table of Contents
Overview
Features
Project Structure
Setup and Installation
API Endpoints
Usage
Contributing
Overview
This project is a basic Order Processing System for an e-commerce application. It simulates how an order is processed by managing customers, products, and orders in a simple .NET Core API.

Key Features:
Customer Management: Allows creating and managing customers.
Product Management: Manage products including pricing.
Order Management: Allows customers to place orders containing multiple products.
Total Calculation: Each order calculates the total price based on the products in the order.
Features
Customer: Stores customer information such as Name, Email, and Address.
Product: Each product has a Name, Price, and StockQuantity.
Order: Allows placing an order for multiple products. It automatically calculates the total price of the order.
Order Status: Each order has a status (Pending, Completed, Cancelled).
Prerequisites
To run this project locally, you need:

.NET 6.0 or later (can be installed from here)
Visual Studio 2022 or later (with .NET Core support) or any compatible C# editor like Visual Studio Code
SQL Server 


Testing
You can test the endpoints using Postman, cURL, or any other API testing tool.

GET /api/customers
Fetch all customers.

POST /api/orders
Create an order with multiple products and calculate the total price.

You can also inspect the logs to ensure that orders are correctly calculated.


API Endpoints
1. Customers API:
GET /api/customers
Fetch all customers.

GET /api/customers/{id}
Fetch a customer by their ID.

POST /api/customers
Create a new customer.

PUT /api/customers/{id}
Update a customer's information.

DELETE /api/customers/{id}
Delete a customer.

2. Products API:
GET /api/products
Fetch all products.

GET /api/products/{id}
Fetch a product by its ID.

POST /api/products
Create a new product.

PUT /api/products/{id}
Update a product's information.

DELETE /api/products/{id}
Delete a product.

3. Orders API:
GET /api/orders
Fetch all orders.

GET /api/orders/{id}
Fetch an order by its ID.

POST /api/orders
Create a new order. (Requires customer ID and a list of product IDs).
