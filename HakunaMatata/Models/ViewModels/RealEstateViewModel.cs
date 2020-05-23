using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakunaMatata.Models.ViewModels
{
    /// <summary>
    /// use for paging in AdminArea/RealEstate/Index2, not replace VM_RealEstate yet
    /// </summary>
    public class RealEstateViewModel
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string PostDate { get; set; }

        //public string ExpireDate { get; set; }

        public string Agent { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }
    }
}
