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
    public class iDaoCaixa
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

        #region Metodo Abrir Caixa
        public bool dIncluirAbrirCaixa(iModCaixa objCaixa)//string idPdv, string dataCaixa, string seqDiario, string seqGeral, string mascara_Caixa_Inteira, int usuarioAbertura, string nomeUsuarioAbertura, string nomePdv, string serialPdv, string modeloEcf, string ccoECF, string granTotalECF, string numeroSerieECF, DateTime diaHoraAbertura, decimal valorAbertura, int numeroUsuarioLogado, int fkCodigoClienteFuturaData, bool modoIntegrado, string nomeUsuarioLogado, string nomeHost)
        {
            StringBuilder incluir = new StringBuilder();
            incluir.Append("INSERT INTO TB_NEWCAIXA118 ");
            incluir.Append("(IDPDV ");
            incluir.Append(",DATACAIXA ");
            incluir.Append(",SEQUENCIALDIARIO ");
            incluir.Append(",SEQUENCIALGERAL ");
            incluir.Append(",MASCARA_CAIXA_INTEIRA ");
            incluir.Append(",DIA_HORAABERTURA ");
            incluir.Append(",VALOR_ABERTURA ");
            incluir.Append(",STATUS ");
            incluir.Append(",VENDAS_EFETUADAS_LIQ ");
            incluir.Append(",VENDAS_EFETUADAS_BRU ");
            incluir.Append(",QTD_ITENSVENDIDOS ");
            incluir.Append(",QTD_ITENSESTORNADOS ");
            incluir.Append(",VALORESTORNOS ");
            incluir.Append(",REFORCOS ");
            incluir.Append(",SANGRIAS ");
            incluir.Append(",VENDA_AVISTA ");
            incluir.Append(",FECHAMENTO) ");
            incluir.Append("VALUES ");

            incluir.Append("(@IDPDV ");
            incluir.Append(",@DATACAIXA ");
            incluir.Append(",@SEQUENCIALDIARIO ");
            incluir.Append(",@SEQUENCIALGERAL ");
            incluir.Append(",@MASCARA_CAIXA_INTEIRA ");
            incluir.Append(",@DIA_HORAABERTURA ");
            incluir.Append(",@VALOR_ABERTURA ");
            incluir.Append(",@STATUS ");
            incluir.Append(",0 ");
            incluir.Append(",0 ");
            incluir.Append(",0 ");
            incluir.Append(",0 ");
            incluir.Append(",0 ");
            incluir.Append(",0 ");
            incluir.Append(",0 ");
            incluir.Append(",0 ");
            incluir.Append(",0 )");


            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(incluir.ToString(), conexao);

            SqlParameter parametro1 = new SqlParameter("@IDPDV", "1"); //chumbei 1 - quem sabe um dia façamos multiPDV
            SqlParameter parametro2 = new SqlParameter("@DATACAIXA", objCaixa.DataCaixa);
            SqlParameter parametro3 = new SqlParameter("@SEQUENCIALDIARIO", objCaixa.SeqDiario);
            SqlParameter parametro4 = new SqlParameter("@SEQUENCIALGERAL", objCaixa.SeqGeral);
            SqlParameter parametro5 = new SqlParameter("@MASCARA_CAIXA_INTEIRA", objCaixa.Mascara_Caixa_Inteira);
            SqlParameter parametro6 = new SqlParameter("@DIA_HORAABERTURA", DateTime.Now);
            SqlParameter parametro7 = new SqlParameter("@VALOR_ABERTURA", objCaixa.ValorAberturaCaixa);
            SqlParameter parametro8 = new SqlParameter("@STATUS", "CAIXA ABERTO");

            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);
            comando.Parameters.Add(parametro4);
            comando.Parameters.Add(parametro5);
            comando.Parameters.Add(parametro6);
            comando.Parameters.Add(parametro7);
            comando.Parameters.Add(parametro8);


            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco                
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "incluirCaixa()", erro.Message.ToString(), "Incluir Novo Caixa");
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
                parametro7 = null;
                parametro8 = null;
            }
        }
        #endregion

        #region Método Fechar Venda
        public bool dFecharVenda(iModCaixa objCaixa)
        {
            StringBuilder SqlConcatenada = new StringBuilder();
            SqlConcatenada.Append("INSERT INTO PEDIDOS ");
            SqlConcatenada.Append("(CODIGO ");
            SqlConcatenada.Append(",ID_CAIXA ");
            SqlConcatenada.Append(",IDENTIFICACAO_CAIXA ");
            SqlConcatenada.Append(",FK_CODIGOORC ");
            SqlConcatenada.Append(",DATACAD ");
            SqlConcatenada.Append(",EMITIDO_COMPRO_FISCAL ");
            SqlConcatenada.Append(",STATUS_PEDIDO ");
            SqlConcatenada.Append(",VALORBRUTOORCT ");
            SqlConcatenada.Append(",VALORACRESCIMO ");
            SqlConcatenada.Append(",VALORDESCONTO ");
            SqlConcatenada.Append(",VALORFINALPAGO ");
            SqlConcatenada.Append(",VALORDADOCLIENTE ");
            SqlConcatenada.Append(",TROCO ");
            SqlConcatenada.Append(",INFOADICIONAL ");
            SqlConcatenada.Append(",NUMEROCUPOMFISCAL ");
            SqlConcatenada.Append(",ID_PLANODECONTAS ");
            SqlConcatenada.Append(",FORMAPAGTOPRINCIPAL) ");
            SqlConcatenada.Append("VALUES ");
            SqlConcatenada.Append("(@CODIGO ");
            SqlConcatenada.Append(",@ID_CAIXA ");
            SqlConcatenada.Append(",@IDENTIFICACAO_CAIXA ");
            SqlConcatenada.Append(",@FK_CODIGOORC ");
            SqlConcatenada.Append(",@DATACAD ");
            SqlConcatenada.Append(",@EMITIDO_COMPRO_FISCAL ");
            SqlConcatenada.Append(",@STATUS_PEDIDO ");
            SqlConcatenada.Append(",@VALORBRUTOORCT ");
            SqlConcatenada.Append(",@VALORACRESCIMO ");
            SqlConcatenada.Append(",@VALORDESCONTO ");
            SqlConcatenada.Append(",@VALORFINALPAGO ");
            SqlConcatenada.Append(",@VALORDADOCLIENTE ");
            SqlConcatenada.Append(",@TROCO ");
            SqlConcatenada.Append(",@INFOADICIONAL ");
            SqlConcatenada.Append(",@NUMEROCUPOMFISCAL ");
            SqlConcatenada.Append(",@ID_PLANODECONTAS ");
            SqlConcatenada.Append(",@FORMAPAGTOPRINCIPAL) ");

            SqlConnection conexaoSQL = new clsConexao().abrirConexaoBd();

            SqlCommand comandoSQL = new SqlCommand(SqlConcatenada.ToString(), conexaoSQL);
            SqlParameter parametro = new SqlParameter("@CODIGO", objCaixa.orcamento.PkCodigo);
            comandoSQL.Parameters.Add(parametro);
            SqlParameter parametro2 = new SqlParameter("@FK_CODIGOORC", objCaixa.orcamento.PkCodigo);
            comandoSQL.Parameters.Add(parametro2);
            SqlParameter parametro3 = new SqlParameter("@DATACAD", DateTime.Now);
            comandoSQL.Parameters.Add(parametro3);
            SqlParameter parametro4 = new SqlParameter("@EMITIDO_COMPRO_FISCAL", false);
            comandoSQL.Parameters.Add(parametro4);
            SqlParameter parametro5 = new SqlParameter("@STATUS_PEDIDO", "VENDIDO");
            comandoSQL.Parameters.Add(parametro5);
            SqlParameter parametro6 = new SqlParameter("@VALORBRUTOORCT", objCaixa.ValorBrutoOrc);
            comandoSQL.Parameters.Add(parametro6);
            SqlParameter parametro7 = new SqlParameter("@VALORACRESCIMO", objCaixa.ValorAcrescimo);
            comandoSQL.Parameters.Add(parametro7);
            SqlParameter parametro8 = new SqlParameter("@VALORDESCONTO", objCaixa.ValorDesconto);
            comandoSQL.Parameters.Add(parametro8);
            SqlParameter parametro9 = new SqlParameter("@VALORFINALPAGO", objCaixa.ValorPago);
            comandoSQL.Parameters.Add(parametro9);
            SqlParameter parametro10 = new SqlParameter("@VALORDADOCLIENTE", objCaixa.ValorDadoCliente);
            comandoSQL.Parameters.Add(parametro10);
            SqlParameter parametro11 = new SqlParameter("@TROCO", objCaixa.Troco);
            comandoSQL.Parameters.Add(parametro11);
            SqlParameter parametro12 = new SqlParameter("@INFOADICIONAL", objCaixa.InfoAdicional);
            comandoSQL.Parameters.Add(parametro12);
            SqlParameter parametro13 = new SqlParameter("@NUMEROCUPOMFISCAL", objCaixa.NumeroCupomFiscal);
            comandoSQL.Parameters.Add(parametro13);
            SqlParameter parametro14 = new SqlParameter("@FORMAPAGTOPRINCIPAL", objCaixa.FormaPagtoPrincipal);
            comandoSQL.Parameters.Add(parametro14);
            SqlParameter parametro15 = new SqlParameter("@ID_PLANODECONTAS", objCaixa.planoContas.IdCategoriaMestre);
            comandoSQL.Parameters.Add(parametro15);
            SqlParameter parametro16 = new SqlParameter("@ID_CAIXA", objCaixa.IdCaixa);
            comandoSQL.Parameters.Add(parametro16);
            SqlParameter parametro17 = new SqlParameter("@IDENTIFICACAO_CAIXA", objCaixa.IdentificacaoCaixa);
            comandoSQL.Parameters.Add(parametro17);

            try
            {
                comandoSQL.CommandType = CommandType.Text;
                comandoSQL.ExecuteNonQuery();

                SqlCommand comandoSQL3 = new SqlCommand();
                comandoSQL3.Connection = conexaoSQL;
                comandoSQL3.CommandType = CommandType.Text;
                comandoSQL3.CommandText = "UPDATE ORCAMENTOS SET STATUSVENDIDO='VENDIDO' WHERE CODIGO=@CODIGOORC";

                SqlParameter parametro18 = new SqlParameter("@CODIGOORC", objCaixa.orcamento.PkCodigo);
                comandoSQL3.Parameters.Add(parametro18);

                comandoSQL3.ExecuteNonQuery();

                return true;
            }
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixa", "newFechaVenda()", erro.Message.ToString(), "Efetua o fechamento de uma venda");
                return false;
            }

            finally
            {
                conexaoSQL.Close();
                conexaoSQL = null;
            }
        }
        #endregion

        #region Método Incluir forma de pagamento no caixa
        public bool dIncluirRecebimentoAVistaCaixa(iModCaixa objCaixa)
        {
            StringBuilder incluir = new StringBuilder();
            incluir.Append("INSERT INTO RECEBIMENTOSCAIXA ");
            incluir.Append("(FK_CODIGOPEDIDO ");
            incluir.Append(",FK_CODIGOCAIXA ");
            incluir.Append(",FK_CODIGOCLIENTE ");
            incluir.Append(",FK_FORMAPAGTO ");
            incluir.Append(",IDENTIFICACAO_CAIXA ");
            incluir.Append(",NUMERO_FATURA ");
            incluir.Append(",NUMEROPARCELA ");
            incluir.Append(",NUMEROTOTALPARCELAS ");
            incluir.Append(",DATAEMISSAO ");
            incluir.Append(",DATAVENCIMENTO ");
            incluir.Append(",VALORBRUTO ");
            incluir.Append(",VALORFATURA ");
            incluir.Append(",VALORPAGO ");
            incluir.Append(",VALORREMANESCENTE ");
            incluir.Append(",DATAQUITACAO ");
            incluir.Append(",STATUS) ");
            incluir.Append("VALUES ");
            incluir.Append("(@FK_CODIGOPEDIDO ");
            incluir.Append(",@FK_CODIGOCAIXA ");
            incluir.Append(",@FK_CODIGOCLIENTE ");
            incluir.Append(",@FK_FORMAPAGTO ");
            incluir.Append(",@IDENTIFICACAO_CAIXA ");
            incluir.Append(",@NUMERO_FATURA ");
            incluir.Append(",@NUMEROPARCELA ");
            incluir.Append(",@NUMEROTOTALPARCELAS ");
            incluir.Append(",@DATAEMISSAO ");
            incluir.Append(",@DATAVENCIMENTO ");
            incluir.Append(",@VALORBRUTO ");
            incluir.Append(",@VALORFATURA ");
            incluir.Append(",@VALORPAGO ");
            incluir.Append(",@VALORREMANESCENTE ");
            incluir.Append(",@DATAQUITACAO ");
            incluir.Append(",@STATUS) ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(incluir.ToString(), conexao);

            SqlParameter parametro1 = new SqlParameter("@FK_CODIGOPEDIDO", objCaixa.orcamento.PkCodigo);
            SqlParameter parametro2 = new SqlParameter("@FK_CODIGOCAIXA", objCaixa.IdCaixa);
            SqlParameter parametro3 = new SqlParameter("@FK_CODIGOCLIENTE", objCaixa.cliente.Pk_Codigo);
            SqlParameter parametro4 = new SqlParameter("@FK_FORMAPAGTO", objCaixa.ParFormaPagtoPlanoConta);
            SqlParameter parametro5 = new SqlParameter("@IDENTIFICACAO_CAIXA", objCaixa.IdentificacaoCaixa);
            SqlParameter parametro6 = new SqlParameter("@NUMERO_FATURA", objCaixa.ParNumeroFatura);
            SqlParameter parametro7 = new SqlParameter("@NUMEROPARCELA", objCaixa.ParNumeroParcela);
            SqlParameter parametro8 = new SqlParameter("@NUMEROTOTALPARCELAS", objCaixa.ParNumeroTotalParcelas);
            SqlParameter parametro9 = new SqlParameter("@DATAEMISSAO", DateTime.Now);
            SqlParameter parametro10 = new SqlParameter("@DATAVENCIMENTO", DateTime.Now);
            SqlParameter parametro11 = new SqlParameter("@VALORBRUTO", objCaixa.ParValorBruto);
            SqlParameter parametro12 = new SqlParameter("@VALORFATURA", objCaixa.ParValorFatura);
            SqlParameter parametro13 = new SqlParameter("@VALORPAGO", objCaixa.ParValorPago);
            SqlParameter parametro14 = new SqlParameter("@VALORREMANESCENTE", objCaixa.ParValorRemanescente);
            SqlParameter parametro15 = new SqlParameter("@DATAQUITACAO", DateTime.Now);
            SqlParameter parametro16 = new SqlParameter("@STATUS", "QUITADA - ENTRADA A VISTA");

            if (objCaixa.ParMaisInfo == "")
            {
                objCaixa.ParMaisInfo = "Entrada a Vista - Informacao vinda Pedido " + objCaixa.orcamento.PkCodigo.ToString() + ". Caixa ID " + objCaixa.IdCaixa.ToString();
            }

            comando.Parameters.Add(parametro1);
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
            comando.Parameters.Add(parametro13);
            comando.Parameters.Add(parametro14);
            comando.Parameters.Add(parametro15);
            comando.Parameters.Add(parametro16);

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "incluirRecebimentoAVistaCaixa()", erro.Message.ToString(), "Incluir Recebimento a Vista no Caixa");
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
                parametro7 = null;
                parametro8 = null;
                parametro9 = null;
                parametro10 = null;
                parametro11 = null;
                parametro12 = null;
                parametro13 = null;
                parametro14 = null;
                parametro15 = null;
                parametro16 = null;
            }
        }
        //fim da função GravarProduto()
        #endregion

        #region Obter Pedidos Fechados de Acordo com ID de Caixa
        public bool dObterPedidosFechadosPorCaixa(iModCaixa objCaixa)
        {
            StringBuilder consultarPedidos = new StringBuilder();
            consultarPedidos.Append("SELECT PED.CODIGO ");
            consultarPedidos.Append(",PED.ID_CAIXA ");
            consultarPedidos.Append(",PED.IDENTIFICACAO_CAIXA ");
            consultarPedidos.Append(",PED.FK_CODIGOORC ");
            consultarPedidos.Append(",PED.DATACAD ");
            consultarPedidos.Append(",PED.EMITIDO_COMPRO_FISCAL ");
            consultarPedidos.Append(",PED.STATUS_PEDIDO ");
            consultarPedidos.Append(",PED.VALORBRUTOORCT ");
            consultarPedidos.Append(",PED.VALORACRESCIMO ");
            consultarPedidos.Append(",PED.VALORDESCONTO ");
            consultarPedidos.Append(",PED.VALORFINALPAGO ");
            consultarPedidos.Append(",PED.VALORDADOCLIENTE ");
            consultarPedidos.Append(",PED.TROCO ");
            consultarPedidos.Append(",PED.INFOADICIONAL ");
            consultarPedidos.Append(",PED.NUMEROCUPOMFISCAL ");
            consultarPedidos.Append(",PED.ID_PLANODECONTAS ");
            consultarPedidos.Append(",PED.FORMAPAGTOPRINCIPAL ");
            consultarPedidos.Append(",CLI.NOME AS NOMECLIENTE ");
            consultarPedidos.Append("FROM PEDIDOS PED ");
            consultarPedidos.Append("INNER JOIN dbo.ORCAMENTOS ORC ");
            consultarPedidos.Append("ON ORC.CODIGO = PED.CODIGO ");
            consultarPedidos.Append("INNER JOIN dbo.CLIENTES CLI ");
            consultarPedidos.Append("ON CLI.CODIGO = ORC.FK_NUMCLIENTE ");
            consultarPedidos.Append("WHERE ID_CAIXA = @ID ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarPedidos.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            SqlParameter parametro = new SqlParameter("@ID", objCaixa.IdCaixa);
            DataAdap.SelectCommand.Parameters.Add(parametro);

            try
            {
                DataAdap.Fill(DsDataSet);
                ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "obterPedidosFechadosPorIDCaixa()", erro.Message.ToString(), "Obter Pedidos Fechados por um Caixa");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarPedidos = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        #endregion

        #region Efetuar Fechamento do Caixa
        public bool dEfetuaFechamentoCaixa(iModCaixa objCaixa)
        {
            StringBuilder alterar = new StringBuilder();
            alterar.Append("UPDATE TB_NEWCAIXA118 SET ");
            alterar.Append("FECHAMENTO = @FECHAMENTO ");
            alterar.Append(",FECHAMENTO_DATAHORA = @FECHAMENTO_DATAHORA ");
            alterar.Append(",STATUS = 'CAIXA FECHADO' ");
            alterar.Append("WHERE PK = @PK ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlCommand comando = new SqlCommand(alterar.ToString(), conexao);

            SqlParameter parametro1 = new SqlParameter("@FECHAMENTO", objCaixa.ValorFechtCaixa);
            SqlParameter parametro2 = new SqlParameter("@FECHAMENTO_DATAHORA", DateTime.Now);
            SqlParameter parametro3 = new SqlParameter("@PK", objCaixa.IdCaixa);

            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);

            try
            {
                comando.ExecuteNonQuery(); //executa a Query no banco                
                return true;
            }

            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "efetuaFechamentoCaixa()", erro.Message.ToString(), "Efetuar o fechamento do Caixa");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                comando = null;
                parametro1 = null;
                parametro2 = null;
                parametro3 = null;
            }
        }
        #endregion

        #region Obter informacoes do Ultimo Caixa
        public bool dObterInformacoesUltimoCaixa(iModCaixa objCaixa)
        {
            StringBuilder consultarGrupoContaMestre = new StringBuilder();
            consultarGrupoContaMestre.Append("SELECT TOP(1) PK ");
            consultarGrupoContaMestre.Append(",IDPDV ");
            consultarGrupoContaMestre.Append(",DATACAIXA ");
            consultarGrupoContaMestre.Append(",SEQUENCIALDIARIO ");
            consultarGrupoContaMestre.Append(",SEQUENCIALGERAL ");
            consultarGrupoContaMestre.Append(",MASCARA_CAIXA_INTEIRA ");
            consultarGrupoContaMestre.Append(",USUARIO_ABERTURA ");
            consultarGrupoContaMestre.Append(",NOMEUSUARIO_ABERTURA ");
            consultarGrupoContaMestre.Append(",NOME_PDVABERTURA ");
            consultarGrupoContaMestre.Append(",SERIAL_DVABERTURA ");
            consultarGrupoContaMestre.Append(",CCO_ECF_ABERTURA ");
            consultarGrupoContaMestre.Append(",GRAN_TOTALECF_ABERTURA ");
            consultarGrupoContaMestre.Append(",MODELO_ECF ");
            consultarGrupoContaMestre.Append(",NUMERO_SERIEECF ");
            consultarGrupoContaMestre.Append(",DIA_HORAABERTURA ");
            consultarGrupoContaMestre.Append(",VALOR_ABERTURA ");
            consultarGrupoContaMestre.Append(",STATUS ");
            consultarGrupoContaMestre.Append(",VENDAS_EFETUADAS_LIQ ");
            consultarGrupoContaMestre.Append(",VENDAS_EFETUADAS_BRU ");
            consultarGrupoContaMestre.Append(",QTD_ITENSVENDIDOS ");
            consultarGrupoContaMestre.Append(",QTD_ITENSESTORNADOS ");
            consultarGrupoContaMestre.Append(",VALORESTORNOS ");
            consultarGrupoContaMestre.Append(",REFORCOS ");
            consultarGrupoContaMestre.Append(",SANGRIAS ");
            consultarGrupoContaMestre.Append(",VENDA_AVISTA ");
            consultarGrupoContaMestre.Append(",VENDA_ARECEBER ");
            consultarGrupoContaMestre.Append(",VENDA_AFATURAR ");
            consultarGrupoContaMestre.Append(",FECHAMENTO ");
            consultarGrupoContaMestre.Append(",FECHAMENTO_OBS ");
            consultarGrupoContaMestre.Append(",FECHAMENTO_REDZ ");
            consultarGrupoContaMestre.Append(",FECHAMENTO_DATAHORA ");
            consultarGrupoContaMestre.Append(",FECHAMENTO_USUARIO ");
            consultarGrupoContaMestre.Append("FROM TB_NEWCAIXA118 ");
            consultarGrupoContaMestre.Append("WHERE IDPDV = @PK ORDER BY PK DESC ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarGrupoContaMestre.ToString(), conexao);
            DataSet DsDataSet = new DataSet();
            SqlParameter fire = new SqlParameter("@PK", objCaixa.IdCaixa);
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

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "obterUltimoCaixaDeUmPDV()", erro.Message.ToString(), "Obter Ultimo Caixa de um PDV");
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
        #endregion

        #region Obter Pedidos Fechados Por Caixa
        public bool dObterPedidosFechadosPorIDCaixa(iModCaixa objCaixa)
        {
            StringBuilder consultarPedidos = new StringBuilder();
            consultarPedidos.Append("SELECT PED.CODIGO ");
            consultarPedidos.Append(",PED.ID_CAIXA ");
            consultarPedidos.Append(",PED.IDENTIFICACAO_CAIXA ");
            consultarPedidos.Append(",PED.FK_CODIGOORC ");
            consultarPedidos.Append(",PED.DATACAD ");
            consultarPedidos.Append(",PED.EMITIDO_COMPRO_FISCAL ");
            consultarPedidos.Append(",PED.STATUS_PEDIDO ");
            consultarPedidos.Append(",PED.VALORBRUTOORCT ");
            consultarPedidos.Append(",PED.VALORACRESCIMO ");
            consultarPedidos.Append(",PED.VALORDESCONTO ");
            consultarPedidos.Append(",PED.VALORFINALPAGO ");
            consultarPedidos.Append(",PED.VALORDADOCLIENTE ");
            consultarPedidos.Append(",PED.TROCO ");
            consultarPedidos.Append(",PED.INFOADICIONAL ");
            consultarPedidos.Append(",PED.NUMEROCUPOMFISCAL ");
            consultarPedidos.Append(",PED.ID_PLANODECONTAS ");
            consultarPedidos.Append(",PED.FORMAPAGTOPRINCIPAL ");
            consultarPedidos.Append(",CLI.NOME AS NOMECLIENTE ");
            consultarPedidos.Append(",VEND.NOMEFANTASIA AS NOMEVENDEDOR ");
            consultarPedidos.Append("FROM PEDIDOS PED ");
            consultarPedidos.Append("INNER JOIN dbo.ORCAMENTOS ORC ");
            consultarPedidos.Append("ON ORC.CODIGO = PED.CODIGO ");
            consultarPedidos.Append("INNER JOIN dbo.CLIENTES CLI ");
            consultarPedidos.Append("ON CLI.CODIGO = ORC.FK_NUMCLIENTE ");
            consultarPedidos.Append("INNER JOIN dbo.TB_VENDEDORES222 VEND ");
            consultarPedidos.Append("ON VEND.PK_CODIGO = ORC.FK_NUMVENDEDOR ");
            consultarPedidos.Append("WHERE ID_CAIXA = @ID ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarPedidos.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            SqlParameter parametro = new SqlParameter("@ID", objCaixa.IdCaixa);
            DataAdap.SelectCommand.Parameters.Add(parametro);

            try
            {
                DataAdap.Fill(DsDataSet);
                ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "obterPedidosFechadosPorIDCaixa()", erro.Message.ToString(), "Obter Pedidos Fechados por um Caixa");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarPedidos = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        #endregion

        #region Obter Movimentacao Estoque por ID Caixa
        public bool dObterMovimentacaoEstoquePorIDCaixa(iModCaixa objCaixa)
        {
            StringBuilder consultarPedidos = new StringBuilder();
            consultarPedidos.Append("SELECT EST.PK_ID ");
            consultarPedidos.Append(",EST.FK_CODIGOPEDIDO ");
            consultarPedidos.Append(",EST.FK_CODIGOPRODUTO ");
            consultarPedidos.Append(",PRO.DESCRICAO + '-' + PRO.APLICACAO AS DESCRICAOPROD ");
            consultarPedidos.Append(",PRO.CODIGOFABRIC AS CODIGOFABRICANTE ");
            consultarPedidos.Append(",EST.FK_CODIGOCAIXA ");
            consultarPedidos.Append(",EST.FK_CODIGOCLIENTE ");
            consultarPedidos.Append(",EST.IDENTIFICACAO_CAIXA ");
            consultarPedidos.Append(",EST.DATAMOVIMENTACAO ");
            consultarPedidos.Append(",EST.VALORCUSTO ");
            consultarPedidos.Append(",EST.MARGEM_LUCRO ");
            consultarPedidos.Append(",EST.VALORVENDA ");
            consultarPedidos.Append(",EST.QUANTIDADE ");
            consultarPedidos.Append(",EST.VALORTOTAL ");
            consultarPedidos.Append(",EST.TIPO_ES ");
            consultarPedidos.Append(",EST.MAISINFO ");
            consultarPedidos.Append("FROM MOVIMENTOESTOQUE EST ");
            consultarPedidos.Append("INNER JOIN dbo.PRODUTOS PRO ");
            consultarPedidos.Append("ON PRO.PK_CODIGOSIST = EST.FK_CODIGOPRODUTO ");
            consultarPedidos.Append("WHERE EST.FK_CODIGOCAIXA = @ID ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarPedidos.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            SqlParameter parametro = new SqlParameter("@ID", objCaixa.IdCaixa);
            DataAdap.SelectCommand.Parameters.Add(parametro);

            try
            {
                DataAdap.Fill(DsDataSet);
                ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "obterMovimentacaoEstoquePorIDCaixa()", erro.Message.ToString(), "Obter Movimentação de Estoque Fechados por um Caixa");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarPedidos = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        #endregion

        #region Obter Movimentacao Financeira por Caixa
        public bool dObterMovimentacaoFinanceiraPorIDCaixa(iModCaixa objCaixa)
        {
            StringBuilder consultarPedidos = new StringBuilder();
            consultarPedidos.Append("SELECT RECEB.PK_ID ");
            consultarPedidos.Append(",RECEB.FK_CODIGOPEDIDO ");
            consultarPedidos.Append(",RECEB.FK_CODIGOCAIXA ");
            consultarPedidos.Append(",RECEB.FK_CODIGOCLIENTE ");
            consultarPedidos.Append(",RECEB.FK_FORMAPAGTO ");
            consultarPedidos.Append(",SUBCAT.DESCRICAO_SUBCATEGORIA ");
            consultarPedidos.Append(",RECEB.IDENTIFICACAO_CAIXA ");
            consultarPedidos.Append(",RECEB.NUMERO_FATURA ");
            consultarPedidos.Append(",RECEB.NUMEROPARCELA ");
            consultarPedidos.Append(",RECEB.NUMEROTOTALPARCELAS ");
            consultarPedidos.Append(",RECEB.DATAEMISSAO ");
            consultarPedidos.Append(",RECEB.DATAVENCIMENTO ");
            consultarPedidos.Append(",RECEB.VALORBRUTO ");
            consultarPedidos.Append(",RECEB.VALORFATURA ");
            consultarPedidos.Append(",RECEB.VALORPAGO ");
            consultarPedidos.Append(",RECEB.VALORREMANESCENTE ");
            consultarPedidos.Append(",RECEB.DATAQUITACAO ");
            consultarPedidos.Append(",RECEB.STATUS ");
            consultarPedidos.Append(",RECEB.MAIS_INFORMACOES ");
            consultarPedidos.Append("FROM RECEBIMENTOSCAIXA RECEB ");
            consultarPedidos.Append("INNER JOIN dbo.TB_PLANOCONTAS_SUBCAT SUBCAT ");
            consultarPedidos.Append("ON RECEB.FK_FORMAPAGTO = SUBCAT.ID_CATEGORIAPLANO ");
            consultarPedidos.Append("WHERE FK_CODIGOCAIXA = @ID ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarPedidos.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            SqlParameter parametro = new SqlParameter("@ID", objCaixa.IdCaixa);
            DataAdap.SelectCommand.Parameters.Add(parametro);

            try
            {
                DataAdap.Fill(DsDataSet);
                ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "obterMovimentacaoFinanceiraAVistaPorIDCaixa()", erro.Message.ToString(), "Obter Movimentação Financeira a Vista por um Caixa");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarPedidos = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        #endregion

        #region Obter Ultimo Sequencial dos Caixas
        public string dObterUltimoSequencialDosCaixas()
        {
            StringBuilder consultarGrupoContaMestre = new StringBuilder();
            consultarGrupoContaMestre.Append("SELECT TOP(1) SEQUENCIALGERAL ");
            consultarGrupoContaMestre.Append("FROM TB_NEWCAIXA118 ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarGrupoContaMestre.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            try
            {
                DataAdap.Fill(DsDataSet);
                return DsDataSet.Tables[0].Rows[0]["SEQUENCIALGERAL"].ToString();
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "obterUltimoSequencialDosCaixas()", erro.Message.ToString(), "Obter Ultimo Sequencial dos Caixas");
                return "";
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
        #endregion

        #region Obter ID do Ultimo Caixa
        public string dObterIDUltimoCaixa()
        {
            StringBuilder consultarPedidos = new StringBuilder();
            consultarPedidos.Append("SELECT TOP(1) PK ");
            consultarPedidos.Append("FROM TB_NEWCAIXA118 WHERE IDPDV = @IDPDV ORDER BY PK DESC ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarPedidos.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            SqlParameter parametro = new SqlParameter("@IDPDV", "1");
            DataAdap.SelectCommand.Parameters.Add(parametro);

            try
            {
                DataAdap.Fill(DsDataSet);
                Ds_DadosRetorno = DsDataSet;
                if (DsDataSet.Tables[0].Rows.Count != 0)
                {
                    return DsDataSet.Tables[0].Rows[0]["PK"].ToString();
                }
                else
                {
                    return "";
                }
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "obterPKUltimoCaixaPDV()", erro.Message.ToString(), "Obter PK Ultimo Caixa Aberto PDV");
                return "";
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarPedidos = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        #endregion

        #region Obter Informacoes sobre Caixa Atraves PK
        public bool dObterInformacoesSobreCaixaPelaPK(iModCaixa objCaixa)
        {
            StringBuilder consultarPedidos = new StringBuilder();
            consultarPedidos.Append("SELECT PK ");
            consultarPedidos.Append(",IDPDV ");
            consultarPedidos.Append(",DATACAIXA ");
            consultarPedidos.Append(",SEQUENCIALDIARIO ");
            consultarPedidos.Append(",SEQUENCIALGERAL ");
            consultarPedidos.Append(",MASCARA_CAIXA_INTEIRA ");
            consultarPedidos.Append(",DIA_HORAABERTURA ");
            consultarPedidos.Append(",VALOR_ABERTURA ");
            consultarPedidos.Append(",STATUS ");
            consultarPedidos.Append(",VENDAS_EFETUADAS_LIQ ");
            consultarPedidos.Append(",VENDAS_EFETUADAS_BRU ");
            consultarPedidos.Append(",QTD_ITENSVENDIDOS ");
            consultarPedidos.Append(",QTD_ITENSESTORNADOS ");
            consultarPedidos.Append(",VALORESTORNOS ");
            consultarPedidos.Append(",REFORCOS ");
            consultarPedidos.Append(",SANGRIAS ");
            consultarPedidos.Append(",VENDA_AVISTA ");
            consultarPedidos.Append(",FECHAMENTO ");
            consultarPedidos.Append(",FECHAMENTO_DATAHORA ");
            consultarPedidos.Append("FROM TB_NEWCAIXA118 ");
            consultarPedidos.Append("WHERE PK = @PK ");

            SqlConnection conexao = this.clsConexao.abrirConexaoBd();
            SqlDataAdapter DataAdap = new SqlDataAdapter(consultarPedidos.ToString(), conexao);
            DataSet DsDataSet = new DataSet();

            SqlParameter parametro = new SqlParameter("@PK", objCaixa.IdCaixa);
            DataAdap.SelectCommand.Parameters.Add(parametro);

            try
            {
                DataAdap.Fill(DsDataSet);
                Ds_DadosRetorno = DsDataSet;
                return true;
            }
            //caindo no CATCH chama as rotinas que geram os logs de erro
            catch (SqlException erro)
            {

                erroClasse = erro.Message.ToString(); //.efetuarTratamentoDoErroInfoSiga(numeroUsuarioLogado, fkCodigoClienteFuturaData, modoIntegrado, nomeUsuarioLogado, nomeHost, "clsNewCaixav2", "obterInformacoesSobreCaixaPelaPK()", erro.Message.ToString(), "Obter informações sobre caixa pela PK");
                return false;
            }

            //Sempre Executa o Finally para Limpar os Objetos e Fechar as Conexões
            finally
            {
                this.clsConexao.fecharConexaoBd(conexao);
                conexao = null;
                consultarPedidos = null;
                DsDataSet = null;
                DataAdap = null;
            }
        }
        #endregion
    }
}
