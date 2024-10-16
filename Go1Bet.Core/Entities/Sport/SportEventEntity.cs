﻿using Go1Bet.Core.Entities.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Sport
{
    public class SportEventEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
        public CategoryEntity Parent { get; set; }

        [ForeignKey(nameof(Parent))]
        public string? ParentId { get; set; }
        //Foreign keys:
        public ICollection<SportMatchEntity> SportMatches { get; set; } = new List<SportMatchEntity>();
    }
}
