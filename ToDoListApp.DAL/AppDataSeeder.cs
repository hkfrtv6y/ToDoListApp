using ToDoListApp.DAL.Entities;

namespace ToDoListApp.DAL;

public static class AppDataSeeder
{
    public static void Seed(AppDbContext dbContext)
    {
        if (!dbContext.Users.Any())
        {
            dbContext.Users.Add(new User
            {
                FirstName = "John",
                LastName = "Smith",
            });
            dbContext.SaveChanges();
        }
    }

    private static void SeedUsers(AppDbContext dbContext)
    {

    }

    private static void SeedTasks(AppDbContext dbContext)
    {
        List<ToDoList> lists = new()
        {
            new ToDoList()
            {
                Id = 1,
                UserId = 1,
                Name = "sample",
                Created = DateTime.Now,
                Tasks = new List<ToDoTask>
                {
                    new ToDoTask()
                {
                    Id = 1,
                    ToDoListId = 1,
                    CreateTime = DateTime.Now,
                    Name = "task 1"
                },
                    new ToDoTask()
                    {
                        Id = 2,
                        ToDoListId = 1,
                        CreateTime = DateTime.Now,
                        Name = "task 2"
                    }
                }
            },

            new ToDoList()
            {
                Id = 2,
                UserId = 2,
                Name = "test",
                Created = DateTime.Now,
                Tasks = new List<ToDoTask>
                {
                    new ToDoTask()
                {
                    Id = 1,
                    ToDoListId = 2,
                    CreateTime = DateTime.Now,
                    Name = "test task 1"
                },
                    new ToDoTask()
                    {
                        Id = 2,
                        ToDoListId = 2,
                        CreateTime = DateTime.Now,
                        Name = "test task 2"
                    },
                    new ToDoTask()
                    {
                        Id = 3,
                        ToDoListId = 2,
                        CreateTime = DateTime.Now,
                        Name = "test task 3"
                    }
                }
            }

        };
        dbContext.Lists.AddRange(lists);
    }

}