using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Services
{
    public class CustomerServices
    {
        private readonly DevCarsDbContext _dbContext;
        public CustomerServices(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OrderDetailsViewModel GetOrder(int id, int orderId)
        {
            var order = _dbContext.Orders
                .Include(o => o.ExtraItems)
                .SingleOrDefault(o => o.Id == orderId);
            
            if (order == null)
                return null;

            var extraItems = order.ExtraItems
                .Select(e => e.Description)
                .ToList();

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCustomer, extraItems, order.TotalCost);

            return orderViewModel;
        }


        public Customer RegisterCustomer(AddCustomerInputModel model)
        {
            Customer customer = new Customer(model.FullName, model.Document, model.BirthDate);
            
            _dbContext.Add(customer);
            _dbContext.SaveChanges();

            return customer;
        }

        public Order RegisterOrder(int id,AddOrderInputModel model)
        {
            var extraItems = model.ExtraItems
                .Select(e => new ExtraOrderItem(e.Description, e.Price))
                .ToList();

            var car = _dbContext.Cars
                .SingleOrDefault(c => c.Id == model.IdCar);

            Order order = new Order(model.IdCar, model.IdCustormer, car.Price, extraItems);
            
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            
            return order;

        }
    }
}
