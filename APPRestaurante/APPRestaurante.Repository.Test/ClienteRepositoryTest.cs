using System;
using System.Linq;
using APPRestaurante.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APPRestaurante.Repository.Test
{
    [TestClass]
    public class ClienteRepositoryTest
    {
        private readonly IRepository<Cliente> _repository;

        public ClienteRepositoryTest()
        {
            _repository = new BaseRepository<Cliente>();
        }

        [TestMethod]
        public void Lista()
        {
            var result = _repository.GetAll();
            Assert.AreEqual(result.Count() > 0, true);
        }

        [TestMethod]
        public void Insert()
        {
            var cliente = new Cliente
            {
                nombre = "Hugo",
                apellido = "Roca",
                tipo_documento = "DNI",
                num_documento = "12345678",
                estado = true
            };
            var result = _repository.Insert(cliente);
            Assert.AreEqual(result > 0, true);
        }

        [TestMethod]
        public void update()
        {

        }
    }
}
