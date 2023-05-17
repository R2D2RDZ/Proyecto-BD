using Microsoft.EntityFrameworkCore;

namespace Proyecto_BD.Models
{
    public class BDContext : DbContext
    {
        public BDContext(DbContextOptions<DbContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
