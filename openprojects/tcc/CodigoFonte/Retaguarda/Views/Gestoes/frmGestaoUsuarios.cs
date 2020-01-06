using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DllFuturaDataTCC.Gestoes;
using DllFuturaDataCriptografia;
using FuturaDataTCC.Iniciar;
using FuturaDataTCC.Utilitarios;
using DllFuturaDataTCC.Utilitarios;
using System.IO;
using DllFuturaDataContrValidacoes;
using DllFuturaDataTCC.Controllers;
using DllFuturaDataTCC.Models;


namespace FuturaDataTCC.Views.Gestoes
{
    public partial class frmGestaoUsuarios : Form
    {
        #region Atributos e Variaveis do View frmGestaoUsuarios
        
        
        int opcao; //variavel usada para guardar opção do cadastro (alterar, excluir ou novo)
        int indice = 0; //cria um indice para ser incr/decr dentro do Mostradados()
                
        iConUsuario controlUsuario = new iConUsuario();
        iModUsuario[] arrayUsuarios;        
        #endregion

        #region Construtor (inicializador) do Form        
        public frmGestaoUsuarios()
        {
            InitializeComponent();
            carregarCadUsuarios();            
            MostrarDados();
        }//fim inicialização
        #endregion

        #region **************MÉTODOS***************

        #region Método mostrar os dados na Tela e Controles
        /// <summary>
        /// Método que mostra os dados na tela e controles de acordo com o indice dentro do DataTable
        /// </summary>
        private void MostrarDados()
        {
            iModUsuario objUsuarioAtual = arrayUsuarios[indice];
            if (arrayUsuarios.Length == 0)
            {
                MessageBox.Show("Você não possuí usúarios Cadastrados! Insira um novo usuario!", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                
                tbxCodigoUsuario.Text = objUsuarioAtual.Pk_Codigo.ToString();
                tbxLembreteSenha.Text = objUsuarioAtual.Lembrete.ToString();
                tbxLoginUsuario.Text = objUsuarioAtual.LoginUsuario.ToString();
                tbxNomeUsuario.Text = objUsuarioAtual.NomeUsuario.ToString();
                tbxSenhaUsuario.Text = objUsuarioAtual.Senha.ToString();
               
                if (objUsuarioAtual.Funcao.ToString() == "VENDEDOR")
                {
                    rdbVendedor.Checked = true;
                    rdbCaixa.Checked = false;
                }
                else
                {
                    rdbCaixa.Checked = true;
                    rdbVendedor.Checked = false;
                }
                TravarControles();
                DestravaBotaoOpcoes();
            }//fim Else
        }//fim mostradados()
        #endregion

        #region Método Atualizar Array de Cadastro de Usuários
        /// <summary>
        /// Zera indice, atualiza o array, chama MostraDados(), destravabotoes e travacontroles.
        /// </summary>
        private void carregarCadUsuarios()
        {
            indice = 0; //zera o indice sempre aprontar pro primeiro cadastro
            arrayUsuarios = controlUsuario.cObterUsuario();
        }
        #endregion

        #region Método Validar Campos
        /// <summary>
        /// Verifica se todos campos estão preenchidos e se não são maiores que as colunas no BD
        /// </summary>
        /// <returns>Int nível segurança usuario</returns>
        private bool VerificaCampos()
        {
            if (tbxLoginUsuario.Text == "")
            {
                MessageBox.Show("Obrigatório definir login usuario", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxLoginUsuario.Focus();
                clsFuncoes.DesenhaRetanguloVermelho(tbxLoginUsuario);
                return false;
            }//Fim if tbxLogin == ""

            if (tbxLoginUsuario.Text.Length > 30)
            {
                MessageBox.Show("Campo Login pode conter até 30 caracters", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxLoginUsuario.Focus();
                clsFuncoes.DesenhaRetanguloVermelho(tbxLoginUsuario);
                return false;
            }//Fim if tbxLoginUsuario

            if (tbxNomeUsuario.Text == "")
            {
                MessageBox.Show("Obrigatório definir nome usuario", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxNomeUsuario.Focus();
                clsFuncoes.DesenhaRetanguloVermelho(tbxNomeUsuario);
                return false;
            }// fim if tbxNomeUsuario == ""

            if (tbxNomeUsuario.Text.Length > 80)
            {
                MessageBox.Show("Nome do Usuario pode ter até 80 caracters", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxNomeUsuario.Focus();
                clsFuncoes.DesenhaRetanguloVermelho(tbxNomeUsuario);
                return false;
            }// fim if tbxNomeUsuario > 80

            if (tbxSenhaUsuario.Text == "")
            {
                MessageBox.Show("Obrigatório definir senha usuario", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbxSenhaUsuario.Focus();
                clsFuncoes.DesenhaRetanguloVermelho(tbxSenhaUsuario);
                return false;
            }// fim if tbxSenha == ""           
            return true;
        }//fim VerificaCampos()
        #endregion

        #region Método Travar Controles
        /// <summary>
        /// Trava todos os Controles durante a exibição de dados - visualização
        /// </summary>
        private void TravarControles()
        {
            clsFuncoes.TravaControles(tbpDados);
            clsFuncoes.TravaControles(tbcUsuarios);

            tbxCodigoUsuario.ReadOnly = true;
           
        }//Fim ravaControles
        #endregion

        #region Método Destravar Controles
        /// <summary>
        /// Destrava os controles durante a alteração/inserção de dados
        /// </summary>
        private void DestravarControles()
        {
            clsFuncoes.DestravaControles(tbpDados);
            clsFuncoes.DestravaControles(tbcUsuarios);
            tbxCodigoUsuario.ReadOnly = true;
        }//Fim DestravaControles
        #endregion

        #region Método Limpar Campos
        /// <summary>
        /// Limpa todos os Controles no caso de um "novo" -  tbxCodigo = 000, tbxData = DateTimeNOw
        /// </summary>
        private void LimpaControles()
        {
            clsFuncoes.LimpaControles(tbpDados);
            clsFuncoes.LimpaControles(tbcUsuarios);
            
            tbxCodigoUsuario.Text = "0";
            this.tbcUsuarios.SelectedTab = this.tbpDados;
            
        }//fim LimpaControles()
        #endregion

        #region Método Travar Botões Opções
        /// <summary>
        /// Trava todos os botões - cancelar e salvar ficam ativos
        /// </summary>
        private void TravaBotaoOpcoes()
        {
            btnAlterar.Enabled = false;
            btnCadAnterior.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = false;
            btnProximoCad.Enabled = false;
            btnPrimeiroCad.Enabled = false;
            btnUltimoCad.Enabled = false;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
            
        }//fim TravaBotaoOpcoes
        #endregion

        #region Método Destravar Botões Opções
        /// <summary>
        /// Destrava todos os botões - cancelar e salvar ficam desativos
        /// </summary>
        private void DestravaBotaoOpcoes()
        {
            btnAlterar.Enabled = true;
            btnCadAnterior.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            btnProximoCad.Enabled = true;
            btnPrimeiroCad.Enabled = true;
            btnUltimoCad.Enabled = true;
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
        }//fim código DEstravaBotaoOpcoes
        #endregion

        #region Método Executar Operações: Inserir, Alterar e Excluir
        /// <summary>
        /// Chama método correto clsNegócio e efetua operacao (0=Novo, 1=Alterar, 2=Excluir)
        /// </summary>
        private void ExecutarOperacao()
        {
            clsCriptografia Criptografar = new clsCriptografia();
            string senhaCriptografada = Criptografar.Criptografar(tbxSenhaUsuario.Text.ToString());
            controlUsuario.modUsuario.Pk_Codigo = Convert.ToInt32(tbxCodigoUsuario.Text);
            controlUsuario.modUsuario.Lembrete = tbxLembreteSenha.Text;
            controlUsuario.modUsuario.LoginUsuario = tbxLoginUsuario.Text;
            controlUsuario.modUsuario.NomeUsuario = tbxNomeUsuario.Text;
            controlUsuario.modUsuario.Senha = tbxSenhaUsuario.Text;
            if (rdbCaixa.Checked)
            {
                controlUsuario.modUsuario.Funcao = "CAIXA";
            }
            else
            {
                controlUsuario.modUsuario.Funcao = "VENDEDOR";
            }

            //se opcao for 0 - é novo cliente
            if (opcao == 0)
            {
                if (controlUsuario.cInsereUsuario() == true)
                {
                    MessageBox.Show("Usuario Cadastrado com Sucesso!", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuario não registrado! Erro no sistema ou em conexão com banco de dados - Verifique o Servidor, em caso de problemas entre em contato com o suporte FuturaData! Um log com os erros foi gerado!", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //se a opcao for 1 - é alterar o cliente
            if (opcao == 1)
            {
                if (controlUsuario.cAlteraUsuario() == true)
                {
                    MessageBox.Show("Usuario Alterado com Sucesso! O usuário precisará sair e entrar no sistema novamente para que as alterações entrem em vigor.", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuario não Alterado! Erro no sistema ou em conexão com banco de dados - Verifique o Servidor, em caso de problemas entre em contato com o suporte FuturaData! Um log com os erros foi gerado!", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //se a opcao for 2 - é excluir o cliente
            if (opcao == 2)
            {
                if (controlUsuario.cExcluiUsuario() == true)
                {
                    MessageBox.Show("Usuario Excluido com Sucesso!", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuario não registrado! Erro no sistema ou em conexão com banco de dados - Verifique o Servidor, em caso de problemas entre em contato com o suporte FuturaData! Um log com os erros foi gerado!", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            carregarCadUsuarios();  // = AtualizarDataTableCadUsuarios();
        }//fim código do ExecutarOperacao
        #endregion

        #endregion **************MÉTODOS***************

        #region **************EVENTOS***************

        #region Evento Click Botão Cadastro Anterior
        /// <summary>
        /// Verifica Indice>0, decrementa e mostraDados
        /// </summary>
        private void btnCadAnterior_Click(object sender, EventArgs e)
        {
            if (indice > 0)
            {
                indice--;
                MostrarDados();
            }
            else
                MessageBox.Show("Você está no primeiro Cadastro!", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//fim código botão Anterior
        #endregion

        #region Evento Click Botão Proximo Cadastro
        /// <summary>
        /// Verifica se nao é maior que Dt.Count, incrementa e MostraDados
        /// </summary>
        private void btnProximoCad_Click(object sender, EventArgs e)
        {
            if (indice != arrayUsuarios.Length - 1)
            {
                indice++;
                MostrarDados();
            }
            else
                MessageBox.Show("Você está no ultimo Cadastro!", "FuturaData - Gestão de usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//fim código botão Próximo
        #endregion

        #region Evento Click Botão Primeiro cadastro
        /// <summary>
        /// Indice=0, MostraDados()
        /// </summary>
        private void btnPrimeiroCad_Click(object sender, EventArgs e)
        {
            indice = 0;
            MostrarDados();
        }//fim Botão Primeiro
        #endregion

        #region Evento Click Botão Ultimo Cadastro
        /// <summary>
        /// Indice = Dt.Count - 1, MostraDados()
        /// </summary>
        private void btnUltimoCad_Click(object sender, EventArgs e)
        {
            indice = arrayUsuarios.Length - 1;
            MostrarDados();
        }//fim botão ultimo
        #endregion

        #region Eventos Click dos Botões Novo, Alterar, Salvar, Excluir E Cancelar Operação

        #region Evento Click Botão Novo
        /// <summary>
        /// LimpaControles, Destrava, TravaBotao Opcoes, opcao = 0
        /// </summary>
        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimpaControles();
            DestravarControles();
            opcao = 0;
            TravaBotaoOpcoes();

        }//fim código botão novo
        #endregion

        #region Evento Click Botão Alterar
        /// <summary>
        /// Opcao = 1, DestravaControle, TravaBotaoOpcoes
        /// </summary>
        private void btnAlterar_Click_1(object sender, EventArgs e)
        {
            opcao = 1;
            this.tbcUsuarios.SelectedTab = this.tbpDados;
            DestravarControles();
            TravaBotaoOpcoes();
        }//fim código botão altera
        #endregion

        #region Evento Click Botão Salvar
        /// <summary>
        /// Chama VerificaCampos(), ExecutarOperacao(), AtualizarDataTableCadUsuarios()
        /// </summary>
        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            this.tbcUsuarios.SelectedTab = this.tbpDados;
            if (VerificaCampos()) //só irá executar se validar todos campos
            {
                
               ExecutarOperacao();
               indice = arrayUsuarios.Length-1;
               MostrarDados();
               
               TravarControles();
               this.Refresh();
            }
        }//fim código botão salvar
        #endregion

        #region Evento Click 1 Botão Excluir
        /// <summary>
        /// Opcao=2, ExecutaOperacao(), AtualizaDataTable()
        /// </summary>
        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            this.tbcUsuarios.SelectedTab = this.tbpDados;
            if (MessageBox.Show("Essa operação irá excluir o usuário permanentemente, desabilitando todas suas configurações, deseja prosseguir?", "FuturaData - Gestão de usuarios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                opcao = 2;
                ExecutarOperacao();
                indice = arrayUsuarios.Length - 1;
                MostrarDados();
            }
        }//fim código botão excluir

        #endregion

        #region Evento "btnCancelar_Click_1"
        /// <summary>
        /// Botão cancela - atualiza DataTable
        /// </summary>
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            opcao = 0;
            carregarCadUsuarios();
            this.tbcUsuarios.SelectedTab = this.tbpDados;
            this.Refresh();
        }//fim Cancelar
        #endregion
        
        #endregion

        #region Evento Click 1 botão Primeiro Cadastro
        private void btnPrimeiroCad_Click_1(object sender, EventArgs e)
        {
            btnPrimeiroCad_Click(null, null);
            this.tbcUsuarios.SelectedTab = this.tbpDados;
        }
        #endregion
      
        #region Evento Click 1 Botão Cadastro Anterior
        private void btnCadAnterior_Click_1(object sender, EventArgs e)
        {
            btnCadAnterior_Click(null, null);
            this.tbcUsuarios.SelectedTab = this.tbpDados;
        }

        #endregion

        #region Evento Click 1 Botão Proximo Cadastro
        private void btnProximoCad_Click_1(object sender, EventArgs e)
        {
            btnProximoCad_Click(null, null);
            this.tbcUsuarios.SelectedTab = this.tbpDados;
        }
        #endregion 

        #region Evento Click 1 Botão Ultimo Cadastro
        private void btnUltimoCad_Click_1(object sender, EventArgs e)
        {
            btnUltimoCad_Click(null, null);
            this.tbcUsuarios.SelectedTab = this.tbpDados;
        }
        #endregion

        #region Evento TextChanged da TbxSenha Usuario que mostra a complexidade da senha
        private void tbxSenhaUsuario_TextChanged(object sender, EventArgs e)
        {
            int numeroCaractersEspeciais = 0;

            for (int i = 0; i < tbxSenhaUsuario.Text.Length; i++)
            {
                if (tbxSenhaUsuario.Text.Substring(i, 1) == "@" || tbxSenhaUsuario.Text.Substring(i, 1) == "!" || tbxSenhaUsuario.Text.Substring(i, 1) == "#" || tbxSenhaUsuario.Text.Substring(i, 1) == "$" || tbxSenhaUsuario.Text.Substring(i, 1) == "&" || tbxSenhaUsuario.Text.Substring(i, 1) == "*" || tbxSenhaUsuario.Text.Substring(i, 1) == "(" || tbxSenhaUsuario.Text.Substring(i, 1) == ")")
                {
                    numeroCaractersEspeciais++;
                }

                #region Segurança de Senha BAIXA
                if (numeroCaractersEspeciais <= 1)
                {
                    lblTipoDeSenha.ForeColor = Color.Red;
                    lblTipoDeSenha.Text = "Segurança Baixa";
                    lblTipoDeSenha.Refresh();
                }
                #endregion

                #region Segurança de Senha MÉDIA
                if (numeroCaractersEspeciais >= 2 || numeroCaractersEspeciais >= 3)
                {
                    lblTipoDeSenha.ForeColor = Color.YellowGreen;
                    lblTipoDeSenha.Text = "Segurança Média";
                    lblTipoDeSenha.Refresh();
                }
                #endregion

                #region Segurança da senha ALTA
                if (numeroCaractersEspeciais >= 4)
                {
                    lblTipoDeSenha.ForeColor = Color.Green;
                    lblTipoDeSenha.Text = "Segurança Alta";
                    lblTipoDeSenha.Refresh();
                }
                #endregion
            }//fim for
        }//fim evento
        #endregion
        #endregion
    }//fim classe
}//fim namespace