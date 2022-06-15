# Darewise GameBackend Challenge

Following the instructions I received, I develop this solution. I followed the following principles:
- Keep it simple. I just followed the scenario the document describes, nothing extra.
- This solution could be a microservice with a more complex architecture, but I didn't use microservices in this case because I didn't see it necessary for this scenario.

## Features

- Account: You can register an admin account and login into the system using the Account section.
- Bug / Feedback: Anyone can submit a new bug, but only if you are logged you will be able to search a specific bug by Id, or get the list of all the bugs. 
- Player: You can be register a new player,and if you are logged, search a player by ID or get all the players registered on system.

**Considerations**
-I added a simple login/registration system to apply some authorization feature in the test. I didn't apply split authorization by role because I assumed it wouldn't be needed for this test. But in a real scenario, the application must handle at least 3 roles, "guest user" - "player".

## Tech

This app was developed using NET 6, and use the following packages:

- MS Entity Framework Core to work with a sql server database using .NET objects ( I choose Sql server but it could be another one).
- Asp Net Core AuthenticationJWTBearer to add security to the application (generates a token when a login completes successfully).
- MS AutoMapper to handle the data relation between the models and DTOs (in order to protect the information of the system,I'm using DTOs). 

## Development

The application use many resouces:

Services:
- JsonOptions to avoid recursive references within objects.
- DbContext to create the object that will handle communication with the database..
- I added a filter ( ExceptionFilter) to handle the exceptions that the app can throw.
- Authentication service,using AuthenticationJWTBearer package to apply security using TokenValidationParameters, and Identity to check the user access.
- Swagger (OpenAPI).
- I created a service, called LocalFileManager to save the attachments locally on a backend folder. It's something simple because the document did not explain an specific requirement about that so I tried to make it simple.
- I use response catching to apply a 30 sec rules when you request a get all. When you call a get (i.e. get all bugs) the result will be storage in cache for 30 sec. If there's another call to that endpoint,it will retrieve the what is located in cache. When the 30 sec finished, the next call to that endpoint will go to database to bring fresh information and store it for 30 sec. 



Interfaces:
The most important interfaces I applied are:

- ILogger to generate logs when an error occurs
- IMapper to map Models with DTOs
- IFilesManager to handle the attachment upload on BugController.


## Docker

This project is dockerizable. 
By default, the Docker will expose port 80, so change this within the
Dockerfile if necessary. When ready, simply use the Dockerfile to
build the image.
