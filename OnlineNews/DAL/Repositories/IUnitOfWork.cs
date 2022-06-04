using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repositories
{ 
    public interface IUnitOfWork
    {
        IRepository<Author> Author { get; }

        IRepository<News> News { get; }

        IRepository<Rubric> Rubric { get; }

        IRepository<Tag> Tag { get; }

        IRepository<NewsTag> NewsTag { get; }

        Task SaveChangesAsync();
    }
}
