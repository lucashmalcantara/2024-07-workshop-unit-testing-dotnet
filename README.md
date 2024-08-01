# Workshop - Unit Testing with .NET

Project presented at the workshop on the topic of unit testing with .NET.

## Functional requirements

### Send email message

1. The email format must be valid.
2. The subject of the email must have at least three characters.
3. The message must not contain offensive terms. The terms are retrieved from the database. 

## Steps

Branches were created for each stage of the development process.

- `step1`: create the basic project structure.
- `step2`: think about abstractions and create tests.
	- It is common to start creating tests for a class and realize that the responsibility of a function should be transferred to another class. **Feel free to change the route as you wish.**

## How to run

1. Run the project in Visual Studio.
2. In Visual Studio, go to the file `Examples.Notification.Api.http` to execute the requests.
