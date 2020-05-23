﻿using System;
using System.Collections.Generic;

namespace HakunaMatata.Models.DataModels
{
    public partial class RealEstate
    {
        public RealEstate()
        {
            Picture = new HashSet<Picture>();
            Rating = new HashSet<Rating>();
        }

        public int Id { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? ExprireTime { get; set; }
        public int? RealEstateTypeId { get; set; }
        public int? AgentId { get; set; }

        /// <summary>
        /// so dien thoai lien he, co the khac Agent.Number vi crawl data
        /// </summary>
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual RealEstateType ReaEstateType { get; set; }
        public virtual Map Map { get; set; }
        public virtual RealEstateDetail RealEstateDetail { get; set; }
        public virtual ICollection<Picture> Picture { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
    }
}
