> Request #3: Created Web API in .Net Core 2.1
# Movie Library API 

This solution contains four different projects:
- Entities : Model 
- DataTransferObjects : To bind the view model
- AppServices : Bridge between Host and Repository
- Repositories :- To communicate with Database 
- MovieLibraryAPI :- Host, recieve and send response to the client
- MoviesUnitTesting :- A Unit Test Project
- Added Swagger for API Documentation

> I have fixed the Bug #1 and Bug #2 in the existing CodingChallenge projects. 

## Setup for the existing CodingChallenge project:- 

There should be no setup required aside from downloading the source code, validating all packages are downloaded, and compiling / running the application within IIS Express. It's recommended that you use Visual Studio Community edition if you do not already have Visual Studio. 

When you run the application it launches the browser with Swagger page. You can see API documentaion and also can test the controller

1. MoviesController.cs:- 
	It has only one Get Method with FilterOptions as parameter. 
	
	FilterOptions:- FilterBy, Title, Rating, FromYear, ToYear	
	
	FilterBy : Title = 1; RatingAbove = 2; RatingBelow = 3; DateRange = 4; All = 6

All the URLs provided below for reference.

Swagger Documentation / Testing API:-

https://localhost:44353/index.html

To get all records:-

https://localhost:44353/api/v1/Movies?FilterBy=6 
(or) 
https://localhost:44353/api/v1/Movies?FilterBy=1&Title=

With Title:-

https://localhost:44353/api/v1/Movies?FilterBy=1&Title=Karate

Rating Above:-

https://localhost:44353/api/v1/Movies?FilterBy=2&Rating=8

Rating Below:

https://localhost:44353/api/v1/Movies?FilterBy=3&Rating=3

Movie Year Range:

https://localhost:44353/api/v1/Movies?FilterBy=4&FromYear=1983&ToYear=1985


> Note: Added few test cases wired to data retrieval methods. Franchise functionality is not implimented.

