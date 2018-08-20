using APPRestaurante.Models;
using APPRestaurante.Repository.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APPRestaurante.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public IEnumerable<Usuario> ListaUsuario()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>("Usuario_Lista_SP", null, commandType: CommandType.StoredProcedure);
            }
        }

        public Usuario ObtenerUsuario(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return connection.QueryFirstOrDefault<Usuario>("Usuario_Obtener_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public int RegistrarUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@usuario", usuario.usuario);
                parameters.Add("@clave", usuario.clave);
                parameters.Add("@idempleado", usuario.idEmpleado);
                parameters.Add("@idrol", usuario.idRol);
                var result = 0;
                connection.Query<int>("Usuario_Insertar_SP", parameters, commandType: CommandType.StoredProcedure);
                result = 1;
                return result;
            }
        }

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
