using Net_ProgAsync.CLDemo;
using System;
using System.Windows.Forms;

namespace Net_ProgAsync.WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnExecutar_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Evento do botão foi iniciado - Inicio.");

            Exemplo oProgAsync = new Exemplo();
            await oProgAsync.Task_LongaDuracao();

            Console.WriteLine("Evento do botão foi concluído - Fim.");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = "Timer funcionando sem Travar: " + DateTime.Now.ToString("hh:mm:ss");
        }

        private async void btnExecutarXMLDataBase_Click(object sender, EventArgs e)
        {
            SampleXMLInSQL sample = new SampleXMLInSQL();
            await sample.Task_LongaDuracao();


        }
    }
}