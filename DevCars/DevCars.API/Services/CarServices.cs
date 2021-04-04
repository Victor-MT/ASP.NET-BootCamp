using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Services
{
    public class CarServices
    {
        private readonly DevCarsDbContext _dbContext;
        public CarServices(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CarItemViewModel> GetAllCars()
        {
            List<CarItemViewModel> carsListResponse = _dbContext.Cars
                .Select(c => new CarItemViewModel(c.Id,c.Brand,c.Model,c.Price))
                .ToList();

            return carsListResponse;
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
            _dbContext.SaveChanges();

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
