using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakunaMatata.Models.ViewModels
{
    public class VM_RealEstate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime? ExprireTime { get; set; }
        public int RealEstateTypeId { get; set; }
        public decimal Price { get; set; }
        public string AgentName { get; set; }
        public bool IsActive { get; set; }
    }
}
