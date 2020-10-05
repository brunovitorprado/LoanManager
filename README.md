[![Build Status Travis](https://travis-ci.com/brunovitorprado/LoanManager.svg?branch=master)](https://travis-ci.org/brunovitorprado/LoanManager)

# LoanManager
Solution for manage your game midia loans  

## Features

- __Game registration__. You can registrate your game midias one by one individually, recording information like Title, Description, Genre and Platform.
- __Friend registration__. You can register your friends, recording Name and Phone number. You will use that data to register your media game loans.
- __Loan registration__. With this feature, you are able to registrate your game loans, so you can loan a game midia to a friend and manage that loan. You can also end a loan, search loans by friend name and search loans by game identification id.

## Safety

All endpoints on Loan Manager are protected by authentication, so all requisitions need to have a Bearer token in the header.   
To make the authentication process more easy, Loan Manager have an integrated authentication service that uses JWT and provides endpoints to signup and signin.

## How to run the project

Loan manager is a Restfull API project that uses a docker based environment with a docker-compose file, so you just need docker running on your computer.   
So assuming you have docker running, you can push the commands below into your prompt terminal.

```
$ docker-compose build
```
```
$ docker-compose up
```
So voilà! docker will up the API and the PostgreSql server, create the database, create e populating the tables and make the Loan manager API accessible on localhost:8000.  
You can look at http://localhost:8000/docs for endpoints description and tests with swagger.
