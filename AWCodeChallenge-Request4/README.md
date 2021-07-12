# AWCodeChallengeRequest4

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 12.1.1.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

## Modified Object Details

 1. Created movie-library.component for displaying the movie details. 
	  $\AWCodeChallenge-Request4\src\app\movie-library

 2. Added Angular Grid which will handle sort, paging, search without server calls. I added default features. We can expand it with advanced features.
 
 3. Created GetData method in DefaultController in existing codechallenge application and allowing it as cross site so that we can call it from angular.

 4. Created AllowCrossSiteAttribute to decorate it for GetData to handle the cross site (CORS policy)

 5. movie-library.component will call the above method as http get to get the data and display in Angular Grid. 

 6. Run the existing Porject "aw-coding-challenge-main" before running the ng serve
 
 7. You can see there is a URL in movie-library.component for the above service