using APPRestaurante.Models;
using APPRestaurante.Repository.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Repository.Repositories
{
    public class PermisoRepository : BaseRepository<Permiso>, IPermisoRepository
    {
        public IEnumerable<Permiso> ObtenerPermisosDeAcceso(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@idUsuario", id);
                return connection.Query<Permiso>("Usuario_ListarOpcionesAcceso_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
