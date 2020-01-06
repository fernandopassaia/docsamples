using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PersonFisica : Person
    {
        public PersonFisica(string name, string lastName, int age) : base(name, lastName, age)
        {
            Person pessoa = new Person("Fernando", "Passaia", 34);
            pessoa.testeInternal = ""; //aqui eu só tenho acesso as variáveis internas            

            ClasseProtegida protegida = new ClasseProtegida();
            protegida.campoClasseProtegida = ""; //o campo da classe protegida pode ser usado numa classe que DERIVA (herança) dela

            ClasseInterna interna = new ClasseInterna();
            interna.campoClasseInterna = ""; //a classe interna pode ser usada em todo esse assembly
        }
    }
}
