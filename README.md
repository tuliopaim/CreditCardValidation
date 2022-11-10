# Credit Card Validation

## The solution 

Divided in three projects:

### Core
----

Useful things that can be used to standardize several microsservices in a productive environment.
Can be deployed and distributed as a Nuget package, for example.

Some design patterns used: 

- **INotifier** to gather errors across the scope of the request;
- **ValidationPipeline** using MediatR and FluentValidator, to auto validate
the inputs and short circuit in the case of errors; [See more]()
- **ExceptionMiddleware** to log all the unhandled exceptions in the application.
- **IDateTimeProvider** to facilitate mock;


###  Api
----

I opted to use the MediatR, that implements the mediator design pattern,
helping to keep the responsibilities segregated, facilitate error handling and vertical scalability,
and keep the controllers clean.

#### Commands layer

Each use case/command has his own folder inside `Commands/`, and it's composed with:

- Input - the input dto, implements `IRequest<TResponse>`
- InputValidator - will be used to validate the input in the ValidationPipeline
- Response - the output dto
- Handler - the logic of the command, implements `IRequestHandler<TRequest, TResponse>`

No extra configuration is required, **the handler will be auto-injected, 
and the input will be validated with the InputValidator rules, short circuiting in case of validation failures**, 

See more about [MediatR](https://github.com/jbogard/MediatR) and [Validation Pipeline Behavior](https://imasters.com.br/back-end/fail-fast-validations-com-pipeline-behavior-no-mediatr-e-asp-net-core)

#### Controllers layer

The controller's responsibilities is to pass the request input to the Mediator, and maps the result to a standard contract:

In case of validation error or any notifications, HTTP 400:
````json
{
  "isValid": false,
  "errors": [
    "'Customer Id' must be greater than '0'.",
    "'Card Number' must be greater than '0'.",
    "'CVV' must be between 1 and 999. You entered 0."
  ]
}
````
In case of success, HTTP 200:
````json
{
  "isValid": true,
  "result": {
    "registrationDate": "2022-11-10T23:13:03.1935748Z",
    "token": 23,
    "cardId": 1
  }
}
````

#### Domain layer

The entities encapsulating the business rules on how to generate the token, and the contracts (interfaces) for the repositories.


#### Infrastructure layer

DbContext configuration and the repositories implementations.

### Tests
----
Unit and Integrations tests.
