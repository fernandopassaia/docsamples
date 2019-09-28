using DllFuturaDataTCC.Models;
using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.DataAccessObject
{
    public class iDaoProduto
    {
        #region Variaveis Internas da Classe
        clsConexao clsConexao = new clsConexao();
        string erroClasse;
        public string ErroClasse
        {
            get { return erroClasse; }
            set { erroClasse = value; }
        }
        #endregion

        #region Model Inserir Produto
        public bool dInsereProduto(iModProduto objProduto)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("INSERT INTO PRODUTOS ");
            SqlConcatenada.Append("(CODIGOFABRIC ");
            SqlConcatenada.Append(",CODIGOORIGINAL ");
            SqlConcatenada.Append(",DESCRICAO ");
            SqlConcatenada.Append(",APLICACAO ");
            SqlConcatenada.Append(",PRECOCUSTO ");
            SqlConcatenada.Append(",MARGEMLUCRO ");
            SqlConcatenada.Append(",PRECOVENDA ");
            SqlConcatenada.Append(",QTD_ATUAL ");
            SqlConcatenada.Append(",UNIDADE ");
            SqlConcatenada.Append(",PORC_IMP_PAGO ");
            SqlConcatenada.Append(",ICMS ");
            SqlConcatenada.Append(",CORREDORSETOR ");
            SqlConcatenada.Append(",LOCALCAIXA ");
            SqlConcatenada.Append(",MAISINFO ");
            SqlConcatenada.Append(",POSSUI_IMAGEM ");
            if (objProduto.ImagemProduto != null)
            {
                SqlConcatenada.Append(",IMAGEM_BINARIO ");
            }
            SqlConcatenada.Append(",STATUS) ");
            SqlConcatenada.Append("VALUES ");
            SqlConcatenada.Append("(@CODIGOFABRIC ");
            SqlConcatenada.Append(",@CODIGOORIGINAL ");
            SqlConcatenada.Append(",@DESCRICAO ");
            SqlConcatenada.Append(",@APLICACAO ");
            SqlConcatenada.Append(",@PRECOCUSTO ");
            SqlConcatenada.Append(",@MARGEMLUCRO ");
            SqlConcatenada.Append(",@PRECOVENDA ");
            SqlConcatenada.Append(",@QTD_ATUAL ");
            SqlConcatenada.Append(",@UNIDADE ");
            SqlConcatenada.Append(",@PORC_IMP_PAGO ");
            SqlConcatenada.Append(",@ICMS ");
            SqlConcatenada.Append(",@CORREDORSETOR ");
            SqlConcatenada.Append(",@LOCALCAIXA ");
            SqlConcatenada.Append(",@MAISINFO ");
            SqlConcatenada.Append(",@POSSUI_IMAGEM ");
            if (objProduto.ImagemProduto != null)
            {
                SqlConcatenada.Append(",@IMAGEM_BINARIO ");
            }
            SqlConcatenada.Append(",@STATUS) ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlConcatenada.Append("(@CODIGOFABRIC ");
            SqlConcatenada.Append(",@CODIGOORIGINAL ");
            SqlConcatenada.Append(",@DESCRICAO ");
            SqlConcatenada.Append(",@APLICACAO ");
            SqlConcatenada.Append(",@PRECOCUSTO ");
            SqlConcatenada.Append(",@MARGEMLUCRO ");
            SqlConcatenada.Append(",@PRECOVENDA ");
            SqlConcatenada.Append(",@QTD_ATUAL ");
            SqlConcatenada.Append(",@UNIDADE ");
            SqlConcatenada.Append(",@PORC_IMP_PAGO ");
            SqlConcatenada.Append(",@ICMS ");
            SqlConcatenada.Append(",@FK_LOCAL_ESTOQUE ");
            SqlConcatenada.Append(",@CORREDORSETOR ");
            SqlConcatenada.Append(",@LOCALCAIXA ");
            SqlConcatenada.Append(",@MAISINFO ");
            SqlConcatenada.Append(",@POSSUI_IMAGEM ");
            SqlConcatenada.Append(",@IMAGEM_BINARIO ");
            SqlConcatenada.Append(",@STATUS) ");

            SqlParameter parametro = new SqlParameter("@CODIGO", objProduto.Pk_Codigo);
            SqlParameter parametro2 = new SqlParameter("@CODIGOFABRIC", objProduto.CodigoFabric);
            SqlParameter parametro3 = new SqlParameter("@CODIGOORIGINAL", objProduto.CodigoOriginal);
            SqlParameter parametro4 = new SqlParameter("@DESCRICAO", objProduto.Descricao);
            SqlParameter parametro5 = new SqlParameter("@APLICACAO", objProduto.Aplicacao);
            SqlParameter parametro6 = new SqlParameter("@PRECOCUSTO", objProduto.PrecoCusto);
            SqlParameter parametro7 = new SqlParameter("@MARGEMLUCRO", objProduto.MargemLucro);

            SqlParameter parametro8 = new SqlParameter("@PRECOVENDA", objProduto.PrecoVenda);
            SqlParameter parametro9 = new SqlParameter("@QTD_ATUAL", objProduto.QtdAtual);
            SqlParameter parametro10 = new SqlParameter("@UNIDADE", objProduto.Unidade);
            SqlParameter parametro11 = new SqlParameter("@PORC_IMP_PAGO", objProduto.PorcImpPago);
            SqlParameter parametro12 = new SqlParameter("@ICMS", objProduto.Icms);
            SqlParameter parametro14 = new SqlParameter("@CORREDORSETOR", objProduto.CorredorSetor);
            SqlParameter parametro15 = new SqlParameter("@LOCALCAIXA", objProduto.LocalCaixa);
            SqlParameter parametro16 = new SqlParameter("@MAISINFO", objProduto.MaisInfo);
            SqlParameter parametro17 = new SqlParameter("@STATUS", objProduto.Status);

            if (objProduto.ImagemProduto != null)
            {
                SqlParameter parametroImagem = new SqlParameter("@IMAGEM_BINARIO", objProduto.ImagemProduto);
                comando.Parameters.Add(parametroImagem);
                SqlParameter parametroImagem2 = new SqlParameter("@POSSUI_IMAGEM", true);
                comando.Parameters.Add(parametroImagem2);
            }
            else
            {
                SqlParameter parametroImagem = new SqlParameter("@POSSUI_IMAGEM", false);
                comando.Parameters.Add(parametroImagem);
            }

            comando.Parameters.Add(parametro);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);
            comando.Parameters.Add(parametro4);
            comando.Parameters.Add(parametro5);
            comando.Parameters.Add(parametro6);
            comando.Parameters.Add(parametro7);
            comando.Parameters.Add(parametro8);
            comando.Parameters.Add(parametro9);
            comando.Parameters.Add(parametro10);
            comando.Parameters.Add(parametro11);
            comando.Parameters.Add(parametro12);
            comando.Parameters.Add(parametro14);
            comando.Parameters.Add(parametro15);
            comando.Parameters.Add(parametro16);
            comando.Parameters.Add(parametro17);

            try
            {
                comando.ExecuteNonQuery();
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsCliente", "incluir()", erro.Message.ToString(), "Incluir o Cliente");
                return false;
            }

            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                SqlConcatenada = null;
                comando = null;
                parametro = null;
                parametro2 = null;
                parametro3 = null;
                parametro4 = null;
                parametro5 = null;
                parametro6 = null;
                parametro7 = null;
                parametro8 = null;
                parametro9 = null;
                parametro10 = null;
                parametro11 = null;
                parametro12 = null;
                parametro14 = null;
                parametro15 = null;
                parametro16 = null;
                parametro17 = null;
            }
        }
        #endregion

        #region Método Alterar Produto
        public bool dAlteraProduto(iModProduto objProduto)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("UPDATE PRODUTOS ");
            SqlConcatenada.Append("SET CODIGOFABRIC = @CODIGOFABRIC ");
            SqlConcatenada.Append(",CODIGOORIGINAL = @CODIGOORIGINAL ");
            SqlConcatenada.Append(",DESCRICAO = @DESCRICAO ");
            SqlConcatenada.Append(",APLICACAO = @APLICACAO ");
            SqlConcatenada.Append(",PRECOCUSTO = @PRECOCUSTO ");
            SqlConcatenada.Append(",MARGEMLUCRO = @MARGEMLUCRO ");
            SqlConcatenada.Append(",PRECOVENDA = @PRECOVENDA ");
            SqlConcatenada.Append(",QTD_ATUAL = @QTD_ATUAL ");
            SqlConcatenada.Append(",UNIDADE = @UNIDADE ");
            SqlConcatenada.Append(",PORC_IMP_PAGO = @PORC_IMP_PAGO ");
            SqlConcatenada.Append(",ICMS = @ICMS ");
            SqlConcatenada.Append(",CORREDORSETOR = @CORREDORSETOR ");
            SqlConcatenada.Append(",LOCALCAIXA = @LOCALCAIXA ");
            SqlConcatenada.Append(",MAISINFO = @MAISINFO ");
            SqlConcatenada.Append(",POSSUI_IMAGEM = @POSSUI_IMAGEM ");
            if (objProduto.ImagemProduto != null)
            {
                SqlConcatenada.Append(",IMAGEM_BINARIO = @IMAGEM_BINARIO ");
            }
            SqlConcatenada.Append(",STATUS = @STATUS ");
            SqlConcatenada.Append("WHERE PK_CODIGOSIST = @CODIGO ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@CODIGO", objProduto.Pk_Codigo);
            SqlParameter parametro2 = new SqlParameter("@CODIGOFABRIC", objProduto.CodigoFabric);
            SqlParameter parametro3 = new SqlParameter("@CODIGOORIGINAL", objProduto.CodigoOriginal);
            SqlParameter parametro4 = new SqlParameter("@DESCRICAO", objProduto.Descricao);
            SqlParameter parametro5 = new SqlParameter("@APLICACAO", objProduto.Aplicacao);
            SqlParameter parametro6 = new SqlParameter("@PRECOCUSTO", objProduto.PrecoCusto);
            SqlParameter parametro7 = new SqlParameter("@MARGEMLUCRO", objProduto.MargemLucro);

            SqlParameter parametro8 = new SqlParameter("@PRECOVENDA", objProduto.PrecoVenda);
            SqlParameter parametro9 = new SqlParameter("@QTD_ATUAL", objProduto.QtdAtual);
            SqlParameter parametro10 = new SqlParameter("@UNIDADE", objProduto.Unidade);
            SqlParameter parametro11 = new SqlParameter("@PORC_IMP_PAGO", objProduto.PorcImpPago);
            SqlParameter parametro12 = new SqlParameter("@ICMS", objProduto.Icms);
            SqlParameter parametro14 = new SqlParameter("@CORREDORSETOR", objProduto.CorredorSetor);
            SqlParameter parametro15 = new SqlParameter("@LOCALCAIXA", objProduto.LocalCaixa);
            SqlParameter parametro16 = new SqlParameter("@MAISINFO", objProduto.MaisInfo);
            SqlParameter parametro17 = new SqlParameter("@STATUS", objProduto.Status);

            if (objProduto.ImagemProduto != null)
            {
                SqlParameter parametroImagem = new SqlParameter("@IMAGEM_BINARIO", objProduto.ImagemProduto);
                comando.Parameters.Add(parametroImagem);
                SqlParameter parametroImagem2 = new SqlParameter("@POSSUI_IMAGEM", true);
                comando.Parameters.Add(parametroImagem2);
            }
            else
            {
                SqlParameter parametroImagem = new SqlParameter("@POSSUI_IMAGEM", false);
                comando.Parameters.Add(parametroImagem);
            }

            comando.Parameters.Add(parametro);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);
            comando.Parameters.Add(parametro4);
            comando.Parameters.Add(parametro5);
            comando.Parameters.Add(parametro6);
            comando.Parameters.Add(parametro7);
            comando.Parameters.Add(parametro8);
            comando.Parameters.Add(parametro9);
            comando.Parameters.Add(parametro10);
            comando.Parameters.Add(parametro11);
            comando.Parameters.Add(parametro12);
            comando.Parameters.Add(parametro14);
            comando.Parameters.Add(parametro15);
            comando.Parameters.Add(parametro16);
            comando.Parameters.Add(parametro17);

            try
            {
                comando.ExecuteNonQuery();
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString();
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                SqlConcatenada = null;
                comando = null;
                parametro = null;
                parametro2 = null;
                parametro3 = null;
                parametro4 = null;
                parametro5 = null;
                parametro6 = null;
                parametro7 = null;
                parametro8 = null;
                parametro9 = null;
                parametro10 = null;
                parametro11 = null;
                parametro12 = null;
                parametro14 = null;
                parametro15 = null;
                parametro16 = null;
                parametro17 = null;
            }
        }
        #endregion

        #region Método Exclui Produto
        public bool dExcluiProduto(iModProduto objProduto)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append(" UPDATE PRODUTOS ");
            SqlConcatenada.Append(" SET ");
            SqlConcatenada.Append(" STATUS = 'EXCLUIDO'");
            SqlConcatenada.Append(" WHERE ");
            SqlConcatenada.Append(" PK_CODIGOSIST = @codigo");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(SqlConcatenada.ToString(), conexao);

            SqlParameter parametro = new SqlParameter("@codigo", objProduto.Pk_Codigo);
            comando.Parameters.Add(parametro);

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco                
                return true;
            }


        //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsCliente", "excluir()", erro.Message.ToString(), "Excluir o Cliente");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                parametro = null;
                comando = null;
                SqlConcatenada = null;
            }
        }
        #endregion

        #region Método Obtem Produto
        public iModProduto[] dObterProduto()
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("SELECT PK_CODIGOSIST ");
            SqlConcatenada.Append(",CODIGOFABRIC ");
            SqlConcatenada.Append(",CODIGOORIGINAL ");
            SqlConcatenada.Append(",DESCRICAO ");
            SqlConcatenada.Append(",APLICACAO ");
            SqlConcatenada.Append(",PRECOCUSTO ");
            SqlConcatenada.Append(",MARGEMLUCRO ");
            SqlConcatenada.Append(",PRECOVENDA ");
            SqlConcatenada.Append(",QTD_ATUAL ");
            SqlConcatenada.Append(",UNIDADE ");
            SqlConcatenada.Append(",PORC_IMP_PAGO ");
            SqlConcatenada.Append(",ICMS ");
            SqlConcatenada.Append(",CORREDORSETOR ");
            SqlConcatenada.Append(",LOCALCAIXA ");
            SqlConcatenada.Append(",MAISINFO ");
            SqlConcatenada.Append(",POSSUI_IMAGEM ");            
            SqlConcatenada.Append(",STATUS ");
            SqlConcatenada.Append("FROM PRODUTOS WHERE STATUS='ATIVO' ORDER BY DESCRICAO ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(SqlConcatenada.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            try
            {
                DataAdap.Fill(DsDataSet, "PRODUTOS");


                iModProduto[] produtos = new iModProduto[DsDataSet.Tables[0].Rows.Count]; //cria um array com o tamanho do Dataset


                for (int i = 0; i < DsDataSet.Tables[0].Rows.Count; i++)
                {
                    produtos[i] = new iModProduto(); //crio uma nova instância com o índice (1,2,3,4,5,6) para armazenar o item
                    produtos[i].Pk_Codigo = Convert.ToInt32(DsDataSet.Tables[0].Rows[i]["PK_CODIGOSIST"].ToString());
                    produtos[i].CodigoFabric = DsDataSet.Tables[0].Rows[i]["CODIGOFABRIC"].ToString();
                    produtos[i].CodigoOriginal = DsDataSet.Tables[0].Rows[i]["CODIGOORIGINAL"].ToString();
                    produtos[i].Descricao = DsDataSet.Tables[0].Rows[i]["DESCRICAO"].ToString();
                    produtos[i].Aplicacao = DsDataSet.Tables[0].Rows[i]["APLICACAO"].ToString();
                    produtos[i].PrecoCusto = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["PRECOCUSTO"].ToString());
                    produtos[i].MargemLucro = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["MARGEMLUCRO"].ToString());
                    produtos[i].PrecoVenda = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["PRECOVENDA"].ToString());
                    produtos[i].QtdAtual = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["QTD_ATUAL"].ToString());
                    produtos[i].Unidade = DsDataSet.Tables[0].Rows[i]["UNIDADE"].ToString();
                    produtos[i].PorcImpPago = Convert.ToDecimal(DsDataSet.Tables[0].Rows[i]["PORC_IMP_PAGO"].ToString());
                    produtos[i].Icms = DsDataSet.Tables[0].Rows[i]["ICMS"].ToString();
                    produtos[i].CorredorSetor = DsDataSet.Tables[0].Rows[i]["CORREDORSETOR"].ToString();
                    produtos[i].LocalCaixa = DsDataSet.Tables[0].Rows[i]["LOCALCAIXA"].ToString();
                    produtos[i].MaisInfo = DsDataSet.Tables[0].Rows[i]["MAISINFO"].ToString();
                    produtos[i].PossuiImagem = Convert.ToBoolean(DsDataSet.Tables[0].Rows[i]["POSSUI_IMAGEM"].ToString());
                    produtos[i].Status = DsDataSet.Tables[0].Rows[i]["STATUS"].ToString();                    
                }

                return produtos;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsCliente", "obterCad()", erro.Message.ToString(), "Alterar o Cliente");
                return null;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                DsDataSet = null;
                DataAdap = null;
                SqlConcatenada = null;
            }
        }
        #endregion

        #region Método para Recuperar a Imagem do Produto no Banco
        public Image dRecuperarImagemProdutoNoBanco(iModProduto objProduto)
        {
            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand k = new SqlCommand("SELECT IMAGEM_BINARIO FROM PRODUTOS WHERE PK_CODIGOSIST = @CODIGO", conexao);
            SqlParameter param = new SqlParameter("@CODIGO", objProduto.Pk_Codigo);
            k.Parameters.Add(param);
            try
            {
                byte[] imagemEmBytes = (byte[])k.ExecuteScalar();
                MemoryStream ms = new MemoryStream();
                ms.Write(imagemEmBytes, 0, imagemEmBytes.Length);
                return Image.FromStream(ms);
            }
            catch (Exception erro)
            {
                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsCliente", "recuperarImagemClienteBanco()", erro.Message.ToString(), "Recuperar Imagem do Cliente no Banco");
                return null;
            }
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
            }
        }
        #endregion
    }
}
