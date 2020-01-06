using System;
using System.Collections.Generic;
using System.Text;

namespace App01_ConsultarCEP.Service.Models
{
    public class Address
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

        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string unidade { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }        
    }
}