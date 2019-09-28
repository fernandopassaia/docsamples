using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App6_Tarefa.Modelos;

namespace App6_Tarefa.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage
    {
        private byte Prioridade { get; set; }
        public Cadastro()
        {
            InitializeComponent();
        }
        public void PrioridadeSelectAction(object sender, EventArgs args)
        {
            var Stacks = SLPrioridades.Children;

            foreach (var Linha in Stacks)
            {
                Label LblPrioridade = ((StackLayout)Linha).Children[1] as Label;
                LblPrioridade.TextColor = Color.Gray;
            }

            //na minha tela todos os objetos estao com fundo CINZA, quando algum for CLICADO, passarei pra PRETO (como selecionado).
            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            FileImageSource Source = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;

            //aqui ele pega o numero da IMAGEM (p4, p3, p2), tira o P (pra ficar so o int) e depois joga no byte
            String Prioridade = Source.File.ToString().Replace("Resources/", "").Replace(".png", "").Replace("p", "");

            this.Prioridade = byte.Parse(Prioridade);
            //TxtNome.Text = Prioridade; //just for tests purpose
        }

        public void SalvarAction(object sender, EventArgs args)
        {
            bool ErroExiste = false;
            if (!(TxtNome.Text.Trim().Length > 0))
            {
                ErroExiste = true;
                DisplayAlert("ERRO", "Nome não preenchido!", "OK");
            }

            if (!(this.Prioridade > 0))
            {
                ErroExiste = true;
                DisplayAlert("ERRO", "Prioridade não foi informada!", "OK");
            }

            if (ErroExiste == false)
            {
                //Salva esses dados.
                Tarefa tarefa = new Tarefa();
                tarefa.Nome = TxtNome.Text.Trim();
                tarefa.Prioridade = this.Prioridade; //priority was get by "Click" (Pinned) on image

                new GerenciadorTarefa().Salvar(tarefa);
                //TxtNome.Text = new GerenciadorTarefa().Listagem().Count.ToString(); //just for test purpose
                App.Current.MainPage = new NavigationPage(new Inicio());
            }
        }
    }
}