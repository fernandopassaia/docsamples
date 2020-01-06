using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleClassesType
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Person person = new Person("Fernando", "Passaia", 34);
            //person.Age = 12; //will give error because SET is private


            PersonFisica pessoaFisica = new PersonFisica("Fernando", "Passaia", 34);
            //essa classe tem um construtor base por que precisa construir pessoa
            
            
            //Nota: Aqui eu não tenho acesso nem as variáveis nem aos métodos Protected e Privadas da classe Model
        }
    }
}
