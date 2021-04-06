using Dapper;
using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Services
{
    public class CarServices
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly string _connectionString;
        public CarServices(DevCarsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            //_connectionString = dbContext.Database.GetDbConnection().ConnectionString; // another way to get connectionString, but in this way when you use the Inmemory database function it gonna throw an exception
            _connectionString = configuration.GetConnectionString("DevCarsCs");
        }

        public List<CarItemViewModel> GetAllCars()
        {
            // using Entity Framework Core
            //List<CarItemViewModel> carsListResponse = _dbContext.Cars
            //    .Where(c => c.Status == CarStatusEnum.Available)
            //    .Select(c => new CarItemViewModel(c.Id,c.Brand,c.Model,c.Price))
            //    .ToList();
            
            // using dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT Id, Brand, Model, Price WHERE Status = 0";
                var carsListResponse = sqlConnection.Query<CarItemViewModel>(query).ToList();
                return carsListResponse;
            }
                
        }

        public CarDetailsViewModel GetCarById(int id)
        {
            var selectedCar = _dbContext.Cars
                .SingleOrDefault(c => c.Id == id);

            if (selectedCar == null)
                return null;

            var response = new CarDetailsViewModel()
            {
                Id = selectedCar.Id,
                Brand = selectedCar.Brand,
                Model = selectedCar.Model,
                VinCode = selectedCar.VinCode,
                Year = selectedCar.Year,
                Price = selectedCar.Price,
                ProductionDate = selectedCar.ProductioDate,
                Color = selectedCar.Color
            };

            return response;
        }

        public Car RegisterCar(AddCarInputModel model)
        {
            Car newCar = new Car(model.VinCode, model.Brand, model.Model, model.Year, model.Price,model.Color, model.ProductionDate);
            _dbContext.Cars.Add(newCar);
            _dbContext.SaveChanges();

            return newCar;
        }

        public bool UpdateCar(int id,UpdateCarInputModel model)
        {
            var car = _dbContext.Cars
                .SingleOrDefault(c => c.Id == id); 
            
            if (car == null)
                return false;

            car.Update(model.Color, model.Price);
            //_dbContext.SaveChanges(); // using Entity Framework Core

            // using dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Cars SET Color = @color, Price = @price WHERE Id = @id";
                sqlConnection.Execute(query, new { color = car.Color, price = car.Price, id = car.Id });
            }
            return true;
        }

        public bool Delete(int id)
        {
            var car = _dbContext.Cars
                   .SingleOrDefault(c => c.Id == id);

            if (car == null)
                return false;

            car.SetAsSuspended();
            _dbContext.SaveChanges();

            return true;

        }

    }
}
