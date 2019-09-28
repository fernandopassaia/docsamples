using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Service.Models;
using App01_ConsultarCEP.Service;

namespace App01_ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage //contentPage is the principal structure on XAML File
    {
        public MainPage()
        {
            InitializeComponent();


            //note: this components becames variables in the Code-Behind when i use x:Name and give a name in XAML
            btnBuscar.Clicked += BuscarCep; //i call the event
        }

        //Note: Different from Windows Forms, where i do double-click in a button
        //and it creates the "Click" event, here i have to do it by my own hands
        private void BuscarCep(object sender, EventArgs args)
        {
            if (isValidCep(tbxCep.Text.Trim()))
            {
                try
                {
                    //TODO: Validations, ViaCepServico, Return to Label
                    Address adr = ViaCepService.FindAddressByCep(tbxCep.Text.Trim());

                    if (adr == null)
                    {
                        DisplayAlert("ERRO", "O endereco nao foi localizado para o CEP informado: " + tbxCep.Text, "OK");
                    }
                    else
                    {
                        lblResultado.Text = string.Format("Endereco: {3}, {2}, {0}, {1}", adr.localidade, adr.uf, adr.logradouro, adr.bairro);
                    }
                }
                catch(Exception error)
                {
                    DisplayAlert("ERRO CRITICO", error.Message, "OK");
                }
            }
        }

        private bool isValidCep(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "Cep Invalido! Deve conter 8 caracters", "OK");
                valido = false;
            }

            int novoCep = 0;
            if(!int.TryParse(cep, out novoCep))
            {
                DisplayAlert("Erro", "Cep deve ser numerico!", "OK");
                valido = false;
            }

            return valido;
        }

    }
}
