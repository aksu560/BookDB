# BookDB
Small project for a job interview.

Built from the VS2022 Asp.NET Core API template, and uses Entity Framework Core for ORM.

Running the project should be easy, just open the solution in VS, and run.

## Some Notes
There is a commented PUT request functionality in the BooksController. It was not in spec, but if one wishes to enable the functionality, simply uncomment the function.

Swagger configuration is the default Swashbuckle configuration, so you will get minimal, to no explanation on your request beyond the code.

The checks for duplicate books in DB are case sensitive. I did look into changing this, but ended up cutting that exploration short when it started to take a while. The functionality shouldnt be too difficult to add in though, from what I know.