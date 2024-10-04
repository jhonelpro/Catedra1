using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Models;

namespace api.src.Data
{
    public class Seeder
    {
        public static async Task Seed(DataContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var Genders = new List<string> {"masculino" , "femenino", "otro" , "prefiero no decirlo"};
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    Rut = $"{random.Next(10000000, 99999999)}-{random.Next(0, 9)}",
                    Name = $"User {i}",
                    Email = $"User{i}@gmail.com",
                    Gender = Genders[random.Next(0, Genders.Count)],
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-random.Next(18, 60)))
                };

                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }
    }
}