using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDetailsViewModel(int idCar, int idCustomer, List<string> extraItems, decimal totalCost)
        {
            IdCar = idCar;
            IdCustomer = idCustomer;
            ExtraItems = extraItems;
            TotalCost = totalCost;
        }

        public int IdCar { get; set; }
        public int IdCustomer { get; set; }
        public List<string> ExtraItems { get; set; }
        public decimal TotalCost { get; set; }
    }
}
