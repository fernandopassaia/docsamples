using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.Models
{
    public class iModCliente
    {
        #region Atributos da Classe (variaveis internas e métodos de acesso)
        clsConexao clsConexao = new clsConexao();
        string erroClasse;
        public string ErroClasse
        {
            get { return erroClasse; }
            set { erroClasse = value; }
        }

        Byte[] imagemCliente;

        public Byte[] ImagemCliente
        {
            get { return imagemCliente; }
            set { imagemCliente = value; }
        }
        int pk_Codigo;

        public int Pk_Codigo
        {
            get { return pk_Codigo; }
            set { pk_Codigo = value; }
        }
        string pessoaFisicaJuridica;

        public string PessoaFisicaJuridica
        {
            get { return pessoaFisicaJuridica; }
            set { pessoaFisicaJuridica = value; }
        }
        string cpfCnpj;

        public string CpfCnpj
        {
            get { return cpfCnpj; }
            set { cpfCnpj = value; }
        }
        string rg;

        public string Rg
        {
            get { return rg; }
            set { rg = value; }
        }
        string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        string razaoSocial;

        public string RazaoSocial
        {
            get { return razaoSocial; }
            set { razaoSocial = value; }
        }
        string inscrEstadual;

        public string InscrEstadual
        {
            get { return inscrEstadual; }
            set { inscrEstadual = value; }
        }
        string estado;

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        string cep;

        public string Cep
        {
            get { return cep; }
            set { cep = value; }
        }
        string logradouro;

        public string Logradouro
        {
            get { return logradouro; }
            set { logradouro = value; }
        }
        string numero;

        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        string bairro;

        public string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }
        string cidade;

        public string Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }
        string complemento;

        public string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }
        string telefone1;

        public string Telefone1
        {
            get { return telefone1; }
            set { telefone1 = value; }
        }
        string telefone2;

        public string Telefone2
        {
            get { return telefone2; }
            set { telefone2 = value; }
        }
        string fax;

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        string celular1;

        public string Celular1
        {
            get { return celular1; }
            set { celular1 = value; }
        }
        string celular2;

        public string Celular2
        {
            get { return celular2; }
            set { celular2 = value; }
        }
        string operadora1;

        public string Operadora1
        {
            get { return operadora1; }
            set { operadora1 = value; }
        }
        string operadora2;

        public string Operadora2
        {
            get { return operadora2; }
            set { operadora2 = value; }
        }
        string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        string site;

        public string Site
        {
            get { return site; }
            set { site = value; }
        }
        string maisInfo;

        public string MaisInfo
        {
            get { return maisInfo; }
            set { maisInfo = value; }
        }
        string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private bool possuiImagem;

        public bool PossuiImagem
        {
            get { return possuiImagem; }
            set { possuiImagem = value; }
        }

        DataSet ds_DadosRetorno;

        public DataSet Ds_DadosRetorno
        {
            get { return ds_DadosRetorno; }
            set { ds_DadosRetorno = value; }
        }
        #endregion
    }//fim classe
}//fim namespace
