using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Author : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<News> News { get; set; }
    }
}
