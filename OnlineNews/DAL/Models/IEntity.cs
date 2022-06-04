using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{ 
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
