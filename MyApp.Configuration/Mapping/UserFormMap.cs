using System;
using MyApp.Entity.Poco;
using System.Data.Entity.ModelConfiguration;

namespace MyApp.Configuration.Mapping
{
    public class UserFormMap : EntityTypeConfiguration<UserForm>
    {
        public UserFormMap()
        {
            // Map table
            ToTable("[dbo].[UserForm]");

            // Map primary key
            HasKey(x => x.Id);

            // Map other properties
            Property(x => x.Id);
            Property(x => x.UserId);
            Property(x => x.Field1);
            Property(x => x.Field2);
            Property(x => x.Field3);
            Property(x => x.Field4);
            Property(x => x.Field5);
            Property(x => x.Field6);
            Property(x => x.Field7);
            Property(x => x.Field8);
            Property(x => x.Field9);
            Property(x => x.Field10);
            Property(x => x.IsActive);
            Property(x => x.CreatedOn);

        }
    }
}
