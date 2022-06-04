using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class News : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int RubricId { get; set; }

        public Author Author { get; set; }
        public Rubric Rubric { get; set; }

        public virtual ICollection<NewsTag> Tags { get; set; }
    }
}
