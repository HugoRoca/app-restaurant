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
        public int Count(string desde, string hasta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("SELECT count(*) FROM MenuDetalle md inner join Menu m on m.id = md.idMenu WHERE md.estado = 1 and CONVERT(DATE, m.fecha) <= '" + hasta + "' AND CONVERT(DATE, m.fecha) >= '" + desde + "'");
            }
        }

        public int EditarMenu(Menu menu)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@idDetalle", menu.idDetalle);
                parameters.Add("@titulo", menu.titulo);
                parameters.Add("@descripcion", menu.descripcion);
                parameters.Add("@tipo", menu.tipo);
                parameters.Add("@precio", menu.precio);
                parameters.Add("@foto", menu.foto);

                var result = 0;
                connection.Query<int>("Menu_Editar_SP", parameters, commandType: CommandType.StoredProcedure);
                result = 1;

                return result;
            }
        }

        public int EliminarMenu(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@idDetalle", id);

                var result = 0;
                connection.Query<int>("Menu_Eliminar_SP", parameters, commandType: CommandType.StoredProcedure);
                result = 1;

                return result;
            }
        }

        public int InsertarMenu(Menu menu)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@fecha", menu.fechaMenu);
                parameters.Add("@idUsuario", menu.idUsuario);
                parameters.Add("@titulo", menu.titulo);
                parameters.Add("@descripcion", menu.descripcion);
                parameters.Add("@tipo", menu.tipo);
                parameters.Add("@precio", menu.precio);
                parameters.Add("@foto", menu.foto);

                var result = 0;
                connection.Query<int>("Menu_Insertar_SP", parameters, commandType: CommandType.StoredProcedure);
                result = 1;

                return result;
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

        public Menu ObtenerMenu(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                return connection.QueryFirstOrDefault<Menu>("Menu_Obtener_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
