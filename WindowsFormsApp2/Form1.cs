using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Marco"].ConnectionString);
        public void ListaAnios()
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaAnios",cn))
            {

                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    using (DataSet df = new DataSet())
                    {


                        da.Fill(df,"ListaAnios");
                        CboAnios.DataSource = df.Tables["ListaAnios"];
                        CboAnios.DisplayMember = "Anios";
                        CboAnios.ValueMember = "Anios";
                    }
                }
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'neptunoDataSet.clientes' Puede moverla o quitarla según sea necesario.
            //this.clientesTableAdapter.Fill(this.neptunoDataSet.clientes);
            ListaAnios();
        }

        private void CboAnios_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("Usp_Lista_Pedidos_Anios", cn)) {

                using (SqlDataAdapter Da = new SqlDataAdapter()) {

                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.AddWithValue("@anio",CboAnios.SelectedValue);

                    using (DataSet df = new DataSet()) {
                        Da.Fill(df,"Pedidos");
                        DgPedidos.DataSource = df.Tables["Pedidos"];
                        LblNumero.Text = df.Tables["Pedidos"].Rows.Count.ToString();
                    }
                }
            }

        }

        private void LblNumero_Click(object sender, EventArgs e)
        {

        }

        private void DgPedidos_DoubleClick(object sender, EventArgs e)
        {
            int Codigo;
            Codigo = Convert.ToInt32(DgPedidos.CurrentRow.Cells[0].Value);
            using (SqlCommand cmd = new SqlCommand("Usp_Detalle_Pedido",cn)) {

                using (SqlDataAdapter Da = new SqlDataAdapter()) {

                    //QUITAR USING
                  //  DataTable df = new DataTable()
                    //    da.Fill(df);
                    //comboanios.DataSource = df;

                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.AddWithValue("@idpedido", Codigo);
                    using (DataSet df = new DataSet()) {
                        Da.Fill(df,"Detalles");
                        DgDetalle.DataSource = df.Tables["Detalles"];
                        LblMonto.Text = df.Tables["Detalles"].Compute("Sum(Monto)", "").ToString();
                    }
                }
            }

        }
    }
}
