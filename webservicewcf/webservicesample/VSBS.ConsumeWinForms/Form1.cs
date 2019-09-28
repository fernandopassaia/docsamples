using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace VSBS.ConsumeWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            MeuServico.MeuServico servico = new MeuServico.MeuServico();
            var listaMontadoras = servico.ListaDeMontadoras().ToList();
            DataTable dtData = new DataTable(); //just to put the data and put then to grid
            dtData.Columns.Add("id");
            dtData.Columns.Add("name");

            listaMontadoras.ForEach(item =>
            {
                DataRow DR = dtData.NewRow();
                DR["id"] = item.Id.ToString();
                DR["name"] = item.Name;
                dtData.Rows.Add(DR);
                DR = null;
            });
            dgvMontadoras.DataSource = dtData;
            dgvMontadoras.Refresh();
        }
    }
}
