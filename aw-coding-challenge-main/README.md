# Movie Library Instructions

This solution contains four different projects:
- A Data Access Layer
- A Utility Library
- A Web UI Layer
- A Unit Test Project

> I have fixed the Bug #1 and Bug #2 in the existing CodingChallenge projects. 

> Request #3: Created Web API in .Net Core 2.1

> Request #4: Created UI page in Angular (12.1.1) and added a action to the existing CodingChallenge mvc controller to retrieve the data). 

## Setup for the existing CodingChallenge project:- 

There should be no setup required aside from downloading the source code, validating all packages are downloaded, and compiling / running the application within IIS Express. It's recommended that you use Visual Studio Community edition if you do not already have Visual Studio.

### Bug #1: Movies are not sortable
Sort functionality not impliment in the existing project, so i have modified the list of following files to fix the sorting issue.

1. DefaultController.cs:- 
	Index:- Line#29, passing the grid otpions parameter values to SearchMovies 
2. LibraryService.cs:- 
	SearchMovies :- Modified it to call the unique movies function and also to fix the sorting issue. You can refer my flag "Sai" for the code details.
	I am calling an utility to sort all the fields except Title. To sort the Title, using regular expression for case insensitive and to ignore leading articles (a/an/the). 
3. LinqUtilities.cs:-	
	OrderByField :- For sorting the list with the dynamic field name in Descending or Ascending Order. It will work for most of the field types with case sensitive. So we can re use them. If we want to sort the string with out case sensitive then we need to use linq query for the respective column.

> Note: After fixing the above issues all the unit tests wired to sort methods are pass.

### Bug #2: Remove duplicate movies

	LibraryService.cs:- 
	GetUniqueMovies :- Added to get the unique movie details. It internall calls the GetMovies to get movies and then remove duplicates with linq query.
	SearchMovies :- Modified it to call the GetUniqueMovies function. Before that it used to call the existing GetMovies function
