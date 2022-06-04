using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories
{
    public class RubricRepository : Repository<Rubric>
    {
        public RubricRepository(NewsContext context) : base(context)
        {

        }
    }
}
