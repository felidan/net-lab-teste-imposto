using Imposto.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Core.Domain;
using System.Configuration;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();
        private string caminhoXml = ConfigurationManager.AppSettings["caminhoXmlNotaFiscal"];

        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {            
            NotaFiscalService service = new NotaFiscalService();

            GetPedidos();
            
            var criticas = service.GerarNotaFiscal(pedido, caminhoXml);

            if(criticas.Length == 0)
            {
                MessageBox.Show("Operação efetuada com sucesso", "Sucesso");
                LimparCampos();
            }
            else
                MessageBox.Show(criticas.ToString(), "Críticas");
            
        }

        private void GetPedidos()
        {
            pedido.EstadoOrigem = txtEstadoOrigem.Text.ToUpper();
            pedido.EstadoDestino = txtEstadoDestino.Text.ToUpper();
            pedido.NomeCliente = textBoxNomeCliente.Text;

            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            foreach (DataRow row in table.Rows)
            {
                bool brinde;
                Boolean.TryParse(row["Brinde"].ToString(), out brinde);
                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = brinde,
                        CodigoProduto = row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = Convert.ToDouble(row["Valor"].ToString())
                    });
            }
        }

        private void LimparCampos()
        {
            txtEstadoOrigem.Text = "";
            txtEstadoDestino.Text = "";
            textBoxNomeCliente.Text = "";
            dataGridViewPedidos.DataSource = GetTablePedidos();
        }

        private void txtEstadoDestino_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
