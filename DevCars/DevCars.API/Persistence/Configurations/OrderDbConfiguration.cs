using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence.Configurations
{
    public class OrderDbConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                 .HasKey(o => o.Id);

            builder
                .HasMany(o => o.ExtraItems) // relation -- many to one 
                .WithOne() // as ExtraItems has not another relation u can put only ".WithOne()"
                .HasForeignKey(e => e.IdOrder)
                .OnDelete(DeleteBehavior.Restrict); // config the relationship between the tables Order and ExtraItems 

            builder
                .HasOne(o => o.Car)
                .WithOne() // as the relation is one car to one order u can put only ".WithOne()"
                .HasForeignKey<Order>(o => o.IdCar) // as you have a relation one to one you need to put here a foreign key based on the hierarchy between the entities
                                                    // in this case one order needs a car, but car doesn't needs an order
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
