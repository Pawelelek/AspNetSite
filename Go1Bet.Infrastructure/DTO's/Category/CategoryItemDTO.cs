﻿using Go1Bet.Infrastructure.DTO_s.Sport.SportEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.DTO_s.Category
{
    public class CategoryItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string? ParentId { get; set; }
        public string ParentName { get; set; }
        public int countSubcategories { get; set; }
        public int countSportEvents { get; set; }
        public List<CategoryItemDTO> Subcategories { get; set; }
        public List<SportEventItemDTO> SportEvents { get; set; }
    }
}
