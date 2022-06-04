using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories
{
    public class AuthorRepository : Repository<Author>
    {
        public AuthorRepository(NewsContext context) : base(context)
        {

        }
    }
}
