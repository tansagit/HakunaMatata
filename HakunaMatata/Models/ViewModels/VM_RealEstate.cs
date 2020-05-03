using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HakunaMatata.Models.ViewModels
{
    public class VM_RealEstate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        

        [DisplayFormat(DataFormatString ="dd/MM/yyyy")]
        public DateTime PostTime { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime? ExprireTime { get; set; }
        public int RealEstateTypeId { get; set; }
        public decimal Price { get; set; }
        public string AgentName { get; set; }
        public bool IsActive { get; set; }
    }
}
