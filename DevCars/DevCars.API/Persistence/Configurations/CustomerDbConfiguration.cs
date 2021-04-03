using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence.Configurations
{
    public class CustomerDbConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
               .HasKey(c => c.Id);

            builder
                .HasMany(c => c.Orders) // relation -- many to one 
                .WithOne(o => o.Customer) // each order has one costumer 
                .HasForeignKey(o => o.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict); // config the relationship between the tables Customer and Order

        }
    }
}
