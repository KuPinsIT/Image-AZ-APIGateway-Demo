# Image-AZ-APIGateway-Demo
An Azure serverless .NET 8 API with an Angular frontend to display real-time image events received via Azure API Gateway

## Architecture Overview
Clean Architecture + DDD Pattern

## Live demo
Check out the live demo of the project hosted on Azure.
Please note that if it's using a free service plan, it may take some time to wake the server up at the first-time access.

- Swagger API Documentation: [API Documentation URL](https://latestimageviewer-api-djc8gzczfnanfce7.southeastasia-01.azurewebsites.net/swagger/index.html)
- Angular Application: [Angular App Demo URL](https://latestimageviewer-hefeeef7bpa0fcar.southeastasia-01.azurewebsites.net/)
- Gateway API Portal: [Gateway API Portal for creating image](https://latestimageviewer-apigateway.developer.azure-api.net/api-details#api=imageazapigateway-server&operation=post-public-images)

# Development

### Prerequisites
- .NET 8

### Database installation
1.  Update the connection string in appsettings.
2.  Set the Migrations project as a startup project.
3.	Run EF update-database on the Migrations project to generate the database.

### Run application
1.  Update the connection string in appsettings.
1.  Set project WebApi as a startup project.

# Test
- UnitTests project for domain logic tests.
- AutomationTests (Report Updating): Selenium + Specflow project for posting image via api gateway and check image information display correctly on angular page
![image](https://github.com/user-attachments/assets/9620c4c6-8985-4817-adbe-56d8747dd573)
