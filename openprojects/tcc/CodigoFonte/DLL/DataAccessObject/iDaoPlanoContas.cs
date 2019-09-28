using DllFuturaDataTCC.Models;
using DllFuturaDataTCC.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DllFuturaDataTCC.DataAccessObject
{
    public class iDaoPlanoContas
    {
        #region Atributos da Classe (variaveis internas e métodos de acesso)
        clsConexao clsConexao = new clsConexao();
        string erroClasse;
        public string ErroClasse
        {
            get { return erroClasse; }
            set { erroClasse = value; }
        }

        DataSet ds_DadosRetorno;

        public DataSet Ds_DadosRetorno
        {
            get { return ds_DadosRetorno; }
            set { ds_DadosRetorno = value; }
        }
        #endregion
        
        #region Método "Incluir Plano de Conta Mestre"
        public bool dIncluirPlanoDeContasMestre(iModPlanoContas objPlanoContas)
        {
            StringBuilder incluir = new StringBuilder();
            incluir.Append("INSERT INTO TB_PLANOCONTAS_MESTRE ");
            incluir.Append("(DESCRICAO_CATEGORIA ");
            incluir.Append(",MASCARA_PLANO ");
            incluir.Append(",ENTRADA_SAIDA ");
            incluir.Append(",STATUS) ");
            incluir.Append("VALUES ");
            incluir.Append("(@DESCRICAO_CATEGORIA ");
            incluir.Append(",@MASCARA_PLANO ");
            incluir.Append(",@ENTRADA_SAIDA ");
            incluir.Append(",@STATUS) ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(incluir.ToString(), conexao);

            SqlParameter parametro1 = new SqlParameter("@DESCRICAO_CATEGORIA", objPlanoContas.DescricaoCategoriaMestre);
            SqlParameter parametro2 = new SqlParameter("@MASCARA_PLANO", objPlanoContas.MascaraPlanoMestre);
            SqlParameter parametro3 = new SqlParameter("@STATUS", "ATIVO");
            SqlParameter parametro4 = new SqlParameter("@ENTRADA_SAIDA", 1); //chumbei pra sempre entrada...

            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);
            comando.Parameters.Add(parametro4);

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco                
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPlanoDeContas", "incluirPlanoDeContasMestre()", erro.Message.ToString(), "Incluir Plano de Contas Mestre");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                comando = null;
                incluir = null;
                parametro1 = null;
                parametro2 = null;
                parametro3 = null;
                parametro4 = null;
            }
        }
        //fim da função GravarProduto()
        #endregion

        #region Método "Obter todos os Planos de Conta Mestre"
        public bool dObterTodosPlanosDeContaMestres()
        {
            StringBuilder consultarGrupoContaMestre = new StringBuilder();
            consultarGrupoContaMestre.Append("SELECT PK_IDPLANO_CONTAS_MESTRE ");
            consultarGrupoContaMestre.Append(",DESCRICAO_CATEGORIA ");
            consultarGrupoContaMestre.Append(",MASCARA_PLANO ");
            consultarGrupoContaMestre.Append(",ENTRADA_SAIDA ");
            consultarGrupoContaMestre.Append(",MASCARA_PLANO + ' - ' + DESCRICAO_CATEGORIA AS DESC_CBB ");
            consultarGrupoContaMestre.Append(",STATUS ");
            //consultarGrupoContaMestre.Append(",LOG_FKUSUARIOCRIADOR ");
            //consultarGrupoContaMestre.Append(",LOG_DATACRIACAO ");
            //consultarGrupoContaMestre.Append(",LOG_FKCLIENTEFD ");
            //consultarGrupoContaMestre.Append(",LOG_ATIVARMODOINTEGRADO ");
            consultarGrupoContaMestre.Append("FROM TB_PLANOCONTAS_MESTRE WHERE STATUS='ATIVO' ");
            consultarGrupoContaMestre.Append("ORDER BY MASCARA_PLANO ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarGrupoContaMestre.ToString(), conexao);
            DataSet DsDataSet = new DataSet();
            //SqlParameter fire = new SqlParameter("@CODIGOPRODUTO", numeroProduto);
            //DataAdap.SelectCommand.Parameters.Add(fire);
            try
            {
                DataAdap.Fill(DsDataSet);
                Ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPlanoDeContas", "obterPlanosDeContaMestres()", erro.Message.ToString(), "Obter Planos de Contas Mestres");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarGrupoContaMestre = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        //fim do método de ObterCadastro
        #endregion

        #region Método "Obter o Ultimo Plano de Conta Mestre Cadastrado de Entrada ou Saída"
        public bool dObterUltimoPlanosDeContaMestresCadastrado()
        {
            StringBuilder consultarGrupoContaMestre = new StringBuilder();
            consultarGrupoContaMestre.Append("SELECT TOP(1) PK_IDPLANO_CONTAS_MESTRE ");
            consultarGrupoContaMestre.Append(",DESCRICAO_CATEGORIA ");
            consultarGrupoContaMestre.Append(",MASCARA_PLANO ");
            consultarGrupoContaMestre.Append(",ENTRADA_SAIDA ");
            consultarGrupoContaMestre.Append(",STATUS ");
            consultarGrupoContaMestre.Append(",LOG_FKUSUARIOCRIADOR ");
            consultarGrupoContaMestre.Append(",LOG_DATACRIACAO ");
            consultarGrupoContaMestre.Append(",LOG_FKCLIENTEFD ");
            consultarGrupoContaMestre.Append(",LOG_ATIVARMODOINTEGRADO ");
            consultarGrupoContaMestre.Append("FROM TB_PLANOCONTAS_MESTRE WHERE STATUS='ATIVO' AND ENTRADA_SAIDA = @ENTRADASAIDA ORDER BY PK_IDPLANO_CONTAS_MESTRE DESC");


            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarGrupoContaMestre.ToString(), conexao);
            DataSet DsDataSet = new DataSet();
            SqlParameter fire = new SqlParameter("@ENTRADASAIDA", 1);
            DataAdap.SelectCommand.Parameters.Add(fire);

            try
            {
                DataAdap.Fill(DsDataSet);
                Ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPlanoDeContas", "obterUltimoPlanosDeContaMestresCadastrado()", erro.Message.ToString(), "Obter Planos de Contas Mestres");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarGrupoContaMestre = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        //fim do método de ObterCadastro
        #endregion

        #region Método "Obter todos os Planos de Conta Mestre de Entrada ou Saída"
        public bool dObterTodosPlanosDeContaMestresDeEntradaOuSaida()
        {
            StringBuilder consultarGrupoContaMestre = new StringBuilder();
            consultarGrupoContaMestre.Append("SELECT PK_IDPLANO_CONTAS_MESTRE ");
            consultarGrupoContaMestre.Append(",DESCRICAO_CATEGORIA ");
            consultarGrupoContaMestre.Append(",MASCARA_PLANO ");
            consultarGrupoContaMestre.Append(",CBB_MASQ = MASCARA_PLANO + ' - ' + DESCRICAO_CATEGORIA");
            consultarGrupoContaMestre.Append(",ENTRADA_SAIDA ");
            consultarGrupoContaMestre.Append(",STATUS ");
            consultarGrupoContaMestre.Append(",LOG_FKUSUARIOCRIADOR ");
            consultarGrupoContaMestre.Append(",LOG_DATACRIACAO ");
            consultarGrupoContaMestre.Append(",LOG_FKCLIENTEFD ");
            consultarGrupoContaMestre.Append(",LOG_ATIVARMODOINTEGRADO ");
            consultarGrupoContaMestre.Append("FROM TB_PLANOCONTAS_MESTRE WHERE STATUS='ATIVO' AND ENTRADA_SAIDA = @ENTRADASAIDA ");
            consultarGrupoContaMestre.Append("ORDER BY MASCARA_PLANO ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarGrupoContaMestre.ToString(), conexao);
            DataSet DsDataSet = new DataSet();
            SqlParameter fire = new SqlParameter("@ENTRADASAIDA", 1);
            DataAdap.SelectCommand.Parameters.Add(fire);
            try
            {
                DataAdap.Fill(DsDataSet);
                Ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPlanoDeContas", "obterTodosPlanosDeContaMestresDeEntradaOuSaida()", erro.Message.ToString(), "Obter Planos de Contas Mestres");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarGrupoContaMestre = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        //fim do método de ObterCadastro
        #endregion

        #region Método "Incluir Plano de Conta Categoria"
        public bool dIncluirPlanoDeContasSubCategoria(iModPlanoContas objPlanoContas)
        {
            StringBuilder incluir = new StringBuilder();
            incluir.Append("INSERT INTO TB_PLANOCONTAS_SUBCAT ");
            incluir.Append("(FK_CATEGORIAMESTRE ");
            incluir.Append(",DESCRICAO_SUBCATEGORIA ");
            incluir.Append(",NOME_ECF ");
            incluir.Append(",NOME_ECFRESUMIDO ");
            incluir.Append(",MASCARA_SUBCATEGORIA ");
            incluir.Append(",STATUS ");

            incluir.Append(",CHAMAR_TEF ");
            incluir.Append(",CHAMAR_ECF ");
            incluir.Append(",CHAMAR_NFE ");
            incluir.Append(",FORMA_VENDA_AVISTA ");
            incluir.Append(",FORMA_VENDA_ARECEBER ");
            incluir.Append(",FORMA_VENDA_AFATURAR ");
            incluir.Append(",NUM_PARCELAS ");
            incluir.Append(",INTERVALO_DIAS ");
            incluir.Append(",PRIMEIRA_PARC_AVISTA ");
            incluir.Append(",QTD_PARC_SEMJUROS ");
            incluir.Append(",ACRESCIMO_FIXO ");
            incluir.Append(",DESCONTO_FIXO ");
            incluir.Append(",DIA_VENCT_FIXO ");
            incluir.Append(",TIPO_DOC) ");

            incluir.Append("VALUES ");
            incluir.Append("(@FK_CATEGORIAMESTRE ");
            incluir.Append(",@DESCRICAO_SUBCATEGORIA ");
            incluir.Append(",@NOME_ECF ");
            incluir.Append(",@NOME_ECFRESUMIDO ");
            incluir.Append(",@MASCARA_SUBCATEGORIA ");
            incluir.Append(",'ATIVO' ");

            incluir.Append(",@CHAMAR_TEF ");
            incluir.Append(",@CHAMAR_ECF ");
            incluir.Append(",@CHAMAR_NFE ");
            incluir.Append(",@FORMA_VENDA_AVISTA ");
            incluir.Append(",@FORMA_VENDA_ARECEBER ");
            incluir.Append(",@FORMA_VENDA_AFATURAR ");
            incluir.Append(",@NUM_PARCELAS ");
            incluir.Append(",@INTERVALO_DIAS ");
            incluir.Append(",@PRIMEIRA_PARC_AVISTA ");
            incluir.Append(",@QTD_PARC_SEMJUROS ");
            incluir.Append(",@ACRESCIMO_FIXO ");
            incluir.Append(",@DESCONTO_FIXO ");
            incluir.Append(",@DIA_VENCT_FIXO ");
            incluir.Append(",@TIPO_DOC) ");


            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(incluir.ToString(), conexao);

            SqlParameter parametro1 = new SqlParameter("@FK_CATEGORIAMESTRE", objPlanoContas.PlanoMestreSubPlano);
            SqlParameter parametro2 = new SqlParameter("@DESCRICAO_SUBCATEGORIA", objPlanoContas.DescricaoSubCategoria);
            SqlParameter parametro3 = new SqlParameter("@NOME_ECF", "");
            SqlParameter parametro4 = new SqlParameter("@NOME_ECFRESUMIDO", "");
            SqlParameter parametro5 = new SqlParameter("@MASCARA_SUBCATEGORIA", objPlanoContas.MascaraSubPlano);
            SqlParameter parametro6 = new SqlParameter("@STATUS", "ATIVO");

            SqlParameter parametro7 = new SqlParameter("@CHAMAR_TEF", false);
            SqlParameter parametro8 = new SqlParameter("@CHAMAR_ECF", false);
            SqlParameter parametro9 = new SqlParameter("@CHAMAR_NFE", false);
            SqlParameter parametro10 = new SqlParameter("@FORMA_VENDA_AVISTA", true);
            SqlParameter parametro11 = new SqlParameter("@FORMA_VENDA_ARECEBER", false);
            SqlParameter parametro12 = new SqlParameter("@FORMA_VENDA_AFATURAR", false);
            SqlParameter parametro13 = new SqlParameter("@NUM_PARCELAS", Convert.ToDecimal(0));
            SqlParameter parametro14 = new SqlParameter("@INTERVALO_DIAS", 1);
            SqlParameter parametro15 = new SqlParameter("@PRIMEIRA_PARC_AVISTA", true);
            SqlParameter parametro16 = new SqlParameter("@QTD_PARC_SEMJUROS", 1);
            SqlParameter parametro17 = new SqlParameter("@ACRESCIMO_FIXO", Convert.ToDecimal(0));
            SqlParameter parametro18 = new SqlParameter("@DESCONTO_FIXO", Convert.ToDecimal(0));
            SqlParameter parametro19 = new SqlParameter("@DIA_VENCT_FIXO", 1);
            SqlParameter parametro20 = new SqlParameter("@TIPO_DOC", "Dinheiro");

            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);
            comando.Parameters.Add(parametro4);
            comando.Parameters.Add(parametro5);
            comando.Parameters.Add(parametro7);
            comando.Parameters.Add(parametro8);
            comando.Parameters.Add(parametro9);
            comando.Parameters.Add(parametro10);
            comando.Parameters.Add(parametro11);
            comando.Parameters.Add(parametro12);
            comando.Parameters.Add(parametro13);
            comando.Parameters.Add(parametro14);
            comando.Parameters.Add(parametro15);
            comando.Parameters.Add(parametro16);
            comando.Parameters.Add(parametro17);
            comando.Parameters.Add(parametro18);
            comando.Parameters.Add(parametro19);
            comando.Parameters.Add(parametro20);
                       

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco                
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPlanoDeContas", "incluirPlanoDeContasSubCategoria()", erro.Message.ToString(), "Incluir Plano de Contas SubCategoria");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                comando = null;
                incluir = null;
                parametro1 = null;
                parametro2 = null;
                parametro3 = null;
                parametro4 = null;
                parametro5 = null;
                parametro6 = null;
            }
        }
        //fim da função GravarProduto()
        #endregion

        #region Método "Obter o Ultimo Plano de Conta Mestre Cadastrado de Entrada ou Saída"
        public bool dObterUltimoPlanosDeSubCategoriaCadastrado(iModPlanoContas objPlanoContas)
        {
            StringBuilder consultarGrupoContaMestre = new StringBuilder();
            consultarGrupoContaMestre.Append("SELECT TOP(1) ID_CATEGORIAPLANO ");
            consultarGrupoContaMestre.Append(",DESCRICAO_SUBCATEGORIA ");
            consultarGrupoContaMestre.Append(",MASCARA_SUBCATEGORIA ");
            //consultarGrupoContaMestre.Append(",MASCARA_SUBCATEGORIA ");
            consultarGrupoContaMestre.Append(",STATUS ");
            //consultarGrupoContaMestre.Append(",LOG_FKUSUARIOCRIADOR ");
            //consultarGrupoContaMestre.Append(",LOG_DATACRIACAO ");
            //consultarGrupoContaMestre.Append(",LOG_FKCLIENTEFD ");
            //consultarGrupoContaMestre.Append(",LOG_ATIVARMODOINTEGRADO ");
            consultarGrupoContaMestre.Append("FROM TB_PLANOCONTAS_SUBCAT WHERE STATUS='ATIVO' AND FK_CATEGORIAMESTRE = @FK_CATEGORIAMESTRE ORDER BY ID_CATEGORIAPLANO DESC");


            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarGrupoContaMestre.ToString(), conexao);
            DataSet DsDataSet = new DataSet();
            SqlParameter fire = new SqlParameter("@FK_CATEGORIAMESTRE", objPlanoContas.IdCategoriaMestre);
            DataAdap.SelectCommand.Parameters.Add(fire);

            try
            {
                DataAdap.Fill(DsDataSet);
                Ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPlanoDeContas", "obterUltimoPlanosDeSubCategoriaCadastrado()", erro.Message.ToString(), "Obter Planos de Contas Mestres");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarGrupoContaMestre = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        //fim do método de ObterCadastro
        #endregion

        #region Método "Obter Todos as SubCategorias dos Planos de Contas"
        public bool dObterTodasSubCategoriasDePlanosDeContasCadastrados()
        {
            StringBuilder consultarGrupoContaMestre = new StringBuilder();
            consultarGrupoContaMestre.Append("SELECT SUBCAT.ID_CATEGORIAPLANO ");
            consultarGrupoContaMestre.Append(",SUBCAT.FK_CATEGORIAMESTRE ");
            consultarGrupoContaMestre.Append(",SUBCAT.DESCRICAO_SUBCATEGORIA ");
            consultarGrupoContaMestre.Append(",SUBCAT.MASCARA_SUBCATEGORIA ");
            consultarGrupoContaMestre.Append(",SUBCAT.MASCARA_SUBCATEGORIA + ' - ' + SUBCAT.DESCRICAO_SUBCATEGORIA AS DESC_CBB ");
            consultarGrupoContaMestre.Append(",PLANMEST.DESCRICAO_CATEGORIA ");
            consultarGrupoContaMestre.Append(",PLANMEST.ENTRADA_SAIDA ");
            consultarGrupoContaMestre.Append(",PLANMEST.MASCARA_PLANO ");
            consultarGrupoContaMestre.Append("FROM TB_PLANOCONTAS_SUBCAT SUBCAT ");
            consultarGrupoContaMestre.Append("INNER JOIN dbo.TB_PLANOCONTAS_MESTRE PLANMEST ");
            consultarGrupoContaMestre.Append("ON PLANMEST.PK_IDPLANO_CONTAS_MESTRE = SUBCAT.FK_CATEGORIAMESTRE ");
            consultarGrupoContaMestre.Append("WHERE PLANMEST.STATUS = 'ATIVO' AND SUBCAT.STATUS = 'ATIVO' ");
            consultarGrupoContaMestre.Append("ORDER BY SUBCAT.MASCARA_SUBCATEGORIA ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarGrupoContaMestre.ToString(), conexao);
            DataSet DsDataSet = new DataSet();
            //SqlParameter fire = new SqlParameter("@ENTRADA_SAIDA", entradaOuSaida);
            //DataAdap.SelectCommand.Parameters.Add(fire);
            try
            {
                DataAdap.Fill(DsDataSet);
                Ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPlanoDeContas", "obterTodasSubCategoriasDePlanosDeContasCadastrados()", erro.Message.ToString(), "Obter todas Categorias de Planos de Contas Mestres");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarGrupoContaMestre = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        //fim do método de ObterCadastro
        #endregion

        #region Método "Obter Todos as SubCategorias dos Planos de Contas"
        public bool dObterTodasSubCategoriasDeEntradaDosPlanosDeContasCadastrados()
        {
            StringBuilder consultarGrupoContaMestre = new StringBuilder();
            consultarGrupoContaMestre.Append("SELECT SUBCAT.ID_CATEGORIAPLANO ");
            consultarGrupoContaMestre.Append(",SUBCAT.FK_CATEGORIAMESTRE ");
            consultarGrupoContaMestre.Append(",SUBCAT.DESCRICAO_SUBCATEGORIA ");
            consultarGrupoContaMestre.Append(",SUBCAT.MASCARA_SUBCATEGORIA ");
            consultarGrupoContaMestre.Append(",SUBCAT.MASCARA_SUBCATEGORIA + ' - ' + SUBCAT.DESCRICAO_SUBCATEGORIA AS DESC_CBB ");

            consultarGrupoContaMestre.Append(",SUBCAT.NOME_ECF ");
            consultarGrupoContaMestre.Append(",SUBCAT.NOME_ECFRESUMIDO ");
            consultarGrupoContaMestre.Append(",SUBCAT.CHAMAR_TEF ");
            consultarGrupoContaMestre.Append(",SUBCAT.CHAMAR_ECF ");
            consultarGrupoContaMestre.Append(",SUBCAT.CHAMAR_NFE ");
            consultarGrupoContaMestre.Append(",SUBCAT.FORMA_VENDA_AVISTA ");
            consultarGrupoContaMestre.Append(",SUBCAT.FORMA_VENDA_ARECEBER ");
            consultarGrupoContaMestre.Append(",SUBCAT.FORMA_VENDA_AFATURAR ");
            consultarGrupoContaMestre.Append(",SUBCAT.NUM_PARCELAS ");
            consultarGrupoContaMestre.Append(",SUBCAT.INTERVALO_DIAS ");
            consultarGrupoContaMestre.Append(",SUBCAT.PRIMEIRA_PARC_AVISTA ");
            consultarGrupoContaMestre.Append(",SUBCAT.QTD_PARC_SEMJUROS ");
            consultarGrupoContaMestre.Append(",SUBCAT.ACRESCIMO_FIXO ");
            consultarGrupoContaMestre.Append(",SUBCAT.DESCONTO_FIXO ");
            consultarGrupoContaMestre.Append(",SUBCAT.DIA_VENCT_FIXO ");
            consultarGrupoContaMestre.Append(",SUBCAT.TIPO_DOC ");

            consultarGrupoContaMestre.Append(",PLANMEST.DESCRICAO_CATEGORIA ");
            consultarGrupoContaMestre.Append(",PLANMEST.ENTRADA_SAIDA ");
            consultarGrupoContaMestre.Append(",PLANMEST.MASCARA_PLANO ");
            consultarGrupoContaMestre.Append("FROM TB_PLANOCONTAS_SUBCAT SUBCAT ");
            consultarGrupoContaMestre.Append("INNER JOIN dbo.TB_PLANOCONTAS_MESTRE PLANMEST ");
            consultarGrupoContaMestre.Append("ON PLANMEST.PK_IDPLANO_CONTAS_MESTRE = SUBCAT.FK_CATEGORIAMESTRE ");
            consultarGrupoContaMestre.Append("WHERE PLANMEST.STATUS = 'ATIVO' AND SUBCAT.STATUS = 'ATIVO' AND PLANMEST.ENTRADA_SAIDA = 1 ");
            consultarGrupoContaMestre.Append("ORDER BY SUBCAT.MASCARA_SUBCATEGORIA ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarGrupoContaMestre.ToString(), conexao);
            DataSet DsDataSet = new DataSet();
            //SqlParameter fire = new SqlParameter("@ENTRADA_SAIDA", entradaOuSaida);
            //DataAdap.SelectCommand.Parameters.Add(fire);
            try
            {
                DataAdap.Fill(DsDataSet);
                Ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPlanoDeContas", "obterTodasSubCategoriasDePlanosDeContasCadastrados()", erro.Message.ToString(), "Obter todas Categorias de Planos de Contas Mestres");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarGrupoContaMestre = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        //fim do método de ObterCadastro
        #endregion

        #region Método "Obter SubCategoria do Plano de Contas por Código"
        public bool dObterSubCategoriaPlanoContasPorCodigo(iModPlanoContas objPlanoContas)
        {
            StringBuilder consultarGrupoContaMestre = new StringBuilder();
            consultarGrupoContaMestre.Append("SELECT ID_CATEGORIAPLANO ");
            consultarGrupoContaMestre.Append(",FK_CATEGORIAMESTRE ");
            consultarGrupoContaMestre.Append(",DESCRICAO_SUBCATEGORIA ");
            consultarGrupoContaMestre.Append(",NOME_ECF ");
            consultarGrupoContaMestre.Append(",NOME_ECFRESUMIDO ");
            consultarGrupoContaMestre.Append(",MASCARA_SUBCATEGORIA ");
            consultarGrupoContaMestre.Append(",STATUS ");
            consultarGrupoContaMestre.Append(",CHAMAR_TEF ");
            consultarGrupoContaMestre.Append(",CHAMAR_ECF ");
            consultarGrupoContaMestre.Append(",CHAMAR_NFE ");
            consultarGrupoContaMestre.Append(",FORMA_VENDA_AVISTA ");
            consultarGrupoContaMestre.Append(",FORMA_VENDA_ARECEBER ");
            consultarGrupoContaMestre.Append(",FORMA_VENDA_AFATURAR ");
            consultarGrupoContaMestre.Append(",NUM_PARCELAS ");
            consultarGrupoContaMestre.Append(",INTERVALO_DIAS ");
            consultarGrupoContaMestre.Append(",PRIMEIRA_PARC_AVISTA ");
            consultarGrupoContaMestre.Append(",QTD_PARC_SEMJUROS ");
            consultarGrupoContaMestre.Append(",ACRESCIMO_FIXO ");
            consultarGrupoContaMestre.Append(",DESCONTO_FIXO ");
            consultarGrupoContaMestre.Append(",DIA_VENCT_FIXO ");
            consultarGrupoContaMestre.Append(",TIPO_DOC ");            
            consultarGrupoContaMestre.Append("FROM TB_PLANOCONTAS_SUBCAT WHERE MASCARA_SUBCATEGORIA = @MASCARA_SUBCATEGORIA ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarGrupoContaMestre.ToString(), conexao);
            DataSet DsDataSet = new DataSet();
            SqlParameter fire = new SqlParameter("@MASCARA_SUBCATEGORIA", objPlanoContas.MascaraSubPlano);
            DataAdap.SelectCommand.Parameters.Add(fire);
            try
            {
                DataAdap.Fill(DsDataSet);
                Ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsPlanoDeContas", "obterSubCategoriaPlanoContasPorCodigo()", erro.Message.ToString(), "Obter Sub Categoria do Plano de Contas por Código");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarGrupoContaMestre = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        //fim do método de ObterCadastro
        #endregion
    }
}
