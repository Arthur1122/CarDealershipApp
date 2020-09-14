using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealership.Web.API.ViewModels
{
    public class ClientViewModel
    {
        public string Name { get; set; }
        public string PasportId { get; set; }
        public List<CarViewModel> Cars { get; set; }

    }
}
