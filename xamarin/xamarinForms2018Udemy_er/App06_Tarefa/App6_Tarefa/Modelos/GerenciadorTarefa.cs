using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App6_Tarefa.Modelos
{    
    public class GerenciadorTarefa
    {
        private List<Tarefa> Lista { get; set; }

        public void Salvar(Tarefa tarefa)
        {
            Lista = Listagem();
            Lista.Add(tarefa);

            SalvarNoProperties(Lista);
        }
        public void Deletar(int index)
        {
            Lista = Listagem();
            Lista.RemoveAt(index);

            SalvarNoProperties(Lista);
        }
        public void Finalizar(int index, Tarefa tarefa)
        {
            Lista = Listagem();
            Lista.RemoveAt(index);

            tarefa.DataFinalizacao = DateTime.Now;
            Lista.Add(tarefa);
            SalvarNoProperties(Lista);
        }
        public List<Tarefa> Listagem()
        {
            return ListagemNoProperties();
        }

        private void SalvarNoProperties(List<Tarefa> Lista)
        {
            //App.Current eh um Recurso do Xamarin Local (tipo um banco de dados temporario) onde voce pode armazenar Chave + Valor (qualquer objeto)
            //entao nesse caso eu terei uma chave TAREFAS e dentro dela irei armazenar um OBJETO do tipo Array (convertido pra JSON) de tarefas
            if (App.Current.Properties.ContainsKey("Tarefas"))
            {
                App.Current.Properties.Remove("Tarefas"); //se ja houver um objeto Tarefas (array) - removo ele antes
            }

            //I'm converting always to JSON, because if i save a "complex" type (like the Class Object) it will lose stats on every reboot of APP
            string JsonVal = JsonConvert.SerializeObject(Lista);

            App.Current.Properties.Add("Tarefas", JsonVal);
        }
        private List<Tarefa> ListagemNoProperties()
        {
            if (App.Current.Properties.ContainsKey("Tarefas"))
            {
                String JsonVal = (String)App.Current.Properties["Tarefas"];

                //I'm converting always to JSON, because if i save a "complex" type (like the Class Object) it will lose stats on every reboot of APP
                List<Tarefa> Lista = JsonConvert.DeserializeObject<List<Tarefa>>(JsonVal);
                return Lista;
                //return (List<Tarefa>)App.Current.Properties["Tarefas"];
            }

            return new List<Tarefa>();
        }
    }
}
