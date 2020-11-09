# ShoppingBasket

Run
----------
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

Swagger
----------
http://localhost:8090/swagger

Sample Run
----------
http://localhost:8090/api/v1/ShoppingBasket/AddItemToBasket

Body:
{
    "CatalogItemId": "HB000006C0DF",
    "Sku": "HBV000006C0DG",
    "Price": 102.4,
    "Quantity": 1
}

Header:
Content-Type: application/json
