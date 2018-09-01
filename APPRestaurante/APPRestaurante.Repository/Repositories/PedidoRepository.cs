﻿using APPRestaurante.Models;
using APPRestaurante.Repository.Interfaces;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace APPRestaurante.Repository.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
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
                        pedido.idEmpleado = 0;
                        pedido.idUsuario = 0;
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
