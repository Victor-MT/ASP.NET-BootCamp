using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Car
    {
        protected Car() { }
        public Car( int vinCode, string brand, string model, int year, decimal price, string color, DateTime productioDate, CarStatusEnum status, DateTime registeredAt)
        {
           
            VinCode = vinCode;
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
            Color = color;
            ProductioDate = productioDate;
            Status = CarStatusEnum.Available;
            RegisteredAt = DateTime.Now;
        }

        public int Id { get; private set; }
        public int VinCode { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public bool IsAutomatic { get; set; }
        public decimal Price { get; private set; }
        public string Color { get; private set; }
        public DateTime ProductioDate { get; private set; }
        public CarStatusEnum Status { get; private set; }
        public DateTime RegisteredAt { get; private set; }


    }
}
