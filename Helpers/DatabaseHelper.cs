using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using WeightTracker.Interfaces;
using WeightTracker.Models;
using WeightTracker.Services;

namespace WeightTracker.Helpers
{
    public class DatabaseHelper :IDatabaseHelper
    {
        private readonly Context _context;

        public DatabaseHelper(Context context)
        {
            _context = context;
        }

        public async Task GetCurrentTDEE(int UserId)
        {
            
            var userEntry = await _context.Entries
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

        public async Task AddEntry(Entries entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CreateNewUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}
