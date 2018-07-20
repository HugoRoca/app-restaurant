using APPRestaurante.Models;
using APPRestaurante.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace APPRestaurante.Repository.Repositories
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public int Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("select count(id) from menudetalle where estado = 1");
            }
        }

        public IEnumerable<Menu> ListaMenuPaginacion(DateTime desde, DateTime hasta, int startRow, int endRow)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@desde", desde);
                parameters.Add("@hasta", hasta);
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);

                return connection.Query<Menu>("menu_lista_sp", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
