using APPRestaurante.Models;
using APPRestaurante.Repository.Interfaces;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace APPRestaurante.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public Usuario ValidarUsuario(string usuario, string clave)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var paramatro = new DynamicParameters();
                paramatro.Add("@usuario", usuario);
                paramatro.Add("@clave", clave);
                return connection.QueryFirstOrDefault<Usuario>("Usuario_Validar_SP", paramatro, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
