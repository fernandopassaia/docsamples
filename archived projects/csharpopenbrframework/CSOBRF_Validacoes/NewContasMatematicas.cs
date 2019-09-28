using System;

namespace CSOBRF_Validacoes
{    
    public class NewContasMatematicas
    {
        #region Verifica se Número é Inteiro (Método PRIVADO e apenas Interno)
        /// <summary>
        /// Verifica se o valor recebido é um inteiro, se não há letras e etc
        /// </summary>
        /// <param name="valor">Valor a ser testado - converter para string</param>
        /// <returns>Retorna True se for Inteiro, False se não for</returns>
        public bool verificaSeEInteiro(string valor)
        {
            ulong resultadoConversao;
            if (valor != "")
            {

                //  Tenta converter o texto na textbox para float
                if ((ulong.TryParse(valor.ToString(), out resultadoConversao) == true))
                {
                    try
                    {
                        ulong teste = Convert.ToUInt64(valor);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }//fim rotina que verifica se é inteiro
        #endregion

        #region Tenta Converter em Decimal (Método PRIVADO e apenas Interno)
        /// <summary>
        /// Método interno usado pelo Verifica se é Decimal, esse verifica se é possível converter
        /// Ou seja, caso haja uma letra ele já retorna falso
        /// </summary>
        /// <param name="valor">String</param>
        /// <returns></returns>
        private bool tentaConverterEmDecimal(string valor)
        {
            decimal resultadoConversao;
            if ((decimal.TryParse(valor.ToString(), out resultadoConversao) == true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }//fim tentaConverterEmDecimal
        #endregion

        #region Converte Decimal para Inteiro
        /// <summary>
        /// Retorna em Formato String um decimal já arredondado com 2 casas
        /// </summary>
        /// <param name="valor">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <returns>Retorna um Decimal convertido para Int (ex: 12,12 ou 12,00 - irá virar 12) - caso de erro retornará 0</returns>
        public int ConverteDecimalParaInteiro(Decimal valor)
        {
            if(!verificaSeEDecimal(valor.ToString()) && !verificaSeEInteiro(valor.ToString()))
            {return 0;}

            if(verificaSeEInteiro(valor.ToString()))
            {return Convert.ToInt32(valor);}

            string valorConverter = valor.ToString();
            string valorConvertido = "";
            bool achouPrimeiraVirgulaOuPonto = false;

            for (int i = 0; i < valorConverter.Length; i++)
            {
                if (achouPrimeiraVirgulaOuPonto == false)
                {
                    if(valorConverter.Substring(i, 1) != "." && valorConverter.Substring(i, 1) != ",")
                    {
                        valorConvertido = valorConvertido + valorConverter.Substring(i, 1);
                    }
                    else
                    {
                        achouPrimeiraVirgulaOuPonto = true;
                    }
                }
            }

            return Convert.ToInt32(valorConvertido);
        }
        #endregion
        
        #region Evento Ajusta 2 Casas Decimais
        /// <summary>
        /// Retorna em Formato String um decimal já arredondado com 2 casas
        /// </summary>
        /// <param name="valor">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <returns>Retorna um Decimal sempre com 2 casas Decimais 12,12 - se retornar "ERRO", é por que houve erro e o número enviado não é decimal</returns>
        public string newValidaAjustaArredonda2CasasDecimais(string valor)
        {
            valor = valor.Replace(".", ",");
            bool retornoDecimal = tentaConverterEmDecimal(valor);
            bool retornoInteiro = verificaSeEInteiro(valor);

            if (!retornoDecimal && !retornoInteiro)
            {
                return "0,00";
            }
            else
            {
                decimal valorConvertido = Convert.ToDecimal(valor);
                return valorConvertido.ToString("N2").Replace(".", "");
            }
        }
        #endregion

        #region Evento Ajusta 3 Casas Decimais
        /// <summary>
        /// Retorna em Formato String um decimal já arredondado com 3 casas
        /// </summary>
        /// <param name="valor">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <returns>Retorna um Decimal sempre com 3 casas Decimais 12,123 - se retornar "ERRO", é por que houve erro e o número enviado não é decimal</returns>
        public string newValidaAjustaArredonda3CasasDecimais(string valor)
        {
            valor = valor.Replace(".", ",");
            bool retornoDecimal = tentaConverterEmDecimal(valor);
            bool retornoInteiro = verificaSeEInteiro(valor);

            if (!retornoDecimal && !retornoInteiro)
            {
                return "0,000";
            }
            else
            {
                decimal valorConvertido = Convert.ToDecimal(valor);
                return valorConvertido.ToString("N3").Replace(".", "");
            }
        }
        #endregion

        #region Evento Ajusta 4 Casas Decimais
        /// <summary>
        /// Retorna em Formato String um decimal já arredondado com 4 casas
        /// </summary>
        /// <param name="valor">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <returns>Retorna um Decimal sempre com 4 casas Decimais 12,1234 - se retornar "ERRO", é por que houve erro e o número enviado não é decimal</returns>
        public string newValidaAjustaArredonda4CasasDecimais(string valor)
        {
            valor = valor.Replace(".", ",");
            bool retornoDecimal = tentaConverterEmDecimal(valor);
            bool retornoInteiro = verificaSeEInteiro(valor);

            if (!retornoDecimal && !retornoInteiro)
            {
                return "0,0000";
            }
            else
            {
                decimal valorConvertido = Convert.ToDecimal(valor);
                return valorConvertido.ToString("N4").Replace(".","");
            }
        }
        #endregion

        #region Evento MultiPlica Dois Valores em 4 Casas Decimais
        /// <summary>
        /// Retorna em Formato String um decimal multiplicado por outro já arredondado com 4 casas
        /// </summary>
        /// <param name="valor1">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <param name="valor2">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <returns>Retorna um Decimal sempre com 4 casas Decimais 12,1234 - se retornar "ERRO", é por que houve erro e o número enviado não é decimal</returns>
        public string newMultiplicaCampos4CasasDecimais(string valor1, string valor2)
        {
            valor1 = valor1.Replace(".", ",");
            valor2 = valor2.Replace(".", ",");
            bool retornoDecimal = tentaConverterEmDecimal(valor1);
            bool retornoInteiro = verificaSeEInteiro(valor1);

            if (!retornoDecimal && !retornoInteiro)
            {
                return "0,0000";
            }

            bool retornoDecimal2 = tentaConverterEmDecimal(valor2);
            bool retornoInteiro2 = verificaSeEInteiro(valor2);

            if (!retornoDecimal2 && !retornoInteiro2)
            {
                return "0,0000";
            }

            else
            {
                decimal valorConvertido1 = Convert.ToDecimal(valor1);
                decimal valorConvertido2 = Convert.ToDecimal(valor2);
                return (valorConvertido1 * valorConvertido2).ToString("N4");
            }
        }
        #endregion

        #region Evento Soma Dois Valores em 4 Casas Decimais
        /// <summary>
        /// Retorna em Formato String um decimal somado por outro já arredondado com 4 casas
        /// </summary>
        /// <param name="valor1">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <param name="valor2">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <returns>Retorna um Decimal sempre com 4 casas Decimais 12,1234 - se retornar "ERRO", é por que houve erro e o número enviado não é decimal</returns>
        public string newSomaCampos4CasasDecimais(string valor1, string valor2)
        {
            valor1 = valor1.Replace(".", ",");
            valor2 = valor2.Replace(".", ",");
            bool retornoDecimal = tentaConverterEmDecimal(valor1);
            bool retornoInteiro = verificaSeEInteiro(valor1);

            if (!retornoDecimal && !retornoInteiro)
            {
                return "0,0000";
            }

            bool retornoDecimal2 = tentaConverterEmDecimal(valor2);
            bool retornoInteiro2 = verificaSeEInteiro(valor2);

            if (!retornoDecimal2 && !retornoInteiro2)
            {
                return "0,0000";
            }

            else
            {
                decimal valorConvertido1 = Convert.ToDecimal(valor1);
                decimal valorConvertido2 = Convert.ToDecimal(valor2);
                return (valorConvertido1 + valorConvertido2).ToString("N4");
            }
        }
        #endregion

        #region Evento Subtrai Dois Valores em 4 Casas Decimais
        /// <summary>
        /// Retorna em Formato String um decimal subtraido por outro já arredondado com 4 casas
        /// </summary>
        /// <param name="valor1">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <param name="valor2">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <returns>Retorna um Decimal sempre com 4 casas Decimais 12,1234 - se retornar "ERRO", é por que houve erro e o número enviado não é decimal</returns>
        public string newSubtraiCampos4CasasDecimais(string valor1, string valor2)
        {
            valor1 = valor1.Replace(".", ",");
            valor2 = valor2.Replace(".", ",");
            bool retornoDecimal = tentaConverterEmDecimal(valor1);
            bool retornoInteiro = verificaSeEInteiro(valor1);

            if (!retornoDecimal && !retornoInteiro)
            {
                return "0,0000";
            }

            bool retornoDecimal2 = tentaConverterEmDecimal(valor2);
            bool retornoInteiro2 = verificaSeEInteiro(valor2);

            if (!retornoDecimal2 && !retornoInteiro2)
            {
                return "0,0000";
            }

            else
            {
                decimal valorConvertido1 = Convert.ToDecimal(valor1);
                decimal valorConvertido2 = Convert.ToDecimal(valor2);
                return (valorConvertido1 - valorConvertido2).ToString("N4");
            }
        }
        #endregion

        #region Evento Divide Dois Valores em 4 Casas Decimais
        /// <summary>
        /// Retorna em Formato String um decimal dividido por outro já arredondado com 4 casas
        /// </summary>
        /// <param name="valor1">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <param name="valor2">Recebe a String com 2 casas, 3 casas, 4 casas ou Inteiro e Retorna Sempre 00,0000</param>
        /// <returns>Retorna um Decimal sempre com 4 casas Decimais 12,1234 - se retornar "ERRO", é por que houve erro e o número enviado não é decimal</returns>
        public string newDivideCampos4CasasDecimais(string valor1, string valor2)
        {
            valor1 = valor1.Replace(".", ",");
            valor2 = valor2.Replace(".", ",");
            bool retornoDecimal = tentaConverterEmDecimal(valor1);
            bool retornoInteiro = verificaSeEInteiro(valor1);

            if (!retornoDecimal && !retornoInteiro)
            {
                return "0,0000";
            }

            bool retornoDecimal2 = tentaConverterEmDecimal(valor2);
            bool retornoInteiro2 = verificaSeEInteiro(valor2);

            if (!retornoDecimal2 && !retornoInteiro2)
            {
                return "0,0000";
            }

            else
            {
                decimal valorConvertido1 = Convert.ToDecimal(valor1);
                decimal valorConvertido2 = Convert.ToDecimal(valor2);
                return (valorConvertido1 / valorConvertido2).ToString("N4");
            }
        }
        #endregion

        #region Verifica se valor é uma Data Válida (ex: 12/12/2008 ou 12/12/2008 11:11:11)
        /// <summary>
        /// Verifica se o valor recebido é uma Data Válida. P.S: Observe que a Data é Validada no esquema
        /// brasileiro (ex: 12/12/2008 ou 12/12/2008 11:11:11), ou seja? Datas com - ou ainda com mês no lugar
        /// de dias (como no formato americano) serão negadas no método, também datas sem 2 digitos (1/1/2008)
        /// serão negadas! Meses Inexistentes (mês 13) e dias (dia 32>) também serão negados! Somente será
        /// aceita data válida e no formato 11/11/2009 ou 11/11/2009 11:11:11
        /// </summary>
        /// <param name="valor">Valor a ser testado - converter para string</param>
        /// <returns>Retorna True se for uma Data Válida, False se não for</returns>
        public bool verificaSeEData(string valor)
        {
            bool retorno = true;
            if (valor.Length < 10)
            {
                retorno = false;
            }

            if (valor.Length > 20)
            {
                retorno = false;
            }

            try
            {
                int dias = Convert.ToInt32(valor.Substring(0, 2));
                if (dias > 31)
                {
                    retorno = false;
                }

                int mes = Convert.ToInt32(valor.Substring(3, 2));
                if (mes > 12)
                {
                    retorno = false;
                }

                if (valor.Substring(2, 1) != "/" && valor.Substring(5, 1) != "/")
                {
                    retorno = false;
                }


                DateTime data = Convert.ToDateTime(valor);
                int ano = data.Year;

                if (ano < 1800)
                {
                    retorno = false;
                }
                return retorno;
            }
            catch
            {
                return false;
            }
        }//fim rotina que verifica se é inteiro
        #endregion       

        #region Verifica se valor é um DECIMAL (ex: 15,0000 - 18,2546)
        /// <summary>
        /// Verifica se o valor recebido é um inteiro, se não há letras e etc
        /// </summary>
        /// <param name="valor">Valor a ser testado - converter para string</param>
        /// <returns>Retorna True se for DECIMAL, False se não for DECIMAL</returns>
        public bool verificaSeEDecimal(string valor)
        {
            if (valor != "")
            {
                if (tentaConverterEmDecimal(valor) == true)
                {
                    return true;
                }


                else
                {
                    return false;
                }
            }            
            return false;
        }//fim rotina que verifica se é inteiro
        #endregion     
    }//fim classe
}//fim namespace