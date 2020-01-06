using System;
using System.Collections.Generic;
using System.Text;

namespace App1_Vagas.Banco
{
    public interface ICaminho
    {
        //nota: O nome do banco de dados é database.sqlite. Porém cada sistema tem o seu caminho e estrutura de arquivos, dessa forma,
        //eu não posso unificiar as plataformas e caminhos. Isso terá que ser definido em cada plataforma... então note que CADA projeto
        //terá uma pasta chamada Banco e dentro dessa um Caminho.cs que irá retornar o Path do banco em si. Isso será resolvido
        //usando o Dependency Service... É como se os meus outros projetos "injetassem" o Path do DB aqui...
        string ObterCaminho(string NomeArquivoBanco);
    }
}
