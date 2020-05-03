using HakunaMatata.Models.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HakunaMatata.Models.ViewModels
{
    public class VM_RealEstateDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int Acreage { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime PostTime { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime? LastUpdate { get; set; }

        public string ExprireTime { get; set; }
        public int RoomNumber { get; set; }
        public string Description { get; set; }
        public string AgentName { get; set; }
        public List<string> ImageUrls { get; set; }
        public bool HasPrivateWc { get; set; }
        public bool HasMezzanine { get; set; }
        public bool AllowCook { get; set; }
        public bool FreeTime { get; set; }
        public bool SecurityCamera { get; set; }
        public decimal? WaterPrice { get; set; }
        public decimal? ElectronicPrice { get; set; }
        public decimal? WifiPrice { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longtitude { get; set; }
        public int? RealEstateTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool isFreeWater { get; set; }
        public bool isFreeElectronic { get; set; }
        public bool isFreeWifi { get; set; }

        [NotMapped]
        public List<IFormFile> Files { get; set; }

    }
}
