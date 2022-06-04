using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class NewsTag : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
