using Microsoft.EntityFrameworkCore;
using JeetDonga_Practical.DAL.Entities;

namespace JeetDonga_Practical.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}