﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App07_Cell.Modelo;

namespace App07_Cell.Pagina
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageCellPage : ContentPage
	{
		public ImageCellPage ()
		{
			InitializeComponent ();

            List<Funcionario> Lista = new List<Funcionario>();
            Lista.Add(new Funcionario() { Foto = "perfil.png", Nome = "José", Cargo = "Presidente da Empresa" });
            Lista.Add(new Funcionario() { Foto = "perfil.png", Nome = "Maria", Cargo = "Gerente de Vendas" });
            Lista.Add(new Funcionario() { Foto = "https://ssl.gstatic.com/ui/v1/icons/mail/rfr/logo_gmail_lockup_default_1x.png", Nome = "Elaine", Cargo = "Gerente de Marketing" });
            Lista.Add(new Funcionario() { Foto = "perfil.png", Nome = "Felipe", Cargo = "Entregador" });
            Lista.Add(new Funcionario() { Foto = "perfil.png", Nome = "João", Cargo = "Vendedor" });

            ListaFuncionario.ItemsSource = Lista;
        }
	}
}