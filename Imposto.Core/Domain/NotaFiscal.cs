using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }

        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }
        
        public void EmitirNotaFiscal(Pedido pedido)
        {
            List<string> estadosSudeste = new List<string>() { "SP", "RJ", "ES", "MG" };

            this.NumeroNotaFiscal = 99999;
            this.Serie = new Random().Next(Int32.MaxValue);
            this.NomeCliente = pedido.NomeCliente;
            this.EstadoDestino = pedido.EstadoDestino; 
            this.EstadoOrigem = pedido.EstadoOrigem;

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();

                notaFiscalItem.Cfop = GetCfop();

                if (this.EstadoDestino == this.EstadoOrigem)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }
                if (notaFiscalItem.Cfop == "6.009")
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido*0.90; //redução de base
                }
                else
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
                }
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms*notaFiscalItem.AliquotaIcms;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                    notaFiscalItem.ValorIpi = 0;
                }
                else
                {
                    notaFiscalItem.ValorIpi = itemPedido.ValorItemPedido * 0.10;
                }

                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                if (estadosSudeste.Contains(pedido.EstadoDestino.ToUpper()))
                    notaFiscalItem.ValorDesconto = itemPedido.ValorItemPedido * 0.10;
                else
                    notaFiscalItem.ValorDesconto = 0;

                this.ItensDaNotaFiscal.Add(notaFiscalItem);
            }
        }

        public string GetCfop()
        {
            string Cfop = "";

            if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RJ"))
            {
                Cfop = "6.000";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PE"))
            {
                Cfop = "6.001";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "MG"))
            {
                Cfop = "6.002";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PB"))
            {
                Cfop = "6.003";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PR"))
            {
                Cfop = "6.004";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PI"))
            {
                Cfop = "6.005";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RO"))
            {
                Cfop = "6.006";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "SE"))
            {
                Cfop = "6.007";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "TO"))
            {
                Cfop = "6.008";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "SE"))
            {
                Cfop = "6.009";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PA"))
            {
                Cfop = "6.010";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "RJ"))
            {
                Cfop = "6.000";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PE"))
            {
                Cfop = "6.001";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "MG"))
            {
                Cfop = "6.002";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PB"))
            {
                Cfop = "6.003";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PR"))
            {
                Cfop = "6.004";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PI"))
            {
                Cfop = "6.005";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "RO"))
            {
                Cfop = "6.006";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "SE"))
            {
                Cfop = "6.007";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "TO"))
            {
                Cfop = "6.008";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "SE"))
            {
                Cfop = "6.009";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PA"))
            {
                Cfop = "6.010";
            }
            return Cfop;
        }
    }
}
