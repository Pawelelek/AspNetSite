using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Entities.Category
{
    [Table("tbl_Categories")]
    public class CategoryEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [DisplayName("Category")]
        //Every category have a category
        public CategoryEntity Parent { get; set; }

        [ForeignKey(nameof(Parent))]
        public string? ParentId { get; set; }
        //Foreign keys:
        public ICollection<CategoryEntity> Subcategories { get; set; } = new List<CategoryEntity>();
    }
}
