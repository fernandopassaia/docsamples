using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIIndicadores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; //objeto de configuração pra passar a connection string
using System.Data.SqlClient; //preciso desse objeto por que irei usar o Dapper
using Dapper; //referência ao Dapper
using System.Net;

namespace APIIndicadores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicadoresController : ControllerBase
    {
        //Asp.Net Core passará o objeto de configuração nesse construtor - pra pegar a string de conexão
        private IConfiguration _configuration; 
        public IndicadoresController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //https://localhost:5001/api/indicadores (retorna um Json com os registros do banco de dados)
        [HttpGet]        
        [ProducesResponseType(typeof(IEnumerable<Indicador>), (int)HttpStatusCode.OK)]
        //digo que o retorno será uma lista de Indicadores, quando o Code for 200 (OK) (faço cast pra INT) (doc pro Swagger funcionar)
        public IEnumerable<Indicador> Get()
        {
            //pega a conexão lá no appsettings.json que é onde encontra-se a string de conexão
            using(SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("BaseIndicadores")))
            {
                //cara, que interessante, ele mesmo converte pra indicador sozinho... pqp
                return conexao.Query<Indicador>("Select * from Indicadores").ToList();
            }
        }

        //https://localhost:5001/api/indicadores (retorna um Json com os registros do banco de dados)
        [HttpGet("{siglaIndicador}")] //configuro a rota, falando que vai precisar receber um parametro mapeando pra sigla indicador
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))] //essa DOC e o ProcucesResponceType são pro Swagger funcionar
        public ActionResult<Indicador> Get(string siglaIndicador)
        {
            Indicador resultado = null;
            //pega a conexão lá no appsettings.json que é onde encontra-se a string de conexão
            using(SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("BaseIndicadores")))
            {
                //cara, que interessante, ele mesmo converte pra indicador sozinho... pqp
                resultado = conexao.QueryFirstOrDefault<Indicador>("Select * from Indicadores WHERE Sigla = @Sigla", new { Sigla = siglaIndicador });                
            }
            if(resultado != null)
            { return resultado; }
            else
            {
                 //retorna o erro 404 (not found) e um JSON com uma mensagem de erro customizada
                 return NotFound( new { Mensagem = "Indicador inexistente ou inválido" }); 
            }
        }
    }//fim classe
}//fim namespace
