using Alicunde.PruebaTecnica.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Alicunde.PruebaTecnica.Database;

public class RetailersContext(DbContextOptions<RetailersContext> options) : DbContext(options)
{
    public DbSet<Retailer> Retailers { get; set; }
}