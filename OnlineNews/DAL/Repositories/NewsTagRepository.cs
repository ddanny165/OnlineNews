using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories
{
    public class NewsTagRepository : Repository<NewsTag>
    {
        public NewsTagRepository(NewsContext context) : base(context)
        {

        }
    }
}
