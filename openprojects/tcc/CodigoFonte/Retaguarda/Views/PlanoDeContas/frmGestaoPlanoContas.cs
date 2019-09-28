using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FuturaDataTCC.Iniciar;
using DllFuturaDataTCC.Controllers;

namespace FuturaDataTCC.Views.PlanoDeContas
{
    public partial class frmGestaoPlanoContas : Form
    {
        #region Construtor e Variaveis Internas do Form
        frmInicializacao frmInicial;
        iConPlanoContas controlPlanoContas = new iConPlanoContas();
        public frmGestaoPlanoContas(frmInicializacao frmIni)
        {
            InitializeComponent();
            frmInicial = frmIni;
            carregaTodosPlanosContas();
        }
        #endregion

        #region Carrega Todos Planos de Contas
        public void carregaTodosPlanosContas()
        {
            lvwPlanoDeContas.Items.Clear(); //limpo o ListView para mostrar nova consulta
            lvwPlanoDeContas.Columns.Clear();

            lvwPlanoDeContas.Columns.Add("", 0, HorizontalAlignment.Left);
            //lvwPlanoDeContas.Columns.Add("IDM", -2, HorizontalAlignment.Left);
            //lvwPlanoDeContas.Columns.Add("IDC", -2, HorizontalAlignment.Left);
            lvwPlanoDeContas.Columns.Add("Grupo", 60, HorizontalAlignment.Left);
            lvwPlanoDeContas.Columns.Add("Descricao", 460, HorizontalAlignment.Left);

            DataTable dt_PlanosDeContasMestre = new DataTable();
            DataTable dt_RetornoPlanoContas = new DataTable();
            bool retorno = controlPlanoContas.cObterTodasSubCategoriasDePlanosDeContasCadastrados();
            if (retorno)
            {
                dt_RetornoPlanoContas = controlPlanoContas.daoPlanCont.Ds_DadosRetorno.Tables[0];
            }
            bool retorno2 = controlPlanoContas.cObterTodosPlanosDeContaMestres();
            if (retorno2)
            {
                dt_PlanosDeContasMestre = controlPlanoContas.daoPlanCont.Ds_DadosRetorno.Tables[0];
            }
            
            if (dt_PlanosDeContasMestre.Rows.Count != 0 && dt_RetornoPlanoContas.Rows.Count != 0)
            {                
                DataTable dt_DadosFiltrados = new DataTable();
                dt_DadosFiltrados.Columns.Add("IDM");
                dt_DadosFiltrados.Columns.Add("IDC");
                dt_DadosFiltrados.Columns.Add("MASCARA");
                dt_DadosFiltrados.Columns.Add("DESCRICAO");

                for (int i = 0; i < dt_PlanosDeContasMestre.Rows.Count; i++)
                {
                    DataRow DR = dt_DadosFiltrados.NewRow();
                    DR["IDM"] = dt_PlanosDeContasMestre.Rows[i]["PK_IDPLANO_CONTAS_MESTRE"].ToString();
                    DR["IDC"] = "";
                    DR["MASCARA"] = dt_PlanosDeContasMestre.Rows[i]["MASCARA_PLANO"].ToString();
                    DR["DESCRICAO"] = dt_PlanosDeContasMestre.Rows[i]["DESCRICAO_CATEGORIA"].ToString();
                    dt_DadosFiltrados.Rows.Add(DR);
                    DR = null;
                    for (int i2 = 0; i2 < dt_RetornoPlanoContas.Rows.Count; i2++)
                    {
                        if (dt_PlanosDeContasMestre.Rows[i]["PK_IDPLANO_CONTAS_MESTRE"].ToString() == dt_RetornoPlanoContas.Rows[i2]["FK_CATEGORIAMESTRE"].ToString())
                        {
                            DataRow DR2 = dt_DadosFiltrados.NewRow();
                            DR2["IDM"] = "";
                            DR2["IDC"] = dt_RetornoPlanoContas.Rows[i2]["ID_CATEGORIAPLANO"].ToString();
                            DR2["MASCARA"] = dt_RetornoPlanoContas.Rows[i2]["MASCARA_SUBCATEGORIA"].ToString();
                            DR2["DESCRICAO"] = dt_RetornoPlanoContas.Rows[i2]["DESCRICAO_SUBCATEGORIA"].ToString();
                            dt_DadosFiltrados.Rows.Add(DR2);
                            DR2 = null;
                        }
                    }//fim do segundo for
                }//fim do primeiro for


                //preenche o ListView e Desenha tudo na tela...16/09/2013 Fernando
                lvwPlanoDeContas.BeginUpdate();
                for (int i = 0; i < dt_DadosFiltrados.Rows.Count; i++)
                {
                    lvwPlanoDeContas.Items.Add("");
                    
                    if (dt_DadosFiltrados.Rows[i]["IDM"].ToString() == "" && dt_DadosFiltrados.Rows[i]["MASCARA"].ToString().StartsWith("1"))
                    {
                        lvwPlanoDeContas.Items[i].BackColor = Color.LightSkyBlue; //para produtos filho = DarkSlateGray                        
                    }

                    if (dt_DadosFiltrados.Rows[i]["IDM"].ToString() != "" && dt_DadosFiltrados.Rows[i]["MASCARA"].ToString().StartsWith("1"))
                    {
                        lvwPlanoDeContas.Items[i].BackColor = Color.DeepSkyBlue; //para produtos filho = DarkSlateGray                        
                    }


                    if (dt_DadosFiltrados.Rows[i]["IDM"].ToString() == "" && dt_DadosFiltrados.Rows[i]["MASCARA"].ToString().StartsWith("2"))
                    {
                        lvwPlanoDeContas.Items[i].BackColor = Color.MistyRose; //para produtos filho = DarkSlateGray                        
                    }

                    if (dt_DadosFiltrados.Rows[i]["IDM"].ToString() != "" && dt_DadosFiltrados.Rows[i]["MASCARA"].ToString().StartsWith("2"))
                    {
                        lvwPlanoDeContas.Items[i].BackColor = Color.LightCoral; //para produtos filho = DarkSlateGray                        
                    }

                    //lvwPlanoDeContas.Items[i].SubItems.Add(dt_DadosFiltrados.Rows[i]["IDM"].ToString());
                    //lvwPlanoDeContas.Items[i].SubItems.Add(dt_DadosFiltrados.Rows[i]["IDC"].ToString());
                    lvwPlanoDeContas.Items[i].SubItems.Add(dt_DadosFiltrados.Rows[i]["MASCARA"].ToString());
                    lvwPlanoDeContas.Items[i].SubItems.Add(dt_DadosFiltrados.Rows[i]["DESCRICAO"].ToString());
                }
                lvwPlanoDeContas.EndUpdate();
                //lvwPlanoDeContas.Refresh();
            }//fim if (dt_PlanosDeContasMestre.Rows.Count != 0 && dt_RetornoPlanoContas.Rows.Count != 0)            
        }
        #endregion

        #region Evento do Botao Incluir Plano Mestre Receita
        private void btnIncluirPlanoMestreReceita_Click(object sender, EventArgs e)
        {
            frmCadastroCategoriaPlanoContas plano = new frmCadastroCategoriaPlanoContas(this.frmInicial);
            plano.ShowDialog();
            carregaTodosPlanosContas();
        }
        #endregion

        #region Evento do Botao Incluir Plano Mestre Despesa
        private void btnIncluirPlanoMestreDespesa_Click(object sender, EventArgs e)
        {
            frmCadastroItemPlanoContas itemPlano = new frmCadastroItemPlanoContas(this.frmInicial,"");
            itemPlano.ShowDialog();
            carregaTodosPlanosContas();
        }
        #endregion

        #region Evento do Double Click no listview para Alterar uma Subcategoria do Plano de Contas
        private void lvwPlanoDeContas_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string mascaraSelecionada = lvwPlanoDeContas.SelectedItems[0].SubItems[1].Text.ToString().Trim();
                if (mascaraSelecionada.Length > 5)
                {
                    frmCadastroItemPlanoContas itemPlano = new frmCadastroItemPlanoContas(this.frmInicial, mascaraSelecionada);
                    itemPlano.ShowDialog();
                    carregaTodosPlanosContas();
                }
                else
                {
                    MessageBox.Show(null, "Não é possível alterar a Descrição de um Plano de Contas Mestre. Apenas é possível alterar a Configuração de suas SubCategorias.", "FuturaData Business", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {

            }
        }
        #endregion
    }//fim classe
}//fim namespace
