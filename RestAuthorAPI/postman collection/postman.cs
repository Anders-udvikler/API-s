/*
{
    "info": {
        "name": "Library REST API",
    "_postman_id": "b3f6b1c2-1234-5678-9012-abcdef123456",
    "description": "Collection for testing Library REST API",
    "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
    },
  "variable": [
    {
        "key": "baseUrl",
      "value": "http://localhost:5134"
    }
  ],
  "item": [
    {
        "name": "Books",
      "item": [
        {
            "name": "Get All Books",
          "request": {
                "method": "GET",
            "url": "{{baseUrl}}/api/books"
          }
        },
        {
            "name": "Get Books (Pagination)",
          "request": {
                "method": "GET",
            "url": "{{baseUrl}}/api/books?limit=2&offset=0"
          }
        },
        {
            "name": "Get Book By ID",
          "request": {
                "method": "GET",
            "url": "{{baseUrl}}/api/books/1000"
          }
        },
        {
            "name": "Create Book",
          "request": {
                "method": "POST",
            "header": [
              {
                    "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
                    "mode": "raw",
              "raw": "{\n  \"title\": \"Postman Book\",\n  \"authorId\": 1,\n  \"publishingYear\": 2024,\n  \"publishingCompanyId\": 1\n}"
            },
            "url": "{{baseUrl}}/api/books"
          }
        },
        {
            "name": "Update Book",
          "request": {
                "method": "PUT",
            "header": [
              {
                    "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
                    "mode": "raw",
              "raw": "{\n  \"id\": 1000,\n  \"title\": \"Updated Book\",\n  \"authorId\": 1,\n  \"publishingYear\": 2025,\n  \"publishingCompanyId\": 1\n}"
            },
            "url": "{{baseUrl}}/api/books/1000"
          }
        },
        {
            "name": "Delete Book",
          "request": {
                "method": "DELETE",
            "url": "{{baseUrl}}/api/books/1000"
          }
        }
      ]
    },
    {
        "name": "Authors",
      "item": [
        {
            "name": "Get All Authors",
          "request": {
                "method": "GET",
            "url": "{{baseUrl}}/api/authors"
          }
        },
        {
            "name": "Get Author By ID",
          "request": {
                "method": "GET",
            "url": "{{baseUrl}}/api/authors/1"
          }
        },
        {
            "name": "Create Author",
          "request": {
                "method": "POST",
            "header": [
              {
                    "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
                    "mode": "raw",
              "raw": "{\n  \"name\": \"John\",\n  \"surname\": \"Doe\"\n}"
            },
            "url": "{{baseUrl}}/api/authors"
          }
        },
        {
            "name": "Update Author",
          "request": {
                "method": "PUT",
            "header": [
              {
                    "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
                    "mode": "raw",
              "raw": "{\n  \"id\": 1,\n  \"name\": \"Jane\",\n  \"surname\": \"Doe\"\n}"
            },
            "url": "{{baseUrl}}/api/authors/1"
          }
        },
        {
            "name": "Delete Author",
          "request": {
                "method": "DELETE",
            "url": "{{baseUrl}}/api/authors/1"
          }
        }
      ]
    },
    {
        "name": "Publishers",
      "item": [
        {
            "name": "Get All Publishers",
          "request": {
                "method": "GET",
            "url": "{{baseUrl}}/api/publishers"
          }
        },
        {
            "name": "Get Publisher By ID",
          "request": {
                "method": "GET",
            "url": "{{baseUrl}}/api/publishers/1"
          }
        },
        {
            "name": "Create Publisher",
          "request": {
                "method": "POST",
            "header": [
              {
                    "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
                    "mode": "raw",
              "raw": "{\n  \"name\": \"New Publisher\"\n}"
            },
            "url": "{{baseUrl}}/api/publishers"
          }
        },
        {
            "name": "Update Publisher",
          "request": {
                "method": "PUT",
            "header": [
              {
                    "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
                    "mode": "raw",
              "raw": "{\n  \"id\": 1,\n  \"name\": \"Updated Publisher\"\n}"
            },
            "url": "{{baseUrl}}/api/publishers/1"
          }
        },
        {
            "name": "Delete Publisher",
          "request": {
                "method": "DELETE",
            "url": "{{baseUrl}}/api/publishers/1"
          }
        }
      ]
    }
  ]
}
*/