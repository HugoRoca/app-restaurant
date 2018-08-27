using APPRestaurante.Models;
using APPRestaurante.Repository.Interfaces;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace APPRestaurante.Repository.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        public bool PedidoyDetallePedido(Pedido pedido, IEnumerable<PedidoDetalle> items)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
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
