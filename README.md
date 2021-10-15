# VismaDevTask

 A .NET developer task given by Visma for internship 2021

## Try out the program

Currently the only available thing to try out the program would be through Visual Studio, or by going to /src/VismaDevTask and running `dotnet run` through CLI of your choice.

There is a bug with the program where it stores files, if ran through `dotnet run` it will store the files in `src\VismaDevTask\bin\Debug\net5.0`
and if you ran it through Visual Studio it will store it in `src\VismaDevTask`

### The Task

Create a console application to manage Visma’s book library using .NET5.
Requirements:

    - Command to add a new book. All the book data should be stored in a JSON file. Book model should contain the following properties:
        - Name
        - Author
        - Category
        - Language
        - Publication date
        - ISBN

    - Command to take a book from the library. The command should specify who is taking the book and for what period the book is taken. Taking the book longer than two months should not be allowed. Taking more than 3 books should not be allowed.
    
    - Command to return a book.
        - If a return is late you could display a funny message :)
    
    - Command to list all the books. Add the following parameters to filter the data:
        - Filter by author
        - Filter by category
        - Filter by language
        - Filter by ISBN
        - Filter by name
        - Filter taken or available books.
    
    - Command to delete a book.

### Time spent: I lost the time :(
