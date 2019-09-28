using App6_Tarefa.Modelos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App6_Tarefa.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            CultureInfo culture = new CultureInfo("pt-BR");
            string Data = DateTime.Now.ToString("dddd, dd {0} MMMM {0} yyyy", culture);
            DataHoje.Text = string.Format(Data, "de");
            CarregarTarefas();
        }

        private void ActionGoCadastro(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cadastro()); //push always need NavigationPage
        }

        private void CarregarTarefas()
        {
            SLTarefas.Children.Clear();

            List<Tarefa> Lista = new GerenciadorTarefa().Listagem();

            int i = 0;
            foreach (Tarefa tarefa in Lista)
            {
                LinhaStackLayout(tarefa, i); //here i call my method passing the TAREFA to add it to GridView
                i++;
            }
        }

        public void LinhaStackLayout(Tarefa tarefa, int index)
        {
            //aqui eu vou desenhar meu STACKLAYOUT do front, so que no code behind, uma vez que eh C#
            //Note que esse metodo recebe linha por linha, e vai desenhando na tela...

            #region StackCentral que contem as informacoes sobre a Tarefa, Data (basicamente a Linha / Label)
            View StackCentral = null;
            if (tarefa.DataFinalizacao == null)
            {
                StackCentral = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, Text = tarefa.Nome };
            }
            else
            {
                StackCentral = new StackLayout() { VerticalOptions = LayoutOptions.Center, Spacing = 0, HorizontalOptions = LayoutOptions.FillAndExpand };
                ((StackLayout)StackCentral).Children.Add(new Label() { Text = tarefa.Nome, TextColor = Color.Gray });
                //if my DataFinalizacao is different of NULL, i will print it on Screen
                ((StackLayout)StackCentral).Children.Add(new Label() { Text = "Finalizado em " + tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy - hh:mm") + "h", TextColor = Color.Gray, FontSize = 10 });
            }
            #endregion

            #region Crio a imagem de Exclusao (delete)
            Image Delete = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("Delete.png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                Delete.Source = ImageSource.FromFile("Resources/Delete.png");
            }
            #endregion

            #region Crio a Imagem de Prioridade e Adiciono no Grid
            Image Prioridade = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("p" + tarefa.Prioridade + ".png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                Prioridade.Source = ImageSource.FromFile("Resources/p" + tarefa.Prioridade + ".png");
            }
            #endregion

            #region Crio a Imagem Check pra adicionar na linha "Vistinho"
            Image Check = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("CheckOff.png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                Check.Source = ImageSource.FromFile("Resources/CheckOff.png");
            }
            if (tarefa.DataFinalizacao != null)
            {
                Check.Source = ImageSource.FromFile("CheckOn.png");
                if (Device.RuntimePlatform == Device.UWP)
                {
                    Check.Source = ImageSource.FromFile("Resources/CheckOn.png");
                }
            }
            #endregion

            #region Evento (Tapped / Click) do Delete
            TapGestureRecognizer DeleteTap = new TapGestureRecognizer(); //if click on DELETE button
            DeleteTap.Tapped += delegate
            {
                new GerenciadorTarefa().Deletar(index);
                CarregarTarefas();
            };
            Delete.GestureRecognizers.Add(DeleteTap);
            #endregion

            #region Evento (Tapped / Click) do Check (checked, se tarefa foi feita ou nao)
            TapGestureRecognizer CheckTap = new TapGestureRecognizer(); //if click on "CHECKED"
            CheckTap.Tapped += delegate
            {
                new GerenciadorTarefa().Finalizar(index, tarefa);
                CarregarTarefas();
            };
            Check.GestureRecognizers.Add(CheckTap);
            #endregion

            //aqui eh como se eu criasse uma STACKLAYOUT como se fosse no XML, e dentro dela adicionarei o Check (imagem), StackCentral, Prioridade e Delete
            StackLayout Linha = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 15 };

            Linha.Children.Add(Check); //minha imagem pra CHECAR a tarefa... "Vistinho"
            Linha.Children.Add(StackCentral);
            Linha.Children.Add(Prioridade);
            Linha.Children.Add(Delete);

            SLTarefas.Children.Add(Linha); //SLTarefas eh o nome do meu StackLayout
        }
    }
}