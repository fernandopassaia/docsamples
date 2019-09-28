using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using App1_Vagas.Modelos;
using Xamarin.Forms;

namespace App1_Vagas.Banco
{
    class Database
    {
        private SQLiteConnection _conexao;

        public Database()
        {
            var dep = DependencyService.Get<ICaminho>(); //passo a minha interface
            string caminho = dep.ObterCaminho("database.sqlite"); //o Dep vai identificar a implementação lá no projeto Android-iOS

            //nota: O nome do banco de dados é database.sqlite. Porém cada sistema tem o seu caminho e estrutura de arquivos, dessa forma,
            //eu não posso unificiar as plataformas e caminhos. Isso terá que ser definido em cada plataforma... então note que CADA projeto
            //terá uma pasta chamada Banco e dentro dessa um Caminho.cs que irá retornar o Path do banco em si. Isso será resolvido
            //usando o Dependency Service... É como se os meus outros projetos "injetassem" o Path do DB aqui...

            _conexao = new SQLiteConnection(caminho);
            _conexao.CreateTable<Vaga>(); //essa classe precisa ter data anotations pra saber como criar a tabela...
        }

        public List<Vaga> Consultar()
        {
            return _conexao.Table<Vaga>().ToList();
        }
        public List<Vaga> Pesquisar(string palavra)
        {
            return _conexao.Table<Vaga>().Where(a=>a.NomeVaga.Contains(palavra)).ToList();
        }
        public Vaga ObterVagaPorId(int id)
        {
            return _conexao.Table<Vaga>().Where(a=>a.Id == id).FirstOrDefault();
        }
        public void Cadastro(Vaga vaga)
        {
            _conexao.Insert(vaga);
        }
        public void Atualizacao(Vaga vaga)
        {
            _conexao.Update(vaga);
        }
        public void Exclusao(Vaga vaga)
        {
            _conexao.Delete(vaga);
        }
    }
}
