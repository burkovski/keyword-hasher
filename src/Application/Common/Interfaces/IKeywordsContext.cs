using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IKeywordsContext
    {
        DbSet<Keyword> Keywords { get; set; }
    }
}
