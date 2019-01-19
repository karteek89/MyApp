using System.Data.Entity;
using MyApp.Entity.Poco;
using MyApp.Configuration.Mapping;

namespace MyApp.Configuration
{
    public class MyAppContext : DbContext
    {
        public MyAppContext()
            : base("MyAppContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        //public MyAppContext(string connectionString)
        //    : base(connectionString)
        //{
        //    this.Configuration.LazyLoadingEnabled = false;
        //    this.Configuration.ProxyCreationEnabled = false;
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserFormMap());
        }
    }
}
