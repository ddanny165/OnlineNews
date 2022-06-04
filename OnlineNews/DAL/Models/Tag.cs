using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Tag : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<NewsTag> Tags { get; set; }
    }
}
