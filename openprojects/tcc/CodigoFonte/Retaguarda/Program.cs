using System;
using System.Collections.Generic;

using System.Windows.Forms;
using FuturaDataTCC.Iniciar;
using FuturaDataTCC;
using System.Data;
using FuturaDataTCC.Utilitarios;
using DllFuturaDataContrValidacoes;
using FuturaDataTCC.Utilitarios;
using System.IO;
using DllFuturaDataTCC;
using DllFuturaDataTCC.Utilitarios;
using System.Threading;
using System.Diagnostics;
using DllFuturaDataCriptografia;

namespace FuturaDataTCC
{
    static class Program
    {
        #region Main (Load do Form)
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            clsInicializacao inicial = new clsInicializacao();
            frmInicializacao frmInicial = new frmInicializacao();
            //frmInicial = frmInicial;
            bool sistemaValidado = true;
                        
            clsAtivacaoSoftware ativa = new clsAtivacaoSoftware(new clsConexao().recuperaStringConexaoSQLServer());

            //VERIFICA SE EXISTE CONEXAO MULTIPLA PARA 2 EMPRESAS NA MESMA MÁQUINA
            if (Directory.Exists(@"c:\FuturaData\TCC\CONEXOES"))
            {
                if (File.Exists(@"c:\FuturaData\TCC\CONEXOES\CONEXAO1.XML") && File.Exists(@"c:\FuturaData\TCC\CONEXOES\CONEXAO2.XML"))
                {
                    frmSelecaoConexoes conexoes = new frmSelecaoConexoes();
                    conexoes.ShowDialog();
                    //Thread.Sleep(1500);
                }
            }

            bool retornoXML = new clsConexao().criaArquivoConexaoXML();

            if (inicial.verificaComunicacaoBanco() == false)
            {
                if (File.Exists(@"c:\FuturaData\TCC\\sqlb.bat") == false)
                {
                    MessageBox.Show("Atenção: Não foi detectado conexão com o banco de Dados do Sistema. Verifique:" + Environment.NewLine + "1-O Servidor do Sistema e o Serviço do Banco de Dados. Verifique se nenhum Firewall bloqueou a conexão" + Environment.NewLine + "2-A comunicação da rede, hubs, conexão, cabos (integridade fisica)." + Environment.NewLine + Environment.NewLine + "Será aberta uma tela para configuração de conexão do sistema, para que possa ser testada e diagnosticada a conectividade com o Servidor. As configurações dessa tela não precisam ser alteradas caso você identifique o problema em sua rede e/ou servidor e resolva, bastando fecha-la e reiniciar o sistema. Do contrário, basta informar o Servidor, nome do Serviço, e do Banco de Dados para efetuar os testes e salvar as novas configurações!", "Tentativa de conexão com o banco de dados falhou", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                frmConfigConexaoServidor configConexao = new frmConfigConexaoServidor();
                configConexao.ShowDialog();
                clsFilaProcessosWindows.finalizarPrograma();
            }

        #endregion

            #region Valida os Componentes
            string respostaValidacaoComponentes = inicial.verificaComponentesEDependencias(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);

            if (respostaValidacaoComponentes != "OK")
            {
                MessageBox.Show(respostaValidacaoComponentes, "Erro em 1 ou mais componentes do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sistemaValidado = false;
                clsFilaProcessosWindows.finalizarPrograma();
            }
            #endregion

            #region Verifica se Já foi Configurado os Dados da Empresa Usuária
            DataTable dt_Dados = new DataTable();
            bool retorno = inicial.retornaDadosCliente(frmInicial.numeroUsuarioLogado, 0, true, frmInicial.nomeUsuarioLogado, frmInicial.nomeHost);

            if (retorno == true)
            {
                dt_Dados = inicial.getDs_DadosRetorno().Tables[0];
            }

            if (dt_Dados.Rows.Count == 0)
            {
                //MessageBox.Show("Seja Muito Bem vindo ao Sistema FuturaData." + Environment.NewLine + Environment.NewLine + "Você logo poderá desfrutar de todo o potêncial de interface, rapidez e resultados focados de nosso sistema. Mas antes, você precisa efetuar o Cadastro dos Dados de Sua Empresa." + Environment.NewLine + Environment.NewLine + "Preencha os dados PRINCIPAIS de sua MATRIZ na próxima tela. Ao entrar no sistema, você poderá cadastrar Filiais, MultiEmpresas, PDVs, Clientes, Produtos, ver seu Caixa, Financeiro, e ter acesso aos demais cadastros e configurações." + Environment.NewLine + Environment.NewLine + "Além disso, sinta-se a vontade para procurar nosso Suporte se sentir qualquer dificuldade ou dúvida. Temos uma equipe direta e focada para responder suas questões e alcançar rapidamente os resultados desejados! Seja bem vindo a FuturaData!", "Seja bem vindo ao FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmInicial.primeiroUsoSistema = true;
                sistemaValidado = false;
                frmConfigSistema configSist = new frmConfigSistema(frmInicial);
                configSist.ShowDialog();
                clsFilaProcessosWindows.finalizarPrograma();
            }
            #endregion

            #region Abre o Sistema se tudo estiver OK
            if (sistemaValidado == true)
            {
                frmInicial.carregarSistema();
                Application.Run();
            }
            #endregion
        }        
    }//fim classe
}//fim namespace
