using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories
{
    public class NewsRepository : Repository<News>
    {
        public NewsRepository(NewsContext context) : base(context)
        {

        }
    }
}
