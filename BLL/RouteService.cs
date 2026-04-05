using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class RouteService
    {
        public List<Routes> GetAllRoutes()
        {
            using (var db = new TransportDBContext())
            {
                return db.RoutesTable
                         .OrderBy(r => r.RouteType)
                         .ThenBy(r => r.RouteCode)
                         .ToList();
            }
        }

        public Routes GetRouteById(int id)
        {
            using (var db = new TransportDBContext())
            {
                return db.RoutesTable.Find(id);
            }
        }
    }
}