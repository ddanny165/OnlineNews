using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories
{
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(NewsContext context) : base(context)
        {

        }
    }
}
