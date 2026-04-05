using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TransportDBContext : DbContext
    {
        // Using the connection string defined in App.config
        public TransportDBContext() : base("name=TransportDBConn")
        {
            Database.SetInitializer<TransportDBContext>(null);
        }

        public DbSet<Routes> RoutesTable { get; set; } // Renamed the property for clarity
    }
}
