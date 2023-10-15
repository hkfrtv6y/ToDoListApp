# ToDo List App
An application built with Windows Presentation Foundation (WPF) using .NET Core 7.

The app uses Entity Framework Core as an Object-Relational Mapping (ORM) tool and follows a code-first approach, with Microsoft SQL Server as the database provider.

Allows the user to create and manipulate todo lists and tasks.

# How to run

 1. Edit `ToDoListApp.DAL.AppDbContext.cs` class and enter the correct connection string into `OnConfiguring` method. 
 2. Apply migration by entering command `update-database` in Package Manager Console (please note that `ToDoListApp.WPF` must be selected as the startup project, and `ToDoListApp.WPF' as a default project in Package Manager Console!)
 3. Run the app - it will check if the database is empty, if so, the data will be seeded automatically.

## Notes

The app doesn't offer adding/removing users, so any new users or changes in existing ones can be only done directly via the database.

## Author & License
Author: Jakub Kasperek
License: MIT