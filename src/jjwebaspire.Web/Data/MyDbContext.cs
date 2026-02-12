using Microsoft.EntityFrameworkCore;

namespace jjwebaspire.Web.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<SampleEntity> SampleEntities { get; set; }
    }

    public class SampleEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
