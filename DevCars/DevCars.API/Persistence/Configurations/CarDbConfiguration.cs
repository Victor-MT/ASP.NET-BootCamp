using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
               .HasKey(c => c.Id); // config the primary key

            /*
             modelBuilder.Entity<Car>()
                .ToTable("db_Car"); // config the name of the table... by default the DbContext use the name of the atribute
            */

            builder
             .Property(c => c.Brand)
             //.IsRequired() // setting this atributte to be required e.g. can not be null
             //.HasColumnName("Marca") // change the namo of the column in database
             .HasDefaultValue("Padrão")  // define the default value 
             .HasColumnType("VARCHAR(100)") // add the type of the column -- default NVARCHAR(MAX)
             .HasMaxLength(100); // limits the length of the atribute to 100 char

            builder
            .Property(c => c.ProductioDate)
            .HasDefaultValueSql("getdate()"); // define the default value with a sql server function
        }
    }
}
