using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VSBS.Model;
using VSBS.Core;

namespace VSBS.WebService
{
    /// <summary>
    /// Implementação do WebService Meu Serviço em C#
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MeuServico : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Montadora> ListaDeMontadoras()
        {
            return new MontadoraCore().getAll();
        }

        [WebMethod]
        public Montadora PegaMontadoraPorCodigo(int id)
        {
            return new MontadoraCore().getById(id);
        }

        [WebMethod]
        public List<Montadora> PegaMontadorPorFiltroDeNome(string name)
        {
            return new MontadoraCore().getByQuery(name);
        }
    }
}
