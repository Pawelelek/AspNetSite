using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Sport
{
    public class VariantSportsEvents
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }  //(гол в першому таймі, досрочна перемога в 4 раунді, нічия, пермога тої чи іншої команди)
        public string Description { get; set; }
        public double WinRate { get; set; } //Coef
        public SportMatchEntity SportMatch { get; set; }
        [ForeignKey(nameof(SportMatch))]
        public string? SportMatchId { get; set; }
    }
}
