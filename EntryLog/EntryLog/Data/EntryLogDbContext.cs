using EntryLog.Controllers;
using EntryLog.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntryLog.Data
{
    public class EntryLogDbContext : DbContext
    {
        public EntryLogDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        
            
        public DbSet<PeopleEntryLogs> PeopleEntryLogs { get; set; }
    }
}
