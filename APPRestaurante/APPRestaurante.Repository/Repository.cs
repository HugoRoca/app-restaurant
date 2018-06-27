using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using APPRestaurante.Models;
using Dapper.Contrib.Extensions;

namespace APPRestaurante.Repository
{
    public class Repository : IRepository
    {
        private readonly string _connectionString;

        public Repository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["APPRestaurante"].ConnectionString;
        }

        public bool Delete(Cliente cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Delete(cliente);
                return false;
            }
        }

        public Cliente getClientePorId(int idCliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Get<Cliente>(idCliente);
            }
        }

        public int Insert(Cliente cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return (int) connection.Insert(cliente);
            }
        }

        public IEnumerable<Cliente> Lista()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<Cliente>();
            }
        }

        public bool Update(Cliente cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Update(cliente);
            }
        }
    }
}
