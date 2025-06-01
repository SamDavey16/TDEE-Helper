using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightTracker.Models;

namespace WeightTracker.Interfaces
{
    public interface IDatabaseHelper
    {
        Task GetCurrentTDEE(int userId);
        Task AddEntry(Entries entry);
        Task<int> CreateNewUser(Users user);
    }
}
