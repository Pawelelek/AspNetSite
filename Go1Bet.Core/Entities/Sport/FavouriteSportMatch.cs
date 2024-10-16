﻿using Go1Bet.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Sport
{
    public class FavouriteSportMatch
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public AppUser User { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } // ID користувача
        public SportMatchEntity SportMatch { get; set; }

        [ForeignKey(nameof(SportMatch))]
        public string SportMatchId { get; set; } // ID користувача
    }

}
