using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ClasseSemHerancaNaoPodeProtegida
    {
        //essa classe não faz herança da Pessoa, logo, não tem acesso a Protegida
        Person pessoa = new Person("Fernando", "Passaia", 34);
        //readonly string teste = pessoa.LastName; //consegue dar um get por exemplo
        //pessoa.testeInternal = ""; //aqui eu só tenho acesso as variáveis internas            

    }
}
