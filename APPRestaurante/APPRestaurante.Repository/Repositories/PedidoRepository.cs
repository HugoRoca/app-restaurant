using APPRestaurante.Models;
using APPRestaurante.Repository.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace APPRestaurante.Repository.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        public IEnumerable<DetalleCobranzaResult> detalleCobranzaResults(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return connection.Query<DetalleCobranzaResult>("Cobranza_Detalle_SP", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Pedido> lista()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Pedido>("select * from pedido order by 2 desc");
            }
        }

        public IEnumerable<DetalleCobranzaResult> LoMasPedido()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<DetalleCobranzaResult>("Inicio_LoMasPedido_SP");
            }
        }

        public IEnumerable<Pedido> PedidosPorFecha()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@fecha", DateTime.Now);
                return connection.Query<Pedido>("Incio_ListaPedidos_SP", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public bool PedidoyDetallePedido(int mesa, IEnumerable<PedidoDetalle> items)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        Pedido pedido = new Pedido();
                        var total = 0.0;
                        foreach (var item in items)
                        {
                            total = total + Convert.ToDouble(item.subtotal);
                        }

                        pedido.fecha = DateTime.Now;
                        pedido.total = total;
                        pedido.nombres = "";
                        pedido.mesa = mesa;
                        pedido.tipoPago = "";
                        pedido.idEmpleado = 1;
                        pedido.idUsuario = 1;
                        pedido.estado = 1;

                        var id = (int)connection.Insert(pedido, transaction);
                        foreach (var pedidoItem in items)
                        {
                            pedidoItem.idPedido = id;
                            connection.Insert(pedidoItem, transaction);
                        }
                        transaction.Commit();
                        return true;

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
