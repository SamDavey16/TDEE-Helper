using Microsoft.EntityFrameworkCore;
using WeightTracker.Helpers;
using WeightTracker.Models;
using WeightTracker.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace WeightTracker.Tests
{
    public class DatabaseHelperTests
    {
        private Context GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            return new Context(options);
        }

        [Fact]
        public async Task AddEntry_ShouldAddEntryToDatabase()
        {
            var context = GetInMemoryDbContext();
            var helper = new DatabaseHelper(context);

            var entry = new Entries { UserId = 1, TDEE = 2500 };

            await helper.AddEntry(entry);

            var added = await context.Entries.FirstOrDefaultAsync();
            Assert.NotNull(added);
            Assert.Equal(1, added.UserId);
            Assert.Equal(2500, added.TDEE);
        }

        [Fact]
        public async Task CreateNewUser_ShouldAddUserAndReturnId()
        {
            var context = GetInMemoryDbContext();
            var helper = new DatabaseHelper(context);

            var user = new Users { Name = "Alice" };

            var userId = await helper.CreateNewUser(user);

            var created = await context.Users.FirstOrDefaultAsync();
            Assert.NotNull(created);
            Assert.Equal(userId, created.Id);
            Assert.Equal("Alice", created.Name);
        }

        [Fact]
        public async Task GetCurrentTDEE_ShouldWriteTDEE_WhenUserHasEntry()
        {
            var context = GetInMemoryDbContext();
            var helper = new DatabaseHelper(context);

            context.Entries.Add(new Entries { UserId = 42, TDEE = 2300 });
            await context.SaveChangesAsync();

            using var sw = new StringWriter();
            Console.SetOut(sw);

            await helper.GetCurrentTDEE(42);

            var output = sw.ToString().Trim();
            Assert.Contains("Current TDEE for User 42: 2300", output);
        }

        [Fact]
        public async Task GetCurrentTDEE_ShouldWriteNotFound_WhenUserHasNoEntry()
        {
            var context = GetInMemoryDbContext();
            var helper = new DatabaseHelper(context);

            using var sw = new StringWriter();
            Console.SetOut(sw);

            await helper.GetCurrentTDEE(99);

            var output = sw.ToString().Trim();
            Assert.Contains("User with ID 99 not found", output);
        }
    }
}

