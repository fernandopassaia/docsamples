using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person
    {
        public Person(string name, string lastName, int age)
        {
            Name = name;
            LastName = lastName;
            Age = age;
            testeInternal = ""; //essa variável é visível de dentro desse form
            testeProtected = ""; //essa variável é visível apenas de dentro do assembly
        }

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }

        private string somethingPrivate { get; set; } //this is available just HERE in this classe - not outside.
        internal string testeInternal { get; set; } //this is available internal in this Assembly (DLL) but not outside
        protected string testeProtected { get; set; } //essa variável poderá ser acessada apenas de dentro do Assembly (não do WF)
        
        

        protected class ClasseProtegida
        {
            public string campoClasseProtegida { get; set; }
        }

        private class ClassePrivada
        {
            public string campoClassePrivada { get; set; }
        }

        internal class ClasseInterna
        {
            public string campoClasseInterna { get; set; }
        }
    }
}

