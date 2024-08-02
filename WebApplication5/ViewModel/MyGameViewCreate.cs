using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication5.Model;

namespace WebApplication5.ViewModel
{
    public class MyGameViewCreate
    {
        public  string Name { get; set; }

        public  string Description { get; set; }
        public decimal Price { get; set; }

        public  IFormFile Image { get; set; }
        public IEnumerable<SelectListItem> CategoriesList{ get; set; }
        public IEnumerable<SelectListItem> DevicesList { get; set; }

        public  List<int> Devices { get; set; }

        public  List<int> Categories { get; set; }

    }
}
