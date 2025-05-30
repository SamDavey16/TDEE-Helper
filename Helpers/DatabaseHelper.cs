using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using WeightTracker.Models;
using WeightTracker.Services;

namespace WeightTracker.Helpers
{
    public class DatabaseHelper
    {
        private static readonly DbContextOptions<Context> _options;

        static DatabaseHelper()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("Default");

            _options = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public static async Task GetCurrentTDEE(int UserId)
        {
            using (var context = new Context(_options))
            {
                var userEntry = await context.Entries
                    .Where(e => e.UserId == UserId)
                    .OrderByDescending(e => e.Id)
                    .FirstOrDefaultAsync();

                if (userEntry != null)
                {
                    Console.WriteLine($"Current TDEE for User {UserId}: {userEntry.TDEE}");
                }
                else
                {
                    Console.WriteLine($"User with ID {UserId} not found.");
                }
            }
        }

        public static async Task AddEntry(Entries entry)
        {
            using (var context = new Context(_options))
            {
                context.Entries.Add(entry);
                await context.SaveChangesAsync();
            }
        }

        public static async Task<int> CreateNewUser(Users user)
        {
            using (var context = new Context(_options))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return user.Id;
            }
        }
    }
}
