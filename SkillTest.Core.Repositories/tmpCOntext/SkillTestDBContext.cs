using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTest.Core.Domain.Entity;

namespace SkillTest.Core.Repositories.tmpCOntext
{
    public  class SkillTestDBContext : DbContext
    {
        public SkillTestDBContext(DbContextOptions<SkillTestDBContext> options) : base(options)
        { }

        public DbSet<AppUser> Books { get; set; }
    }
}
