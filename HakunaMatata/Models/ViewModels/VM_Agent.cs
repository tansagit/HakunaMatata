using HakunaMatata.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakunaMatata.Models.ViewModels
{
    public class VM_Agent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public int ActivePosts { get; set; }

        public int TotalPosts { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; }

        public bool IsConfirmedNumber { get; set; }

        public List<RealEstate> Posts { get; set; }
    }
}
