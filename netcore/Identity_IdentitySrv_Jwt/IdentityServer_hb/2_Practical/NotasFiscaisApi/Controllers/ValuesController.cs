using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NotasFiscaisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        // Nota: É esse o GET que irei proteger, um Get de uma Lista de Strings. É um negócio SIMPLES, mas funcional!
        // Pra acessar esse método eu precisarei do meu Token gerado pelo IdentityServer (ver a partir da img22).

        // Meu Token de autorização PRECISARÁ seguir as seguintes regras:
        // (1) Tem que ser um Bearer Token.
        // (2) Tem que ter sido autenticado pelo meu Servidor de Identidade.
        // (3) Tem que estar Assinado.
        // (4) Precisa ter o escopo que foi solicitado.

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
