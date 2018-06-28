using System;
using System.Linq;
using APPRestaurante.Models;
using APPRestaurante.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APPRestaurante.Repository.Test
{
    [TestClass]
    public class ClienteRepositoryTest
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteRepositoryTest()
        {
            _unitOfWork = new AppRestauranteUnitOfWork();
        }

        [TestMethod]
        public void Lista()
        {
            var result = _unitOfWork.Clientes.GetAll();
            Assert.AreEqual(result.Count() > 0, true);
        }

        [TestMethod]
        public void ObtenerPorId()
        {
            var result = _unitOfWork.Clientes.GetEntitybyId(5);
            Assert.AreEqual(result != null, true);
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
            var result = _unitOfWork.Clientes.Insert(cliente);
            Assert.AreEqual(result > 0, true);
        }

        [TestMethod]
        public void update()
        {
            var cliente = new Cliente
            {
                id = 4,
                nombre = "Hugo",
                apellido = "Roca",
                tipo_documento = "DNI",
                num_documento = "12345678",
                estado = false
            };
            Assert.AreEqual(_unitOfWork.Clientes.Update(cliente), true);
        }

        [TestMethod]
        public void Delete()
        {
            var cliente = new Cliente
            {
                id = 4,
                nombre = "Hugo",
                apellido = "Roca",
                tipo_documento = "DNI",
                num_documento = "12345678",
                estado = false
            };
            Assert.AreEqual(_unitOfWork.Clientes.Delete(cliente), true);
        }

    }
}
