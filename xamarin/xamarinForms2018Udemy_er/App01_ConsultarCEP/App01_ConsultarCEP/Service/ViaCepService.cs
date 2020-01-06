using App01_ConsultarCEP.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App01_ConsultarCEP.Service
{
    public class ViaCepService
    {
        //this app is consuming the Brazilian-Address API at: http://viacep.com.br/ws/08775520/json/
        //the JSON Return looks like this:
        //{
        //  "cep": "08775-520",
        //  "logradouro": "Avenida Ezelino da Cunha Glória",
        //  "complemento": "",
        //  "bairro": "Jardim Marica",
        //  "localidade": "Mogi das Cruzes",
        //  "uf": "SP",
        //  "unidade": "",
        //  "ibge": "3530607",
        //  "gia": "4546"
        //}

        private static string apiUrl = "http://viacep.com.br/ws/{0}/json/"; //{0} should be the CEP

        public static Address FindAddressByCep(string cep)
        {
            string NewAddressUrl = string.Format(apiUrl, cep);
            WebClient wc = new WebClient();
            string content = wc.DownloadString(NewAddressUrl);
            Address adr = JsonConvert.DeserializeObject<Address>(content);

            if (adr.cep == null) return null; //in case CEP doesn't exists, JsonConvert will fill all fields with "null"
            //in this case i will return null and made the messaging in UI

            return adr;
        }
    }
}
