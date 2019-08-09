using Imposto.Core.DB;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        SqlConnection db = new SqlConnection();
        public NotaFiscalRepository(DbNetshoesConnection db)
        {
            this.db = db.Conectar();
        }
        public void PersistirNotaFiscal(NotaFiscal notaFiscal)
        {
            var id = SalvarNotaFiscal(notaFiscal);
            if (id == -1)
                return;

            SalvarNotaFiscalItem(notaFiscal.ItensDaNotaFiscal, id);
        }

        private int SalvarNotaFiscal(NotaFiscal notaFiscal)
        {
            int id = -1;
            
            SqlCommand command = new SqlCommand("[dbo].[P_NOTA_FISCAL]", db);

            command.CommandType = CommandType.StoredProcedure;
            SqlParameter paramRetorno = command.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
            command.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int)).Value = 0;
            command.Parameters.Add(new SqlParameter("@pNumeroNotaFiscal", SqlDbType.Int)).Value = notaFiscal.NumeroNotaFiscal;
            command.Parameters.Add(new SqlParameter("@pSerie", SqlDbType.Int)).Value = notaFiscal.Serie;
            command.Parameters.Add(new SqlParameter("@pNomeCliente", SqlDbType.VarChar)).Value = notaFiscal.NomeCliente;
            command.Parameters.Add(new SqlParameter("@pEstadoDestino", SqlDbType.VarChar)).Value = notaFiscal.EstadoDestino;
            command.Parameters.Add(new SqlParameter("@pEstadoOrigem", SqlDbType.VarChar)).Value = notaFiscal.EstadoOrigem;
            paramRetorno.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            id = (int)command.Parameters["@pId"].Value;
            
            notaFiscal.Id = id;
            return id;
        }
        private void SalvarNotaFiscalItem(List<NotaFiscalItem> notaFiscalItem, int id)
        {
            foreach (var item in notaFiscalItem)
            {
                SqlCommand command = new SqlCommand("[dbo].[P_NOTA_FISCAL_ITEM]", db);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int)).Value = 0;
                command.Parameters.Add(new SqlParameter("@pIdNotaFiscal", SqlDbType.Int)).Value = item.IdNotaFiscal;
                command.Parameters.Add(new SqlParameter("@pCfop", SqlDbType.VarChar)).Value = item.Cfop;
                command.Parameters.Add(new SqlParameter("@pTipoIcms", SqlDbType.VarChar)).Value = item.TipoIcms;
                command.Parameters.Add(new SqlParameter("@pBaseIcms", SqlDbType.Decimal)).Value = item.BaseIcms;
                command.Parameters.Add(new SqlParameter("@pAliquotaIcms", SqlDbType.Decimal)).Value = item.AliquotaIcms;
                command.Parameters.Add(new SqlParameter("@pValorIcms", SqlDbType.Decimal)).Value = item.ValorIcms;
                command.Parameters.Add(new SqlParameter("@pNomeProduto", SqlDbType.VarChar)).Value = item.NomeProduto;
                command.Parameters.Add(new SqlParameter("@pCodigoProduto", SqlDbType.VarChar)).Value = item.CodigoProduto;
                command.Parameters.Add(new SqlParameter("@ValorIpi", SqlDbType.Decimal)).Value = item.ValorIpi;
                command.Parameters.Add(new SqlParameter("@ValorDesconto", SqlDbType.Decimal)).Value = item.ValorDesconto;
                command.ExecuteNonQuery();
         
            }
        }
    }
}
