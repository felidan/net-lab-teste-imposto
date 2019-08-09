using System;
using System.Collections.Generic;
using Imposto.Core.Domain;
using Imposto.Core.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imposto.Test
{
    [TestClass]
    public class ImpostoTest
    {
        [TestMethod]
        public void TestNotaFiscal()
        {
            NotaFiscalService nf = new NotaFiscalService();
            
            Pedido pedido = new Pedido()
            {
                NomeCliente = "Felipe Dantas",
                EstadoOrigem = "SP",
                EstadoDestino = "PE",
                ItensDoPedido = new List<PedidoItem>()
                {
                   new PedidoItem(){ NomeProduto = "Carro", CodigoProduto = "60", ValorItemPedido = 50000.00, Brinde = false },
                   new PedidoItem(){ NomeProduto = "Camiseta", CodigoProduto = "100", ValorItemPedido = 40.00, Brinde = true },
                   new PedidoItem(){ NomeProduto = "Tênis", CodigoProduto = "40", ValorItemPedido = 200.00, Brinde = false }
                }
            };

            var retorno = nf.GerarNotaFiscal(pedido, @"C:\");
            Assert.AreEqual(0, retorno.Length);
        }
        [TestMethod]
        public void TestCfop()
        {
            NotaFiscal nf = new NotaFiscal()
            {
                EstadoOrigem = "SP",
                EstadoDestino = "PE"
            };
            
            var retorno = nf.GetCfop();

            Assert.AreEqual("6.001", retorno);
        }
    }
}
