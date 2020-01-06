using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using DllFuturaDataContrValidacoes;
using System.Windows.Forms;
using DllFuturaDataCriptografia;
using DllFuturaDataTCC.Models;

namespace DllFuturaDataTCC.Utilitarios
{
    public class clsECF
    {
        private int IRetornoBematech;
        public void leituraX()
        {
            IRetornoBematech = clsInterfaceBematech.Bematech_FI_LeituraX();
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);
        }

        public void reducaoZ()
        {
            IRetornoBematech = clsInterfaceBematech.Bematech_FI_ReducaoZ("", "");
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);
        }

        public void gravarAliquotas()
        {
            IRetornoBematech = clsInterfaceBematech.Bematech_FI_ProgramaAliquota("18,00", 0);
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);

            IRetornoBematech = clsInterfaceBematech.Bematech_FI_ProgramaAliquota("13,00", 0);
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);

            IRetornoBematech = clsInterfaceBematech.Bematech_FI_ProgramaAliquota("15,00", 0);
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);

            IRetornoBematech = clsInterfaceBematech.Bematech_FI_ProgramaAliquota("17,00", 0);
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);

            IRetornoBematech = clsInterfaceBematech.Bematech_FI_ProgramaAliquota("II", 0);
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);

            IRetornoBematech = clsInterfaceBematech.Bematech_FI_ProgramaAliquota("FF", 0);
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);

            IRetornoBematech = clsInterfaceBematech.Bematech_FI_ProgramaAliquota("NN", 0);
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);
        }

        public string emitirCF(iModItensOrcamento[] itens, string cpfCnpj, string formaPagto, string valorFinalCF)
        {
            IRetornoBematech = clsInterfaceBematech.Bematech_FI_AbreCupom(cpfCnpj);
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);
            clsNewContasMatematicas contas = new clsNewContasMatematicas();
            if (itens != null)
            {
                foreach (iModItensOrcamento item in itens)
                {
                    string codigoFabr = item.PkIdItemVenda + "-" + item.CodigoFabric;
                    string descricaoProd = item.DescricaoAplicacao;
                    string icms = "18,00";

                    string quantidade = contas.newValidaAjustaArredonda3CasasDecimais(item.Quantidade.ToString());
                    string valorUnit = contas.newValidaAjustaArredonda3CasasDecimais(item.ValorUnit.ToString());
                    string desconto = contas.newValidaAjustaArredonda2CasasDecimais(item.Desconto.ToString());
                    string acrescimo = contas.newValidaAjustaArredonda2CasasDecimais(item.Acrescimo.ToString());
                    string valorTotal = item.ValorTotal.ToString();                    
                    string unidadeMedida = "UN";

                    //VALIDA A QUANTIDADE DE CARACTERS PERMITIDOS NAS STRINGS PRO ECF NÃO RECUSAR
                    if (codigoFabr.Length > 13)
                    {
                        codigoFabr = codigoFabr.Substring(0, 13);
                    }

                    if (descricaoProd.Length > 29)
                    {
                        descricaoProd = descricaoProd.Substring(0, 29);
                    }

                    //Holly Shit - Método 
                    //IRetornoBematech = clsInterfaceBematech.Bematech_FI_VendeItem(codigoFabr, descricaoProd, icms, "F", quantidade, 3, valorUnit, "$", desconto);
                    IRetornoBematech = clsInterfaceBematech.Bematech_FI_VendeItemDepartamento(codigoFabr, descricaoProd, icms, valorUnit, quantidade, acrescimo, desconto, "01", unidadeMedida);//"F", quantidade, 3, valorUnit, "$", desconto);                        

                    codigoFabr = null;
                    descricaoProd = null;
                    icms = null;

                    quantidade = null;
                    valorUnit = null;
                    desconto = null;
                    acrescimo = null;
                    valorTotal = null;                    
                }//final do FOR

                IRetornoBematech = clsInterfaceBematech.Bematech_FI_IniciaFechamentoCupom("D", "$", "0");
                clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);
                
                if (formaPagto != "")
                {
                    string primeiroCaracter = formaPagto.Substring(0, 1);
                    if (contas.verificaSeEInteiro(primeiroCaracter.ToString()))
                    {
                        formaPagto = formaPagto.Substring(11, formaPagto.Length - 11); //remove os números do plano de contas da frente da descrição da forma de pagamento... Whas...
                    }

                    if (formaPagto.Length > 16)
                    {
                        formaPagto = formaPagto.Substring(0, 16);
                    }
                    IRetornoBematech = clsInterfaceBematech.Bematech_FI_EfetuaFormaPagamentoMFD(formaPagto, contas.newValidaAjustaArredonda2CasasDecimais(valorFinalCF), "1", "");
                    clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);
                }
                else
                {
                    IRetornoBematech = clsInterfaceBematech.Bematech_FI_EfetuaFormaPagamentoMFD("DINHEIRO", contas.newValidaAjustaArredonda2CasasDecimais(valorFinalCF), "1", "");
                    clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);
                }

                IRetornoBematech = clsInterfaceBematech.Bematech_FI_TerminaFechamentoCupom("FuturaData TCC - Obrigado Volte Sempre.");
                clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);

                string cco = new string('\x20', 14);
                IRetornoBematech = clsInterfaceBematech.Bematech_FI_NumeroCupom(ref cco);
                clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);

                return cco;
            }//fim if dt_Dados.Rows.Count>0
            return "ERRO";
        }

        public void cancelaUltimoCupom()
        {
            IRetornoBematech = clsInterfaceBematech.Bematech_FI_CancelaCupom();
            clsInterfaceBematech.Analisa_iRetorno(IRetornoBematech);
        }
    }//fim classe
}//fim namespace
