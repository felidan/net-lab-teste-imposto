using Imposto.Core.Data;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        NotaFiscalRepository repository = new NotaFiscalRepository(new DB.DbNetshoesConnection());
        public StringBuilder GerarNotaFiscal(Pedido pedido, string caminhoXml)
        {
            var criticas = ValidarNotaFiscal(pedido);
            if (criticas.Length > 0)
                return criticas;

            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);
            SalvarXml(notaFiscal, caminhoXml);
            repository.PersistirNotaFiscal(notaFiscal);

            return new StringBuilder();
        }

        private void SalvarXml(NotaFiscal notaFiscal, string caminhoXml)
        {
            XmlSerializer serializador = new XmlSerializer(notaFiscal.GetType());
            StreamWriter stream = new StreamWriter(caminhoXml + String.Format("NotaFiscal-{0}.xml", notaFiscal.NomeCliente));
            serializador.Serialize(stream, notaFiscal);
            stream.Close();
        }

        private StringBuilder ValidarNotaFiscal(Pedido pedido)
        {
            StringBuilder criticas = new StringBuilder();
            List<string> estadosDisponiveis = new List<string>()
            {
                "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
            };

            if (String.IsNullOrEmpty(pedido.NomeCliente))
                criticas.AppendLine("- O campo Nome é obrigatório");

            if (String.IsNullOrEmpty(pedido.EstadoOrigem))
                criticas.AppendLine("- O estado de origem é obrigatório");

            else if(!estadosDisponiveis.Contains(pedido.EstadoOrigem.ToUpper()))
                criticas.AppendLine("- Escolha um estado de origem válido");

            if (String.IsNullOrEmpty(pedido.EstadoDestino))
                criticas.AppendLine("- O estado de destino é obrigatório");

            else if (!estadosDisponiveis.Contains(pedido.EstadoDestino.ToUpper()))
                criticas.AppendLine("- Escolha um estado de destino válido");
            
            return criticas;
        }
    }
}
