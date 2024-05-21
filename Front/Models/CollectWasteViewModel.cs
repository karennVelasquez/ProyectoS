using SGRA2._0.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SGRA2._0.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Front.Models
{
    public class CollectWasteViewModel
    {
        public CollectWasteViewModel()
        {
            Suppliers = new List<SelectListItem>();
            Person = new List<SelectListItem>();
            Composter = new List<SelectListItem>();
        }
        [DisplayName("Id")]
        public int IdCollectWaste { get; set; }


        public int IdSuppliers { get; set; }
        public IEnumerable<SelectListItem> Suppliers { get; set; }
        [DisplayName("Suppliers")]
        public int IdPerson { get; set; }
        public IEnumerable<SelectListItem> Person { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }



        [DisplayName("Composter")]
        public int IdComposter { get; set; }

        public IEnumerable<SelectListItem> Composter { get; set; }
        public string Material { get; set; }

        public DateTime CollectionDate { get; set; }
        public int Amount { get; set; }
        public bool IsDelete { get; set; }
    }
}