using System.Threading.Tasks;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Author> Author { get; }

        public IRepository<News> News { get; }

        public IRepository<Tag> Tag { get; }

        public IRepository<Rubric> Rubric { get; }

        public IRepository<NewsTag> NewsTag { get; }

        private readonly NewsContext _context;

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public UnitOfWork(IRepository<Author> employees, IRepository<News> teams,
            IRepository<Tag> statuses, IRepository<Rubric> grindstones,
            IRepository<NewsTag> asignments, NewsContext context)
        {
            _context = context;
            Author = employees;
            News = teams;
            Tag = statuses;
            Rubric = grindstones;
            NewsTag = asignments;
        }
    }
}
