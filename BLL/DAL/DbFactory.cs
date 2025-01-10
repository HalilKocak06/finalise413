using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BLL.DAL
{
    public class DbFactory : IDesignTimeDbContextFactory<Db> //Scaffholidng yapmadığı için bunu kullanıyoruz.
    {
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=EZEvaluationDB;trusted_connection=true;");
            return new Db(optionsBuilder.Options);
        }
    }
}
