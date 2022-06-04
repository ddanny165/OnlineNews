using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Rubric : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<News> News { get; set; }
    }
}
